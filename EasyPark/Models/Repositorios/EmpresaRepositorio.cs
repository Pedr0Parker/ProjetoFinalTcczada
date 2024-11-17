using Dapper;
using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Funcionario;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
    public class EmpresaRepositorio
    {
		private readonly string _connectionString;
		private readonly string sql;

		public EmpresaRepositorio(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DbEasyParkConnection");
			sql = "SELECT * FROM empresas e";
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

        public Empresas GetEmpresaByEmail(string login, string senha)
        {
			using (MySqlConnection connection = new MySqlConnection(_connectionString))
			{
				connection.Open();
				var sqlEmail = $"{sql} WHERE e.login = @login AND e.senha = @senha";
				var empresaEmail = connection.QuerySingleOrDefault<Empresas>(sqlEmail, new { login, senha });

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
					//valorPlano = funcionario.ValorPlano,
					contato = funcionario.Contato,
					dataCadastro = funcionario.DataCadastro,
                    idPlano = funcionario.IdPlano,
                    idEmpresa = funcionario.IdEmpresa,
				});
			}
		}
	}
}
