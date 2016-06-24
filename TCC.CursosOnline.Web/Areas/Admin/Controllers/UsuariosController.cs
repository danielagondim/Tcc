using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Repositorio;

namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    public class UsuariosController : Controller
    {
        private UsuariosRepositorio _repositorio;
        // GET: Admin/Usuarios
        public ActionResult Index()
        {
            _repositorio = new UsuariosRepositorio();
            var Usuarios = _repositorio.Usuarios;

            return View(Usuarios);
        }

        public ActionResult CadastroUsuario()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroUsuario(Usuario Usuario)
        {
            if (ModelState.IsValid)
            {
                new UsuariosRepositorio().Usuarios.add;
                return RedirectToAction("ListaFuncionarios");
            }

            return View(funcionario);
        }


    }
}