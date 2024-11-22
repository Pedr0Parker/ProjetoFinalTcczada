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

		public IEnumerable<Estacionamentos> GetEstacionamentoByEmail(string login, string senha)
		{
			if (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(senha))
			{
				throw new ArgumentException("Email de estacionamento inválido", nameof(login));
			}

			var emailEstacionamento = _repositorio.GetEstacionamentoByEmail(login, senha);
			return emailEstacionamento;
		}

		public IEnumerable<VisitasEstacionamento> VerificaFuncionarios(int idFuncionario)
		{
			if (idFuncionario <= 0)
			{
				throw new ArgumentException("Funcionário inválido", nameof(idFuncionario));
			}

			var visitaFuncionario = _repositorio.VerificaFuncionarios(idFuncionario);
			return visitaFuncionario;
		}

		public VisitasEstacionamento VerificaUltimaVisita(int idFuncionario)
		{
			if (idFuncionario <= 0)
			{
				throw new ArgumentException("Funcionário inválido", nameof(idFuncionario));
			}

			var visitaFuncionario = _repositorio.VerificaUltimaVisita(idFuncionario);
			return visitaFuncionario;
		}

		public void RegistraVisitaEstacionamento(DateTime horaChegada, DateTime horaSaida, int estacionamento, int funcionario, int status, int veiculo)
		{
			if (estacionamento == 0)
			{
				throw new ArgumentNullException(nameof(estacionamento), "Estacionamento inválido");
			}

			if (funcionario == 0)
			{
				throw new ArgumentNullException(nameof(funcionario), "Funcionário inválido");
			}

			if (veiculo == 0)
			{
				throw new ArgumentNullException(nameof(funcionario), "Veículo inválido");
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

			_repositorio.RegistraVisitaEstacionamento(horaChegada, horaSaida, estacionamento, funcionario, status, veiculo);
		}

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
