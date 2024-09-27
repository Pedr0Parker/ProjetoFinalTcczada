using Dapper;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.VisitaEstacionamento;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
	public class EstacionamentoRepositorio
	{
		private List<Estacionamentos> estacionamentos;

		private readonly IConfiguration _configuration;

		string connectionString = "Server=localhost;Database=easypark;Uid=root;";

		public EstacionamentoRepositorio(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// To Do: implementar posteriormente, uma possível busca por filtro

		public IEnumerable<Estacionamentos> GetAllEstacionamentos()
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM estacionamentos e;";
				var estacionamentos = connection.Query<Estacionamentos>(sql).AsList();

				return estacionamentos;
			}
		}

		public Estacionamentos GetEstacionamentoById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM estacionamentos e WHERE e.id = @id;";
				var estacionamentoId = connection.QuerySingleOrDefault<Estacionamentos>(sql, new { id });

				return estacionamentoId;
			}
		}

		public Estacionamentos GetEstacionamentoByNome(string nome)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM estacionamentos e WHERE e.nome = @nome;";
				var estacionamentoNome = connection.QuerySingleOrDefault<Estacionamentos>(sql, new { nome });

				return estacionamentoNome;
			}
		}

		public IEnumerable<Funcionarios> VerificaFuncionarios(string cpfFuncionario)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
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
					using (MySqlConnection connection = new MySqlConnection(connectionString))
					{
						connection.Open();
						var sql = "UPDATE visitaestacionamento SET status = 0 WHERE id_funcionario = @idFuncionario AND id_estacionamento = @idEstacionamento;";
						connection.Execute(sql, new
						{
							idFuncionario = visita.IdFuncionario,
							idEstacionamento = visita.IdEstacionamento
						});
					}
				});
			}

			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO visitaestacionamento (hora_chegada, hora_saida, status, id_estacionamento, id_funcionario) VALUES (@horaChegada, @horaSaida, @status, @idEstacionamento, @idFuncionario);";

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

		public void AplicaDesconto()
		{

		}
	}
}
