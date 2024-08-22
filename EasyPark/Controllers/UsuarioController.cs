using EasyPark.Models.Repositorios;
using EasyPark.Models.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace EasyPark.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioRepositorio repositorio = new UsuarioRepositorio();

        [HttpGet]
        public ActionResult Index()
        {
            var usuarios = repositorio.GetAllUsuarios();
            return View(usuarios);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                var usuario = repositorio.GetUsuarioById(id);
                if (usuario == null)
                {
                    throw new Exception();
                }

                return View(usuario);
            }
            catch
            {
                return View("Error", model: "Usuário não localizado!");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                repositorio.AddUsuario(usuario);
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        [HttpPut]
        public ActionResult Update(Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                repositorio.UpdateUsuario(usuario);
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                repositorio.DeleteUsuario(id);
                return RedirectToAction("Index");
            }

            return View(id);
        }
    }
}
