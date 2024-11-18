using EasyPark.Models.Entidades.Dependente;
using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.Veiculo;
using EasyPark.Models.Repositorios;
using static Slapper.AutoMapper;

namespace EasyPark.Models.RegrasNegocio.Funcionario
{
	public class FuncionarioBusinessRule
	{
		private readonly FuncionarioRepositorio _repositorio;

		public FuncionarioBusinessRule(FuncionarioRepositorio repositorio)
		{
			_repositorio = repositorio;
		}

		public IEnumerable<Funcionarios> GetAllFuncionarios()
		{
			return _repositorio.GetAllFuncionarios();
		}

        public Funcionarios GetFuncionarioByEmail(string login, string senha)
        {
            if (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(senha))
            {
                throw new ArgumentException("Email funcionário inválido", nameof(login));
            }

            var loginFuncionario = _repositorio.GetFuncionarioByEmail(login, senha);
            return loginFuncionario;
        }

        public Funcionarios GetFuncionarioById(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentException("Id inválido", nameof(id));
			}

			var funcionario = _repositorio.GetFuncionarioById(id);
			return funcionario;
		}

        public IEnumerable<Funcionarios> GetFuncionarioByIdEmpresa(int idEmpresa)
        {
            if (idEmpresa <= 0)
            {
                throw new ArgumentException("Id inválido", nameof(idEmpresa));
            }

           return _repositorio.GetFuncionarioByIdEmpresa(idEmpresa);
         
        }

        public void CadastraVeiculo(Veiculos veiculo)
		{
			//var idFuncionario = Convert.ToInt64(veiculo.IdFuncionario);
			//var funcionario = _repositorio.GetFuncionarioById(idFuncionario);

			//if (funcionario == null)
			//{
			//	throw new InvalidOperationException("Funcionário não encontrado");
			//}

			if (veiculo == null)
			{
				throw new ArgumentNullException(nameof(veiculo), "Veículo inválido");
			}

			if (string.IsNullOrEmpty(veiculo.Placa))
			{
				throw new ArgumentException("Placa do veículo é obrigatória", nameof(veiculo));
			}

			_repositorio.CadastraVeiculo(veiculo);
		}

		public void CadastraDependente(Funcionarios funcionario, Dependentes dependente)
		{
			if (funcionario == null)
			{
				throw new ArgumentNullException(nameof(funcionario), "Funcionário inválido");
			}

			if (dependente == null)
			{
				throw new ArgumentNullException(nameof(dependente), "Dependente inválido");
			}

			var funcionarioExistente = _repositorio.GetFuncionarioById(funcionario.Id);
			if (funcionarioExistente == null)
			{
				throw new InvalidOperationException("Funcionário não encontrado");
			}

			if (string.IsNullOrEmpty(dependente.Nome))
			{
				throw new ArgumentException("Nome do dependente é obrigatório", nameof(dependente));
			}

			_repositorio.CadastraDependente(funcionario, dependente);
		}

		public void UpdateSenhaFuncionario(Funcionarios funcionario, string novaSenha)
		{
			if (funcionario == null)
			{
				throw new ArgumentNullException(nameof(funcionario), "Funcionário inválido");
			}

			var funcionarioExistente = _repositorio.GetFuncionarioById(funcionario.Id);
			if (funcionarioExistente == null)
			{
				throw new InvalidOperationException("Funcionário não encontrado");
			}

			if (string.IsNullOrEmpty(novaSenha))
			{
				throw new ArgumentException("Nova senha é obrigatória", nameof(novaSenha));
			}

			_repositorio.UpdateSenhaFuncionario(funcionario, novaSenha);
		}
	}
}
