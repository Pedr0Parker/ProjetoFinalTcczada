using EasyPark.Models.Entidades.Usuario;

namespace EasyPark.Models.Repositorios.Interfaces.Usuario
{
    public interface IUsuarioRepositorio
    {
        // To Do: Verificar funcionalidades da Interface do Usuário

        IEnumerable<Usuarios> GetUsuarios();
        Usuarios GetUsuarioById(long id);
        void AdicionarUsuario(Usuarios usuario);
        void UpdateUsuario(Usuarios usuarios);
        void DeleteUsuario(long id);

    }
}
