using Dapper;
using EasyPark.Models.Entidades.Dependente;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.Veiculo;
using EasyPark.Models.Entidades.VisitaEstacionamento;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
	public class EstacionamentoRepositorio
	{
		private readonly string _connectionString;
		private readonly string sql;

		public EstacionamentoRepositorio(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DbEasyParkConnection");

			sql = "SELECT e.id," +
				" e.login AS Login," +
				" e.senha AS Senha," +
				" e.nome AS Nome," +
				" e.cnpj AS Cnpj," +
				" e.endereco AS Endereco," +
				" e.contato AS Contato," +
				" e.data_cadastro AS DataCadastro" +
				" FROM estacionamentos e";
		}

		public IEnumerable<Estacionamentos> GetAllEstacionamentos()
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
                var sqlEstacionamentos = $"{sql}";

                var estacionamentos = connection.Query<Estacionamentos>(sql).AsList();

				return estacionamentos;
			}
		}

		public Estacionamentos GetEstacionamentoById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlId = $"{sql} WHERE e.id = @id ORDER BY e.id DESC";
				var estacionamentoId = connection.QuerySingleOrDefault<Estacionamentos>(sqlId, new { id });

				return estacionamentoId;
			}
		}

		public IEnumerable<Estacionamentos> GetEstacionamentoByEmail(string login, string senha)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlNome = $"{sql} WHERE e.login = @login AND e.senha = @senha";
				var estacionamentoEmail = connection.Query<Estacionamentos>(sqlNome, new { login, senha }).AsList();

				return estacionamentoEmail;
			}
		}

		public IEnumerable<VisitasEstacionamento> VerificaFuncionarios(int idFuncionario)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "SELECT v.id," +
					" v.hora_chegada AS HoraChegada," +
					" v.hora_saida AS HoraSaida," +
					" v.status AS Status," +
					" v.id_estacionamento AS IdEstacionamento," +
					" v.id_funcionario AS IdFuncionario FROM visitas_estacionamento v WHERE v.id_funcionario = @idFuncionario AND v.status = 2 ORDER BY v.id DESC;";

				var visitasFuncionarios = connection.Query<VisitasEstacionamento>(sql, new { idFuncionario }).ToList();

				return visitasFuncionarios;
			}
		}

        public VisitasEstacionamento VisitasPendentesFuncionarios(int idFuncionario)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT v.id," +
                    " v.hora_chegada AS HoraChegada," +
                    " v.hora_saida AS HoraSaida," +
                    " v.status AS Status," +
                    " v.id_estacionamento AS IdEstacionamento," +
                    " v.id_funcionario AS IdFuncionario," +
					" v.id_veiculo AS IdVeiculo FROM visitas_estacionamento v WHERE v.id_funcionario = @idFuncionario AND v.status < 2";

                var visitasPendentesFuncionarios = connection.QuerySingleOrDefault<VisitasEstacionamento>(sql, new { idFuncionario });

                return visitasPendentesFuncionarios;
            }
        }

        public IEnumerable<VisitasEstacionamento> VerificaVisitasEstacionamento(int idEstacionamento)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "SELECT v.id," +
					" v.hora_chegada AS HoraChegada," +
					" v.hora_saida AS HoraSaida," +
					" v.status AS Status," +
					" v.id_estacionamento AS IdEstacionamento," +
					" v.id_funcionario AS IdFuncionario FROM visitas_estacionamento v WHERE v.id_estacionamento = @idEstacionamento AND v.status > 0 ORDER BY v.id DESC;";

				var visitasEstacionamento = connection.Query<VisitasEstacionamento>(sql, new { idEstacionamento }).ToList();

				return visitasEstacionamento;
			}
		}

		public IEnumerable<VisitasEstacionamento> VerificaSolicitacaoVisitas(int idEstacionamento)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "SELECT v.id," +
					" v.hora_chegada AS HoraChegada," +
					" v.hora_saida AS HoraSaida," +
					" v.status AS Status," +
					" v.id_estacionamento AS IdEstacionamento," +
					" v.id_veiculo AS IdVeiculo," +
					" v.id_funcionario AS IdFuncionario FROM visitas_estacionamento v WHERE v.id_estacionamento = @idEstacionamento AND v.status = 0;";

				var solicitacaoVisitasEstacionamento = connection.Query<VisitasEstacionamento>(sql, new { idEstacionamento }).ToList();

				return solicitacaoVisitasEstacionamento;
			}
		}

		public IEnumerable<VisitasEstacionamento> VerificaVagasOcupadas(int idEstacionamento)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "SELECT v.id," +
					" v.hora_chegada AS HoraChegada," +
					" v.hora_saida AS HoraSaida," +
					" v.status AS Status," +
					" v.id_estacionamento AS IdEstacionamento," +
					" v.id_veiculo AS IdVeiculo," +
					" v.id_funcionario AS IdFuncionario FROM visitas_estacionamento v WHERE v.id_estacionamento = @idEstacionamento AND v.status = 1;";

				var vagasOcupadas = connection.Query<VisitasEstacionamento>(sql, new { idEstacionamento }).ToList();

				return vagasOcupadas;
			}
		}

		public VisitasEstacionamento VerificaUltimaVisita(int idFuncionario)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "SELECT v.id," +
					" v.hora_chegada AS HoraChegada," +
					" v.hora_saida AS HoraSaida," +
					" v.status AS Status," +
					" v.id_estacionamento AS IdEstacionamento," +
					" v.id_funcionario AS IdFuncionario FROM visitas_estacionamento v WHERE v.id_funcionario = @idFuncionario AND v.status = 2 ORDER BY v.id DESC LIMIT 1;";

				var visitasFuncionarios = connection.QuerySingleOrDefault<VisitasEstacionamento>(sql, new { idFuncionario });

				return visitasFuncionarios;
			}
		}

		public void RegistraVisitaEstacionamento(VisitasEstacionamento visitasEstacionamento)
		{
			if (visitasEstacionamento.Status == 0) // Não chegou
			{
				visitasEstacionamento.HoraChegada = DateTime.MinValue;
				visitasEstacionamento.HoraSaida = DateTime.MinValue;
				visitasEstacionamento.Status = 0;
			}
			else if (visitasEstacionamento.Status == 1) // Chegada
			{
				visitasEstacionamento.HoraChegada = DateTime.Now;
				visitasEstacionamento.HoraSaida = DateTime.MinValue;
				visitasEstacionamento.Status = 1;
			}
			else if (visitasEstacionamento.Status == 2) // Saída
			{
				visitasEstacionamento.HoraSaida = DateTime.Now;
				visitasEstacionamento.Status = 2;

				// Agende uma tarefa para trocar o status para 0 após 5 minutos
				Task.Delay(TimeSpan.FromMinutes(5)).ContinueWith(t =>
				{
					using (MySqlConnection connection = new MySqlConnection(_connectionString))
					{
						connection.Open();
						var sql = "UPDATE visitas_estacionamento SET status = 0 WHERE id_funcionario = @idFuncionario AND id_estacionamento = @idEstacionamento;";
						connection.Execute(sql, new
						{
							idFuncionario = visitasEstacionamento.IdFuncionario,
							idEstacionamento = visitasEstacionamento.IdEstacionamento
						});
					}
				});
			}

			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO visitas_estacionamento (hora_chegada, hora_saida, status, id_estacionamento, id_funcionario, id_veiculo) VALUES (@horaChegada, @horaSaida, @status, @idEstacionamento, @idFuncionario, @idVeiculo);";

				connection.Execute(sql, new
				{
					id = visitasEstacionamento.Id,
					horaChegada = visitasEstacionamento.HoraChegada,
					horaSaida = visitasEstacionamento.HoraSaida,
					status = visitasEstacionamento.Status,
					idEstacionamento = visitasEstacionamento.IdEstacionamento,
					idFuncionario = visitasEstacionamento.IdFuncionario,
					idVeiculo = visitasEstacionamento.IdVeiculo,
				});
			}
		}

		public void RegistraSolicitacaoVisitaEstacionamento(int idVisita, DateTime horaChegada)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "UPDATE visitas_estacionamento SET status = 1, hora_chegada = @horaChegada WHERE id = @idVisita;";
				connection.Execute(sql, new { idVisita, horaChegada });
			}
		}

		public void AplicaDesconto(VisitasEstacionamento visita, decimal percentualDescontoEstacionamento, decimal taxaHorariaEstacionamento)
		{
			// Calcula a duração da visita
			TimeSpan duracaoVisita = visita.HoraSaida - visita.HoraChegada;
			decimal valorTarifa = CalculaValorTarifa(duracaoVisita, taxaHorariaEstacionamento);

			// Verifica se o funcionário ou dependente é elegível para um desconto
			if ((visita.IdFuncionario != null) && visita.Status == 2) // Saída
			{
				// Aplica o desconto configurado pelo estacionamento
				decimal valorDesconto = valorTarifa * percentualDescontoEstacionamento;

				// Atualiza o valor da tarifa com o desconto
				valorTarifa -= valorDesconto;
			}

			using (MySqlConnection conexao = new MySqlConnection(_connectionString))
			{
				conexao.Open();
				var sql = "UPDATE visitas_estacionamento SET valor_pago = @valorPago WHERE id = @idVisita;";
				conexao.Execute(sql, new
				{
					valorPago = valorTarifa,
					idVisita = visita.Id
				});
			}
		}

		public void ExcluirSolicitacaoVisita(int idVisita)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "DELETE FROM visitas_estacionamento WHERE id = @id;";
				connection.Execute(sql, new { id = idVisita });
			}
		}

		private decimal CalculaValorTarifa(TimeSpan duracaoVisita, decimal taxaHorariaEstacionamento)
		{
			decimal valorTarifa = taxaHorariaEstacionamento * (decimal)duracaoVisita.TotalHours;

			return valorTarifa;
		}
	}
}
