﻿using Dapper;
using DapperExtensions;
using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Plano;
using EasyPark.Models.Entidades.SistemaEasyPark;
using MySql.Data.MySqlClient;

namespace EasyPark.Models.Repositorios
{
	public class SistemaEasyParkRepositorio
	{
		private readonly IConfiguration _configuration;

		string connectionString = "Server=localhost;Database=easypark;Uid=root;";

		public SistemaEasyParkRepositorio(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void CadastraPlano(Planos plano)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO planos (id, tipo, nome, status) VALUES (@id, @tipoPlano, @nomePlano, @statusPlano);";

				connection.Execute(sql, new
				{
					id = plano.Id,
					tipoPlano = plano.TipoPlano,
					nomePlano = plano.NomePlano,
					statusPlano = plano.StatusPlano
				});
			}
		}

		public void CadastraEmpresa(Empresas empresa)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "INSERT INTO empresas (id, login, senha, nome, nome_usual, nome_dono, cnpj, valor_assinatura, endereco, contato, data_cadastro) VALUES (@id, @login, @senha, @nome, @nomeFantasia, @nomeDono, @cnpj, @valorAssinatura, @endereco, @contato, @dataCadastro);";

				connection.Execute(sql, new
				{
					id = empresa.Id,
					login = empresa.Login,
					senha = empresa.Senha,
					nome = empresa.Nome,
					nomeFantasia = empresa.NomeFantasia,
					nomeDono = empresa.NomeDono,
					cnpj = empresa.Cnpj,
					valorAssinatura = empresa.ValorAssinatura,
					endereco = empresa.Endereco,
					contato = empresa.Contato,
					dataCadastro = empresa.DataCadastro
				});
			}
		}

		public void CadastraEstacionamento(Estacionamentos estacionamento)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
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

		public void UpdatePlano(Planos plano)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				var existingPlano = GetPlanoById(plano.Id);
				connection.Open();
				if (existingPlano != null)
				{
					existingPlano.TipoPlano = plano.TipoPlano;
					existingPlano.NomePlano = plano.NomePlano;
					existingPlano.StatusPlano = plano.StatusPlano;
				}

				connection.UpdateAsync(existingPlano);
			}
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

		public void UpdateEstacionamento(Estacionamentos estacionamento)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
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

		public void DeletePlano(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "DELETE FROM planos WHERE id = @id;";

				int rowsAffected = connection.Execute(sql, new { id });

				if (rowsAffected == 0)
				{
					Console.WriteLine("Erro!");
				}
				else
				{
					Console.WriteLine("Sucesso!");
				}
			}
		}

		public void DeleteEmpresa(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "DELETE FROM empresas WHERE id = @id;";

				int rowsAffected = connection.Execute(sql, new { id });

				if (rowsAffected == 0)
				{
					Console.WriteLine("Erro!");
				}
				else
				{
					Console.WriteLine("Sucesso!");
				}
			}
		}

		public void DeleteEstacionamento(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "DELETE FROM estacionamentos WHERE id = @id;";

				int rowsAffected = connection.Execute(sql, new { id });

				if (rowsAffected == 0)
				{
					Console.WriteLine("Erro!");
				}
				else
				{
					Console.WriteLine("Sucesso!");
				}
			}
		}

		private Planos GetPlanoById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM planos p WHERE p.id = @id;";
				var planoId = connection.QuerySingleOrDefault<Planos>(sql, new { id });

				return planoId;
			}
		}

		private Empresas GetEmpresaById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM empresas e WHERE e.id = @id;";
				var empresaId = connection.QuerySingleOrDefault<Empresas>(sql, new { id });

				return empresaId;
			}
		}

		private Estacionamentos GetEstacionamentoById(long id)
		{
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var sql = "SELECT * FROM estacionamentos e WHERE e.id = @id;";
				var estacionamentoId = connection.QuerySingleOrDefault<Estacionamentos>(sql, new { id });

				return estacionamentoId;
			}
		}
	}
}