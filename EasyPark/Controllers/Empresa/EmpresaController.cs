using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Repositorios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Empresa
{
	[ApiController]
	[Route("empresa")]
	public class EmpresaController : ControllerBase
	{
		private readonly EmpresaRepositorio repositorio;

		public EmpresaController(IConfiguration configuration)
		{
			repositorio = new EmpresaRepositorio(configuration);
		}

		#region Métodos Get

		/// <summary>
		/// Realiza a busca de todas as empresas cadastradas no banco de dados
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-empresas")]
		public IActionResult BuscarEmpresas()
		{
			var empresas = repositorio.GetAllEmpresas();
			if (empresas is null) return BadRequest("Houve um erro ao buscar as empresas.");

			return Ok(empresas);
		}

		/// <summary>
		/// Realiza a busca da empresa via Id cadastrado no banco de dados
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-empresa/id/{id}")]
		public IActionResult BuscarEmpresaViaId(int id)
		{
			var idEmpresa = repositorio.GetEmpresaById(id);
			if (idEmpresa is null) return NotFound($"Empresa de Id {id} não cadastrado no sistema.");

			return Ok(idEmpresa);
		}

		/// <summary>
		/// Realiza a busca da empresa via Nome cadastrado no banco de dados
		/// </summary>
		/// <param name="nome"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-empresa/nome/{nome}")]
		public IActionResult BuscarEmpresaViaNome(string nome)
		{
			var nomeEmpresa = repositorio.GetEmpresaByNome(nome);
			if (nomeEmpresa is null) return NotFound($"Empresa de nome {nome} não cadastrada no sistema.");

			return Ok(nomeEmpresa);
		}

		#endregion

		#region Métodos Post

		/// <summary>
		/// Realiza o cadastro do funcionário pela empresa
		/// </summary>
		/// <param name="funcionario"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("cadastrar-funcionario")]
		public IActionResult CadastrarFuncionario(Funcionarios funcionario)
		{
			try
			{
				repositorio.CadastraFuncionario(funcionario);
				return Ok($"Cadastro do funcionário {funcionario.Nome} realizado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao cadastrar um funcionário da empresa.");
				throw;
			}
		}

		#endregion
	}
}
