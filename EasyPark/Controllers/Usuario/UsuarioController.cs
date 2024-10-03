using EasyPark.Models.RegrasNegocio.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Usuario
{
	[ApiController]
	[Route("usuario")]
	public class UsuarioController : ControllerBase
	{
		private readonly UsuarioBusinessRule _businessRule;

		public UsuarioController(UsuarioBusinessRule businessRule)
		{
			_businessRule = businessRule;
		}

		#region Métodos Get

		/// <summary>
		/// Realiza a busca de todos os usuários cadastrados no banco de dados
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-usuarios")]
		public IActionResult BuscarUsuarios()
		{
			var usuarios = _businessRule.GetAllUsuarios();
			if (usuarios is null) return BadRequest("Houve um erro ao buscar os usuarios.");

			return Ok(usuarios);
		}

		/// <summary>
		/// Realiza a busca do usuário via Id cadastrado no banco de dados
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-usuario/id/{id}")]
		public IActionResult BuscarUsuarioViaId(int id)
		{
			var idUsuario = _businessRule.GetUsuarioById(id);
			if (idUsuario is null) return NotFound($"Usuario de Id {id} não cadastrado no sistema.");

			return Ok(idUsuario);
		}

		#endregion
	}
}
