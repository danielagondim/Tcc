using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class MeusCursosRepositorio
    {
        string conexao = WebConfigurationManager.ConnectionStrings["EfDbContext"].ConnectionString;
        private readonly EfDbContext _context = new EfDbContext();

        //Lista os cursos que o usuario esta inscrito
        public List<MeusCursosViewModel> ListaMeusCursos(string id_usuario)
        {
            var sql = " select " +
                      "    cursos.id_curso, " +
                      "    cursos.titulo_curso, " +
                      "    Categorias.id_categoria, " +
                      "    Categorias.descricao, " +
                      "    Inscricoes.id_inscricao, " +
                      "    Inscricoes.data, " +
                      "    Inscricoes.finalizado, " +
                      "    Inscricoes.nota_final, " +
                      "    Inscricoes.data_resultado, " +
                      "    (convert(varchar(5), (count(Andamentos.id_video) * 100) / count(Videos.id_video)) + '%') as andamento " +
                      " from " +
                      "     Inscricoes " +
                      "     inner join Cursos on Cursos.id_curso = Inscricoes.id_curso " +
                      "     inner join Categorias on Categorias.id_categoria = Cursos.id_categoria " +
                      "     inner join Unidades on Unidades.id_curso = Cursos.id_curso " +
                      "     left join Videos on Videos.id_unidade = Unidades.id_unidade " +
                      "     Left Join Andamentos on Andamentos.id_inscricao = Inscricoes.id_inscricao and Andamentos.id_video = Videos.id_video " +
                      " where " +
                      "     Inscricoes.id_usuario = " + id_usuario +
                      "     and Inscricoes.ativo = 1 " +
                      "     and Cursos.ativo = 1 " +
                      "     and Unidades.ativo = 1 " +
                      "     and Videos.ativo = 1 " +
                      " group by " +
                      "     cursos.id_curso, " +
                      "     cursos.titulo_curso," +
                      "     Categorias.id_categoria," +
                      "     Categorias.descricao," +
                      "     Inscricoes.id_inscricao," +
                      "     Inscricoes.data," +
                      "     Inscricoes.finalizado," +
                      "     Inscricoes.nota_final," +
                      "     Inscricoes.data_resultado ";

            using (var conn = new SqlConnection(conexao))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    List<MeusCursosViewModel> dados = new List<MeusCursosViewModel>();
                    MeusCursosViewModel p = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                p = new MeusCursosViewModel();
                                p.Id_inscricao = (int)reader["id_inscricao"];
                                p.Id_curso = (int)reader["id_curso"];
                                p.Titulo_curso = (string)reader["titulo_curso"];
                                p.Id_categoria = (int)reader["id_categoria"];
                                p.descricao_categoria = (string)reader["descricao"];
                                p.Dt_inscricao = (DateTime)reader["data"];
                                p.finalizado = (int)reader["finalizado"];
                                p.NotaFinal = (int)reader["nota_final"];
                                p.Andamento = (string)reader["andamento"];
                                dados.Add(p);
                            }

                            reader.Close();
                            conn.Close();
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }
                    return dados;
                }

            }
        }

        //Busca os dados do curso
        public MeusCursosViewModel BuscaDadosDoCurso(string id_curso, string id_usuario)
        {
            var sql = " select " +
                     "    cursos.id_curso, " +
                     "    cursos.titulo_curso, " +
                     "    Categorias.id_categoria, " +
                     "    Categorias.descricao, " +
                     "    Inscricoes.id_inscricao, " +
                     "    Inscricoes.data, " +
                     "    Inscricoes.finalizado, " +
                     "    Inscricoes.data_resultado, " +
                     "    (convert(varchar(5), (count(Andamentos.id_video) * 100) / count(Videos.id_video)) + '%') as andamento, " +
                     "    (select sum(resultados.nota) from resultados where id_inscricao =  Inscricoes.id_inscricao) as nota_final " +
                     " from " +
                     "     Inscricoes " +
                     "     inner join Cursos on Cursos.id_curso = Inscricoes.id_curso " +
                     "     inner join Categorias on Categorias.id_categoria = Cursos.id_categoria " +
                     "     inner join Unidades on Unidades.id_curso = Cursos.id_curso " +
                     "     left join Videos on Videos.id_unidade = Unidades.id_unidade " +
                     "     Left Join Andamentos on Andamentos.id_inscricao = Inscricoes.id_inscricao and Andamentos.id_video = Videos.id_video " +
                     " where " +
                     "     Inscricoes.id_usuario = " + id_usuario +
                     "     and Cursos.id_curso = " + id_curso +
                     "     and Inscricoes.ativo = 1 " +
                     "     and Cursos.ativo = 1 " +
                      "     and Unidades.ativo = 1 " +
                      "     and Videos.ativo = 1 " +
                     " group by " +
                     "     cursos.id_curso, " +
                     "     cursos.titulo_curso," +
                     "     Categorias.id_categoria," +
                     "     Categorias.descricao," +
                     "     Inscricoes.id_inscricao," +
                     "     Inscricoes.data," +
                     "     Inscricoes.finalizado," +
                     "     Inscricoes.nota_final," +
                     "     Inscricoes.data_resultado ";

            using (var conn = new SqlConnection(conexao))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {

                    MeusCursosViewModel p = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                p = new MeusCursosViewModel();
                                p.Id_inscricao = (int)reader["id_inscricao"];
                                p.Id_curso = (int)reader["id_curso"];
                                p.Titulo_curso = (string)reader["titulo_curso"];
                                p.Id_categoria = (int)reader["id_categoria"];
                                p.descricao_categoria = (string)reader["descricao"];
                                p.Dt_inscricao = (DateTime)reader["data"];
                                p.finalizado = (int)reader["finalizado"];
                                p.NotaFinal = (decimal)reader["nota_final"];
                                p.Andamento = (string)reader["andamento"];

                            }

                            reader.Close();
                            conn.Close();
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }


                    return p;
                }
            }
        }


        //Gravar Andamento
        public void InsereAndamento(int id_video, int id_inscricao)
        {
            var Andamento = _context.Andamentos.Where(p => p.Id_inscricao.Equals(id_inscricao) && p.Id_video.Equals(id_video)).ToList();
            Andamento novoAndamento = new Andamento();

            if (Andamento.Count == 0)
            {

                novoAndamento.Data = DateTime.Now;
                novoAndamento.Id_inscricao = id_inscricao;
                novoAndamento.Id_video = id_video;

                //Salvar
                _context.Andamentos.Add(novoAndamento);
                _context.SaveChanges();
            }



        }

        public List<Questao> BuscaQuestoesAtividade(int id_atividade, int id_resultado)
        {
            var sql = "select " +
                      "     top 1 " +
                      "     Questoes.id_questao, " +
                      "     Questoes.enunciado,  " +
                      "     Questoes.ordem,      " +
                      "     Questoes.ativo,      " +
                      "     Questoes.id_atividade " +
                      "from " +
                      "     Questoes " +
                      "where " +
                      "     Questoes.id_atividade = " + id_atividade +
                      " and Questoes.ativo = 1 " +
                      " and Questoes.id_questao not in (select id_questao from Respostas where id_resultado = " + id_resultado + ") " +
                      "order by Questoes.ordem";

            using (var conn = new SqlConnection(conexao))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    List<Questao> dados = new List<Questao>();
                    Questao p = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                p = new Questao();
                                p.Id_atividade = (int)reader["id_atividade"];
                                p.Id_questao = (int)reader["id_questao"];
                                p.Enunciado = (string)reader["enunciado"];
                                p.Ordem = (int)reader["ordem"];
                                p.Ativo = (bool)reader["ativo"];
                                dados.Add(p);
                            }

                            reader.Close();
                            conn.Close();
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }
                    return dados;
                }

            }
        }

        public void FinalizaResultado(int id_resultado)
        {
            //Calcula a nota do aluno
            var sql = "select " +
                      "     tabela.id_resultado, " +
                      "     convert(decimal(10,2),sum(tabela.correta * valor))as total " +
                      "from( " +
                      "       select " +
                      "         Respostas.id_resultado, " +
                      "         Respostas.id_questao, " +
                      "         Respostas.id_opcao, " +
                      "         Opcoes.correta, " +
                      "         (select 100 / count(1) from Respostas as resp where resp.id_resultado = id_resultado) as valor " +
                      "       from " +
                      "          Respostas " +
                      "          inner join Opcoes on Opcoes.id_opcao = Respostas.id_opcao " +
                      "       where " +
                      "          id_resultado = " + id_resultado +
                      "     ) as tabela " +
                      "group by tabela.id_resultado ";

            using (var conn = new SqlConnection(conexao))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {

                    decimal? nota = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                nota = new decimal();
                                nota = (decimal)reader["total"];

                            }

                            reader.Close();
                            conn.Close();
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }

                    if (nota != null)
                    {
                        //grava o resultado
                        Resultado resultadoBanco = _context.Resultados.Find(id_resultado);
                        if (resultadoBanco != null)
                        {
                            //Alterar
                            resultadoBanco.finalizado = true;
                            resultadoBanco.Data = DateTime.Now;
                            resultadoBanco.nota = nota;

                            _context.SaveChanges();
                        }

                    }
                }
            }
        }

    }
}
