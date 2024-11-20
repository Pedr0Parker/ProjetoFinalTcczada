namespace EasyPark.Models.Entidades.Empresa
{
    public class Empresas
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public string NomeDono { get; set; }
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public string Contato { get; set; }
        public DateTime DataCadastro { get; set; }
        public int IdPlano { get; set; }    

    }
}
