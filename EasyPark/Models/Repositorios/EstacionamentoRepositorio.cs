using Dapper;
using EasyPark.Models.Entidades.Dependente;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Funcionario;
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
			sql = "SELECT * FROM estacionamentos e";
		}

		// To Do: implementar posteriormente, uma possível busca por filtro

		public IEnumerable<Estacionamentos> GetAllEstacionamentos()
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var estacionamentos = connection.Query<Estacionamentos>(sql).AsList();

				return estacionamentos;
			}
		}

		public Estacionamentos GetEstacionamentoById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlId = $"{sql} WHERE e.id = @id";
				var estacionamentoId = connection.QuerySingleOrDefault<Estacionamentos>(sqlId, new { id });

				return estacionamentoId;
			}
		}

		public Estacionamentos GetEstacionamentoByNome(string nome)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlNome = $"{sql} WHERE e.nome = @nome";
				var estacionamentoNome = connection.QuerySingleOrDefault<Estacionamentos>(sqlNome, new { nome });

				return estacionamentoNome;
			}
		}

		public IEnumerable<Funcionarios> VerificaFuncionarios(string cpfFuncionario)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM funcionarios f WHERE f.cpf = @cpfFuncionario;";
				var funcionario = connection.Query<Funcionarios>(sql, new { cpfFuncionario }).AsList();

				if (funcionario != null)
				{
					var status = 1;
					var estacionamento = new Estacionamentos();
					RegistraVisitaEstacionamento(estacionamento, funcionario.FirstOrDefault(), status);
				}

				return funcionario;
			}
		}

		public void RegistraVisitaEstacionamento(Estacionamentos estacionamento, Funcionarios funcionario, int status)
		{
			VisitasEstacionamento visita = new VisitasEstacionamento();

			visita.IdEstacionamento = estacionamento;
			visita.IdFuncionario = funcionario;

			if (status == 0) // Não chegou
			{
				visita.HoraChegada = DateTime.MinValue;
				visita.HoraSaida = DateTime.MaxValue;
				visita.Status = 0;
			}
			else if (status == 1) // Chegada
			{
				visita.HoraChegada = DateTime.Now;
				visita.HoraSaida = DateTime.MaxValue;
				visita.Status = 1;
			}
			else if (status == 2) // Saída
			{
				visita.HoraSaida = DateTime.Now;
				visita.Status = 2;

				// Agende uma tarefa para trocar o status para 0 após 5 minutos
				Task.Delay(TimeSpan.FromMinutes(5)).ContinueWith(t =>
				{
					using (MySqlConnection connection = new MySqlConnection(_connectionString))
					{
						connection.Open();
						var sql = "UPDATE visitas_estacionamento SET status = 0 WHERE id_funcionario = @idFuncionario AND id_estacionamento = @idEstacionamento;";
						connection.Execute(sql, new
						{
							idFuncionario = visita.IdFuncionario,
							idEstacionamento = visita.IdEstacionamento
						});
					}
				});
			}

			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO visitas_estacionamento (hora_chegada, hora_saida, status, id_estacionamento, id_funcionario) VALUES (@horaChegada, @horaSaida, @status, @idEstacionamento, @idFuncionario);";

				connection.Execute(sql, new
				{
					id = visita.Id,
					horaChegada = visita.HoraChegada,
					horaSaida = visita.HoraSaida,
					status = visita.Status,
					idEstacionamento = visita.IdEstacionamento,
					idFuncionario = visita.IdFuncionario
				});
			}
		}

		public void CriarVisitaDependente(string cpfDependente, Estacionamentos estacionamento, int status, Funcionarios funcionario)
		{
			using (MySqlConnection conexao = new MySqlConnection(_connectionString))
			{
				conexao.Open();
				var sql = "SELECT * FROM dependentes WHERE cpf = @cpfDependente;";
				var dependente = conexao.QuerySingleOrDefault<Dependentes>(sql, new { cpfDependente });

				if (dependente != null)
				{
					// Verifica se o dependente está vinculado a um funcionário
					if (dependente.IdFuncionario != null)
					{
						// Cria uma nova visita para o dependente
						VisitasEstacionamento visita = new VisitasEstacionamento();
						visita.IdEstacionamento = new Estacionamentos { Id = estacionamento.Id };
						visita.IdFuncionario = new Funcionarios { Id = funcionario.Id };
						visita.IdDependente = dependente;
						visita.Status = status;

						if (status == 0) // Não chegou
						{
							visita.HoraChegada = DateTime.MinValue;
							visita.HoraSaida = DateTime.MaxValue;
						}
						else if (status == 1) // Chegada
						{
							visita.HoraChegada = DateTime.Now;
							visita.HoraSaida = DateTime.MaxValue;
						}
						else if (status == 2) // Saída
						{
							visita.HoraSaida = DateTime.Now;
						}

						using (MySqlConnection conexao2 = new MySqlConnection(_connectionString))
						{
							conexao2.Open();
							var sql2 = "INSERT INTO visitas_estacionamento (hora_chegada, hora_saida, status, id_estacionamento, id_funcionario, id_dependente) VALUES (@horaChegada, @horaSaida, @status, @idEstacionamento, @idFuncionario, @idDependente);";
							conexao2.Execute(sql2, new
							{
								horaChegada = visita.HoraChegada,
								horaSaida = visita.HoraSaida,
								status = visita.Status,
								idEstacionamento = estacionamento.Id,
								idFuncionario = dependente.IdFuncionario,
								idDependente = dependente.Id
							});
						}
					}
					else
					{
						throw new Exception($"Dependente {dependente.Nome} não está vinculado a um funcionário.");
					}
				}
				else
				{
					throw new Exception("Dependente não encontrado.");
				}
			}
		}

		public void AplicaDesconto(VisitasEstacionamento visita, decimal percentualDescontoEstacionamento, decimal taxaHorariaEstacionamento)
		{
			// Calcula a duração da visita
			TimeSpan duracaoVisita = visita.HoraSaida - visita.HoraChegada;
			decimal valorTarifa = CalculaValorTarifa(duracaoVisita, taxaHorariaEstacionamento);

			// Verifica se o funcionário ou dependente é elegível para um desconto
			if ((visita.IdFuncionario != null || visita.IdDependente != null) && visita.Status == 2) // Saída
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

		private decimal CalculaValorTarifa(TimeSpan duracaoVisita, decimal taxaHorariaEstacionamento)
		{
			decimal valorTarifa = taxaHorariaEstacionamento * (decimal)duracaoVisita.TotalHours;

			return valorTarifa;
		}
	}
}
