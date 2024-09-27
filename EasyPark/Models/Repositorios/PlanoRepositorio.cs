using Dapper;
using EasyPark.Models.Entidades.Plano;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
	public class PlanoRepositorio
	{
		private List<Planos> planos;

		private readonly IConfiguration _configuration;

		string connectionString = "Server=localhost;Database=easypark;Uid=root;";

		public PlanoRepositorio(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IEnumerable<Planos> GetAllPlanos()
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM planos p;";
				var planos = connection.Query<Planos>(sql).AsList();

				return planos;
			}
		}

		public Planos GetPlanoById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM planos p WHERE p.id = @id;";
				var planoId = connection.QuerySingleOrDefault<Planos>(sql, new { id });

				return planoId;
			}
		}
	}
}
