using Dapper;
using EasyPark.Models.Entidades.Dependente;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
	public class DependenteRepositorio
	{
		private List<Dependentes> dependentes;

		private readonly IConfiguration _configuration;

		string connectionString = "Server=localhost;Database=easypark;Uid=root;";

		public DependenteRepositorio(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IEnumerable<Dependentes> GetAllDependentes()
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM dependentes d;";
				var dependentes = connection.Query<Dependentes>(sql).AsList();

				return dependentes;
			}
		}

		public Dependentes GetDependenteById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM dependentes d WHERE d.id = @id;";
				var dependenteId = connection.QuerySingleOrDefault<Dependentes>(sql, new { id });

				return dependenteId;
			}
		}
	}
}
