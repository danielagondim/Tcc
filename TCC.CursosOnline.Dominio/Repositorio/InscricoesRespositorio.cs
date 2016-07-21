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
    public class InscricoesRespositorio
    {
        private EfDbContext _context;

        string conexao = WebConfigurationManager.ConnectionStrings["EfDbContext"].ConnectionString;

        public List<Curso> ListaCursosDisponiveis(string id_aluno)
        {
            var sql = "SELECT CURSOS.* " +
                      "  FROM CURSOS " +
                      " WHERE CURSOS.ATIVO = 1 " +
                      "   AND CURSOS.ID_CURSO NOT IN(SELECT INSCRICOES.ID_CURSO FROM INSCRICOES WHERE ID_USUARIO =  " + id_aluno +
                      "                             AND INSCRICOES.ATIVO = 1 )" +
                      " ORDER BY CURSOS.TITULO_CURSO ";

            using (var conn = new SqlConnection(conexao))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    List<Curso> dados = new List<Curso>();
                    Curso p = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                p = new Curso();
                                p.Ativo = (bool)reader["Ativo"];
                                p.Id_curso = (int)reader["Id_curso"];
                                p.Id_categoria = (int)reader["Id_categoria"];
                                p.Titulo_curso = (string)reader["titulo_curso"];
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

        //Salvar a inscrição no curso
        public void InscreverNoCurso(Inscricao insc)
        {
            if (insc.Id_inscricao == 0)
            {
                insc.Data_resultado = (DateTime?)null;

                _context = new EfDbContext();
                //Salvar
                _context.Inscricoes.Add(insc);
                _context.SaveChanges();

            }
        }
    }
}
