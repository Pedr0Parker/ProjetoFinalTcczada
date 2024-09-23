using EasyPark.Models.Entidades.Funcionario;
using EasyPark.Models.Entidades.VisitaEstacionamento;

namespace EasyPark.Models.Repositorios
{
    public class VisitaEstacionamentoRepositorio
    {
        private List<VisitasEstacionamento> visitasEstacionamento;

        /// <summary>
        /// Realiza a busca da visita ao estacionamento via Id cadastrado no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VisitasEstacionamento GetVisitaById(long id)
        {
            return visitasEstacionamento.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Realiza a busca de todas as visitas cadastradas no banco de dados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VisitasEstacionamento> GetAllVisitas()
        {
            return visitasEstacionamento;
        }

        //public IEnumerable<VisitasEstacionamento> CadastraVisita(long idFuncionario, )
        //{

        //}
    }
}
