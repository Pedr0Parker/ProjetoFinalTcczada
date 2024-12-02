using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers
{
	[ApiController]
	[Route("home")]
	public class HomeController : ControllerBase
	{
		#region Métodos Get

		[HttpGet]
		public IActionResult Index()
		{
			return Ok("Bem-vindo à API do EasyPark");
		}

		[HttpGet("para-empresas")]
		public IActionResult ParaEmpresas()
		{
			return Ok("Redirecionando para a tela de empresas...");
		}

		[HttpGet("para-estacionamentos")]
		public IActionResult ParaEstacionamentos()
		{
			return Ok("Redirecionando para a tela de estacionamentos...");
		}

		[HttpGet("login")]
		public IActionResult Login()
		{
			return Ok("Redirecionando para a tela de login...");
		}

		#endregion
	}
}