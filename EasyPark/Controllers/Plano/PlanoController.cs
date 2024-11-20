using EasyPark.Models.RegrasNegocio.Plano;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Plano
{
	[ApiController]
	[Route("plano")]
	public class PlanoController : ControllerBase
	{
		private readonly PlanoBusinessRule _businessRule;

		public PlanoController(PlanoBusinessRule businessRule)
		{
			_businessRule = businessRule;
		}

		#region Métodos Get

		/// <summary>
		/// Realiza a busca de todos os planos cadastrados no banco de dados
		/// </summary>
		/// <returns></returns>
		[HttpGet("buscar-planos")]
		public IActionResult BuscarPlanos()
		{
			var planos = _businessRule.GetAllPlanos();
			if (planos is null) return BadRequest("Houve um erro ao buscar os planos.");

			return Ok(planos);
		}

		/// <summary>
		/// Realiza a busca do plano via Id cadastrado no banco de dados
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("buscar-plano/id/{id}")]
		public IActionResult BuscarPlanoViaId(int id)
		{
			var idPlano = _businessRule.GetPlanoById(id);
			if (idPlano is null) return NotFound($"Plano de Id {id} não cadastrado no sistema.");

			return Ok(idPlano);
		}

		#endregion
	}
}
