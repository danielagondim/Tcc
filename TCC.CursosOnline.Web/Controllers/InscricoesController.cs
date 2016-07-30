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
    [Authorize]
    public class InscricoesController : Controller
    {
        private InscricoesRespositorio _repositorio;
        private CategoriasRepositorio _repositorioCategoria;

        // GET: Lista primeiro os cursos disponíveis para inscrição
        public ActionResult Index()
        {
            _repositorio = new InscricoesRespositorio();
            _repositorioCategoria = new CategoriasRepositorio();
          
            IPrincipal principal = HttpContext.User;


            var listaCursos = _repositorio.ListaCursosDisponiveis(principal.Identity.Name.ToString());
            Categoria cat = new Categoria();

            for (int x = 0; x < listaCursos.Count; x++)
            {
                cat = _repositorioCategoria.RetornaCategoriaPorId(listaCursos[x].Id_categoria);
                listaCursos[x].Categoria = cat;
            }

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
            inscricao.Data_resultado = (DateTime?)null;

            _repositorio.InscreverNoCurso(inscricao);

            TempData["mensagem"] = "Inscrição realizada com sucesso!";

            return RedirectToAction("Index");

            
        }
    }
}