using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Repositorio;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    public class CursosController : Controller
    {
        private CursosRepositorio _repositorio;

        // GET: Admin/Cursos
        public ActionResult Index()
        {
            _repositorio = new CursosRepositorio();
            var Cursos = _repositorio.ListaTodosCursos();
            return View(Cursos);
        }
    }
}