namespace EasyPark.Models.Entidades.Funcionario
{
	public class Funcionarios
	{
		public long Id { get; set; }
		public string Login { get; set; }
		public string Senha { get; set; }
		public string Nome { get; set; }
		public string CpfCnpj { get; set; }
		public string Contato { get; set; }
		public DateTime DataCadastro { get; set; }
		public int IdPlano { get; set; }
		public int IdEmpresa { get; set; }
	}
}
