using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Entidades;
using TCC.CursosOnline.Dominio.Repositorio;

namespace TCC.CursosOnline.Web.Controllers
{
    [Authorize]
    public class DuvidasController : Controller
    {
        private ForumTopicosRepositorio _repositorioForumTopico;
        private ForumRespostasRepositorio _repositorioForumResposta;
        //private Forum_RespostaRepositorio _repositorioForumResposta;

        // GET: Duvidas
        public ActionResult Index()
        {
            _repositorioForumTopico = new ForumTopicosRepositorio();


            //Busca os topicos de forum
            var listatopicos = _repositorioForumTopico.ListaTodosTopicos();
            return View(listatopicos);
        }

        public ActionResult CadastrarTopico()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarTopico(Forum_topico topico)
        {
            topico.Id_usuario = Convert.ToInt32(HttpContext.User.Identity.Name.ToString());
            topico.Data = DateTime.Now;
            if (ModelState.IsValid)
            {
                _repositorioForumTopico = new ForumTopicosRepositorio();
                _repositorioForumTopico.Salvar(topico);

                TempData["mensagem"] = "Dúvida cadastrada com sucesso!";

                return RedirectToAction("Index");
            }


            return View(topico);
        }

        public ActionResult ListaRespostas(int id_topico)
        {
            _repositorioForumResposta = new ForumRespostasRepositorio();
            TempData["id_topico"] = id_topico;
            //Busca as respostas de um forum
            var listaRespostas = _repositorioForumResposta.ListaRespostasPorTopico(id_topico);
            return View(listaRespostas);
        }

        public ActionResult CadastrarResposta()
        {
            TempData.Keep("id_topico");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarResposta(Forum_resposta resposta)
        {
            resposta.Id_usuario = Convert.ToInt32(HttpContext.User.Identity.Name.ToString());
            resposta.Data = DateTime.Now;
            resposta.Id_topico = Convert.ToInt32(TempData["id_topico"]);
            if (ModelState.IsValid)
            {
                _repositorioForumResposta = new ForumRespostasRepositorio();
                _repositorioForumResposta.Salvar(resposta);

                TempData["mensagem"] = "Resposta cadastrada com sucesso!";

                return RedirectToAction("Index");
            }


            return View(resposta);
        }
    }
}