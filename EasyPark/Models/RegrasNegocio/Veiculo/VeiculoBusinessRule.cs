using EasyPark.Models.Entidades.Veiculo;
using EasyPark.Models.Repositorios;

namespace EasyPark.Models.RegrasNegocio.Veiculo
{
	public class VeiculoBusinessRule
	{
		private readonly VeiculoRepositorio _repositorio;

		public VeiculoBusinessRule(VeiculoRepositorio repositorio)
		{
			_repositorio = repositorio;
		}

		public IEnumerable<Veiculos> GetAllVeiculos()
		{
			return _repositorio.GetAllVeiculos();
		}

		public Veiculos GetVeiculoById(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentException("Id inválido", nameof(id));
			}

			var veiculo = _repositorio.GetVeiculoById(id);
			return veiculo;
		}
	}
}
