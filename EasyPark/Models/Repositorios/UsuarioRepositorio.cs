using Dapper;
using DapperExtensions;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.Usuario;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
    public class UsuarioRepositorio
    {
		private List<Usuarios> empresas;

		private readonly IConfiguration _configuration;

		string connectionString = "Server=localhost;Database=easypark;Uid=root;";

		public UsuarioRepositorio(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IEnumerable<Usuarios> GetAllUsuarios()
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM ep_usuarios u;";
				var empresas = connection.Query<Usuarios>(sql).AsList();

				return empresas;
			}
		}

		public Usuarios GetUsuarioById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM ep_usuarios u WHERE u.id = @id;";
				var usuarioId = connection.QuerySingleOrDefault<Usuarios>(sql, new { id });

				return usuarioId;
			}
		}
    }
}
