using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Funcionario;

namespace EasyPark.Models.Repositorios.Interfaces.Estacionamento
{
    public interface IEstacionamentoRepositorio
    {
		public IEnumerable<Estacionamentos> GetAllEstacionamentos();

		public Estacionamentos GetEstacionamentoById(long id);

		public Estacionamentos GetEstacionamentoByNome(string nome);

		public IEnumerable<Funcionarios> VerificaFuncionarios(string cpfFuncionario);

		public void RegistraVisitaEstacionamento(Estacionamentos estacionamento, Funcionarios funcionario, int status);

	}
}
