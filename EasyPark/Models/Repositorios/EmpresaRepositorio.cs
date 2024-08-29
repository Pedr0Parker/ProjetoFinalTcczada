using EasyPark.Models.Entidades.Empresa;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Models.Repositorios
{
    public class EmpresaRepositorio
    {
        private List<Empresas> empresas;

        public EmpresaRepositorio()
        {
            empresas = new List<Empresas>
            {
                new Empresas { Id = 1, Login = "abc@gmail.com", Senha = "123456", Nome = "EmpresaX", NomeFantasia = "X",
                NomeDono = "Paulo", Cnpj = "11111111111111", ValorAssinatura = 100, Endereco = "Av. Paraná, 10", Contato = "(99)99999-9999", DataCadastro = DateTime.Now },
            };
        }

        #region Métodos Get

        /// <summary>
        /// Realiza a busca de todas as empresas cadastradas no banco de dados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Empresas> GetAllEmpresas()
        {
            return empresas;
        }

        /// <summary>
        /// Realiza a busca da empresa via Id cadastrado no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Empresas GetEmpresaById(long id)
        {
            return empresas.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Realiza a busca da empresa via Nome cadastrado no banco de dados
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public Empresas GetEmpresaByNome(string nome)
        {
            return empresas.FirstOrDefault(p => p.Nome == nome);
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

        /// <summary>
        /// Atualiza uma empresa de acordo com o seu Id
        /// </summary>
        /// <param name="empresa"></param>
        public void UpdateEmpresa(Empresas empresa)
        {
            var existingEmpresa = GetEmpresaById(empresa.Id);
            if (existingEmpresa != null)
            {
                // To Do: Implementar update para os parâmetros da empresa

                //existingEmpresa.Nome = usuario.Nome;
                //existingEmpresa.Cpf = usuario.Cpf;
                //existingEmpresa.NomeInstituicao = usuario.NomeInstituicao;
            }
        }

        /// <summary>
        /// Deleta a empresa desejada de acordo com seu Id
        /// </summary>
        /// <param name="id"></param>
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
