using EasyPark.Models.Entidades.VisitaEstacionamento;
using EasyPark.Models.RegrasNegocio.Estacionamento;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Estacionamento
{
	[ApiController]
	[Route("estacionamento")]
	public class EstacionamentoController : ControllerBase
	{
		private readonly EstacionamentoBusinessRule _businessRule;

		public EstacionamentoController(EstacionamentoBusinessRule businessRule)
		{
			_businessRule = businessRule;
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
			var estacionamentos = _businessRule.GetAllEstacionamentos();
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
			var idEstacionamento = _businessRule.GetEstacionamentoById(id);
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
			var nomeEstacionamento = _businessRule.GetEstacionamentoByNome(nome);
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
			var buscaFuncionarioCpf = _businessRule.VerificaFuncionarios(cpf);
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

			_businessRule.RegistraVisitaEstacionamento(estacionamento, funcionario, status);

			return Ok("Visita estacionamento cadastrada com sucesso.");
		}

		/// <summary>
		/// Realiza o cadastro de visita do dependente
		/// </summary>
		/// <param name="cpfDependente"></param>
		/// <param name="idEstacionamento"></param>
		/// <param name="status"></param>
		/// <param name="idFuncionario"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("cadastrar-visita-dependente")]
		public IActionResult CadastrarVisitaDependente(string cpfDependente, long idEstacionamento, int status, long idFuncionario)
		{
			try
			{
				_businessRule.CriarVisitaDependente(cpfDependente, idEstacionamento, status, idFuncionario);
				return Ok("Cadastro de visita realizado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao cadastrar visita.");
				throw;
			}
		}

		/// <summary>
		/// Realiza a aplicação de desconto caso o estacionamento aprovar
		/// </summary>
		/// <param name="visitaEstacionamento"></param>
		/// <param name="percentualDescontoEstacionamento"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("aplicar-desconto")]
		public IActionResult AplicarDesconto(VisitasEstacionamento visitaEstacionamento, decimal percentualDescontoEstacionamento, decimal taxaHorariaEstacionamento)
		{
			if (taxaHorariaEstacionamento <= 0)
			{
				return BadRequest("Taxa horária inválida. Deve ser um valor maior que zero.");
			}

			_businessRule.AplicaDesconto(visitaEstacionamento, percentualDescontoEstacionamento, taxaHorariaEstacionamento);
			return Ok("Desconto aplicado com sucesso!");
		}

		#endregion

		// To Do: Verificar se haverá tela de busca de estacionamento por filtros

		//public IActionResult AplicarDesconto()
		//{
		//    return Ok("Desconto aplicado com sucesso!"); // Retorna sucesso ao aplicar desconto
		//}
	}
}
