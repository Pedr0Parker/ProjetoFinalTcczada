using Dapper;
using DapperExtensions;
using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;
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

		#region Métodos Get

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

        #endregion

        /// <summary>
        /// Adiciona uma nova empresa
        /// </summary>
        /// <param name="empresa"></param>
        public void AddEmpresa(Empresas empresa)
        {
            empresa.Id = empresas.Max(p => p.Id) + 1;
            empresas.Add(empresa);
        }

        public void UpdateEmpresa(Empresas empresa)
        {
			using (MySqlConnection connection = new MySqlConnection(connectionString))
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
					existingEmpresa.ValorAssinatura = empresa.ValorAssinatura;
					existingEmpresa.Endereco = empresa.Endereco;
					existingEmpresa.Contato = empresa.Contato;
					existingEmpresa.DataCadastro = empresa.DataCadastro;
				}

				connection.UpdateAsync(existingEmpresa);
			}
		}

        public void DeleteEmpresa(long id)
        {
            var empresa = GetEmpresaById(id);
            if (empresa != null)
            {
                empresas.Remove(empresa);
            }
        }

        // To Do: Verificar método de cadastro de funcionários da empresa
        public void CadastraFuncionario()
        {

        }
    }
}
