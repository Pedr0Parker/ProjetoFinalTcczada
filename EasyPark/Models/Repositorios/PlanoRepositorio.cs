using Dapper;
using EasyPark.Models.Entidades.Plano;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
	public class PlanoRepositorio
	{
		private readonly string _connectionString;
		private readonly string sql;

		public PlanoRepositorio(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DbEasyParkConnection");

			sql = "SELECT p.id as Id," +
				" p.nome AS NomePlano," +
				" p.status AS StatusPlano," +
				" p.valor AS ValorPlano" +
				" FROM planos p";
		}

		public IEnumerable<Planos> GetAllPlanos()
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlPlanos = $"{sql} WHERE id < 1000";

                var planos = connection.Query<Planos>(sqlPlanos).AsList();

				return planos;
			}
		}

		public Planos GetPlanoById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlPlanoId = $"{sql} WHERE p.id = @id";
				var planoId = connection.QuerySingleOrDefault<Planos>(sqlPlanoId, new { id });

				return planoId;
			}
		}
	}
}