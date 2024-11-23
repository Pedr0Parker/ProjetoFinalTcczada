using Dapper;
using EasyPark.Models.Entidades.Veiculo;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
	public class VeiculoRepositorio
	{
		private readonly string _connectionString;
		private readonly string sql;

		public VeiculoRepositorio(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DbEasyParkConnection");

			sql = "SELECT v.id as Id," +
				" v.placa AS Placa," +
				" v.modelo AS Modelo," +
				" v.cor AS Cor," +
				" v.marca AS Marca," +
				" v.id_funcionario AS IdFuncionario" +
				" FROM veiculos v";
		}

		public IEnumerable<Veiculos> GetAllVeiculos()
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlVeiculos = $"{sql}";

				var veiculos = connection.Query<Veiculos>(sqlVeiculos).AsList();

				return veiculos;
			}
		}

		public Veiculos GetVeiculoById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlVeiculosId = $"{sql} WHERE v.id = @id";
				var veiculoId = connection.QuerySingleOrDefault<Veiculos>(sqlVeiculosId, new { id });

				return veiculoId;
			}
		}
	}
}
