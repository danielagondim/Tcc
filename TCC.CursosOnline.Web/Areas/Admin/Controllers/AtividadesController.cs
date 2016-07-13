using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Repositorio;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    public class AtividadesController : Controller
    {
        private AtividadesRepositorio _repositorio;
        private UnidadesRepositorio _repositorio_unidade;
        private CursosRepositorio _repositorio_curso;
        private QuestoesRepositorio _repositorio_questao;
        private OpcoesRepositorio _repositorio_opcao;


        public ActionResult Index(int id_unidade)
        {
            _repositorio = new AtividadesRepositorio();
            _repositorio_unidade = new UnidadesRepositorio();
            _repositorio_curso = new CursosRepositorio();

            var atividades = _repositorio.ListaAtividadesPorUnidade(id_unidade);
            var unidade = _repositorio_unidade.RetornaUnidadePorId(id_unidade);
            var curso = _repositorio_curso.RetornaCursoPorId(unidade.Id_curso);

            ViewData["Id_curso"] = curso.Id_curso;
            ViewData["Nome_curso"] = curso.Titulo_curso.ToString();

            ViewData["Id_unidade"] = unidade.Id_unidade;
            ViewData["Nome_unidade"] = unidade.Nome.ToString();

            return View(atividades);
        }

        public ActionResult CadastroAtividade(int id_unidade)
        {
            _repositorio_unidade = new UnidadesRepositorio();
            var unidades = _repositorio_unidade.ListaUnidadePorId(id_unidade);

            ViewData["listaUnidade"] = new SelectList(unidades, "Id_unidade", "Nome");
            ViewData["Id_unidade"] = id_unidade;

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroAtividade(Atividade Atividade)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new AtividadesRepositorio();
                _repositorio.Salvar(Atividade);

                TempData["mensagem"] = "Atividade cadastrada com sucesso!";

                return RedirectToAction("Index", new { id_unidade = Atividade.Id_unidade });
            }

            _repositorio_unidade = new UnidadesRepositorio();
            var unidades = _repositorio_unidade.ListaUnidadePorId(Atividade.Id_unidade);
            ViewData["listaUnidade"] = new SelectList(unidades, "Id_unidade", "Nome");
            ViewData["Id_unidade"] = Atividade.Id_unidade;
            return View(Atividade);
        }

        public ViewResult EditarAtividade(Int32 id_atividade)
        {
            _repositorio = new AtividadesRepositorio();
            _repositorio_unidade = new UnidadesRepositorio();

            Atividade atividade = _repositorio.RetornaAtividadesPorId(id_atividade);
            var unidades = _repositorio_unidade.ListaUnidadePorId(atividade.Id_unidade);

            ViewData["listaUnidade"] = new SelectList(unidades, "Id_unidade", "Nome");
            ViewData["Id_unidade"] = atividade.Id_unidade;

            return View(atividade);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarAtividade(Atividade Atividade)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new AtividadesRepositorio();
                _repositorio.Salvar(Atividade);

                TempData["mensagem"] = "Atividade alterada com sucesso!";

                return RedirectToAction("Index", new { id_unidade = Atividade.Id_unidade });
            }
            _repositorio_unidade = new UnidadesRepositorio();
            var unidades = _repositorio_unidade.ListaUnidadePorId(Atividade.Id_unidade);
            ViewData["listaUnidade"] = new SelectList(unidades, "Id_unidade", "Nome");
            ViewData["Id_unidade"] = Atividade.Id_unidade;

            return View(Atividade);
        }

        public ActionResult VisualizarAtividade(int id_atividade)
        {
            var atividadeVM = new AtividadeViewModel();
            _repositorio = new AtividadesRepositorio();
            _repositorio_questao = new QuestoesRepositorio();
            _repositorio_opcao = new OpcoesRepositorio();

            var atividade = _repositorio.RetornaAtividadesPorId(id_atividade);
            atividadeVM.Nome_atividade = atividade.Titulo.ToString();
            ViewData["id_unidade"] = atividade.Id_unidade;

            var questao = _repositorio_questao.ListaQuestoesPorAtividadeAtivasOrdenadas(id_atividade);
            var opcao = _repositorio_opcao.ListaOpcoesPorAtividade(id_atividade);
           
            atividadeVM.ListaQuestoes = questao;
            atividadeVM.ListaOpcoes = opcao;

            return PartialView(atividadeVM);
            


        }
    }
}