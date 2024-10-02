using EasyPark.Models.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Usuario
{
	[ApiController]
	[Route("usuario")]
	public class UsuarioController : ControllerBase
	{
		private readonly UsuarioRepositorio repositorio;

		public UsuarioController(IConfiguration configuration)
		{
			repositorio = new UsuarioRepositorio(configuration);
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
			var usuarios = repositorio.GetAllUsuarios();
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
			var idUsuario = repositorio.GetUsuarioById(id);
			if (idUsuario is null) return NotFound($"Usuario de Id {id} não cadastrado no sistema.");

			return Ok(idUsuario);
		}

		#endregion
	}
}
