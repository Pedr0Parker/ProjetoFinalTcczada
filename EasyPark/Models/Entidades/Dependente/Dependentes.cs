using EasyPark.Models.Entidades.Funcionario;

namespace EasyPark.Models.Entidades.Dependente
{
	public class Dependentes
	{
		public long Id { get; set; }
		public string Login { get; set; }
		public string Senha { get; set; }
		public string Nome { get; set; }
		public string Cpf { get; set; }
		public string Contato { get; set; }
		public string Email { get; set; }
		public int IdFuncionario { get; set; }
	}
}
