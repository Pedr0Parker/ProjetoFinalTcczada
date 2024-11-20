using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.VisitaEstacionamento;

namespace EasyPark.Models.Repositorios.Interfaces.VisitaEstacionamento
{
	public interface IVisitaEstacionamentoRepositorio
	{
		public IEnumerable<VisitasEstacionamento> GetAllVisitas(Estacionamentos estacionamento);

		public VisitasEstacionamento GetVisitaById(long id);
	}
}
