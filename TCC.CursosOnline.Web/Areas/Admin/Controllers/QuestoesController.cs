using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Repositorio;
using TCC.CursosOnline.Dominio.Entidades;


namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    public class QuestoesController : Controller
    {
        private QuestoesRepositorio _repositorio;
        private AtividadesRepositorio _repositorio_atividade;
        private UnidadesRepositorio _repositorio_unidade;
        private CursosRepositorio _repositorio_curso;

        // GET: Admin/Questoes
        public ActionResult Index(int id_atividade)
        {
            _repositorio = new QuestoesRepositorio();
            _repositorio_atividade = new AtividadesRepositorio();
            _repositorio_unidade = new UnidadesRepositorio();
            _repositorio_curso = new CursosRepositorio();

            var questoes = _repositorio.ListaQuestoesPorAtividade(id_atividade);
            var atividade = _repositorio_atividade.RetornaAtividadesPorId(id_atividade);
            var unidade = _repositorio_unidade.RetornaUnidadePorId(atividade.Id_unidade);
            var curso = _repositorio_curso.RetornaCursoPorId(unidade.Id_curso);

            ViewData["Id_curso"] = curso.Id_curso;
            ViewData["Nome_curso"] = curso.Titulo_curso.ToString();

            ViewData["Id_unidade"] = unidade.Id_unidade;
            ViewData["Nome_unidade"] = unidade.Nome.ToString();

            ViewData["Id_atividade"] = atividade.Id_atividade;
            ViewData["Nome_atividade"] = atividade.Titulo.ToString();

            return View(questoes);
        }

        public ActionResult CadastroQuestao(int id_atividade)
        {
            _repositorio_atividade = new AtividadesRepositorio();
            var atividades = _repositorio_atividade.ListaAtividadesPorId(id_atividade);

            ViewData["listaAtividades"] = new SelectList(atividades, "Id_atividade", "Titulo");
            ViewData["Id_atividade"] = id_atividade;

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroQuestao(Questao Questao)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new QuestoesRepositorio();
                _repositorio.Salvar(Questao);

                TempData["mensagem"] = "Questão cadastrada com sucesso!";

                return RedirectToAction("Index", new { id_atividade = Questao.Id_atividade });
            }

            _repositorio_atividade = new AtividadesRepositorio();
            var atividades = _repositorio_atividade.ListaAtividadesPorId(Questao.Id_atividade);
            ViewData["listaAtividades"] = new SelectList(atividades, "Id_atividade", "Titulo");
            ViewData["Id_atividade"] = Questao.Id_atividade;

            return View(Questao);
        }

        public ViewResult EditarQuestao(Int32 id_questao)
        {
            _repositorio = new QuestoesRepositorio();
            _repositorio_atividade = new AtividadesRepositorio();

            Questao questao = _repositorio.RetornaQuestãoPorId(id_questao);
            var atividades = _repositorio_atividade.ListaAtividadesPorId(questao.Id_atividade);

            ViewData["listaAtividades"] = new SelectList(atividades, "Id_unidade", "titulo");
            ViewData["Id_atividade"] = questao.Id_atividade;

            return View(questao);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarQuestao(Questao Questao)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new QuestoesRepositorio();
                _repositorio.Salvar(Questao);

                TempData["mensagem"] = "Questão alterada com sucesso!";

                return RedirectToAction("Index", new { id_atividade = Questao.Id_atividade });
            }

            _repositorio_atividade = new AtividadesRepositorio();
            var atividades = _repositorio_atividade.ListaAtividadesPorId(Questao.Id_atividade);
            ViewData["listaAtividades"] = new SelectList(atividades, "Id_unidade", "titulo");
            ViewData["Id_atividade"] = Questao.Id_atividade;

            return View(Questao);
        }
    }
}