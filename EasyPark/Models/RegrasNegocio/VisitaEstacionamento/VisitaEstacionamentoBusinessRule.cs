﻿using EasyPark.Models.Entidades.Estacionamento;
using EasyPark.Models.Entidades.VisitaEstacionamento;
using EasyPark.Models.Repositorios;

namespace EasyPark.Models.RegrasNegocio.VisitaEstacionamento
{
	public class VisitaEstacionamentoBusinessRule
	{
		private readonly VisitaEstacionamentoRepositorio _repositorio;

		public VisitaEstacionamentoBusinessRule(VisitaEstacionamentoRepositorio repositorio)
		{
			_repositorio = repositorio;
		}

		public IEnumerable<VisitasEstacionamento> GetAllVisitas(Estacionamentos estacionamento)
		{
			return _repositorio.GetAllVisitas(estacionamento);
		}

		public VisitasEstacionamento GetVisitaById(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentException("Id inválido", nameof(id));
			}

			var visita = _repositorio.GetVisitaById(id);
			return visita;
		}
	}
}