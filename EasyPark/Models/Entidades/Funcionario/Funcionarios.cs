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
        public string Email { get; set; }
        public decimal ValorPlano { get; set; }
        public DateTime DataCadastro { get; set; }
        public Planos IdPlano { get; set; }
    }
}
