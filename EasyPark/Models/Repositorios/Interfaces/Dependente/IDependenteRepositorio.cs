using EasyPark.Models.Entidades.Dependente;

namespace EasyPark.Models.Repositorios.Interfaces.Dependente
{
	public interface IDependenteRepositorio
	{
		public IEnumerable<Dependentes> GetAllDependentes();

		public Dependentes GetDependenteById(long id);
	}
}
