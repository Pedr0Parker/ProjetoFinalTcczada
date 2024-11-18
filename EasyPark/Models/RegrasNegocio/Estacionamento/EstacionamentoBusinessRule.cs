using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.VisitaEstacionamento;
using EasyPark.Models.Repositorios;

namespace EasyPark.Models.RegrasNegocio.Estacionamento
{
	public class EstacionamentoBusinessRule
	{
		private readonly EstacionamentoRepositorio _repositorio;

		public EstacionamentoBusinessRule(EstacionamentoRepositorio repositorio)
		{
			_repositorio = repositorio;
		}

		public IEnumerable<Estacionamentos> GetAllEstacionamentos()
		{
			return _repositorio.GetAllEstacionamentos();
		}

		public Estacionamentos GetEstacionamentoById(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentException("Id inválido", nameof(id));
			}

			var estacionamento = _repositorio.GetEstacionamentoById(id);
			return estacionamento;
		}

		public Estacionamentos GetEstacionamentoByEmail(string login, string senha)
		{
			if (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(senha))
			{
				throw new ArgumentException("Email de Estacionamento inválido", nameof(login));
			}

			var emailEstacionamento = _repositorio.GetEstacionamentoByEmail(login, senha);
			return emailEstacionamento;
		}

		//public IEnumerable<Funcionarios> VerificaFuncionarios(string cpf)
		//{
		//	if (string.IsNullOrEmpty(cpf))
		//	{
		//		throw new ArgumentException("CPF de funcionário inválido", nameof(cpf));
		//	}

		//	var cpfFuncionario = _repositorio.VerificaFuncionarios(cpf);
		//	return cpfFuncionario;
		//}

		public void RegistraVisitaEstacionamento(int estacionamento, int funcionario, int status)
		{
			if (estacionamento == 0)
			{
				throw new ArgumentNullException(nameof(estacionamento), "Estacionamento inválido");
			}

			if (funcionario == 0)
			{
				throw new ArgumentNullException(nameof(funcionario), "Funcionário inválido");
			}

			if (status > 2)
			{
				throw new ArgumentException(nameof(status), "O Status não pode ser maior que 2");
			}

			var estacionamentoExistente = _repositorio.GetEstacionamentoById(estacionamento);
			if (estacionamentoExistente == null)
			{
				throw new InvalidOperationException("Estacionamento não encontrado");
			}

			_repositorio.RegistraVisitaEstacionamento(estacionamento, funcionario, status);
		}

		//public void CriarVisitaDependente(string cpfDependente, Estacionamentos estacionamento, int status, Funcionarios funcionario)
		//{
		//	if (cpfDependente == null)
		//	{
		//		throw new ArgumentNullException(nameof(cpfDependente), "CPF do dependente inválido");
		//	}

		//	if (estacionamento == null)
		//	{
		//		throw new ArgumentNullException(nameof(estacionamento), "Estacionamento inválido");
		//	}

		//	if (funcionario == null)
		//	{
		//		throw new ArgumentNullException(nameof(funcionario), "Funcionário inválido");
		//	}

		//	if (status > 2)
		//	{
		//		throw new ArgumentException(nameof(status), "O Status não pode ser maior que 2");
		//	}

		//	var estacionamentoExistente = _repositorio.GetEstacionamentoById(estacionamento.Id);
		//	if (estacionamentoExistente == null)
		//	{
		//		throw new InvalidOperationException("Estacionamento não encontrado");
		//	}

		//	_repositorio.CriarVisitaDependente(cpfDependente, estacionamento, status, funcionario);
		//}

		public void AplicaDesconto(VisitasEstacionamento visitaEstacionamento, decimal percentualDescontoEstacionamento, decimal taxaHorariaEstacionamento)
		{
			if (visitaEstacionamento == null)
			{
				throw new ArgumentNullException(nameof(visitaEstacionamento), "Visita estacionamento inválida");
			}

			if (percentualDescontoEstacionamento == 0)
			{
				throw new ArgumentException(nameof(percentualDescontoEstacionamento), "Não foi aplicado nenhum percentual de desconto");
			}

			if (taxaHorariaEstacionamento == 0)
			{
				throw new ArgumentException(nameof(taxaHorariaEstacionamento), "Taxa horária do estacionamento não aplicada");
			}

			_repositorio.AplicaDesconto(visitaEstacionamento, percentualDescontoEstacionamento, taxaHorariaEstacionamento);
		}
	}
}
