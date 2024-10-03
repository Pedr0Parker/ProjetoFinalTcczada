using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Plano;
using EasyPark.Models.Entidades.Usuario;
using EasyPark.Models.Repositorios;

namespace EasyPark.Models.RegrasNegocio.SistemaEasyPark
{
	public class SistemaEasyParkBusinessRule
	{
		private readonly SistemaEasyParkRepositorio _repositorio;

		public SistemaEasyParkBusinessRule(SistemaEasyParkRepositorio repositorio)
		{
			_repositorio = repositorio;
		}

		public void CadastraPlano(Planos plano)
		{
			_repositorio.CadastraPlano(plano);
		}

		public void UpdatePlano(Planos plano)
		{
			_repositorio.UpdatePlano(plano);
		}

		public void DeletePlano(long id)
		{
			_repositorio.DeletePlano(id);
		}

		public void CadastraEmpresa(Empresas empresa)
		{
			_repositorio.CadastraEmpresa(empresa);
		}

		public void UpdateEmpresa(Empresas empresa)
		{
			_repositorio.UpdateEmpresa(empresa);
		}

		public void DeleteEmpresa(long id)
		{
			_repositorio.DeleteEmpresa(id);
		}

		public void CadastraEstacionamento(Estacionamentos estacionamento)
		{
			_repositorio.CadastraEstacionamento(estacionamento);
		}

		public void UpdateEstacionamento(Estacionamentos estacionamento)
		{
			_repositorio.UpdateEstacionamento(estacionamento);
		}

		public void DeleteEstacionamento(long id)
		{
			_repositorio.DeleteEstacionamento(id);
		}

		public void CadastraUsuario(Usuarios usuario)
		{
			_repositorio.CadastraUsuario(usuario);
		}

		public void UpdateUsuario(Usuarios usuario)
		{
			_repositorio.UpdateUsuario(usuario);
		}

		public void DeleteUsuario(long id)
		{
			_repositorio.DeleteUsuario(id);
		}
	}
}
