using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class AtividadesRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();


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


    }
}
