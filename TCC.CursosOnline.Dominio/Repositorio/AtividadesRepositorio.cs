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
    public class AtividadesRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();
        string conexao = WebConfigurationManager.ConnectionStrings["EfDbContext"].ConnectionString;

        public List<Atividade> ListaAtividadesPorUnidade(int id_unidade)
        {
            var atividades = _context.Atividades.Where(p => p.Id_unidade.Equals(id_unidade)).ToList();

            return atividades;
                         
                
        }

        public List<Atividade> ListaAtividadesPorId(int Id)
        {
            return _context.Atividades.Where(p => p.Id_atividade.Equals(Id)).ToList();
        }

        public Atividade RetornaAtividadesPorId(int id_atividade)
        {
            return _context.Atividades.FirstOrDefault(p => p.Id_atividade == id_atividade);
        }

        //Salvar ou Alterar uma Atividade
        public void Salvar(Atividade Atividade)
        {
            if (Atividade.Id_atividade == 0)
            {
                //Salvar
                _context.Atividades.Add(Atividade);


            }
            else
            {
                Atividade AtividadeBanco = _context.Atividades.Find(Atividade.Id_atividade);
                if (AtividadeBanco != null)
                {
                    //Alterar
                    AtividadeBanco.Ativo = Atividade.Ativo;
                    AtividadeBanco.Id_unidade = Atividade.Id_unidade;
                    AtividadeBanco.Titulo = Atividade.Titulo;
                    AtividadeBanco.Ordem = Atividade.Ordem;
                   
                }
            }

            _context.SaveChanges();

        }

        public List<Atividade> ListaAtividadesPorCurso(int id_curso)
        {
            var sql = " select Atividades.* " +
                      " from Cursos " +
                      " inner join Unidades on Unidades.id_curso = Cursos.id_curso " +
                      " inner join Atividades on Atividades.id_unidade = Unidades.id_unidade " +
                      " where " +
                      " (1 = 1) " +
                      " and Cursos.ativo = 1 " +
                      " and Cursos.id_curso = " + id_curso.ToString() +
                      " and Unidades.ativo = 1 " +
                      " and Atividades.ativo = 1 " +
                      " order by " +
                      " Atividades.ordem ";

            using (var conn = new SqlConnection(conexao))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    List<Atividade> dados = new List<Atividade>();
                    Atividade p = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                p = new Atividade();
                                p.Id_unidade = (int)reader["id_unidade"];
                                p.Id_atividade = (int)reader["id_atividade"];
                                p.Titulo = (string)reader["titulo"];
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


    }
}
