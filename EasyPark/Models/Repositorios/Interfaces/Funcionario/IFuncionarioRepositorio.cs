using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.Veiculo;

namespace EasyPark.Models.Repositorios.Interfaces.Funcionario
{
	public interface IFuncionarioRepositorio
	{
		public IEnumerable<Funcionarios> GetAllFuncionarios();

		public Funcionarios GetFuncionarioById(long id);

		public void CadastraVeiculo(Veiculos veiculo);

		public void UpdateSenhaFuncionario(Funcionarios funcionario, string novaSenha);

		public void CriarVisitaDependente(string cpfDependente, long idEstacionamento, int status, long idFuncionario);
	}
}
