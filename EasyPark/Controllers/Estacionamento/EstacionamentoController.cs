using EasyPark.Models.Entidades.VisitaEstacionamento;
using EasyPark.Models.RegrasNegocio.Estacionamento;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace EasyPark.Controllers.Estacionamento
{
    [ApiController]
    [Route("estacionamento")]
    public class EstacionamentoController : ControllerBase
    {
        private readonly EstacionamentoBusinessRule _businessRule;

        public EstacionamentoController(EstacionamentoBusinessRule businessRule)
        {
            _businessRule = businessRule;
        }

        #region Métodos Get

        /// <summary>
        /// Realiza a busca de todos os estacionamentos cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        [HttpGet("buscar-estacionamentos")]
        public IActionResult BuscarEstacionamentos()
        {
            var estacionamentos = _businessRule.GetAllEstacionamentos();
            if (estacionamentos is null) return BadRequest("Houve um erro ao buscar os estacionamentos.");

            return Ok(estacionamentos);
        }

        /// <summary>
        /// Realiza a busca do estacionamento via Id cadastrado no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("buscar-estacionamento/id/{id}")]
        public IActionResult BuscasEstacionamentoViaId(int id)
        {
            var idEstacionamento = _businessRule.GetEstacionamentoById(id);
            if (idEstacionamento is null) return NotFound($"Estacionamento de Id {id} não cadastrado no sistema.");

            return Ok(idEstacionamento);
        }

        /// <summary>
        /// Realiza a busca do estacionamento via Nome cadastrado no banco de dados
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        [HttpGet("buscar-estacionamento/login/{login}/senha/{senha}")]
        public IActionResult BuscasEstacionamentoViaEmail(string login, string senha)
        {
            var emailEstacionamento = _businessRule.GetEstacionamentoByEmail(login, senha);
            if (emailEstacionamento is null) return NotFound($"Estacionamento de email {login} não cadastrado no sistema.");

            var estacionamentoFormatado = emailEstacionamento.Select(e => new
            {
                e.Id,
                e.Login,
                e.Senha,
                e.Nome,
                e.Cnpj,
                e.Endereco,
                e.Contato,
                DataCadastro = e.DataCadastro.ToString("dd/MM/yyyy HH:mm"),
            });

            return Ok(estacionamentoFormatado);
        }

        /// <summary>
        /// Realiza a busca do funcionário que realizou o check-in no estacionamento, via CPF cadastrado no banco de dados
        /// </summary>
        /// <param name="idFuncionario"></param>
        /// <returns></returns>
        [HttpGet("verifica-visitas-funcionario/idFuncionario/{idFuncionario}")]
        public IActionResult VerificarFuncionarios(int idFuncionario)
        {
            var buscaVisitasFuncionario = _businessRule.VerificaFuncionarios(idFuncionario);
            if (buscaVisitasFuncionario is null) return NotFound($"Visitas de funcionário de não encontrado.");

            var visitasFormatadas = buscaVisitasFuncionario.Select(v => new
            {
                v.Id,
                HoraChegada = v.HoraChegada.ToString("dd/MM/yyyy HH:mm"),
                HoraSaida = v.HoraSaida.ToString("dd/MM/yyyy HH:mm"),
                v.Status,
                v.IdEstacionamento,
                v.IdFuncionario
            });

            return Ok(visitasFormatadas);
        }

        /// <summary>
        /// Realiza a busca do funcionário que realizou o check-in no estacionamento, via CPF cadastrado no banco de dados
        /// </summary>
        /// <param name="idFuncionario"></param>
        /// <returns></returns>
        [HttpGet("visitas-pendentes-funcionario/idFuncionario/{idFuncionario}")]
        public IActionResult VisitasPendentesFuncionarios(int idFuncionario)
        {
            var buscaVisitasFuncionario = _businessRule.VisitasPendentesFuncionarios(idFuncionario);
            if (buscaVisitasFuncionario is null)  return NotFound(new { message = "Nenhuma visita encontrada." });

            return Ok(buscaVisitasFuncionario);
        }

        /// <summary>
        /// Realiza a busca das visitas feitas no estacionamento
        /// </summary>
        /// <param name="idEstacionamento"></param>
        /// <returns></returns>
        [HttpGet("verifica-visitas-estacionamento/idEstacionamento/{idEstacionamento}")]
        public IActionResult VerificarVisitasEstacionamento(int idEstacionamento)
        {
            var buscaVisitasEstacionamento = _businessRule.VerificaVisitasEstacionamento(idEstacionamento);
            if (buscaVisitasEstacionamento is null) return NotFound($"Visitas de estacionamento não encontrado.");

            var visitasFormatadas = buscaVisitasEstacionamento.Select(v => new
            {
                v.Id,
                HoraChegada = v.HoraChegada.ToString("dd/MM/yyyy HH:mm"),
                HoraSaida = v.HoraSaida.ToString("dd/MM/yyyy HH:mm"),
                v.Status,
                v.IdEstacionamento,
                v.IdFuncionario
            });

            return Ok(visitasFormatadas);
        }

        /// <summary>
        /// Realiza a busca das solicitações de novas visitas ao estacionamento
        /// </summary>
        /// <param name="idEstacionamento"></param>
        /// <returns></returns>
        [HttpGet("verifica-solicitacao-visitas/idEstacionamento/{idEstacionamento}")]
        public IActionResult VerificarSolicitacaoVisitas(int idEstacionamento)
        {
            var solicitacaoVisita = _businessRule.VerificaSolicitacaoVisitas(idEstacionamento);
            if (solicitacaoVisita is null) return NotFound($"Não foi possível realizar a busca de solicitações de novas visitas.");

            return Ok(solicitacaoVisita);
        }


        [HttpGet("verifica-vagas-ocupadas/idEstacionamento/{idEstacionamento}")]
        public IActionResult VerificarVagasOcupadas(int idEstacionamento)
        {
            var vagasOcupadas = _businessRule.VerificaVagasOcupadas(idEstacionamento);
            if (vagasOcupadas is null) return NotFound($"Não foi possível realizar a busca de vagas ocupadas.");

            return Ok(vagasOcupadas);
        }

        /// <summary>
        /// Realiza a busca da última visita cadastrada no banco de dados do funcionário
        /// </summary>
        /// <param name="idFuncionario"></param>
        /// <returns></returns>
        [HttpGet("verifica-ultima-visita/idFuncionario/{idFuncionario}")]
        public IActionResult VerificarUltimaVisita(int idFuncionario)
        {
            var buscaUltimaVisita = _businessRule.VerificaUltimaVisita(idFuncionario);
            if (buscaUltimaVisita is null) return NotFound(false);

            return Ok(buscaUltimaVisita);
        }

        #endregion

        #region Métodos Post

        /// <summary>
        /// Realiza o cadastro de uma visita feita no estacionamento
        /// </summary>
        /// <param name="visitaEstacionamento"></param>
        /// <returns></returns>
        [HttpPost("cadastra-visita-estacionamento")]
        public IActionResult CadastrarVisitaEstacionamento(VisitasEstacionamento visitasEstacionamento)
        {
            try
            {
                _businessRule.RegistraVisitaEstacionamento(visitasEstacionamento);
                return Ok("Visita estacionamento cadastrada com sucesso.");
            }
            catch
            {
                return BadRequest("Erro ao cadastrar visita.");
                throw;
            }
        }

        /// <summary>
        /// Realiza a aceitação de uma visita pelo estacionamento
        /// </summary>
        /// <param name="idVisita"></param>
        /// <returns></returns>
        [HttpPost("aceitar-solicitacao-visita/idVisita/{idVisita}/horaChegda/{horaChegada}")]
        public IActionResult AceitarSolicitacaoVisita(int idVisita, DateTime horaChegada)
        {
            try
            {
                _businessRule.RegistraSolicitacaoVisitaEstacionamento(idVisita, horaChegada);
                return Ok("Visita estacionamento aceita com sucesso.");
            }
            catch
            {
                return BadRequest("Erro ao aceitar uma visita.");
                throw;
            }
        }

        /// <summary>
        /// Realiza a aplicação de desconto caso o estacionamento aprovar
        /// </summary>
        /// <param name="visitaEstacionamento"></param>
        /// <param name="percentualDescontoEstacionamento"></param>
        /// <returns></returns>
        [HttpPost("aplicar-desconto")]
        public IActionResult AplicarDesconto(VisitasEstacionamento visitaEstacionamento, decimal percentualDescontoEstacionamento, decimal taxaHorariaEstacionamento)
        {
            try
            {
                if (taxaHorariaEstacionamento <= 0)
                {
                    return BadRequest("Taxa horária inválida. Deve ser um valor maior que zero.");
                }

                _businessRule.AplicaDesconto(visitaEstacionamento, percentualDescontoEstacionamento, taxaHorariaEstacionamento);
                return Ok("Desconto aplicado com sucesso!");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao aplicar desconto.");
                throw;
            }
        }

        #endregion

        #region Métodos Delete

        /// <summary>
        /// Realiza a rejeição e exclusão de uma visita ao estacionamento
        /// </summary>
        /// <param name="idVista"></param>
        /// <returns></returns>
        [HttpDelete("rejeitar-excluir-solicitacao/idVisita/{idVista}")]
        public IActionResult RejeitarExcluirSolicitacaoVisita(int idVista)
        {
            try
            {
                _businessRule.ExcluirSolicitacaoVisita(idVista);
                return Ok($"Rejeição de visita ao estacionamento realizada com sucesso!");
            }
            catch
            {
                return BadRequest("Erro ao rejeitar uma solicitação de visita ao estacionamento.");
                throw;
            }
        }

        #endregion
    }
}
