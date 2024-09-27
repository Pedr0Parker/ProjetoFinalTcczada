using Dapper;
using DapperExtensions;
using EasyPark.Models.Entidades.Dependente;
using EasyPark.Models.Entidades.Estacionamento;
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
		private List<Funcionarios> estacionamentos;

		private readonly IConfiguration _configuration;

		string connectionString = "Server=localhost;Database=easypark;Uid=root;";

		public FuncionarioRepositorio(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IEnumerable<Funcionarios> GetAllFuncionarios()
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM funcionarios f;";
				var funcionarios = connection.Query<Funcionarios>(sql).AsList();

				return funcionarios;
			}
		}

		public Funcionarios GetFuncionarioById(long id)
        {
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM funcionarios f WHERE f.id = @id;";
				var funcionarioId = connection.QuerySingleOrDefault<Funcionarios>(sql, new { id });

				return funcionarioId;
			}
		}

		public void CadastraVeiculo(Veiculos veiculo)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
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

		public void CadastraDependente(Funcionarios funcionario, Dependentes dependente)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT COUNT(*) FROM dependentes WHERE id_funcionario = @idFuncionario;";
				var countDependentes = connection.QuerySingleOrDefault<int>(sql, new { idFuncionario = funcionario.Id });

				if (countDependentes >= 4)
				{
					throw new Exception("Funcionário já possui 4 dependentes cadastrados.");
				}
			}

			using (MySqlConnection connection = new MySqlConnection(connectionString))
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

					using (MySqlConnection connection = new MySqlConnection(connectionString))
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

		public void PagarPlano(Funcionarios funcionario)
		{
			
		}
	}
}
