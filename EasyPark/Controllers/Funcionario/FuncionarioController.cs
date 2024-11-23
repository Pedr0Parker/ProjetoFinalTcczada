﻿using EasyPark.Models.Entidades.Funcionario;
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
		[HttpGet("buscar-funcionarios")]
		public IActionResult BuscarFuncionarios()
		{
			var funcionarios = _businessRule.GetAllFuncionarios();
			if (funcionarios is null) return BadRequest("Houve um erro ao buscar os funcionários.");

			return Ok(funcionarios);
		}

        /// <summary>
		/// Realiza a busca de funcionarios cadastrados no banco de dados para o login 
		///	<param name="login"></param>
		/// <param name="senha"></param>
		/// </summary>
		/// <returns></returns>
        [HttpGet("buscar-funcionarios/login/{login}/senha/{senha}")]
        public IActionResult BuscarFuncionariosLogin(string login, string senha)
        {
            var funcionarios = _businessRule.GetFuncionarioByEmail(login, senha);
            if (funcionarios is null) return BadRequest("Houve um erro ao buscar os funcionários.");

            var funcionariosFormatados = funcionarios.Select(f => new
            {
                f.Id,
                f.Login,
                f.Senha,
                f.Nome,
                f.CpfCnpj,
                f.Contato,
                DataCadastro = f.DataCadastro.ToString("dd/MM/yyyy HH:mm"),
                f.IdPlano,
                f.IdEmpresa
            });

            return Ok(funcionariosFormatados);
        }

        /// <summary>
        /// Realiza a busca do funcionário via Id cadastrado no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		[HttpGet("buscar-funcionario/id/{id}")]
		public IActionResult BuscasFuncionarioViaId(int id)
		{
			var idFuncionario = _businessRule.GetFuncionarioById(id);
			if (idFuncionario is null) return NotFound($"Funcionário de Id {id} não cadastrado no sistema.");

			return Ok(idFuncionario);
		}

        /// <summary>
        /// Realiza a busca do funcionário via Id cadastrado no banco de dados
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        [HttpGet("buscar-funcionarios-empresa/idEmpresa/{idEmpresa}")]
        public IActionResult BuscasFuncionarioViaIdEmpresa(int idEmpresa)
        {
            var idFuncionariosEmpresa = _businessRule.GetFuncionarioByIdEmpresa(idEmpresa);
            if (idFuncionariosEmpresa is null) return NotFound($"Empresa de Id {idEmpresa} não cadastrado no sistema.");

            var funcionariosFormatados = idFuncionariosEmpresa.Select(f => new
            {
                f.Id,
                f.Login,
                f.Senha,
                f.Nome,
                f.CpfCnpj,
                f.Contato,
                DataCadastro = f.DataCadastro.ToString("dd/MM/yyyy HH:mm"),
                f.IdPlano,
                f.IdEmpresa
            });

            return Ok(funcionariosFormatados);
        }

		/// <summary>
		/// Realiza a busca do veiculo via Id do funcionário cadastrado no banco de dados
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("buscar-veiculo-funcionario/id/{id}")]
		public IActionResult BuscarVeiculoViaIdFuncionario(int id)
		{
			var idFuncionario = _businessRule.GetVeiculoByIdFuncionario(id);
			if (idFuncionario is null) return NotFound($"Veículo não cadastrado no sistema.");

			return Ok(idFuncionario);
		}

		/// <summary>
		/// Realiza a busca dos funcionários ativos pelo Id da Empresa
		/// </summary>
		/// <param name="idEmpresa"></param>
		/// <returns></returns>
		[HttpGet("buscar-funcionarios-ativos/idEmpresa/{idEmpresa}")]
		public IActionResult BuscarFuncionariosAtivos(int idEmpresa)
		{
			var idFuncionariosEmpresa = _businessRule.GetFuncionariosAtivos(idEmpresa);
			if (idFuncionariosEmpresa is null) return NotFound($"Erro ao buscar os funcionários ativos.");

			var funcionariosFormatados = idFuncionariosEmpresa.Select(f => new
			{
				f.Id,
				f.Login,
				f.Senha,
				f.Nome,
				f.CpfCnpj,
				f.Contato,
				DataCadastro = f.DataCadastro.ToString("dd/MM/yyyy HH:mm"),
				f.IdPlano,
				f.IdEmpresa
			});

			return Ok(funcionariosFormatados);
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
		/// Realiza o cadastro da saída do funcionário ao estacionamento
		/// </summary>
		/// <param name="idVisita"></param>
		/// <returns></returns>
		[HttpPost("cadastrar-saida-estacionamento/idVisita/{idVisita}")]
		public IActionResult CadastraSaidaEstacionamento(int idVisita)
		{
			try
			{
				_businessRule.RegistraSaidaEstacionamento(idVisita);
				return Ok("Saída ao estacionamento realizada com sucesso.");
			}
			catch
			{
				return BadRequest("Erro ao realizar a saída do estacionamento.");
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
		[HttpPut("atualizar-senha-funcionario")]
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
