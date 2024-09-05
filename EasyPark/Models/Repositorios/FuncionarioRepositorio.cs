using EasyPark.Models.Entidades.Funcionario;

namespace EasyPark.Models.Repositorios
{
    public class FuncionarioRepositorio
    {
        private List<Funcionarios> funcionarios;

        #region Métodos Get

        /// <summary>
        /// Realiza a busca do funcionário via Id cadastrado no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Funcionarios GetFuncionarioById(long id)
        {
            return funcionarios.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Realiza a busca de todos os funcionários cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Funcionarios> GetAllFuncionarios()
        {
            return funcionarios;
        }

        #endregion

        public void UpdateFuncionario(Funcionarios funcionario)
        {
            var existingFuncionario = GetFuncionarioById(funcionario.Id);
            if (existingFuncionario != null)
            {
                // To Do: Implementar update para os parâmetros dos funcionários

                existingFuncionario.Login = funcionario.Login;
                existingFuncionario.Senha = funcionario.Senha;
                existingFuncionario.Nome = funcionario.Nome;
                existingFuncionario.CpfCnpj = funcionario.CpfCnpj;
                existingFuncionario.Contato = funcionario.Contato;
                existingFuncionario.Email = funcionario.Email;
            }
        }

        public void CriarVisita(Funcionarios funcionario)
        {
            // Implementar método de Criar Visitas
        }

        public void PagarPlano(Funcionarios funcionario)
        {
            // Implementar método de Pagar Plano
        }
    }
}
