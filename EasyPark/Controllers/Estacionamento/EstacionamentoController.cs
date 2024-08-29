using EasyPark.Models.Entidades.Usuario;
using EasyPark.Models.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Estacionamento
{
    public class EstacionamentoController
    {
        private EstacionamentoRepositorio repositorio = new EstacionamentoRepositorio();

        #region Métodos Get

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    var estacionamentos = repositorio.GetAllEstacionamentos();
        //    return View(estacionamentos);
        //}

        // To Do: verificar se haverá tela de busca de estacionamento por filtros

        #endregion

        // To Do: Verificar métodos implementados no diagrama de classe

        //public ActionResult VerificarUsuarios(Usuarios usuarios)
        //{
        //    var busca = repositorio.VerificaUsuarios(usuarios);

        //    return View(busca);
        //}

        //public ActionResult AplicarDesconto()
        //{
        //    return View();
        //}
    }
}
