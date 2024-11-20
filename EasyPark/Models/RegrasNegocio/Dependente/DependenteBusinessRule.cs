using EasyPark.Models.Entidades.Dependente;
using EasyPark.Models.Repositorios;

namespace EasyPark.Models.RegrasNegocio.Dependente
{
	public class DependenteBusinessRule
	{
		private readonly DependenteRepositorio _repositorio;

		public DependenteBusinessRule(DependenteRepositorio repositorio)
		{
			_repositorio = repositorio;
		}

		public IEnumerable<Dependentes> GetAllDependentes()
		{
			return _repositorio.GetAllDependentes();
		}

		public Dependentes GetDependenteById(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentException("Id inválido", nameof(id));
			}

			var dependente = _repositorio.GetDependenteById(id);
			return dependente;
		}
	}
}
