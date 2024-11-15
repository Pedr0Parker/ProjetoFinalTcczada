using EasyPark.Models.Entidades.CadastroDependente;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.Veiculo;
using EasyPark.Models.RegrasNegocio.Funcionario;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Funcionario
{
	[ApiController]
	[Route("funcionario")]
	public class FuncionarioController : ControllerBase
    {
		private readonly FuncionarioBusinessRule _businessRule;

		public FuncionarioController(FuncionarioBusinessRule businessRule)
		{
			_businessRule = businessRule;
		}

		#region Métodos Get

		/// <summary>
		/// Realiza a busca de todos os funcionários cadastrados no banco de dados
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-funcionarios")]
		public IActionResult BuscarFuncionarios()
		{
			var funcionarios = _businessRule.GetAllFuncionarios();
			if (funcionarios is null) return BadRequest("Houve um erro ao buscar os funcionários.");

			return Ok(funcionarios);
		}

		/// <summary>
		/// Realiza a busca do funcionário via Id cadastrado no banco de dados
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-funcionario/id/{id}")]
		public IActionResult BuscasFuncionarioViaId(int id)
		{
			var idFuncionario = _businessRule.GetFuncionarioById(id);
			if (idFuncionario is null) return NotFound($"Funcionário de Id {id} não cadastrado no sistema.");

			return Ok(idFuncionario);
		}

		/// <summary>
		/// Realiza a busca do funcionário via Login cadastrado no banco de dados
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("buscar-funcionario/login/{login}")]
		public IActionResult BuscarFuncionarioViaEmail(string login)
		{
			var loginFuncionario = _businessRule.GetFuncionarioByEmail(login);
			if (loginFuncionario is null) return NotFound($"Funcionário de login {login} não cadastrado no sistema.");

			return Ok(loginFuncionario);
		}

		#endregion

		#region Métodos Post

		/// <summary>
		/// Realiza o cadastro de veículo do funcionário
		/// </summary>
		/// <param name="veiculo"></param>
		/// <returns></returns>
		[HttpPost("cadastrar-veiculo-funcionario")]
		public IActionResult CadastrarVeiculoFuncionario(Veiculos veiculo)
		{
			try
			{
				_businessRule.CadastraVeiculo(veiculo);
				return Ok("Cadastro de veículo realizado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao cadastrar o veículo do funcionário.");
				throw;
			}
		}

		/// <summary>
		/// Realiza o cadastro dos dependentes do funcionário
		/// </summary>
		/// <param name="funcionario"></param>
		/// <param name="dependente"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("cadastrar-dependente")]
		public IActionResult CadastrarDependente(CadastrarDependenteRequest request)
		{
			try
			{
				var funcionario = request.Funcionario;
				var dependente = request.Dependente;

				_businessRule.CadastraDependente(funcionario, dependente);
				return Ok("Dependente cadastrado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao cadastrar dependente.");
				throw;
			}
		}

		#endregion

		#region Métodos Put

		/// <summary>
		/// Realiza a atualização de senha do Funcionário
		/// </summary>
		/// <param name="funcionario"></param>
		/// <param name="novaSenha"></param>
		/// <returns></returns>
		[HttpPut]
		[Route("atualizar-senha-funcionario")]
		public IActionResult AtualizarSenhaFuncionario(Funcionarios funcionario, string novaSenha)
		{
			try
			{
				_businessRule.UpdateSenhaFuncionario(funcionario, novaSenha);
				return Ok("Plano atualizado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao atualizar plano.");
				throw;
			}
		}

		#endregion
	}
}
