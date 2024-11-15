using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.RegrasNegocio.Empresa;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Empresa
{
	[ApiController]
	[Route("empresa")]
	public class EmpresaController : ControllerBase
	{
		private readonly EmpresaBusinessRule _businessRule;

		public EmpresaController(EmpresaBusinessRule businessRule)
		{
			_businessRule = businessRule;
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
			var empresas = _businessRule.GetAllEmpresas();
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
			var idEmpresa = _businessRule.GetEmpresaById(id);
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
			var nomeEmpresa = _businessRule.GetEmpresaByNome(nome);
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
		[HttpPost("cadastrar-funcionario")]
		public IActionResult CadastrarFuncionario(Funcionarios funcionario)
		{
			try
			{
				_businessRule.CadastraFuncionario(funcionario);
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
