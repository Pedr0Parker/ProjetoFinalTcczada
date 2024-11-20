using EasyPark.Models.Entidades.Plano;

namespace EasyPark.Models.Repositorios.Interfaces.Plano
{
	public interface IPlanoRepositorio
	{
		public IEnumerable<Planos> GetAllPlanos();

		public Planos GetPlanoById(long id);
	}
}
