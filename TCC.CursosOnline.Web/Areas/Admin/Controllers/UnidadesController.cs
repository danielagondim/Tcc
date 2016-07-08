using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Repositorio;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    public class UnidadesController : Controller
    {
        private UnidadesRepositorio _repositorio;
        private CursosRepositorio _repositorio_curso;

        
        public ActionResult Index(int id_curso)
        {
            _repositorio = new UnidadesRepositorio();
            _repositorio_curso = new CursosRepositorio();

            var unidades = _repositorio.ListaUnidadesPorCurso(id_curso);
            var nome_curso = _repositorio_curso.RetornaCursoPorId(id_curso).Titulo_curso.ToString();

            ViewData["Id_curso"] = id_curso;
            ViewData["Nome_curso"] = nome_curso;

            return View(unidades);
        }

        
        public ActionResult CadastroUnidade(int id_curso)
        {
            _repositorio_curso = new CursosRepositorio();
            var cursos = _repositorio_curso.ListaCursoPorId(id_curso);
            ViewData["listaCurso"] = new SelectList(cursos, "Id_curso", "Titulo_curso");
            ViewData["Id_curso"] = id_curso;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroUnidade(Unidade Unidade)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new UnidadesRepositorio();
                _repositorio.Salvar(Unidade);

                TempData["mensagem"] = "Unidade cadastrada com sucesso!";

                return RedirectToAction("Index", new { id_curso = Unidade.Id_curso });
            }

            _repositorio_curso = new CursosRepositorio();
            var cursos = _repositorio_curso.ListaCursoPorId(Unidade.Id_curso);
            ViewData["listaCurso"] = new SelectList(cursos, "Id_curso", "Titulo_curso");
            ViewData["Id_curso"] = Unidade.Id_curso;

            return View(Unidade);
        }

        public ViewResult EditarUnidade(Int32 id)
        {
            _repositorio = new UnidadesRepositorio();
            Unidade unidade = _repositorio.RetornaUnidadePorId(id);

            _repositorio_curso = new CursosRepositorio();
            var cursos = _repositorio_curso.ListaCursoPorId(unidade.Id_curso);
            ViewData["listaCurso"] = new SelectList(cursos, "Id_curso", "Titulo_curso");
            ViewData["Id_curso"] = unidade.Id_curso;

            return View(unidade);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarUnidade(Unidade Unidade)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new UnidadesRepositorio();
                _repositorio.Salvar(Unidade);

                TempData["mensagem"] = "Unidade alterada com sucesso!";

                return RedirectToAction("Index", new { id_curso = Unidade.Id_curso });
            }

            _repositorio_curso = new CursosRepositorio();
            var cursos = _repositorio_curso.ListaCursoPorId(Unidade.Id_curso);
            ViewData["listaCurso"] = new SelectList(cursos, "Id_curso", "Titulo_curso");
            ViewData["Id_curso"] = Unidade.Id_curso;

            return View(Unidade);
        }
    }
}