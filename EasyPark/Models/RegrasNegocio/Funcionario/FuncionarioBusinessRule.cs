using EasyPark.Models.Entidades.Dependente;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.Veiculo;
using EasyPark.Models.Repositorios;

namespace EasyPark.Models.RegrasNegocio.Funcionario
{
	public class FuncionarioBusinessRule
	{
		private readonly FuncionarioRepositorio _repositorio;

		public FuncionarioBusinessRule(FuncionarioRepositorio repositorio)
		{
			_repositorio = repositorio;
		}

		public IEnumerable<Funcionarios> GetAllFuncionarios()
		{
			return _repositorio.GetAllFuncionarios();
		}

		public Funcionarios GetFuncionarioById(long id)
		{
			return _repositorio.GetFuncionarioById(id);
		}

		public void CadastraVeiculo(Veiculos veiculo)
		{
			_repositorio.CadastraVeiculo(veiculo);
		}

		public void CadastraDependente(Funcionarios funcionario, Dependentes dependente)
		{
			_repositorio.CadastraDependente(funcionario, dependente);
		}

		public void UpdateSenhaFuncionario(Funcionarios funcionario, string novaSenha)
		{
			_repositorio.UpdateSenhaFuncionario(funcionario, novaSenha);
		}
	}
}
