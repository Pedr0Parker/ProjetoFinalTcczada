namespace EasyPark.Models.Entidades.VisitaEstacionamento
{
	public class VisitasEstacionamento
	{
		public long Id { get; set; }
		public DateTime HoraChegada { get; set; }
		public DateTime HoraSaida { get; set; }
		public int Status { get; set; }
		public int IdEstacionamento { get; set; }
		public int IdFuncionario { get; set; }
		public int IdVeiculo { get; set; }
	}
}
