using EasyPark.Models.Entidades.Usuario;
using EasyPark.Models.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Estacionamento
{
	[ApiController]
	[Route("api/[controller]")]
	public class EstacionamentoController : ControllerBase
	{
		private EstacionamentoRepositorio repositorio = new EstacionamentoRepositorio();

		#region Métodos Get

		[HttpGet]
		public IActionResult Index()
		{
			var estacionamentos = repositorio.GetAllEstacionamentos();
			return Ok(estacionamentos);
		}

		#endregion

		// To Do: Verificar se haverá tela de busca de estacionamento por filtros

		// To Do: Verificar métodos implementados no diagrama de classe

		[HttpGet]
		[Route("verifica-funcionario/cpf/{cpf}")]
		public IActionResult VerificarUsuarios(string cpf)
		{
			var busca = repositorio.VerificaFuncionarios(cpf);
			return Ok(busca);
		}

		//public IActionResult AplicarDesconto()
		//{
		//    return Ok("Desconto aplicado com sucesso!"); // Retorna sucesso ao aplicar desconto
		//}
	}
}
