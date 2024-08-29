using EasyPark.Models.Entidades.Empresa;
using EasyPark.Models.Entidades.Usuario;
using EasyPark.Models.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers.Empresa
{
    public class EmpresaController
    {
        private EmpresaRepositorio repositorio = new EmpresaRepositorio();

        #region Métodos Get

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    var empresas = repositorio.GetAllEmpresas();
        //    return View(empresas);
        //}

        //[HttpGet]
        //public ActionResult Details(int id)
        //{
        //    try
        //    {
        //        var empresa = repositorio.GetEmpresaById(id);
        //        if (empresa == null)
        //        {
        //            throw new Exception();
        //        }

        //        return View(empresa);
        //    }
        //    catch
        //    {
        //        return View("Error", model: "Empresa não localizada!");
        //    }
        //}

        #endregion

        #region Métodos Post

        //[HttpPost]
        //public ActionResult Create(Empresas empresa)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repositorio.AddEmpresa(empresa);
        //        return RedirectToAction("Index");
        //    }

        //    return View(empresa);
        //}

        //[HttpPost]
        //public ActionResult CadastrarFuncionario(Funcionario funcionario)
        //{
            
        //}

        #endregion

        #region Métodos Put

        //[HttpPut]
        //public ActionResult Update(Empresas empresa)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repositorio.UpdateEmpresa(empresa);
        //        return RedirectToAction("Index");
        //    }

        //    return View(empresa);
        //}

        #endregion

        #region Métodos Delete

        //[HttpDelete]
        //public ActionResult Delete(long id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repositorio.DeleteEmpresa(id);
        //        return RedirectToAction("Index");
        //    }

        //    return View(id);
        //}

        #endregion
    }
}
