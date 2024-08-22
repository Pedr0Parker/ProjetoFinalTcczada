using EasyPark.Models.Usuario;
using System.Collections.Generic;
using System.Linq;

namespace EasyPark.Models.Repositorios
{
    public class UsuarioRepositorio
    {
        private List<Usuarios> usuarios;

        public UsuarioRepositorio()
        {
            usuarios = new List<Usuarios>
            {
                new Usuarios { Id = 1, Nome = "Gabriel", Cpf = "11111111111", NomeInstituicao = "IRTrade" },
                new Usuarios { Id = 2, Nome = "Pedro", Cpf = "22222222222", NomeInstituicao = "IRTrade" },
                new Usuarios { Id = 3, Nome = "Rodrigo", Cpf = "33333333333", NomeInstituicao = "IRTrade" },
                new Usuarios { Id = 4, Nome = "Vinicius", Cpf = "44444444444", NomeInstituicao = "IRTrade" },
                new Usuarios { Id = 5, Nome = "FedShow", Cpf = "55555555555", NomeInstituicao = "FedsonLandia" },
                new Usuarios { Id = 6, Nome = "Vitor Henrique", Cpf = "66666666666", NomeInstituicao = "TCS" }
            };
        }

        public IEnumerable<Usuarios> GetAllUsuarios()
        {
            return usuarios;
        }

        public Usuarios GetUsuarioById(int id)
        {
            return usuarios.FirstOrDefault(p => p.Id == id);
        }

        public void AddUsuario(Usuarios usuario)
        {
            usuario.Id = usuarios.Max(p => p.Id) + 1;
            usuarios.Add(usuario);
        }

        public void UpdateUsuario(Usuarios usuario)
        {
            var existingUsuario = GetUsuarioById(usuario.Id);
            if (existingUsuario != null)
            {
                existingUsuario.Nome = usuario.Nome;
                existingUsuario.Cpf = usuario.Cpf;
                existingUsuario.NomeInstituicao = usuario.NomeInstituicao;
            }
        }

        public void DeleteUsuario(int id)
        {
            var usuario = GetUsuarioById(id);
            if (usuario != null)
            {
                usuarios.Remove(usuario);
            }
        }
    }
}
