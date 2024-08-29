using EasyPark.Models.Entidades.Empresa;

namespace EasyPark.Models.Repositorios.Interfaces.Empresa
{
    public interface IEmpresaRepositorio
    {
        // To Do: Verificar funcionalidades da Interface da Empresa

        IEnumerable<Empresas> GetEmpresas();
        Empresas GetEmpresaById(int id);
        void AdicionarEmpresa(Empresas empresa);
        void UpdateEmpresa(Empresas empresa);
        void DeleteEmpresa(long id);
    }
}
