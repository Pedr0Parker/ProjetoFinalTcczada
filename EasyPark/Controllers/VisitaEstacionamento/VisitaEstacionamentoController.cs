using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.VisitaEstacionamento
{
	[ApiController]
	[Route("visitaEstacionamento")]
	public class VisitaEstacionamentoController : ControllerBase
	{
		private readonly VisitaEstacionamentoRepositorio repositorio;

		public VisitaEstacionamentoController(IConfiguration configuration)
		{
			repositorio = new VisitaEstacionamentoRepositorio(configuration);
		}

		#region Métodos Get

		/// <summary>
		/// Realiza a busca de todas as visitas cadastradas no banco de dados
		/// </summary>
		/// <param name="estacionamento"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-visitas-estacionamento")]
		public IActionResult BuscarVisitasEstacionamento(Estacionamentos estacionamento)
		{
			var visitas = repositorio.GetAllVisitas(estacionamento);
			if (visitas is null) return BadRequest($"Houve um erro ao buscar as visitas do estacionamento {estacionamento.Nome}.");

			return Ok(visitas);
		}

		/// <summary>
		/// Realiza a busca da visita ao estacionamento via Id cadastrado no banco de dados
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-visita-estacionamento/id/{id}")]
		public IActionResult BuscarVisitasEstacionamentoViaId(int id)
		{
			var idVisita = repositorio.GetVisitaById(id);
			if (idVisita is null) return NotFound($"Visita de Id {id} não cadastrada no sistema.");

			return Ok(idVisita);
		}

		#endregion
	}
}
