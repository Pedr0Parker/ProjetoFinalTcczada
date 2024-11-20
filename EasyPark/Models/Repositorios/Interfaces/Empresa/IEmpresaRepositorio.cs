using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Funcionario;

namespace EasyPark.Models.Repositorios.Interfaces.Empresa
{
    public interface IEmpresaRepositorio
    {
		// To Do: Verificar funcionalidades da Interface da Empresa

		public IEnumerable<Empresas> GetAllEmpresas();

		public Empresas GetEmpresaById(long id);

		public Empresas GetEmpresaByNome(string nome);

		public void CadastraFuncionario(Funcionarios funcionario);

	}
}
