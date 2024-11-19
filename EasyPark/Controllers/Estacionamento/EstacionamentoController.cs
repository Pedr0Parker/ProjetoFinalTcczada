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
		[HttpGet("buscar-estacionamentos")]
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
		[HttpGet("buscar-estacionamento/id/{id}")]
		public IActionResult BuscasEstacionamentoViaId(int id)
		{
			var idEstacionamento = _businessRule.GetEstacionamentoById(id);
			if (idEstacionamento is null) return NotFound($"Estacionamento de Id {id} não cadastrado no sistema.");

			return Ok(idEstacionamento);
		}

		/// <summary>
		/// Realiza a busca do estacionamento via Nome cadastrado no banco de dados
		/// </summary>
		/// <param name="login"></param>
		/// <param name="senha"></param>
		/// <returns></returns>
		[HttpGet("buscar-estacionamento/login/{login}/senha/{senha}")]
		public IActionResult BuscasEstacionamentoViaEmail(string login, string senha)
		{
			var emailEstacionamento = _businessRule.GetEstacionamentoByEmail(login, senha);
			if (emailEstacionamento is null) return NotFound($"Estacionamento de email {login} não cadastrado no sistema.");

            var estacionamentoFormatado = emailEstacionamento.Select(e => new
            {
                e.Id,
				e.Login,
                e.Senha,
                e.Nome,
                e.Cnpj,
				e.Endereco,
				e.Contato,
                 DataCadastro = e.DataCadastro.ToString("dd/MM/yyyy HH:mm"),
            });

            return Ok(estacionamentoFormatado);
		}

		/// <summary>
		/// Realiza a busca do funcionário que realizou o check-in no estacionamento, via CPF cadastrado no banco de dados
		/// </summary>
		/// <param name="idFuncionario"></param>
		/// <returns></returns>
		[HttpGet("verifica-visitas-funcionario/idFuncionario/{idFuncionario}")]
		public IActionResult VerificarFuncionarios(int idFuncionario)
	{
			var buscaFuncionarioCpf = _businessRule.VerificaFuncionarios(idFuncionario);
			if (buscaFuncionarioCpf is null) return NotFound($"Funcionário de CPF {idFuncionario} não cadastrado no sistema.");

            var visitasFormatadas = buscaFuncionarioCpf.Select(v => new
            {
                v.Id,
                HoraChegada = v.HoraChegada.ToString("dd/MM/yyyy HH:mm"),
                HoraSaida = v.HoraSaida.ToString("dd/MM/yyyy HH:mm"),
                v.Status,
                v.IdEstacionamento,
                v.IdFuncionario
            });

            return Ok(visitasFormatadas);
		}

		#endregion

		#region Métodos Post

		/// <summary>
		/// Realiza o cadastro de uma visita feita no estacionamento
		/// </summary>
		/// <param name="visitaEstacionamento"></param>
		/// <returns></returns>
		[HttpPost("cadastra-visita-estacionamento")]
		public IActionResult CadastrarVisitaEstacionamento(VisitasEstacionamento visitaEstacionamento)
		{
			try
			{
				var estacionamento = visitaEstacionamento.IdEstacionamento;
				var funcionario = visitaEstacionamento.IdFuncionario;
				var status = visitaEstacionamento.Status;

				_businessRule.RegistraVisitaEstacionamento(estacionamento, funcionario, status);

				return Ok("Visita estacionamento cadastrada com sucesso.");
			}
			catch
			{
				return BadRequest("Erro ao cadastrar visita.");
				throw;
			}
		}

		/// <summary>
		/// Realiza o cadastro de visita do dependente
		/// </summary>
		/// <param name="cpfDependente"></param>
		/// <param name="idEstacionamento"></param>
		/// <param name="status"></param>
		/// <param name="idFuncionario"></param>
		/// <returns></returns>
		//[HttpPost("cadastrar-visita-dependente")]
		//public IActionResult CadastrarVisitaDependente(string cpfDependente, VisitasEstacionamento visitaEstacionamento)
		//{
		//	try
		//	{
		//		var estacionamento = visitaEstacionamento.IdEstacionamento;
		//		var funcionario = visitaEstacionamento.IdFuncionario;
		//		var status = visitaEstacionamento.Status;

		//		_businessRule.CriarVisitaDependente(cpfDependente, estacionamento, status, funcionario);
		//		return Ok("Visita estacionamento cadastrada com sucesso.");
		//	}
		//	catch
		//	{
		//		return BadRequest("Erro ao cadastrar visita.");
		//		throw;
		//	}
		//}

		/// <summary>
		/// Realiza a aplicação de desconto caso o estacionamento aprovar
		/// </summary>
		/// <param name="visitaEstacionamento"></param>
		/// <param name="percentualDescontoEstacionamento"></param>
		/// <returns></returns>
		[HttpPost("aplicar-desconto")]
		public IActionResult AplicarDesconto(VisitasEstacionamento visitaEstacionamento, decimal percentualDescontoEstacionamento, decimal taxaHorariaEstacionamento)
		{
			try
			{
				if (taxaHorariaEstacionamento <= 0)
				{
					return BadRequest("Taxa horária inválida. Deve ser um valor maior que zero.");
				}

				_businessRule.AplicaDesconto(visitaEstacionamento, percentualDescontoEstacionamento, taxaHorariaEstacionamento);
				return Ok("Desconto aplicado com sucesso!");
			}
			catch (Exception)
			{
				return BadRequest("Erro ao aplicar desconto.");
				throw;
			}
		}

		#endregion
	}
}
