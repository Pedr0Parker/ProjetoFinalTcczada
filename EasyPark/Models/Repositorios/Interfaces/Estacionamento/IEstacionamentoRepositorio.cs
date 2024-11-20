using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.VisitaEstacionamento;

namespace EasyPark.Models.Repositorios.Interfaces.Estacionamento
{
    public interface IEstacionamentoRepositorio
    {
		public IEnumerable<Estacionamentos> GetAllEstacionamentos();

		public Estacionamentos GetEstacionamentoById(long id);

		public Estacionamentos GetEstacionamentoByNome(string nome);

		public IEnumerable<Funcionarios> VerificaFuncionarios(string cpfFuncionario);

		public void RegistraVisitaEstacionamento(Estacionamentos estacionamento, Funcionarios funcionario, int status);

		public void CriarVisitaDependente(string cpfDependente, Estacionamentos estacionamento, int status, Funcionarios funcionario);

		public void AplicaDesconto(VisitasEstacionamento visita, decimal percentualDescontoEstacionamento, decimal taxaHorariaEstacionamento);

	}
}
