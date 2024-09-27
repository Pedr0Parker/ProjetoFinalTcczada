using Dapper;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.VisitaEstacionamento;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
    public class VisitaEstacionamentoRepositorio
    {
        private List<VisitasEstacionamento> visitasEstacionamento;

		private readonly IConfiguration _configuration;

		string connectionString = "Server=localhost;Database=easypark;Uid=root;";

		public VisitaEstacionamentoRepositorio(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IEnumerable<VisitasEstacionamento> GetAllVisitas(Estacionamentos estacionamento)
		{
			var idEstacionamento = GetEstacionamentoById(estacionamento.Id);

			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM visitas_estacionamento v WHERE v.id_estacionamento = @idEstacionamento;";
				var visitas = connection.Query<VisitasEstacionamento>(sql).AsList();

				return visitas;
			}
		}

		public VisitasEstacionamento GetVisitaById(long id)
        {
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM visitas_estacionamento v WHERE v.id = @id;";
				var visitasId = connection.QuerySingleOrDefault<VisitasEstacionamento>(sql, new { id });

				return visitasId;
			}
		}

		private Estacionamentos GetEstacionamentoById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM estacionamentos e WHERE e.id = @id;";
				var estacionamentoId = connection.QuerySingleOrDefault<Estacionamentos>(sql, new { id });

				return estacionamentoId;
			}
		}
	}
}
