using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Repositorio;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    public class OpcoesController : Controller
    {
        private OpcoesRepositorio _repositorio;
        private QuestoesRepositorio _repositorio_questao;
        private AtividadesRepositorio _repositorio_atividade;
        private UnidadesRepositorio _repositorio_unidade;
        private CursosRepositorio _repositorio_curso;

        // GET: Admin/Opcoes
        public ActionResult Index(int id_questao)
        {
            _repositorio = new OpcoesRepositorio();
            _repositorio_questao = new QuestoesRepositorio();
            _repositorio_atividade = new AtividadesRepositorio();
            _repositorio_unidade = new UnidadesRepositorio();
            _repositorio_curso = new CursosRepositorio();

            var opcoes = _repositorio.ListaOpcoesPorQuestao(id_questao);
            var questoes = _repositorio_questao.RetornaQuestãoPorId(id_questao);
            var atividade = _repositorio_atividade.RetornaAtividadesPorId(questoes.Id_atividade);
            var unidade = _repositorio_unidade.RetornaUnidadePorId(atividade.Id_unidade);
            var curso = _repositorio_curso.RetornaCursoPorId(unidade.Id_curso);

            ViewData["Id_curso"] = curso.Id_curso;
            ViewData["Nome_curso"] = curso.Titulo_curso.ToString();

            ViewData["Id_unidade"] = unidade.Id_unidade;
            ViewData["Nome_unidade"] = unidade.Nome.ToString();

            ViewData["Id_atividade"] = atividade.Id_atividade;
            ViewData["Nome_atividade"] = atividade.Titulo.ToString();

            ViewData["Id_questao"] = questoes.Id_questao;
            ViewData["Nome_questao"] = questoes.Enunciado.ToString();

            return View(opcoes);
        }

        public ActionResult CadastroOpcao(int id_questao)
        {
            _repositorio_questao = new QuestoesRepositorio();
            var questoes = _repositorio_questao.ListaQuestoesPorId(id_questao);

            ViewData["listaQuestoes"] = new SelectList(questoes, "Id_questao", "Enunciado");
            ViewData["Id_atividade"] = id_questao;

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroOpcao(Opcao opcao)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new OpcoesRepositorio();
                _repositorio.Salvar(opcao);

                TempData["mensagem"] = "Opção cadastrada com sucesso!";

                return RedirectToAction("Index", new { id_questao = opcao.Id_questao });
            }

            _repositorio_questao = new QuestoesRepositorio();
            var questoes = _repositorio_questao.ListaQuestoesPorId(opcao.Id_questao);

            ViewData["listaQuestoes"] = new SelectList(questoes, "Id_questao", "Enunciado");
            ViewData["Id_atividade"] = opcao.Id_questao;

            return View(opcao);
        }

        public ViewResult EditarOpcao(Int32 id_opcao)
        {
            _repositorio = new OpcoesRepositorio();
            _repositorio_questao = new QuestoesRepositorio();

            Opcao opcao = _repositorio.RetornaOpcaoPorId(id_opcao);
            var questoes = _repositorio_questao.ListaQuestoesPorId(opcao.Id_questao);

            ViewData["listaQuestoes"] = new SelectList(questoes, "Id_questao", "enunciado");
            ViewData["Id_questao"] = opcao.Id_questao;

            return View(opcao);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarOpcao(Opcao opcao)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new OpcoesRepositorio();
                _repositorio.Salvar(opcao);

                TempData["mensagem"] = "Opção alterada com sucesso!";

                return RedirectToAction("Index", new { id_questao = opcao.Id_questao });
            }

            _repositorio_questao = new QuestoesRepositorio();
            var questoes = _repositorio_questao.ListaQuestoesPorId(opcao.Id_questao);

            ViewData["listaQuestoes"] = new SelectList(questoes, "Id_questao", "enunciado");
            ViewData["Id_questao"] = opcao.Id_questao;

            return View(opcao);
        }
    }
}