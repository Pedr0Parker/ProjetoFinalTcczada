using Dapper;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.VisitaEstacionamento;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
    public class VisitaEstacionamentoRepositorio
    {
		private readonly string _connectionString;
		private readonly string sql;

		public VisitaEstacionamentoRepositorio(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DbEasyParkConnection");
			sql = "SELECT * FROM visitas_estacionamento v";
		}

		public IEnumerable<VisitasEstacionamento> GetAllVisitas(Estacionamentos estacionamento)
		{
			var idEstacionamento = GetEstacionamentoById(estacionamento.Id);

			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlVisitas = $"{sql} WHERE v.id_estacionamento = @idEstacionamento";
				var visitas = connection.Query<VisitasEstacionamento>(sqlVisitas, new { idEstacionamento = estacionamento.Id }).AsList();

				return visitas;
			}
		}

		public VisitasEstacionamento GetVisitaById(long id)
        {
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlId = $"{sql} WHERE v.id = @id";
				var visitasId = connection.QuerySingleOrDefault<VisitasEstacionamento>(sqlId, new { id });

				return visitasId;
			}
		}

		private Estacionamentos GetEstacionamentoById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM estacionamentos e WHERE e.id = @id;";
				var estacionamentoId = connection.QuerySingleOrDefault<Estacionamentos>(sql, new { id });

				return estacionamentoId;
			}
		}
	}
}
