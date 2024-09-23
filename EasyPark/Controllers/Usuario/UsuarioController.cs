using EasyPark.Models.Entidades.Usuario;
using EasyPark.Models.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Usuario
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsuarioController : ControllerBase
	{
		private UsuarioRepositorio repositorio = new UsuarioRepositorio();

		[HttpGet]
		public IActionResult Index()
		{
			var usuarios = repositorio.GetAllUsuarios();
			return Ok(usuarios);
		}

		[HttpGet("{id}")]
		public IActionResult Details(long id)
		{
			try
			{
				var usuario = repositorio.GetUsuarioById(id);
				if (usuario == null)
				{
					return NotFound("Usuário não localizado!");
				}

				return Ok(usuario);
			}
			catch
			{
				return BadRequest("Erro ao localizar usuário!");
			}
		}

		[HttpPost]
		public IActionResult Create(Usuarios usuario)
		{
			if (ModelState.IsValid)
			{
				repositorio.AddUsuario(usuario);
				return Ok("Usuário criado com sucesso!");
			}

			return BadRequest("Dados inválidos!");
		}

		[HttpPut]
		public IActionResult Update(Usuarios usuario)
		{
			if (ModelState.IsValid)
			{
				repositorio.UpdateUsuario(usuario);
				return Ok("Usuário atualizado com sucesso!");
			}

			return BadRequest("Dados inválidos!");
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(long id)
		{
			if (ModelState.IsValid)
			{
				repositorio.DeleteUsuario(id);
				return Ok("Usuário deletado com sucesso!");
			}

			return BadRequest("Erro ao deletar usuário!");
		}
	}
}
