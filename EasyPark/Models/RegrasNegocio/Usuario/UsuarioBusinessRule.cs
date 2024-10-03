using EasyPark.Models.Entidades.Usuario;
using EasyPark.Models.Repositorios;

namespace EasyPark.Models.RegrasNegocio.Usuario
{
	public class UsuarioBusinessRule
	{
		private readonly UsuarioRepositorio _repositorio;

		public UsuarioBusinessRule(UsuarioRepositorio repositorio)
		{
			_repositorio = repositorio;
		}

		public IEnumerable<Usuarios> GetAllUsuarios()
		{
			return _repositorio.GetAllUsuarios();
		}

		public Usuarios GetUsuarioById(long id)
		{
			return _repositorio.GetUsuarioById(id);
		}
	}
}
