using EasyPark.Models.Entidades.CadastroDependente;
using EasyPark.Models.Entidades.Dependente;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.Veiculo;
using EasyPark.Models.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Funcionario
{
	[ApiController]
	[Route("funcionario")]
	public class FuncionarioController : ControllerBase
    {
		private readonly FuncionarioRepositorio repositorio;

		public FuncionarioController(IConfiguration configuration)
		{
			repositorio = new FuncionarioRepositorio(configuration);
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
			var funcionarios = repositorio.GetAllFuncionarios();
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
			var idFuncionario = repositorio.GetFuncionarioById(id);
			if (idFuncionario is null) return NotFound($"Funcionário de Id {id} não cadastrado no sistema.");

			return Ok(idFuncionario);
		}

		#endregion

		#region Métodos Post

		/// <summary>
		/// Realiza o cadastro de veículo do funcionário
		/// </summary>
		/// <param name="veiculo"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("cadastrar-veiculo-funcionario")]
		public IActionResult CadastrarVeiculoFuncionario(Veiculos veiculo)
		{
			try
			{
				repositorio.CadastraVeiculo(veiculo);
				return Ok("Cadastro de veículo realizado com sucesso!");
			}
			catch
			{
				return BadRequest("Erro ao cadastrar uma empresa.");
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

				repositorio.CadastraDependente(funcionario, dependente);
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
				repositorio.UpdateSenhaFuncionario(funcionario, novaSenha);
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
