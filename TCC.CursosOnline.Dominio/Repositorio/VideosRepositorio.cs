using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class VideosRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();


        public List<Video> ListaVideosPorUnidade(int id_unidade)
        {
            var videos = _context.Videos.Where(p => p.Id_Unidade.Equals(id_unidade)).ToList();

            return videos;
                         
                
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
