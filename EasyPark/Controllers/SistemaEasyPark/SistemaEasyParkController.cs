using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Plano;
using EasyPark.Models.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.SistemaEasyPark
{
	[ApiController]
	[Route("easyPark")]
	public class SistemaEasyParkController : ControllerBase
	{
		private readonly SistemaEasyParkRepositorio repositorio;

		public SistemaEasyParkController(IConfiguration configuration)
		{
			repositorio = new SistemaEasyParkRepositorio(configuration);
		}

		#region Planos

		/// <summary>
		/// Realiza o cadastro de um novo plano
		/// </summary>
		/// <param name="plano"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("cadastrar-plano")]
		public IActionResult CadastrarPlano(Planos plano)
		{
			try
			{
				repositorio.CadastraPlano(plano);
				return Ok($"Cadastro do Plano {plano.NomePlano} realizado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao cadastrar um novo Plano.");
				throw;
			}
		}

		/// <summary>
		/// Atualiza um plano de acordo com o seu Id
		/// </summary>
		/// <param name="plano"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("atualizar-plano")]
		public IActionResult AtualizarPlano(Planos plano)
		{
			try
			{
				repositorio.UpdatePlano(plano);
				return Ok("Plano atualizado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao atualizar plano.");
				throw;
			}
		}

		/// <summary>
		/// Deleta o plano desejado de acordo com seu Id
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete]
		[Route("excluir-plano")]
		public IActionResult ExcluirPlano(long id)
		{
			try
			{
				repositorio.DeletePlano(id);
				return Ok("Plano excluído com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao excluir plano.");
			}
		}

		#endregion

		#region Empresa

		/// <summary>
		/// Realiza o cadastro de uma nova empresa
		/// </summary>
		/// <param name="empresa"></param>
		[HttpPost]
		[Route("cadastrar-empresa")]
		public IActionResult CadastrarEmpresa(Empresas empresa)
		{
			try
			{
				repositorio.CadastraEmpresa(empresa);
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
		[HttpPut]
		[Route("atualizar-empresa")]
		public IActionResult AtualizarEmpresa(Empresas empresa)
		{
			try
			{
				repositorio.UpdateEmpresa(empresa);
				return Ok("Empresa atualizada com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao atualizar empresa.");
				throw;
			}
		}

		/// <summary>
		/// Deleta a empresa desejada de acordo com seu Id
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete]
		[Route("excluir-empresa")]
		public IActionResult ExcluirEmpresa(long id)
		{
			try
			{
				repositorio.DeleteEmpresa(id);
				return Ok("Empresa excluída com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao deletar empresa.");
			}
		}

		#endregion

		#region Estacionamento

		/// <summary>
		/// Realiza o cadastro de um novo estacionamento
		/// </summary>
		/// <param name="estacionamento"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("cadastrar-estacionamento")]
		public IActionResult CadastrarEstacionamento(Estacionamentos estacionamento)
		{
			try
			{
				repositorio.CadastraEstacionamento(estacionamento);
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
		[HttpPut]
		[Route("atualizar-estacionamento")]
		public IActionResult AtualizarEstacionamento(Estacionamentos estacionamento)
		{
			try
			{
				repositorio.UpdateEstacionamento(estacionamento);
				return Ok("Estacionamento atualizado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao atualizar estacionamento.");
				throw;
			}
		}

		/// <summary>
		/// Deleta o estacionamento desejado de acordo com seu Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("excluir-estacionamento")]
		public IActionResult ExcluirEstacionamento(long id)
		{
			try
			{
				repositorio.DeleteEstacionamento(id);
				return Ok("Estacionamento excluído com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao deletar estacionamento.");
			}
		}

		#endregion
	}
}
