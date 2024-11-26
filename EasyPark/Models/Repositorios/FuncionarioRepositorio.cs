using Dapper;
using DapperExtensions;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.Veiculo;
using EasyPark.Models.Entidades.VisitaEstacionamento;
using MySql.Data.MySqlClient;
using System.Text;
using XSystem.Security.Cryptography;

namespace EasyPark.Models.Repositorios
{
	public class FuncionarioRepositorio
	{
		private readonly string _connectionString;
		private readonly string sql;

		public FuncionarioRepositorio(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DbEasyParkConnection");

			sql = "SELECT f.id AS Id," +
				" f.login AS Login," +
				" f.senha AS Senha," +
				" f.nome AS Nome," +
				" f.cpf AS CpfCnpj," +
				" f.contato AS Contato," +
				" f.data_cadastro AS DataCadastro," +
				" f.id_plano AS IdPlano," +
				" f.id_empresa AS IdEmpresa FROM funcionarios f";
		}

		public IEnumerable<Funcionarios> GetAllFuncionarios()
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var funcionarios = connection.Query<Funcionarios>(sql).AsList();

				return funcionarios;
			}
		}

		public Funcionarios GetFuncionarioById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlId = $"{sql} WHERE f.id = @id";
				var funcionarioId = connection.QuerySingleOrDefault<Funcionarios>(sqlId, new { id });

				return funcionarioId;
			}
		}

		public IEnumerable<Funcionarios> GetFuncionarioByIdEmpresa(int idEmpresa)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlIdEmpresa = $"{sql} WHERE f.id_empresa = @idEmpresa";
				var funcionarioId = connection.Query<Funcionarios>(sqlIdEmpresa, new { idEmpresa }).AsList();

				return funcionarioId;
			}
		}

		public IEnumerable<Funcionarios> GetFuncionarioByEmail(string login, string senha)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlLogin = $"{sql} WHERE f.login = @login AND f.senha = @senha";
				var funcionarioLogin = connection.Query<Funcionarios>(sqlLogin, new { login, senha }).AsList();

				return funcionarioLogin;
			}
		}

		public IEnumerable<Veiculos> GetVeiculoByIdFuncionario(int idFuncionario)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlVeiculo = "SELECT v.id AS Id," +
								 " v.placa AS Placa," +
								 " v.modelo AS Modelo," +
								 " v.cor AS Cor," +
								 " v.marca AS Marca," +
								 " v.id_funcionario AS IdFuncionario" +
								 " FROM veiculos v" +
								 " WHERE id_funcionario = @idFuncionario";

				var funcionarioId = connection.Query<Veiculos>(sqlVeiculo, new { idFuncionario }).AsList();

				return funcionarioId;
			}
		}

		public IEnumerable<Funcionarios> GetFuncionariosAtivos(int idEmpresa)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlIdEmpresa = $"{sql} WHERE f.id_empresa = @idEmpresa AND f.id_plano < 1000";
				var funcionarioId = connection.Query<Funcionarios>(sqlIdEmpresa, new { idEmpresa }).AsList();

				return funcionarioId;
			}
		}

		public void CadastraVeiculo(Veiculos veiculo)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO veiculos (id, placa, modelo, cor, marca, id_funcionario) VALUES (@id, @placa, @modelo, @cor, @marca, @idFuncionario);";

				connection.Execute(sql, new
				{
					id = veiculo.Id,
					placa = veiculo.Placa,
					modelo = veiculo.Modelo,
					cor = veiculo.Cor,
					marca = veiculo.Marca,
					idFuncionario = veiculo.IdFuncionario
				});
			}
		}

		public void RegistraSaidaEstacionamento(int id, DateTime horaSaida)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{

				connection.Open();
				var sql = "UPDATE visitas_estacionamento SET status = 2, hora_saida = @horaSaida WHERE id = @id;";
				connection.Execute(sql, new { id, horaSaida });
			}
		}

		public void UpdateSenhaFuncionario(Funcionarios funcionario, string novaSenha)
		{
			var existingFuncionario = GetFuncionarioById(funcionario.Id);
			if (existingFuncionario != null)
			{
				if (VerificaSenha(existingFuncionario.Senha, funcionario.Senha))
				{
					existingFuncionario.Senha = HashSenha(novaSenha);

					using (MySqlConnection connection = new MySqlConnection(_connectionString))
					{
						connection.Open();
						existingFuncionario.Senha = funcionario.Senha;

						connection.UpdateAsync(existingFuncionario);
					}
				}
				else
				{
					throw new Exception("Senha atual inválida.");
				}
			}
			else
			{
				throw new Exception("Funcionário não encontrado.");
			}
		}

		public void UpdatePlanoFuncionario(int idFuncionario, int idPlano)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "UPDATE funcionarios SET id_plano = @idPlano WHERE id = @idFuncionario;";
				connection.Execute(sql, new { idPlano, idFuncionario });
			}
		}

		public void ExcluirVeiculo(int idVeiculo)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlSelect = "SELECT v.id as Id, v.hora_chegada AS HoraChegada, v.hora_saida AS HoraSaida, v.status AS Status, v.id_estacionamento AS IdEstacionamento, v.id_funcionario AS IdFuncionario, v.id_veiculo as IdVeiculo FROM visitas_estacionamento v;";
				var visitasVeiculo = connection.Query<VisitasEstacionamento>(sqlSelect, new { idVeiculo }).ToList();

				if (visitasVeiculo != null)
				{
					var sqlVisita = "DELETE FROM visitas_estacionamento WHERE id_veiculo = @id;";
					connection.Execute(sqlVisita, new { id = idVeiculo });
				}

				var sql = "DELETE FROM veiculos WHERE id = @id;";
				connection.Execute(sql, new { id = idVeiculo });
			}
		}

		private bool VerificaSenha(string senhaAtual, string senhaInformada)
		{
			string hashedSenhaInformada = HashSenha(senhaInformada);
			return hashedSenhaInformada == senhaAtual;
		}

		private string HashSenha(string senha)
		{
			using (var sha256 = new SHA256Managed())
			{
				var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
				return BitConverter.ToString(bytes).Replace("-", "").ToLower();
			}
		}

		// Verificar como será realizado o pagamento do Plano
		public void PagarPlano(Funcionarios funcionario)
		{

		}
	}
}