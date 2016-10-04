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
    public class RelatorioRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();
        string conexao = WebConfigurationManager.ConnectionStrings["EfDbContext"].ConnectionString;
    
        public List<RelatorioViewModel> RelatorioPorCurso(int? id_curso)
        {
            var sql ="";
            if (id_curso == 0) {
                sql = " select " +
                          "   Usuarios.id_usuario, " +
                          "   Usuarios.nome, " +
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
                         "     inner join Usuarios on Usuarios.id_usuario = Inscricoes.id_usuario " +
                         " where " +
                         "     Inscricoes.ativo = 1 " +
                         "     and Cursos.ativo = 1 " +
                         "     and Unidades.ativo = 1 " +
                         "     and Videos.ativo = 1 " +
                         " group by " +
                         "     Usuarios.id_usuario, " +
                         "     Usuarios.nome, " +
                         "     cursos.id_curso, " +
                         "     cursos.titulo_curso," +
                         "     Categorias.id_categoria," +
                         "     Categorias.descricao," +
                         "     Inscricoes.id_inscricao," +
                         "     Inscricoes.data," +
                         "     Inscricoes.finalizado," +
                         "     Inscricoes.nota_final," +
                         "     Inscricoes.data_resultado ";
            }
            else
            {
                sql = " select " +
                         "   Usuarios.id_usuario, " +
                         "   Usuarios.nome, " +
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
                        "     inner join Usuarios on Usuarios.id_usuario = Inscricoes.id_usuario " +
                        " where " +
                        "     Inscricoes.id_curso = " + id_curso +
                        "     and Inscricoes.ativo = 1 " +
                        "     and Cursos.ativo = 1 " +
                        "     and Unidades.ativo = 1 " +
                        "     and Videos.ativo = 1 " +
                        " group by " +
                        "     Usuarios.id_usuario, " +
                        "     Usuarios.nome, " +
                        "     cursos.id_curso, " +
                        "     cursos.titulo_curso," +
                        "     Categorias.id_categoria," +
                        "     Categorias.descricao," +
                        "     Inscricoes.id_inscricao," +
                        "     Inscricoes.data," +
                        "     Inscricoes.finalizado," +
                        "     Inscricoes.nota_final," +
                        "     Inscricoes.data_resultado ";

            }
            using (var conn = new SqlConnection(conexao))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    List<RelatorioViewModel> dados = new List<RelatorioViewModel>();
                    RelatorioViewModel p = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                p = new RelatorioViewModel();
                                p.Id = (int)reader["id_inscricao"];
                                p.NomeCurso = (string)reader["titulo_curso"];
                                p.Usuario = (string)reader["nome"];
                                p.Andamento = (string)reader["andamento"];
                                p.NotaFinal = (int)reader["nota_final"];
                                p.Finalizado = (int)reader["finalizado"];
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


    }
}
