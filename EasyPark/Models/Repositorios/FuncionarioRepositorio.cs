using Dapper;
using DapperExtensions;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.Plano;
using EasyPark.Models.Entidades.Veiculo;
using MySql.Data.MySqlClient;
using System.Numerics;
using System.Text;
using XSystem.Security.Cryptography;
using static Slapper.AutoMapper;

namespace EasyPark.Models.Repositorios
{
    public class FuncionarioRepositorio
    {
		private List<Funcionarios> estacionamentos;

		private readonly IConfiguration _configuration;

		string connectionString = "Server=localhost;Database=easypark;Uid=root;";

		public FuncionarioRepositorio(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public IEnumerable<Funcionarios> GetAllFuncionarios()
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM funcionarios f;";
				var funcionarios = connection.Query<Funcionarios>(sql).AsList();

				return funcionarios;
			}
		}

		public Funcionarios GetFuncionarioById(long id)
        {
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM funcionarios f WHERE f.id = @id;";
				var funcionarioId = connection.QuerySingleOrDefault<Funcionarios>(sql, new { id });

				return funcionarioId;
			}
		}

		public void CadastraVeiculo(Veiculos veiculo)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO veiculos (id, placa, modelo, cor, marca, id_funcionario) VALUES (@id, @placa, @modelo, @cor, @marca, @idFuncionario);";

				connection.Execute(sql, new
				{
					id = veiculo.Id,
					placa = veiculo.Placa,
					modelo = veiculo.Modelo,
					cor = veiculo.Cor,
					marca = veiculo.Marca,
					idFuncionario = veiculo.IdFuncionario
				});
			}
		}

		public void UpdateSenhaFuncionario(Funcionarios funcionario, string novaSenha)
		{
			var existingFuncionario = GetFuncionarioById(funcionario.Id);
			if (existingFuncionario != null)
			{
				if (VerificaSenha(existingFuncionario.Senha, funcionario.Senha))
				{
					existingFuncionario.Senha = HashSenha(novaSenha);

					using (MySqlConnection connection = new MySqlConnection(connectionString))
					{
						connection.Open();
						existingFuncionario.Senha = funcionario.Senha;

						connection.UpdateAsync(existingFuncionario);
					}
				}
				else
				{
					throw new Exception("Senha atual inválida.");
				}
			}
			else
			{
				throw new Exception("Funcionário não encontrado.");
			}
		}

		private bool VerificaSenha(string senhaAtual, string senhaInformada)
		{
			string hashedSenhaInformada = HashSenha(senhaInformada);
			return hashedSenhaInformada == senhaAtual;
		}

		private string HashSenha(string senha)
		{
			using (var sha256 = new SHA256Managed())
			{
				var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
				return BitConverter.ToString(bytes).Replace("-", "").ToLower();
			}
		}

		public void CriarVisita(Funcionarios funcionario)
        {
            // Implementar método de Criar Visitas -> para dependentes
        }

		public void PagarPlano(Funcionarios funcionario)
		{
			if (funcionario.IdPlano == null)
			{
				throw new InvalidOperationException("Funcionário não tem plano cadastrado");
			}

			decimal valorPlano = funcionario.ValorPlano;

			// Processar o pagamento do plano
			// Isso pode envolver atualizar a conta do funcionário, enviar uma notificação de pagamento, etc.
			// Para simplicidade, vamos supor que temos uma classe PaymentProcessor que lida com o pagamento
			//PaymentProcessor paymentProcessor = new PaymentProcessor();
			//paymentProcessor.ProcessPayment(valorPlano, funcionario.Contato); // Atenção: você pode precisar ajustar isso para usar o campo correto para a conta bancária
		}
	}
}
