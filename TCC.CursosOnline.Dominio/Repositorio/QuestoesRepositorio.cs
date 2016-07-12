using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class QuestoesRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();


        public List<Questao> ListaQuestoesPorAtividade(int id_atividade)
        {
            var questoes = _context.Questoes.Where(p => p.Id_atividade.Equals(id_atividade)).ToList();

            return questoes;
                         
                
        }

        public List<Questao> ListaQuestoesPorId(int Id)
        {
            return _context.Questoes.Where(p => p.Id_questao.Equals(Id)).ToList();
        }

        public Questao RetornaQuestãoPorId(int id_questao)
        {
            return _context.Questoes.FirstOrDefault(p => p.Id_questao == id_questao);
        }

        //Salvar ou Alterar uma Questão
        public void Salvar(Questao Questao)
        {
            if (Questao.Id_questao == 0)
            {
                //Salvar
                _context.Questoes.Add(Questao);


            }
            else
            {
                Questao QuestaoBanco = _context.Questoes.Find(Questao.Id_questao);
                if (QuestaoBanco != null)
                {
                    //Alterar
                    QuestaoBanco.Ativo = Questao.Ativo;
                    QuestaoBanco.Enunciado = Questao.Enunciado;
                    QuestaoBanco.Id_atividade = Questao.Id_atividade;
                    QuestaoBanco.Ordem = Questao.Ordem;
                   
                }
            }

            _context.SaveChanges();

        }


    }
}
