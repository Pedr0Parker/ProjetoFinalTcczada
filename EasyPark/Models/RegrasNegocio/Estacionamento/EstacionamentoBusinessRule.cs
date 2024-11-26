using EasyPark.Models.Entidades.Estacionamento;
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

        public VisitasEstacionamento VisitasPendentesFuncionarios(int idFuncionario)
        {
            if (idFuncionario <= 0)
            {
                throw new ArgumentException("Funcionário inválido", nameof(idFuncionario));
            }

            var visitaPendenteFuncionario = _repositorio.VisitasPendentesFuncionarios(idFuncionario);
            return visitaPendenteFuncionario;
        }

        public IEnumerable<VisitasEstacionamento> VerificaVisitasEstacionamento(int idEstacionamento)
		{
			if (idEstacionamento <= 0)
			{
				throw new ArgumentException("Estacionamento inválido", nameof(idEstacionamento));
			}

			var visitaEstacionamento = _repositorio.VerificaVisitasEstacionamento(idEstacionamento);
			return visitaEstacionamento;
		}

		public IEnumerable<VisitasEstacionamento> VerificaSolicitacaoVisitas(int idEstacionamento)
		{
			if (idEstacionamento <= 0)
			{
				throw new ArgumentException("Estacionamento inválido", nameof(idEstacionamento));
			}

			var solicitacaoVisitaEstacionamento = _repositorio.VerificaSolicitacaoVisitas(idEstacionamento);
			return solicitacaoVisitaEstacionamento;
		}

		public IEnumerable<VisitasEstacionamento> VerificaVagasOcupadas(int idEstacionamento)
		{
			if (idEstacionamento <= 0)
			{
				throw new ArgumentException("Estacionamento inválido", nameof(idEstacionamento));
			}

			var vagasOcupadas = _repositorio.VerificaVagasOcupadas(idEstacionamento);
			return vagasOcupadas;
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

		public void RegistraVisitaEstacionamento(VisitasEstacionamento visitasEstacionamento)
		{
			if (visitasEstacionamento.IdEstacionamento == 0)
			{
				throw new ArgumentNullException(nameof(visitasEstacionamento.IdEstacionamento), "Estacionamento inválido");
			}

			if (visitasEstacionamento.IdFuncionario == 0)
			{
				throw new ArgumentNullException(nameof(visitasEstacionamento.IdFuncionario), "Funcionário inválido");
			}

			if (visitasEstacionamento.IdVeiculo == 0)
			{
				throw new ArgumentNullException(nameof(visitasEstacionamento.IdVeiculo), "Veículo inválido");
			}

			if (visitasEstacionamento.Status > 2)
			{
				throw new ArgumentException(nameof(visitasEstacionamento.Status), "O Status não pode ser maior que 2");
			}

			_repositorio.RegistraVisitaEstacionamento(visitasEstacionamento);
		}

		public void RegistraSolicitacaoVisitaEstacionamento(int idVisita, DateTime horaChegada)
		{
			if (idVisita <= 0)
			{
				throw new ArgumentException("Esta visita não é inválida", nameof(idVisita));
			}

			_repositorio.RegistraSolicitacaoVisitaEstacionamento(idVisita, horaChegada);
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

		public void ExcluirSolicitacaoVisita(int idVisita)
		{
			if (idVisita <= 0)
			{
				throw new ArgumentException("Id da visita inválido", nameof(idVisita));
			}

			_repositorio.ExcluirSolicitacaoVisita(idVisita);
		}
	}
}
