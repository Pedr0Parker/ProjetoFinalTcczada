using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Repositorios;

namespace EasyPark.Models.RegrasNegocio.Empresa
{
	public class EmpresaBusinessRule
	{
		private readonly EmpresaRepositorio _repositorio;

		public EmpresaBusinessRule(EmpresaRepositorio repositorio)
		{
			_repositorio = repositorio;
		}

		public IEnumerable<Empresas> GetAllEmpresas()
		{
			return _repositorio.GetAllEmpresas();
		}

		public Empresas GetEmpresaById(long id)
		{
			return _repositorio.GetEmpresaById(id);
		}

		public Empresas GetEmpresaByNome(string nome)
		{
			return _repositorio.GetEmpresaByNome(nome);
		}

		public void CadastraFuncionario(Funcionarios funcionario)
		{
			_repositorio.CadastraFuncionario(funcionario);
		}
	}
}
