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
					" v.id_funcionario AS IdFuncionario FROM visitas_estacionamento v WHERE v.id_funcionario = @idFuncionario ORDER BY e.id DESC;";

				var visitasFuncionarios = connection.Query<VisitasEstacionamento>(sql, new { idFuncionario }).ToList();

				return visitasFuncionarios;
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
					" v.id_funcionario AS IdFuncionario FROM visitas_estacionamento v WHERE v.id_funcionario = @idFuncionario ORDER BY v.id DESC;";

				var visitasFuncionarios = connection.QuerySingleOrDefault<VisitasEstacionamento>(sql, new { idFuncionario });

				return visitasFuncionarios;
			}
		}

		public void RegistraVisitaEstacionamento(int estacionamento, int funcionario, int status)
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
