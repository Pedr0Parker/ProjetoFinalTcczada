using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Repositorios;

namespace EasyPark.Models.RegrasNegocio.SistemaEasyPark
{
	public class SistemaEasyParkBusinessRule
	{
		private readonly SistemaEasyParkRepositorio _repositorio;

		public SistemaEasyParkBusinessRule(SistemaEasyParkRepositorio repositorio)
		{
			_repositorio = repositorio;
		}

		public void CadastraEmpresa(Empresas empresa)
		{
			if (empresa == null)
			{
				throw new ArgumentNullException(nameof(empresa), "Empresa inválida");
			}

			if (string.IsNullOrEmpty(empresa.Nome))
			{
				throw new ArgumentException("Nome da empresa é obrigatório", nameof(empresa));
			}

			_repositorio.CadastraEmpresa(empresa);
		}

		public void UpdateEmpresa(Empresas empresa)
		{
			if (empresa == null)
			{
				throw new ArgumentNullException(nameof(empresa), "Empresa inválida");
			}

			if (string.IsNullOrEmpty(empresa.Nome))
			{
				throw new ArgumentException("Nome da empresa é obrigatório", nameof(empresa));
			}

			var empresaExistente = _repositorio.GetEmpresaById(empresa.Id);
			if (empresaExistente == null)
			{
				throw new InvalidOperationException("Empresa não encontrada");
			}

			_repositorio.UpdateEmpresa(empresa);
		}

		public void CadastraEstacionamento(Estacionamentos estacionamento)
		{
			if (estacionamento == null)
			{
				throw new ArgumentNullException(nameof(estacionamento), "Estacionamento inválido");
			}

			if (string.IsNullOrEmpty(estacionamento.Nome))
			{
				throw new ArgumentException("Nome do estacionamento é obrigatório", nameof(estacionamento));
			}

			_repositorio.CadastraEstacionamento(estacionamento);
		}

		public void UpdateEstacionamento(Estacionamentos estacionamento)
		{
			if (estacionamento == null)
			{
				throw new ArgumentNullException(nameof(estacionamento), "Estacionamento inválido");
			}

			if (string.IsNullOrEmpty(estacionamento.Nome))
			{
				throw new ArgumentException("Nome do estacionamento é obrigatório", nameof(estacionamento));
			}

			var estacionamentoExistente = _repositorio.GetEstacionamentoById(estacionamento.Id);
			if (estacionamentoExistente == null)
			{
				throw new InvalidOperationException("Estacionamento não encontrado");
			}

			_repositorio.UpdateEstacionamento(estacionamento);
		}
	}
}
