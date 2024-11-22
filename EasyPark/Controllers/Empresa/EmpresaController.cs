using EasyPark.Models.Entidades.Empresa;
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
		[HttpGet("buscar-empresas")]
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
		[HttpGet("buscar-empresa/id/{id}")]
		public IActionResult BuscarEmpresaViaId(int id)
		{
			var idEmpresa = _businessRule.GetEmpresaById(id);
			if (idEmpresa is null) return NotFound($"Empresa de Id {id} não cadastrado no sistema.");

			return Ok(idEmpresa);
		}

        /// <summary>
        /// Realiza a busca da empresa via Nome cadastrado no banco de dados
        /// </summary>
        /// <param name="login"></param>
		/// <param name="senha"></param>
        /// <returns></returns>
        [HttpGet("buscar-empresa/login/{login}/senha/{senha}")]
		public IActionResult BuscarEmpresaViaEmail(string login, string senha)
		{
			var emailEmpresa = _businessRule.GetEmpresaByEmail(login, senha);
			if (emailEmpresa is null) return NotFound($"Empresa de email {login} não cadastrada no sistema.");

			var emailEmpresaFormatada = emailEmpresa.Select(e => new
			{
				e.Id,
				e.Login,
				e.Senha,
				e.Nome,
				e.NomeFantasia,
				e.NomeDono,
				e.Cnpj,
				e.Endereco,
				e.Contato,
				DataCadastro = e.DataCadastro.ToString("dd/MM/yyyy HH:mm"),
				e.IdPlano
			});

			return Ok(emailEmpresaFormatada);
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

		#region Métodos Delete

		/// <summary>
		/// Realiza a exclusão do funcionário pela empresa
		/// </summary>
		/// <param name="idFuncionario"></param>
		/// <returns></returns>
		[HttpDelete("excluir-funcionario/idFuncionario/{idFuncionario}")]
		public IActionResult ExcluirFuncionario(int idFuncionario)
		{
			try
			{
				_businessRule.ExcluirFuncionario(idFuncionario);
				return Ok($"Exclusão do funcionário realizada com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao excluir um funcionário da empresa.");
				throw;
			}
		}

		#endregion
	}
}
