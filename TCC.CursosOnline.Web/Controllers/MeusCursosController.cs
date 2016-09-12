using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Entidades;
using TCC.CursosOnline.Dominio.Repositorio;

namespace TCC.CursosOnline.Web.Controllers
{
    [Authorize]
    public class MeusCursosController : Controller
    {
        private MeusCursosRepositorio _repositorio;
        private UnidadesRepositorio _repositorioUnidade;
        private VideosRepositorio _repositorioVideo;
        private AtividadesRepositorio _repositorioAtividade;
        private MateriaisRepositorio _repositorioMaterial;
        private ResultadosRespositorio _repositorioResultado;
        private OpcoesRepositorio _repositorioOpcao;
        private RespostasRepositorio _respositorioResposta;

        // GET: Listar os cursos que o aluno eta inscrito
        public ActionResult Index()
        {
            _repositorio = new MeusCursosRepositorio();
            IPrincipal principal = HttpContext.User;

            var listaCursos = _repositorio.ListaMeusCursos(principal.Identity.Name.ToString());

            return View(listaCursos);
        }

        //Mostra os videos e atividades do curso selecionado
        public ActionResult Acessar(int id_curso)
        {
            _repositorio = new MeusCursosRepositorio();
            _repositorioUnidade = new UnidadesRepositorio();
            _repositorioVideo = new VideosRepositorio();
            _repositorioAtividade = new AtividadesRepositorio();
            _repositorioMaterial = new MateriaisRepositorio();

            IPrincipal principal = HttpContext.User;

            var dadosCurso = new MeusCursosViewModel();

            dadosCurso = _repositorio.BuscaDadosDoCurso(id_curso.ToString(), principal.Identity.Name.ToString());

            dadosCurso.ListaVideos = _repositorioVideo.ListaVideosPorCurso(dadosCurso.Id_curso);
            dadosCurso.ListaAtividades = _repositorioAtividade.ListaAtividadesPorCurso(dadosCurso.Id_curso);
            dadosCurso.ListaUnidade = _repositorioUnidade.ListaUnidadesAtivasPorCurso(dadosCurso.Id_curso);
            dadosCurso.ListaMaterial = _repositorioMaterial.ListaMateriaisPorCurso(dadosCurso.Id_curso);

            return View(dadosCurso);


        }

        //Exibe o video em um modal
        public ActionResult VerVideo (int id_video, int id_inscricao)
        {
            _repositorio = new MeusCursosRepositorio();
            _repositorioVideo = new VideosRepositorio();
            
            var videoselecionado = new Video();
            videoselecionado = _repositorioVideo.RetornaVideoPorId(id_video);

            _repositorio.InsereAndamento(id_video, id_inscricao);

            return PartialView(videoselecionado);
        }

        
        //Realizar a atividade uma questão por vez
        public ActionResult RealizarAtividade (int id_atividade, int id_inscricao)
        {
           
            _repositorio = new MeusCursosRepositorio();
            _repositorioResultado = new ResultadosRespositorio();
            _repositorioAtividade = new AtividadesRepositorio();
            _repositorioOpcao = new OpcoesRepositorio();
         

            //Verifica se ja existe o registro em Resultados
            //Se não existir adiciona o registro

            List<Resultado> listaresultados = _repositorioResultado.ListaResultados(id_inscricao, id_atividade);
            if (listaresultados.Count > 0)
            {
                if (listaresultados[0].finalizado)
                {
                    //Ativide ja foi finalizada exibe uma mensagem
                }
            }
            else
            {
                Resultado resultado = new Resultado();
                resultado.Id_inscricao = id_inscricao;
                resultado.Id_atividade = id_atividade;
                resultado.Data = DateTime.Now;

                _repositorioResultado.Salvar(resultado);
                //Gravar resultado
            }

            //Retorna o id_resultado dessa atividade
            int id_resultado = _repositorioResultado.RetornaResultado(id_inscricao, id_atividade);

            //Mostra a primeira pergunta
            AtividadeViewModel atividade = new AtividadeViewModel();
            atividade.Id = _repositorioAtividade.RetornaAtividadesPorId(id_atividade).Id_atividade;
            atividade.Nome_atividade = _repositorioAtividade.RetornaAtividadesPorId(id_atividade).Titulo;
            atividade.ListaQuestoes = _repositorio.BuscaQuestoesAtividade(id_atividade, id_resultado);
            atividade.IdResultado = id_resultado;
            if (atividade.ListaQuestoes.Count == 0)
            {
                //Aqui finaliza a atividade e contabiliza a nota
                //Mensagem de atividade realizada
                return PartialView();

            }else
            {
                //Mostra a proxima questão
                atividade.ListaOpcoes = _repositorioOpcao.ListaOpcoesPorAtividade(id_atividade);
                return PartialView(atividade);
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RealizarAtividade(AtividadeViewModel model)
        {
            _respositorioResposta = new RespostasRepositorio();
            var id_opcao_selecionada = model.ListaOpcoes;
            var id_questao = model.ListaQuestoes[0].Id_questao;
            var id_resultado = model.IdResultado;
            var id_inscricao = model.IdInscricao;
            var id_atividade = model.Id;
            Resposta resposta = new Resposta();
            resposta.Data = DateTime.Now;
            resposta.Id_resultado = model.IdResultado;
             

            //Gravar as respostas


            return RedirectToAction("RealizarAtividade", new { id_atividade = id_atividade, id_inscricao = id_inscricao } );
        }




    }
}