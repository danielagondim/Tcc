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
    public class VideosRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();
        string conexao = WebConfigurationManager.ConnectionStrings["EfDbContext"].ConnectionString;

        public List<Video> ListaVideosPorUnidade(int id_unidade)
        {
            var videos = _context.Videos.Where(p => p.Id_Unidade.Equals(id_unidade)).ToList();

            return videos;


        }

        public List<Video> ListaVideosPorCurso(int id_curso)
        {
            var sql = " select Videos.* " +
                      " from Cursos " +
                      " inner join Unidades on Unidades.id_curso = Cursos.id_curso " +
                      " inner join Videos on Videos.id_unidade = Unidades.id_unidade " +
                      " where " +
                      " (1 = 1) " +
                      " and Cursos.ativo = 1 " +
                      " and Cursos.id_curso = " + id_curso.ToString() +
                      " and Unidades.ativo = 1 " +
                      " and Videos.ativo = 1 " +
                      " order by " +
                      " Videos.ordem ";

            using (var conn = new SqlConnection(conexao))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    List<Video> dados = new List<Video>();
                    Video p = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                p = new Video();
                                p.Id_Unidade = (int)reader["id_unidade"];
                                p.Id_video = (int)reader["id_video"];
                                p.Nome = (string)reader["nome"];
                                p.Ordem = (int)reader["ordem"];
                                p.Ativo = (bool)reader["ativo"];
                                p.Url = (String)reader["url"];
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
    
        
        public Video RetornaVideoPorId(int id_video)
        {
            return _context.Videos.FirstOrDefault(p => p.Id_video == id_video);
        }

        //Salvar ou Alterar um Vídeo
        public void Salvar(Video Video)
        {
            if (Video.Id_video == 0)
            {
                //Salvar
                _context.Videos.Add(Video);


            }
            else
            {
                Video VideoBanco = _context.Videos.Find(Video.Id_video);
                if (VideoBanco != null)
                {
                    //Alterar
                    VideoBanco.Ativo = Video.Ativo;
                    VideoBanco.Id_video = Video.Id_video;
                    VideoBanco.Nome = Video.Nome;
                    VideoBanco.Ordem = Video.Ordem;
                    VideoBanco.Url = Video.Url;
                    VideoBanco.Id_Unidade = Video.Id_Unidade;
                }
            }

            _context.SaveChanges();

        }


    }
}
