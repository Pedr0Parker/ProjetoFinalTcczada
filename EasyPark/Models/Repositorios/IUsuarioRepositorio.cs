using EasyPark.Models.Usuario;

namespace EasyPark.Models.Repositorios
{
    public interface IUsuarioRepositorio
    {
        IEnumerable<Usuarios> GetUsuarios();
        Usuarios GetUsuarioById(int id);
        void AdicionarUsuario(Usuarios usuario);
        void UpdateUsuario(Usuarios usuarios);
        void DeleteUsuario(int id);

    }
}
