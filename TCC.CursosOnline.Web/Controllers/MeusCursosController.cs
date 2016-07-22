using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Entidades;
using TCC.CursosOnline.Dominio.Repositorio;

namespace TCC.CursosOnline.Web.Controllers
{
    public class MeusCursosController : Controller
    {
        private MeusCursosRepositorio _repositorio;
        // GET: Listar os cursos que o aluno eta inscrito
        public ActionResult Index()
        {
            _repositorio = new MeusCursosRepositorio();
            IPrincipal principal = HttpContext.User;

            var listaCursos = _repositorio.ListaMeusCursos(principal.Identity.Name.ToString());

            return View(listaCursos);
        }
    }
}