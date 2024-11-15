using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Plano;
using EasyPark.Models.Entidades.Usuario;
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
				_businessRule.CadastraPlano(plano);
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
				_businessRule.UpdatePlano(plano);
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
				_businessRule.DeletePlano(id);
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
		[HttpPut]
		[Route("atualizar-empresa")]
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
				_businessRule.DeleteEmpresa(id);
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
		[HttpPut]
		[Route("atualizar-estacionamento")]
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
				_businessRule.DeleteEstacionamento(id);
				return Ok("Estacionamento excluído com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao deletar estacionamento.");
			}
		}

		#endregion

		#region Usuário

		/// <summary>
		/// Realiza o cadastro de um novo usuario
		/// </summary>
		/// <param name="usuario"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("cadastrar-usuario")]
		public IActionResult CadastrarUsuario(Usuarios usuario)
		{
			try
			{
				_businessRule.CadastraUsuario(usuario);
				return Ok($"Cadastro do usuario {usuario.Nome} realizado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao cadastrar um usuario.");
				throw;
			}
		}

		/// <summary>
		/// Atualiza um usuário de acordo com o seu Id
		/// </summary>
		/// <param name="usuario"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("atualizar-usuario")]
		public IActionResult AtualizarUsuario(Usuarios usuario)
		{
			try
			{
				_businessRule.UpdateUsuario(usuario);
				return Ok("Usuário atualizado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao atualizar usuário.");
				throw;
			}
		}

		/// <summary>
		/// Deleta o usuário desejado de acordo com seu Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		[Route("excluir-usuario")]
		public IActionResult ExcluirUsuario(long id)
		{
			try
			{
				_businessRule.DeleteUsuario(id);
				return Ok("Usuário excluído com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao deletar usuário.");
			}
		}

		#endregion
	}
}
