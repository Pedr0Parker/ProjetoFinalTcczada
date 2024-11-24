using Dapper;
using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.Veiculo;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using static Slapper.AutoMapper;

namespace EasyPark.Models.Repositorios
{
    public class EmpresaRepositorio
    {
		private readonly string _connectionString;
		private readonly string sql;

		public EmpresaRepositorio(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DbEasyParkConnection");

			sql = "SELECT e.id," +
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
		}

		public IEnumerable<Empresas> GetAllEmpresas()
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var empresas = connection.Query<Empresas>(sql).AsList();

				return empresas;
			}
		}

		public Empresas GetEmpresaById(long id)
        {
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlId = $"{sql} WHERE e.id = @id";
				var empresaId = connection.QuerySingleOrDefault<Empresas>(sqlId, new { id });

				return empresaId;
			}
		}

        public IEnumerable<Empresas> GetEmpresaByEmail(string login, string senha)
        {
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlEmail = $"{sql} WHERE e.login = @login AND e.senha = @senha";
				var empresaEmail = connection.Query<Empresas>(sqlEmail, new { login, senha }).AsList();

				return empresaEmail;
			}
		}

		public void CadastraFuncionario(Funcionarios funcionario)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO funcionarios (id, login, senha, nome, cpf, contato, data_cadastro, id_plano, id_empresa) VALUES (@id, @login, @senha, @nome, @cpf, @contato, @dataCadastro, @idPlano, @idEmpresa);";

				connection.Execute(sql, new
				{
					id = funcionario.Id,
					login = funcionario.Login,
					senha = funcionario.Senha,
					nome = funcionario.Nome,
					cpf = funcionario.CpfCnpj,
					contato = funcionario.Contato,
					dataCadastro = funcionario.DataCadastro,
                    idPlano = funcionario.IdPlano,
                    idEmpresa = funcionario.IdEmpresa,
				});
			}
		}

		public void ExcluirFuncionario(int idFuncionario)
		{
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();

				var sqlSelect = "SELECT v.id, v.placa, v.modelo, v.cor, v.marca, v.id_funcionario FROM veiculos v WHERE id_funcionario = @idFuncionario;";
				var funcionarioVeiculo = connection.Query<Veiculos>(sqlSelect, new { idFuncionario }).ToList();

				if (funcionarioVeiculo != null)
				{
					var sqlVeiculo = "DELETE FROM veiculos WHERE id_funcionario = @id;";
					connection.Execute(sqlVeiculo, new { id = idFuncionario });
				}

				var sqlVisitas = "DELETE FROM visitas_estacionamento WHERE id_funcionario = @id;";
				connection.Execute(sqlVisitas, new { id = idFuncionario });

				var sql = "DELETE FROM funcionarios WHERE id = @id;";
				connection.Execute(sql, new { id = idFuncionario });
			}
		}
	}
}
