using EasyPark.Models.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Plano
{
	[ApiController]
	[Route("plano")]
	public class PlanoController : ControllerBase
	{
		private readonly PlanoRepositorio repositorio;

		public PlanoController(IConfiguration configuration)
		{
			repositorio = new PlanoRepositorio(configuration);
		}

		#region Métodos Get

		/// <summary>
		/// Realiza a busca de todos os planos cadastrados no banco de dados
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-planos")]
		public IActionResult BuscarPlanos()
		{
			var planos = repositorio.GetAllPlanos();
			if (planos is null) return BadRequest("Houve um erro ao buscar os planos.");

			return Ok(planos);
		}

		/// <summary>
		/// Realiza a busca do plano via Id cadastrado no banco de dados
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-plano/id/{id}")]
		public IActionResult BuscarPlanoViaId(int id)
		{
			var idPlano = repositorio.GetPlanoById(id);
			if (idPlano is null) return NotFound($"Plano de Id {id} não cadastrado no sistema.");

			return Ok(idPlano);
		}

		#endregion
	}
}
