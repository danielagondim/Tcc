using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Repositorio;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    public class UsuariosController : Controller
    {
        private UsuariosRepositorio _repositorio;
       
        // GET: Admin/Usuarios
        public ActionResult Index()
        {
            _repositorio = new UsuariosRepositorio();
            var Usuarios = _repositorio.ListaTodosUsuarios();

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
                _repositorio = new UsuariosRepositorio();
                _repositorio.Salvar(Usuario);

                TempData["mensagem"] = "Usuário cadastrado com sucesso!";

                return RedirectToAction("Index");
            }

            return View(Usuario);
        }

        public ViewResult EditarUsuario(Int32 id)
        {
            _repositorio = new UsuariosRepositorio();
            Usuario usuario = _repositorio.ListaUsuarioPorId(id);
              
            return View(usuario);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarUsuario(Usuario Usuario)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new UsuariosRepositorio();
                _repositorio.Salvar(Usuario);

                TempData["mensagem"] = "Usuário alterado com sucesso!";

                return RedirectToAction("Index");
            }

            return View(Usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DesativarUsuario(Int32 Id)
        {
            _repositorio = new UsuariosRepositorio();
            Usuario Usuario = _repositorio.Desativar(Id);

            if (Usuario != null)
            {
                TempData["mensagem"] = "Usuário desativado com sucesso!";
            }

            return RedirectToAction("Index");
        }


    }
}