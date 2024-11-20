using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;

namespace EasyPark.Models.Repositorios.Interfaces.SistemaEasyPark
{
	public interface ISistemaEasyParkRepositorio
	{
		public void CadastraEmpresa(Empresas empresa);

		public void CadastraEstacionamento(Estacionamentos estacionamento);

		public void UpdateEmpresa(Empresas empresa);

		public void UpdateEstacionamento(Estacionamentos estacionamento);
	}
}
