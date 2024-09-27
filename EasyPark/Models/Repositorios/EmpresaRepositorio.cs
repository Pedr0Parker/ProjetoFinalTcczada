using Dapper;
using DapperExtensions;
using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Funcionario;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
    public class EmpresaRepositorio
    {
        private List<Empresas> empresas;

		private readonly IConfiguration _configuration;

		string connectionString = "Server=localhost;Database=easypark;Uid=root;";

		public EmpresaRepositorio(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IEnumerable<Empresas> GetAllEmpresas()
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM empresas e;";
				var empresas = connection.Query<Empresas>(sql).AsList();

				return empresas;
			}
		}

		public Empresas GetEmpresaById(long id)
        {
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM empresas e WHERE e.id = @id;";
				var empresaId = connection.QuerySingleOrDefault<Empresas>(sql, new { id });

				return empresaId;
			}
		}

        public Empresas GetEmpresaByNome(string nome)
        {
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM empresas e WHERE e.nome = @nome;";
				var empresaNome = connection.QuerySingleOrDefault<Empresas>(sql, new { nome });

				return empresaNome;
			}
		}

		public void CadastraFuncionario(Funcionarios funcionario)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO funcionarios (id, login, senha, nome, cpf, valor_plano, contato, email, data_cadastro, id_plano) VALUES (@id, @login, @senha, @nome, @cpf, @valorPlano, @contato, @email, @dataCadastro, @idPlano);";

				connection.Execute(sql, new
				{
					id = funcionario.Id,
					login = funcionario.Login,
					senha = funcionario.Senha,
					nome = funcionario.Nome,
					cpf = funcionario.CpfCnpj,
					valorPlano = funcionario.ValorPlano,
					contato = funcionario.Contato,
					email = funcionario.Email,
					dataCadastro = funcionario.DataCadastro,
					idPlano = funcionario.IdPlano
				});
			}
		}
	}
}
