using EasyPark.Models.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Dependente
{
	[ApiController]
	[Route("dependente")]
	public class DependenteController : ControllerBase
	{
		private readonly DependenteRepositorio repositorio;

		public DependenteController(IConfiguration configuration)
		{
			repositorio = new DependenteRepositorio(configuration);
		}

		#region Métodos Get

		/// <summary>
		/// Realiza a busca de todos os dependentes cadastrados no banco de dados
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-dependentes")]
		public IActionResult BuscarDependentes()
		{
			var dependentes = repositorio.GetAllDependentes();
			if (dependentes is null) return BadRequest("Houve um erro ao buscar os dependentes.");

			return Ok(dependentes);
		}

		/// <summary>
		/// Realiza a busca do dependente via Id cadastrado no banco de dados
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-dependente/id/{id}")]
		public IActionResult BuscarDependenteViaId(int id)
		{
			var idDependente = repositorio.GetDependenteById(id);
			if (idDependente is null) return NotFound($"Dependente de Id {id} não cadastrado no sistema.");

			return Ok(idDependente);
		}

		#endregion
	}
}
