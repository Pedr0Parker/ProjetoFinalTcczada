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
			return _repositorio.GetEstacionamentoById(id);
		}

		public Estacionamentos GetEstacionamentoByNome(string nome)
		{
			return _repositorio.GetEstacionamentoByNome(nome);
		}

		public IEnumerable<Funcionarios> VerificaFuncionarios(string cpf)
		{
			return _repositorio.VerificaFuncionarios(cpf);
		}

		public void RegistraVisitaEstacionamento(Estacionamentos estacionamento, Funcionarios funcionario, int status)
		{
			_repositorio.RegistraVisitaEstacionamento(estacionamento, funcionario, status);
		}

		public void CriarVisitaDependente(string cpfDependente, long idEstacionamento, int status, long idFuncionario)
		{
			_repositorio.CriarVisitaDependente(cpfDependente, idEstacionamento, status, idFuncionario);
		}

		public void AplicaDesconto(VisitasEstacionamento visitaEstacionamento, decimal percentualDescontoEstacionamento, decimal taxaHorariaEstacionamento)
		{
			_repositorio.AplicaDesconto(visitaEstacionamento, percentualDescontoEstacionamento, taxaHorariaEstacionamento);
		}
	}
}
