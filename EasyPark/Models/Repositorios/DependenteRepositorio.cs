using Dapper;
using EasyPark.Models.Entidades.Dependente;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
	public class DependenteRepositorio
	{
		private readonly string _connectionString;
		private readonly string sql;

		public DependenteRepositorio(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DbEasyParkConnection");
			sql = "SELECT * FROM dependentes d";
		}

		public IEnumerable<Dependentes> GetAllDependentes()
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var dependentes = connection.Query<Dependentes>(sql).AsList();

				return dependentes;
			}
		}

		public Dependentes GetDependenteById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlId = $"{sql} WHERE d.id = @id";
				var dependenteId = connection.QuerySingleOrDefault<Dependentes>(sqlId, new { id });

				return dependenteId;
			}
		}
	}
}
