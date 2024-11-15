using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Plano;

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
        public decimal ValorPlano { get; set; }
        public DateTime DataCadastro { get; set; }
        public long IdPlano { get; set; }
        public long IdEmpresa { get; set; }

		public virtual Planos Plano { get; set; }
		public virtual Empresas Empresa { get; set; }
	}
}
