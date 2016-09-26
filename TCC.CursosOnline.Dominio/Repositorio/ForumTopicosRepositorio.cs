using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class ForumTopicosRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();

        public List<Forum_topico> ListaTodosTopicos()
        {
            return _context.Forum_topicos.ToList();
        }

        //Salvar um topico
        public void Salvar(Forum_topico topico)
        {
            if (topico.Id_topico == 0)
            {
                //Salvar
                _context.Forum_topicos.Add(topico);


            }
            else
            {
                Forum_topico TopicoBanco = _context.Forum_topicos.Find(topico.Id_topico);
                if (TopicoBanco != null)
                {
                    //Alterar
                    TopicoBanco.Descricao = topico.Descricao;                    

                }
            }

            _context.SaveChanges();

        }


    }
}
