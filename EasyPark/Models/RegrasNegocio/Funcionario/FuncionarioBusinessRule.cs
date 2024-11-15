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
			if (id <= 0)
			{
				throw new ArgumentException("Id inválido", nameof(id));
			}

			var funcionario = _repositorio.GetFuncionarioById(id);
			return funcionario;
		}

		//public Funcionarios GetFuncionarioByEmail(string email)
		//{

		//}

		public void CadastraVeiculo(Veiculos veiculo)
		{
			//var funcionario = _repositorio.GetFuncionarioById(idFuncionario);
			//var idFuncionario = Convert.ToInt64(veiculo.IdFuncionario);

			//if (funcionario == null)
			//{
			//	throw new InvalidOperationException("Funcionário não encontrado");
			//}

			if (veiculo == null)
			{
				throw new ArgumentNullException(nameof(veiculo), "Veículo inválido");
			}

			if (string.IsNullOrEmpty(veiculo.Placa))
			{
				throw new ArgumentException("Placa do veículo é obrigatória", nameof(veiculo));
			}

			_repositorio.CadastraVeiculo(veiculo);
		}

		public void CadastraDependente(Funcionarios funcionario, Dependentes dependente)
		{
			if (funcionario == null)
			{
				throw new ArgumentNullException(nameof(funcionario), "Funcionário inválido");
			}

			if (dependente == null)
			{
				throw new ArgumentNullException(nameof(dependente), "Dependente inválido");
			}

			var funcionarioExistente = _repositorio.GetFuncionarioById(funcionario.Id);
			if (funcionarioExistente == null)
			{
				throw new InvalidOperationException("Funcionário não encontrado");
			}

			if (string.IsNullOrEmpty(dependente.Nome))
			{
				throw new ArgumentException("Nome do dependente é obrigatório", nameof(dependente));
			}

			_repositorio.CadastraDependente(funcionario, dependente);
		}

		public void UpdateSenhaFuncionario(Funcionarios funcionario, string novaSenha)
		{
			if (funcionario == null)
			{
				throw new ArgumentNullException(nameof(funcionario), "Funcionário inválido");
			}

			var funcionarioExistente = _repositorio.GetFuncionarioById(funcionario.Id);
			if (funcionarioExistente == null)
			{
				throw new InvalidOperationException("Funcionário não encontrado");
			}

			if (string.IsNullOrEmpty(novaSenha))
			{
				throw new ArgumentException("Nova senha é obrigatória", nameof(novaSenha));
			}

			_repositorio.UpdateSenhaFuncionario(funcionario, novaSenha);
		}
	}
}
