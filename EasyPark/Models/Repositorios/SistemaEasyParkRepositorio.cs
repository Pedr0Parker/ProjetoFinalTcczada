using Dapper;
using DapperExtensions;
using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
	public class SistemaEasyParkRepositorio
	{
		private readonly string _connectionString;

		public SistemaEasyParkRepositorio(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DbEasyParkConnection");
		}

		public void CadastraEmpresa(Empresas empresa)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO empresas (id, login, senha, nome, nome_usual, nome_dono, cnpj, endereco, contato, data_cadastro, id_plano) VALUES (@id, @login, @senha, @nome, @nomeFantasia, @nomeDono, @cnpj, @endereco, @contato, @dataCadastro, @idPlano);";

				connection.Execute(sql, new
				{
					id = empresa.Id,
					login = empresa.Login,
					senha = empresa.Senha,
					nome = empresa.Nome,
					nomeFantasia = empresa.NomeFantasia,
					nomeDono = empresa.NomeDono,
					cnpj = empresa.Cnpj,
					endereco = empresa.Endereco,
					contato = empresa.Contato,
					dataCadastro = empresa.DataCadastro,
					idPlano = empresa.IdPlano
				});
			}
		}

		public void CadastraEstacionamento(Estacionamentos estacionamento)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO estacionamentos (id, login, senha, nome, cnpj, endereco, contato, data_cadastro) VALUES (@id, @login, @senha, @nome, @cnpj, @endereco, @contato, @dataCadastro);";

				connection.Execute(sql, new
				{
					id = estacionamento.Id,
					login = estacionamento.Login,
					senha = estacionamento.Senha,
					nome = estacionamento.Nome,
					cnpj = estacionamento.Cnpj,
					endereco = estacionamento.Endereco,
					contato = estacionamento.Contato,
					dataCadastro = estacionamento.DataCadastro
				});
			}
		}

		public void UpdateEmpresa(Empresas empresa)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				var existingEmpresa = GetEmpresaById(empresa.Id);
				connection.Open();
				if (existingEmpresa != null)
				{
					existingEmpresa.Login = empresa.Login;
					existingEmpresa.Senha = empresa.Senha;
					existingEmpresa.Nome = empresa.Nome;
					existingEmpresa.NomeFantasia = empresa.NomeFantasia;
					existingEmpresa.NomeDono = empresa.NomeDono;
					existingEmpresa.Cnpj = empresa.Cnpj;
					existingEmpresa.Endereco = empresa.Endereco;
					existingEmpresa.Contato = empresa.Contato;
					existingEmpresa.DataCadastro = empresa.DataCadastro;
					existingEmpresa.IdPlano = empresa.IdPlano;
				}

				connection.UpdateAsync(existingEmpresa);
			}
		}

		public void UpdateEstacionamento(Estacionamentos estacionamento)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				var existingEstacionamento = GetEstacionamentoById(estacionamento.Id);
				connection.Open();
				if (existingEstacionamento != null)
				{
					existingEstacionamento.Login = estacionamento.Login;
					existingEstacionamento.Senha = estacionamento.Senha;
					existingEstacionamento.Nome = estacionamento.Nome;
					existingEstacionamento.Cnpj = estacionamento.Cnpj;
					existingEstacionamento.Endereco = estacionamento.Endereco;
					existingEstacionamento.Contato = estacionamento.Contato;
					existingEstacionamento.DataCadastro = estacionamento.DataCadastro;
				}

				connection.UpdateAsync(existingEstacionamento);
			}
		}

		public Empresas GetEmpresaById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "SELECT e.id," +
				" e.login AS Login," +
				" e.senha AS Senha," +
				" e.nome AS Nome," +
				" e.nome_usual AS NomeFantasia," +
				" e.nome_dono AS NomeDono," +
				" e.cnpj AS Cnpj," +
				" e.endereco AS Endereco," +
				" e.contato AS Contato," +
				" e.data_cadastro AS DataCadastro," +
				" e.id_plano AS IdPlano FROM empresas e";

				var empresaId = connection.QuerySingleOrDefault<Empresas>(sql, new { id });

				return empresaId;
			}
		}

		public Estacionamentos GetEstacionamentoById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "SELECT e.id," +
				" e.login AS Login," +
				" e.senha AS Senha," +
				" e.nome AS Nome," +
				" e.cnpj AS Cnpj," +
				" e.endereco AS Endereco," +
				" e.contato AS Contato," +
				" e.data_cadastro AS DataCadastro," +
				" FROM estacionamentos e";

				var estacionamentoId = connection.QuerySingleOrDefault<Estacionamentos>(sql, new { id });

				return estacionamentoId;
			}
		}
	}
}
