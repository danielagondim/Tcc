using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Repositorio;
using TCC.CursosOnline.Dominio.Entidades;
using TCC.CursosOnline.Web.Security;

namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    [PermissoesFiltro(Roles="Administrador")]
    public class CursosController : Controller
    {
        private CursosRepositorio _repositorio;
        private CategoriasRepositorio _repositorioCategoria;

        // GET: Admin/Cursos
        public ActionResult Index()
        {
            _repositorio = new CursosRepositorio();
            var Cursos = _repositorio.ListaTodosCursos();
           
           
            return View(Cursos);
        }

        public ActionResult CadastroCurso()
        {
            _repositorioCategoria = new CategoriasRepositorio();
            var categorias = _repositorioCategoria.ListaTodasCategoriasAtivas();
            ViewData["listaCategorias"] = new SelectList(categorias, "Id_categoria", "Descricao");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroCurso(Curso Curso)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new CursosRepositorio();
                _repositorio.Salvar(Curso);

                TempData["mensagem"] = "Curso cadastrado com sucesso!";

                return RedirectToAction("Index");
            }
            _repositorioCategoria = new CategoriasRepositorio();
            var categorias = _repositorioCategoria.ListaTodasCategorias();
            ViewData["listaCategorias"] = new SelectList(categorias, "Id_categoria", "Descricao");

            return View(Curso);
        }

        public ViewResult EditarCurso(Int32 id)
        {
            _repositorio = new CursosRepositorio();
            Curso curso = _repositorio.RetornaCursoPorId(id);
            _repositorioCategoria = new CategoriasRepositorio();
            var categorias = _repositorioCategoria.ListaTodasCategorias();
            ViewData["listaCategorias"] = new SelectList(categorias, "Id_categoria", "Descricao");

            return View(curso);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCurso(Curso Curso)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new CursosRepositorio();
                _repositorio.Salvar(Curso);

                TempData["mensagem"] = "Curso alterado com sucesso!";

                return RedirectToAction("Index");
            }

            _repositorioCategoria = new CategoriasRepositorio();
            var categorias = _repositorioCategoria.ListaTodasCategorias();
            ViewData["listaCategorias"] = new SelectList(categorias, "Id_categoria", "Descricao");

            return View(Curso);
        }
    }
}