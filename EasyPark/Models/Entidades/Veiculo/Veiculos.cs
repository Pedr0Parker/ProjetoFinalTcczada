using EasyPark.Models.Entidades.Funcionario;

namespace EasyPark.Models.Entidades.Veiculo
{
    public class Veiculos
    {
        public long Id { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Cor { get; set; }
        public string Marca { get; set; }
        public long IdFuncionario { get; set; }
    }
}
