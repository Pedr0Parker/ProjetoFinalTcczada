using EasyPark.Models.Entidades.Dependente;
using EasyPark.Models.Entidades.Funcionario;

namespace EasyPark.Models.Entidades.CadastroDependente
{
	public class CadastrarDependenteRequest
	{
		public Funcionarios Funcionario { get; set; }
		public Dependentes Dependente { get; set; }
	}
}
