using EasyPark.Models.Entidades.Estacionamento;

namespace EasyPark.Models.Repositorios.Interfaces.Estacionamento
{
    public interface IEstacionamentoRepositorio
    {
        // To Do: Verificar funcionalidades da Interface do Estacionamento

        IEnumerable<Estacionamentos> GetEstacionamentos();
        Estacionamentos GetEstacionamentoById(long id);
        void AdicionarEstacionamento(Estacionamentos estacionamento);
        void UpdateEstacionamento(Estacionamentos estacionamento);
        void DeleteEstacionamento(long id);
    }
}
