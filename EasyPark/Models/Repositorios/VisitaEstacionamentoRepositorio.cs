using Dapper;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.VisitaEstacionamento;
using MySql.Data.MySqlClient;
using static Slapper.AutoMapper;

namespace EasyPark.Models.Repositorios
{
    public class VisitaEstacionamentoRepositorio
    {
		private readonly string _connectionString;
		private readonly string sql;

		public VisitaEstacionamentoRepositorio(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DbEasyParkConnection");

			sql = "SELECT v.id as Id," +
				" v.hora_chegada AS HoraChegada," +
				" v.hora_saida AS HoraSaida," +
				" v.status AS Status," +
				" v.id_estacionamento AS IdEstacionamento," +
				" v.id_funcionario AS IdFuncionario" +
				" FROM visitas_estacionamento v";
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
				var sqlId = $"{sql} WHERE v.id = @id ORDER BY v.id DESC;";
				var visitasId = connection.QuerySingleOrDefault<VisitasEstacionamento>(sqlId, new { id });

				return visitasId;
			}
		}

		public IEnumerable<VisitasEstacionamento> GetVisitaByIdFuncionario(int idFuncionario)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlId = $"{sql} WHERE v.id_funcionario = @idFuncionario;";
				var visitasIdFuncionario = connection.Query<VisitasEstacionamento>(sqlId, new { idFuncionario }).ToList();

				return visitasIdFuncionario;
			}
		}

		private Estacionamentos GetEstacionamentoById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "SELECT e.id," +
				" e.login AS Login," +
				" e.senha AS Senha," +
				" e.nome AS Nome," +
				" e.cnpj AS Cnpj," +
				" e.endereco AS Endereco," +
				" e.contato AS Contato," +
				" e.data_cadastro AS DataCadastro," +
				" FROM estacionamentos e";

				var estacionamentoId = connection.QuerySingleOrDefault<Estacionamentos>(sql, new { id });

				return estacionamentoId;
			}
		}
	}
}
