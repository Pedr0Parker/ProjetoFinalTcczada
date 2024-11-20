using EasyPark.Models.RegrasNegocio.Dependente;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Dependente
{
	[ApiController]
	[Route("dependente")]
	public class DependenteController : ControllerBase
	{
		private readonly DependenteBusinessRule _businessRule;

		public DependenteController(DependenteBusinessRule businessRule)
		{
			_businessRule = businessRule;
		}

		#region Métodos Get

		/// <summary>
		/// Realiza a busca de todos os dependentes cadastrados no banco de dados
		/// </summary>
		/// <returns></returns>
		[HttpGet("buscar-dependentes")]
		public IActionResult BuscarDependentes()
		{
			var dependentes = _businessRule.GetAllDependentes();
			if (dependentes is null) return BadRequest("Houve um erro ao buscar os dependentes.");

			return Ok(dependentes);
		}

		/// <summary>
		/// Realiza a busca do dependente via Id cadastrado no banco de dados
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("buscar-dependente/id/{id}")]
		public IActionResult BuscarDependenteViaId(int id)
		{
			var idDependente = _businessRule.GetDependenteById(id);
			if (idDependente is null) return NotFound($"Dependente de Id {id} não cadastrado no sistema.");

			return Ok(idDependente);
		}

		#endregion
	}
}
