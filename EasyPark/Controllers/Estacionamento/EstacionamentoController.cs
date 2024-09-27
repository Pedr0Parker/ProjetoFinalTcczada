using EasyPark.Models.Entidades.VisitaEstacionamento;
using EasyPark.Models.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Estacionamento
{
	[ApiController]
	[Route("estacionamento")]
	public class EstacionamentoController : ControllerBase
	{
		private readonly EstacionamentoRepositorio repositorio;

		public EstacionamentoController(IConfiguration configuration)
		{
			repositorio = new EstacionamentoRepositorio(configuration);
		}

		#region Métodos Get

		/// <summary>
		/// Realiza a busca de todos os estacionamentos cadastrados no banco de dados
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-estacionamentos")]
		public IActionResult BuscarEstacionamentos()
		{
			var estacionamentos = repositorio.GetAllEstacionamentos();
			if (estacionamentos is null) return BadRequest("Houve um erro ao buscar os estacionamentos.");

			return Ok(estacionamentos);
		}

		/// <summary>
		/// Realiza a busca do estacionamento via Id cadastrado no banco de dados
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-estacionamento/id/{id}")]
		public IActionResult BuscasEstacionamentoViaId(int id)
		{
			var idEstacionamento = repositorio.GetEstacionamentoById(id);
			if (idEstacionamento is null) return NotFound($"Estacionamento de Id {id} não cadastrado no sistema.");

			return Ok(idEstacionamento);
		}

		/// <summary>
		/// Realiza a busca do estacionamento via Nome cadastrado no banco de dados
		/// </summary>
		/// <param name="nome"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-estacionamento/nome/{nome}")]
		public IActionResult BuscasEstacionamentoViaNome(string nome)
		{
			var nomeEstacionamento = repositorio.GetEstacionamentoByNome(nome);
			if (nomeEstacionamento is null) return NotFound($"Estacionamento de nome {nome} não cadastrado no sistema.");

			return Ok(nomeEstacionamento);
		}

		/// <summary>
		/// Realiza a busca do funcionário que realizou o check-in no estacionamento, via CPF cadastrado no banco de dados
		/// </summary>
		/// <param name="cpf"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("verifica-funcionario/cpf/{cpf}")]
		public IActionResult VerificarFuncionarios(string cpf)
		{
			var buscaFuncionarioCpf = repositorio.VerificaFuncionarios(cpf);
			if (buscaFuncionarioCpf is null) return NotFound($"Funcionário de CPF {cpf} não cadastrado no sistema.");

			return Ok(buscaFuncionarioCpf);
		}

		#endregion

		#region Métodos Post
		/// <summary>
		/// Realiza o cadastro de uma visita feita no estacionamento
		/// </summary>
		/// <param name="visitaEstacionamento"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("cadastra-visita-estacionamento")]
		public IActionResult CadastrarVisitaEstacionamento(VisitasEstacionamento visitaEstacionamento)
		{
			var estacionamento = visitaEstacionamento.IdEstacionamento;
			var funcionario = visitaEstacionamento.IdFuncionario;
			var status = visitaEstacionamento.Status;

			repositorio.RegistraVisitaEstacionamento(estacionamento, funcionario, status);

			return Ok("Visita estacionamento cadastrada com sucesso.");
		}

		#endregion

		// To Do: Verificar se haverá tela de busca de estacionamento por filtros

		//public IActionResult AplicarDesconto()
		//{
		//    return Ok("Desconto aplicado com sucesso!"); // Retorna sucesso ao aplicar desconto
		//}
	}
}
