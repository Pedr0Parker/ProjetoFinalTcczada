using Dapper;
using DapperExtensions;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.Usuario;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
    public class UsuarioRepositorio
    {
		private readonly string _connectionString;
		private readonly string sql;

		public UsuarioRepositorio(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DbEasyParkConnection");
			sql = "SELECT * FROM ep_usuarios u";
		}

		public IEnumerable<Usuarios> GetAllUsuarios()
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var usuarios = connection.Query<Usuarios>(sql).AsList();

				return usuarios;
			}
		}

		public Usuarios GetUsuarioById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlId = $"{sql} WHERE u.id = @id";
				var usuarioId = connection.QuerySingleOrDefault<Usuarios>(sqlId, new { id });

				return usuarioId;
			}
		}
    }
}
