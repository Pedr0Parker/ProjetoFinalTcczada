using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.Plano;
using EasyPark.Models.Entidades.Usuario;
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

		public void CadastraPlano(Planos plano)
		{
			if (plano == null)
			{
				throw new ArgumentNullException(nameof(plano), "Plano inválido");
			}

			if (string.IsNullOrEmpty(plano.NomePlano))
			{
				throw new ArgumentException("Nome do plano é obrigatório", nameof(plano));
			}

			var planoExistente = _repositorio.GetPlanoById(plano.Id);
			if (planoExistente != null)
			{
				throw new InvalidOperationException("Plano já existe");
			}

			_repositorio.CadastraPlano(plano);
		}

		public void UpdatePlano(Planos plano)
		{
			if (plano == null)
			{
				throw new ArgumentNullException(nameof(plano), "Plano inválido");
			}

			if (string.IsNullOrEmpty(plano.NomePlano))
			{
				throw new ArgumentException("Nome do plano é obrigatório", nameof(plano));
			}

			var planoExistente = _repositorio.GetPlanoById(plano.Id);
			if (planoExistente == null)
			{
				throw new InvalidOperationException("Plano não encontrado");
			}

			_repositorio.UpdatePlano(plano);
		}

		public void DeletePlano(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentException("Id inválido", nameof(id));
			}

			var planoExistente = _repositorio.GetPlanoById(id);
			if (planoExistente == null)
			{
				throw new InvalidOperationException("Plano não encontrado");
			}

			_repositorio.DeletePlano(id);
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

			var empresaExistente = _repositorio.GetEmpresaById(empresa.Id);
			if (empresaExistente != null)
			{
				throw new InvalidOperationException("Empresa já existe");
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

		public void DeleteEmpresa(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentException("Id inválido", nameof(id));
			}

			var empresaExistente = _repositorio.GetEmpresaById(id);
			if (empresaExistente == null)
			{
				throw new InvalidOperationException("Empresa não encontrada");
			}

			_repositorio.DeleteEmpresa(id);
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

			var estacionamentoExistente = _repositorio.GetEstacionamentoById(estacionamento.Id);
			if (estacionamentoExistente != null)
			{
				throw new InvalidOperationException("Estacionamento já existe");
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

		public void DeleteEstacionamento(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentException("Id inválido", nameof(id));
			}

			var estacionamentoExistente = _repositorio.GetEstacionamentoById(id);
			if (estacionamentoExistente == null)
			{
				throw new InvalidOperationException("Estacionamento não encontrado");
			}

			_repositorio.DeleteEstacionamento(id);
		}

		public void CadastraUsuario(Usuarios usuario)
		{
			if (usuario == null)
			{
				throw new ArgumentNullException(nameof(usuario), "Usuário inválido");
			}

			if (string.IsNullOrEmpty(usuario.Nome))
			{
				throw new ArgumentException("Nome do usuário é obrigatório", nameof(usuario));
			}

			var usuarioExistente = _repositorio.GetUsuarioById(usuario.Id);
			if (usuarioExistente != null)
			{
				throw new InvalidOperationException("Usuário já existe");
			}

			_repositorio.CadastraUsuario(usuario);
		}

		public void UpdateUsuario(Usuarios usuario)
		{
			if (usuario == null)
			{
				throw new ArgumentNullException(nameof(usuario), "Usuário inválido");
			}

			if (string.IsNullOrEmpty(usuario.Nome))
			{
				throw new ArgumentException("Nome do usuário é obrigatório", nameof(usuario));
			}

			var usuarioExistente = _repositorio.GetUsuarioById(usuario.Id);
			if (usuarioExistente == null)
			{
				throw new InvalidOperationException("Usuário não encontrado");
			}

			_repositorio.UpdateUsuario(usuario);
		}

		public void DeleteUsuario(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentException("Id inválido", nameof(id));
			}

			var usuarioExistente = _repositorio.GetUsuarioById(id);
			if (usuarioExistente == null)
			{
				throw new InvalidOperationException("Usuário não encontrado");
			}

			_repositorio.DeleteUsuario(id);
		}
	}
}
