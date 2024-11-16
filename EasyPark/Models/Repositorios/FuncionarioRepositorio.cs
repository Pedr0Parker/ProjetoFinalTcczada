using Dapper;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.Veiculo;
using EasyPark.Models.Entidades.Dependente;
using MySql.Data.MySqlClient;
using DapperExtensions;
using System.Text;
using XSystem.Security.Cryptography;
using static Slapper.AutoMapper;

namespace EasyPark.Models.Repositorios
{
	public class FuncionarioRepositorio
	{
		private readonly string _connectionString;
		private readonly string sql;

		public FuncionarioRepositorio(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DbEasyParkConnection");
			sql = "SELECT * FROM funcionarios f";
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

		public Funcionarios GetFuncionarioByEmail(string login, string senha)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlLogin = $"{sql} WHERE f.login = @login AND f.senha = @senha";
				var funcionarioLogin = connection.QuerySingleOrDefault<Funcionarios>(sqlLogin, new { login, senha });

				return funcionarioLogin;
			}
		}

		public void CadastraVeiculo(Veiculos veiculo)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO veiculos (id, placa, modelo, cor, marca) VALUES (@id, @placa, @modelo, @cor, @marca);";

				connection.Execute(sql, new
				{
					id = veiculo.Id,
					placa = veiculo.Placa,
					modelo = veiculo.Modelo,
					cor = veiculo.Cor,
					marca = veiculo.Marca,
					//idFuncionario = veiculo.IdFuncionario
				});
			}
		}

		public void CadastraDependente(Funcionarios funcionario, Dependentes dependente)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "SELECT COUNT(*) FROM dependentes WHERE id_funcionario = @idFuncionario;";
				var countDependentes = connection.QuerySingleOrDefault<int>(sql, new { idFuncionario = funcionario.Id });

				if (countDependentes >= 4)
				{
					throw new Exception("Funcionário já possui 4 dependentes cadastrados.");
				}
			}

			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO dependentes (login, senha, nome, cpf, contato, email, id_funcionario) VALUES (@login, @senha, @nome, @cpf, @contato, @email, @idFuncionario);";
				connection.Execute(sql, new
				{
					login = dependente.Login,
					senha = dependente.Senha,
					nome = dependente.Nome,
					cpf = dependente.Cpf,
					contato = dependente.Contato,
					email = dependente.Email,
					idFuncionario = funcionario.Id
				});
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