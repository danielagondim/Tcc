using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Entidades;
using TCC.CursosOnline.Dominio.Repositorio;
using TCC.CursosOnline.Web.Security;

namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    [PermissoesFiltro(Roles = "Administrador")]
    public class RelatorioController : Controller
    {
        private CursosRepositorio _repositorioCurso;
        private RelatorioRepositorio _repositorioRelatorio;


        // GET: Relatório
        public ActionResult Index()
        {
            _repositorioCurso = new CursosRepositorio();


            //Busca os topicos de cursos cadastrados
            var listacursos = _repositorioCurso.ListaTodosCursos();
            ViewData["listaCurso"] = new SelectList(listacursos, "Id_curso", "Titulo_curso");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EscolheCursoViewModel curso)
        {
            _repositorioRelatorio = new RelatorioRepositorio();
            List<RelatorioViewModel> relatorio = _repositorioRelatorio.RelatorioPorCurso(curso.Id);

            if (relatorio.Count > 0)
            {

                return RedirectToAction("Gerar", new { id_curso = curso.Id });
            }


            _repositorioCurso = new CursosRepositorio();
            //Busca os topicos de cursos cadastrados
            TempData["mensagem"] = "Nenhum resultado encontrado!";
            var listacursos = _repositorioCurso.ListaTodosCursos();
            ViewData["listaCurso"] = new SelectList(listacursos, "Id_curso", "Titulo_curso");

            return View();


        }


        public ActionResult Gerar(int id_curso)
        {
            _repositorioRelatorio = new RelatorioRepositorio();
            List<RelatorioViewModel> relatorio = _repositorioRelatorio.RelatorioPorCurso(id_curso);

            return View(relatorio);
        }



    }
}