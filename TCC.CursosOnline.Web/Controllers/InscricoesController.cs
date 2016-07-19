using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TCC.CursosOnline.Dominio.Repositorio;

namespace TCC.CursosOnline.Web.Controllers
{
    public class InscricoesController : Controller
    {
        private InscricoesRespositorio _repositorio;
        // GET: Lista primeiro os cursos disponíveis para inscrição
        public ActionResult Index()
        {
            _repositorio = new InscricoesRespositorio();
          
            IPrincipal principal = HttpContext.User;


            var listaCursos = _repositorio.ListaCursosDisponiveis(principal.Identity.Name.ToString());
            return View(listaCursos);
        }
    }
}