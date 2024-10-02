using EasyPark.Models.Entidades.Usuario;

namespace EasyPark.Models.Repositorios.Interfaces.Usuario
{
    public interface IUsuarioRepositorio
    {
		public IEnumerable<Usuarios> GetAllUsuarios();

		public Usuarios GetUsuarioById(long id);
	}
}
