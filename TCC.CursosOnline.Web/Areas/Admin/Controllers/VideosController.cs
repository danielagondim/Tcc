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
    [PermissoesFiltro(Roles = "Administrador")]
    public class VideosController : Controller
    {
        private VideosRepositorio _repositorio;
        private UnidadesRepositorio _repositorio_unidade;
        private CursosRepositorio _repositorio_curso;


        public ActionResult Index(int id_unidade)
        {
            _repositorio = new VideosRepositorio();
            _repositorio_unidade = new UnidadesRepositorio();
            _repositorio_curso = new CursosRepositorio();

            var videos = _repositorio.ListaVideosPorUnidade(id_unidade);
            var unidade = _repositorio_unidade.RetornaUnidadePorId(id_unidade);
            var curso = _repositorio_curso.RetornaCursoPorId(unidade.Id_curso);

            ViewData["Id_curso"] = curso.Id_curso;
            ViewData["Nome_curso"] = curso.Titulo_curso.ToString();

            ViewData["Id_unidade"] = unidade.Id_unidade;
            ViewData["Nome_unidade"] = unidade.Nome.ToString();
            
            return View(videos);
        }

        public ActionResult CadastroVideo(int id_unidade)
        {
            _repositorio_unidade = new UnidadesRepositorio();
            var unidades = _repositorio_unidade.ListaUnidadePorId(id_unidade);

            ViewData["listaUnidade"] = new SelectList(unidades, "Id_unidade", "Nome");
            ViewData["Id_unidade"] = id_unidade;

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroVideo(Video Video)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new VideosRepositorio();
                _repositorio.Salvar(Video);

                TempData["mensagem"] = "Video cadastrado com sucesso!";

                return RedirectToAction("Index", new { id_unidade = Video.Id_Unidade });
            }

            _repositorio_unidade = new UnidadesRepositorio();
            var unidades = _repositorio_unidade.ListaUnidadePorId(Video.Id_Unidade);
            ViewData["listaUnidade"] = new SelectList(unidades, "Id_unidade", "Nome");
            ViewData["Id_unidade"] = Video.Id_Unidade;
            return View(Video);
        }

        public ViewResult EditarVideo(Int32 id_video)
        {
            _repositorio = new VideosRepositorio();
            _repositorio_unidade = new UnidadesRepositorio();

            Video video = _repositorio.RetornaVideoPorId(id_video);
            var unidades = _repositorio_unidade.ListaUnidadePorId(video.Id_Unidade);

            ViewData["listaUnidade"] = new SelectList(unidades, "Id_unidade", "Nome");
            ViewData["Id_unidade"] = video.Id_Unidade;

            return View(video);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarVideo(Video Video)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new VideosRepositorio();
                _repositorio.Salvar(Video);

                TempData["mensagem"] = "Video alterado com sucesso!";

                return RedirectToAction("Index", new { id_unidade = Video.Id_Unidade });
            }
            _repositorio_unidade = new UnidadesRepositorio();
            var unidades = _repositorio_unidade.ListaUnidadePorId(Video.Id_Unidade);
            ViewData["listaUnidade"] = new SelectList(unidades, "Id_unidade", "Nome");
            ViewData["Id_unidade"] = Video.Id_Unidade;

            return View(Video);
        }
    }
}