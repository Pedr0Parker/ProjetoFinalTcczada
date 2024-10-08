﻿using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Usuario;

namespace EasyPark.Models.Repositorios
{
    public class EstacionamentoRepositorio
    {
        private List<Estacionamentos> estacionamentos;

        public EstacionamentoRepositorio()
        {
            estacionamentos = new List<Estacionamentos>
            {
                new Estacionamentos { Id = 1, Login = "def@gmail.com", Senha = "789123", Nome = "EstacionamentoX", 
                Cnpj = "222222222222222", Endereco = "Av. São Paulo, 20", Contato = "(99)99999-9999", DataCadastro = DateTime.Now },
            };
        }

        #region Métodos Get

        // To Do: implementar posteriormente, uma possível busca por filtro

        /// <summary>
        /// Realiza a busca de todos os estacionamentos cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Estacionamentos> GetAllEstacionamentos()
        {
            return estacionamentos;
        }

        /// <summary>
        /// Realiza a busca do estacionamento via Id cadastrado no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Estacionamentos GetEstacionamentoById(long id)
        {
            return estacionamentos.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Realiza a busca do estacionamento via Nome cadastrado no banco de dados
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public Estacionamentos GetEstacionamentoByNome(string nome)
        {
            return estacionamentos.FirstOrDefault(p => p.Nome == nome);
        }

        #endregion

        // To Do: Verificar métodos implementados no diagrama de classe
        // Quais funcionalidades? Como implementar?

        public IEnumerable<Usuarios> VerificaUsuarios(Usuarios usuarios)
        {
            var teste = new List<Usuarios>();
            return teste;
        }

        public void AplicaDesconto()
        {

        }
    }
}
