using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Plano;

namespace EasyPark.Models.Repositorios.Interfaces.SistemaEasyPark
{
	public interface ISistemaEasyParkRepositorio
	{
		public void CadastraPlano(Planos plano);

		public void CadastraEmpresa(Empresas empresa);

		public void CadastraEstacionamento(Estacionamentos estacionamento);

		public void UpdatePlano(Planos plano);

		public void UpdateEmpresa(Empresas empresa);

		public void UpdateEstacionamento(Estacionamentos estacionamento);

		public void DeletePlano(long id);

		public void DeleteEmpresa(long id);

		public void DeleteEstacionamento(long id);
	}
}
