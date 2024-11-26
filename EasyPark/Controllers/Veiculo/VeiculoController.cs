using EasyPark.Models.RegrasNegocio.Veiculo;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Veiculo
{
	[ApiController]
	[Route("veiculo")]
	public class VeiculoController : ControllerBase
	{
		private readonly VeiculoBusinessRule _businessRule;

		public VeiculoController(VeiculoBusinessRule businessRule)
		{
			_businessRule = businessRule;
		}

		#region Métodos Get

		/// <summary>
		/// Realiza a busca de todos os veículos cadastrados no banco de dados
		/// </summary>
		/// <returns></returns>
		[HttpGet("buscar-veiculos")]
		public IActionResult BuscarVeiculos()
		{
			var veiculos = _businessRule.GetAllVeiculos();
			if (veiculos is null) return BadRequest("Houve um erro ao buscar os veículos.");

			return Ok(veiculos);
		}

		/// <summary>
		/// Realiza a busca do veículo via Id cadastrado no banco de dados
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("buscar-veiculo/id/{id}")]
		public IActionResult BuscarVeiculoViaId(int id)
		{
			var idVeiculo = _businessRule.GetVeiculoById(id);
			if (idVeiculo is null) return NotFound($"Veículo de Id {id} não cadastrado no sistema.");

			return Ok(idVeiculo);
		}

		#endregion
	}
}
