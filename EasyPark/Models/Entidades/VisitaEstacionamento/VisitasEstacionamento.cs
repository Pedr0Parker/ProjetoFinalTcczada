using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Funcionario;

namespace EasyPark.Models.Entidades.VisitaEstacionamento
{
    public class VisitasEstacionamento
    {
        public long Id { get; set; }
        public DateTime HoraChegada { get; set; }
        public DateTime HoraSaida { get; set; }
        public int Status { get; set; }
        public Estacionamentos IdEstacionamento { get; set; }
        public Funcionarios IdFuncionario { get; set; }
    }
}
