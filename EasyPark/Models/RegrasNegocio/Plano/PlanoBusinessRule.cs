using EasyPark.Models.Entidades.Plano;
using EasyPark.Models.Repositorios;

namespace EasyPark.Models.RegrasNegocio.Plano
{
	public class PlanoBusinessRule
	{
		private readonly PlanoRepositorio _repositorio;

		public PlanoBusinessRule(PlanoRepositorio repositorio)
		{
			_repositorio = repositorio;
		}

		public IEnumerable<Planos> GetAllPlanos()
		{
			return _repositorio.GetAllPlanos();
		}

		public Planos GetPlanoById(long id)
		{
			return _repositorio.GetPlanoById(id);
		}
	}
}
