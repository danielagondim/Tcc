using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Repositorio;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    public class CategoriasController : Controller
    {

        private CategoriasRepositorio _repositorio;

        // GET: Admin/Categorias
        public ActionResult Index()
        {
            _repositorio = new CategoriasRepositorio();
            var Categoria = _repositorio.ListaTodasCategorias();

            return View(Categoria);
        }

        public ActionResult CadastroCategoria()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroCategoria(Categoria Categoria)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new CategoriasRepositorio();
                _repositorio.Salvar(Categoria);

                TempData["mensagem"] = "Categoria cadastrada com sucesso!";

                return RedirectToAction("Index");
            }

            return View(Categoria);
        }


        public ViewResult EditarCategoria(Int32 id)
        {
            _repositorio = new CategoriasRepositorio();
            Categoria categoria = _repositorio.RetornaCategoriaPorId(id);
           

            return View(categoria);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCategoria(Categoria Categoria)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new CategoriasRepositorio();
                _repositorio.Salvar(Categoria);

                TempData["mensagem"] = "Categoria alterada com sucesso!";

                return RedirectToAction("Index");
            }

            return View(Categoria);
        }
    }
}