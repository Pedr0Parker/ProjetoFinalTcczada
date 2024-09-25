using Dapper;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Funcionario;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
	public class EstacionamentoRepositorio
	{
		private List<Estacionamentos> estacionamentos;

		private readonly IConfiguration _configuration;

		string connectionString = "Server=localhost;Database=easypark;Uid=root;";

		public EstacionamentoRepositorio(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// To Do: implementar posteriormente, uma possível busca por filtro

		public IEnumerable<Estacionamentos> GetAllEstacionamentos()
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM estacionamentos e;";
				var estacionamentos = connection.Query<Estacionamentos>(sql).AsList();

				return estacionamentos;
			}
		}

		public Estacionamentos GetEstacionamentoById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM estacionamentos e WHERE e.id = @id;";
				var estacionamentoId = connection.QuerySingleOrDefault<Estacionamentos>(sql, new { id });

				return estacionamentoId;
			}
		}

		public Estacionamentos GetEstacionamentoByNome(string nome)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM estacionamentos e WHERE e.nome = @nome;";
				var estacionamentoNome = connection.QuerySingleOrDefault<Estacionamentos>(sql, new { nome });

				return estacionamentoNome;
			}
		}

		public IEnumerable<Funcionarios> VerificaFuncionarios(string cpfFuncionario)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM funcionarios f WHERE f.cpf = @cpfFuncionario;";
				var funcionario = connection.Query<Funcionarios>(sql, new { cpfFuncionario }).AsList();

				return funcionario;
			}
		}


		public void AplicaDesconto()
		{

		}

		public void RegistrarVisitante()
		{

		}
	}
}
