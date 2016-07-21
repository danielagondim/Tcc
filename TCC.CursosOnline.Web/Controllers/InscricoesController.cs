using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TCC.CursosOnline.Dominio.Entidades;
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

      
        public ActionResult Inscrever(int id_curso)
        {
            _repositorio = new InscricoesRespositorio();
            Inscricao inscricao = new Inscricao();

            string id_usuario = HttpContext.User.Identity.Name.ToString();

            inscricao.Ativo = true;
            inscricao.Id_curso = id_curso;
            inscricao.Id_usuario = Convert.ToInt32(id_usuario);
            inscricao.Data = DateTime.Now;
            inscricao.Finalizado = 0;
            inscricao.Certificado = false;

            _repositorio.InscreverNoCurso(inscricao);

            return RedirectToAction("Index");

            
        }
    }
}