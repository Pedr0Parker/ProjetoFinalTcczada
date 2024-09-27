using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers
{
	[ApiController]
	[Route("home")]
	public class HomeController : ControllerBase
	{
		[HttpGet]
		public IActionResult Index()
		{
			return Ok("Bem-vindo à API do EasyPark");
		}

		[HttpGet]
		[Route("para-empresas")]
		public IActionResult ParaEmpresas()
		{
			return Ok("Redirecionando para a tela de empresas...");
		}

		[HttpGet]
		[Route("para-estacionamentos")]
		public IActionResult ParaEstacionamentos()
		{
			return Ok("Redirecionando para a tela de estacionamentos...");
		}

		[HttpGet]
		[Route("login")]
		public IActionResult Login()
		{
			return Ok("Redirecionando para a tela de login...");
		}
	}
}