using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.Veiculo;
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
			if (id <= 0)
			{
				throw new ArgumentException("Id inválido", nameof(id));
			}

			var empresa = _repositorio.GetEmpresaById(id);
			return empresa;
		}

		public IEnumerable<Empresas> GetEmpresaByEmail(string login, string senha)
		{
			if (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(senha))
			{
				throw new ArgumentException("Nome de Empresa inválido", nameof(login));
			}

			var emailEmpresa = _repositorio.GetEmpresaByEmail(login, senha);
			return emailEmpresa;
		}

		public void CadastraFuncionario(Funcionarios funcionario)
		{
			var idEmpresa = funcionario.IdEmpresa;

			var empresa = _repositorio.GetEmpresaById(idEmpresa);
			if (empresa == null)
			{
				throw new InvalidOperationException("Empresa não encontrada");
			}

			if (funcionario == null)
			{
				throw new ArgumentNullException(nameof(funcionario), "Funcionário inválido");
			}

			if (string.IsNullOrEmpty(funcionario.CpfCnpj))
			{
				throw new ArgumentException("CPF do funcionário é obrigatório", nameof(funcionario));
			}

			_repositorio.CadastraFuncionario(funcionario);
		}

		public void ExcluirFuncionario(int idFuncionario)
		{
			if (idFuncionario <= 0)
			{
				throw new ArgumentException("Id do funcionário inválido.", nameof(idFuncionario));
			}

			_repositorio.ExcluirFuncionario(idFuncionario);
		}
	}
}
