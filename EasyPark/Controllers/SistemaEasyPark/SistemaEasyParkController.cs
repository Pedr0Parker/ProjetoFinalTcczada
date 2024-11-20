using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.RegrasNegocio.SistemaEasyPark;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.SistemaEasyPark
{
	[ApiController]
	[Route("easyPark")]
	public class SistemaEasyParkController : ControllerBase
	{
		private readonly SistemaEasyParkBusinessRule _businessRule;

		public SistemaEasyParkController(SistemaEasyParkBusinessRule businessRule)
		{
			_businessRule = businessRule;
		}

		#region Empresa

		/// <summary>
		/// Realiza o cadastro de uma nova empresa
		/// </summary>
		/// <param name="empresa"></param>
		[HttpPost("cadastrar-empresa")]
		public IActionResult CadastrarEmpresa(Empresas empresa)
		{
			try
			{
				_businessRule.CadastraEmpresa(empresa);
				return Ok($"Cadastro da empresa {empresa.Nome} realizado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao cadastrar uma empresa.");
				throw;
			}
		}

		/// <summary>
		/// Atualiza uma empresa de acordo com o seu Id
		/// </summary>
		/// <param name="empresa"></param>
		[HttpPut("atualizar-empresa")]
		public IActionResult AtualizarEmpresa(Empresas empresa)
		{
			try
			{
				_businessRule.UpdateEmpresa(empresa);
				return Ok("Empresa atualizada com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao atualizar empresa.");
				throw;
			}
		}

		#endregion

		#region Estacionamento

		/// <summary>
		/// Realiza o cadastro de um novo estacionamento
		/// </summary>
		/// <param name="estacionamento"></param>
		/// <returns></returns>
		[HttpPost("cadastrar-estacionamento")]
		public IActionResult CadastrarEstacionamento(Estacionamentos estacionamento)
		{
			try
			{
				_businessRule.CadastraEstacionamento(estacionamento);
				return Ok($"Cadastro do estacionamento {estacionamento.Nome} realizado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao cadastrar um estacionamento.");
				throw;
			}
		}

		/// <summary>
		/// Atualiza um estacionamento de acordo com o seu Id
		/// </summary>
		/// <param name="estacionamento"></param>
		/// <returns></returns>
		[HttpPut("atualizar-estacionamento")]
		public IActionResult AtualizarEstacionamento(Estacionamentos estacionamento)
		{
			try
			{
				_businessRule.UpdateEstacionamento(estacionamento);
				return Ok("Estacionamento atualizado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao atualizar estacionamento.");
				throw;
			}
		}

		#endregion
	}
}
