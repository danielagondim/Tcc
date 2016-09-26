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
    public class ForumRespostasRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();

        public List<Forum_resposta> ListaRespostasPorTopico(int id_topico)
        {
            var respostas = _context.Forum_respostas.Where(p => p.Id_topico.Equals(id_topico)).ToList();
            return respostas;
        }

        //Salvar uma resposta
        public void Salvar(Forum_resposta resposta)
        {
            if (resposta.Id_resposta == 0)
            {
                //Salvar
                _context.Forum_respostas.Add(resposta);


            }
            else
            {
                Forum_resposta RespostaBanco = _context.Forum_respostas.Find(resposta.Id_resposta);
                if (RespostaBanco != null)
                {
                    //Alterar
                    RespostaBanco.Descricao = resposta.Descricao;                    

                }
            }

            _context.SaveChanges();

        }


    }
}
