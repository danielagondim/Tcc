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
        private UnidadesRepositorio _repositorioUnidade;
        private VideosRepositorio _repositorioVideo;
        private AtividadesRepositorio _repositorioAtividade;

        // GET: Listar os cursos que o aluno eta inscrito
        public ActionResult Index()
        {
            _repositorio = new MeusCursosRepositorio();
            IPrincipal principal = HttpContext.User;

            var listaCursos = _repositorio.ListaMeusCursos(principal.Identity.Name.ToString());

            return View(listaCursos);
        }

        public ActionResult Acessar(int id_curso)
        {
            _repositorio = new MeusCursosRepositorio();
            _repositorioUnidade = new UnidadesRepositorio();
            _repositorioVideo = new VideosRepositorio();
            _repositorioAtividade = new AtividadesRepositorio();

            IPrincipal principal = HttpContext.User;

            var dadosCurso = new MeusCursosViewModel();

            dadosCurso = _repositorio.BuscaDadosDoCurso(id_curso.ToString(), principal.Identity.Name.ToString());

            //dadosCurso.ListaUnidades = _repositorioUnidade.ListaUnidadesPorCurso(dadosCurso.Id_curso);
            dadosCurso.ListaVideos = _repositorioVideo.ListaVideosPorCurso(dadosCurso.Id_curso);
            dadosCurso.ListaAtividades = _repositorioAtividade.ListaAtividadesPorCurso(dadosCurso.Id_curso);

            return View(dadosCurso);


        }
    }
}