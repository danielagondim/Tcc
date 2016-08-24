using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class UnidadesRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();


        public List<Unidade> ListaUnidadesPorCurso(int id_curso)
        {
            var unidades = _context.Unidades.Where(p => p.Id_curso.Equals(id_curso)).ToList();
            return unidades;    
        }

        public List<Unidade> ListaUnidadesAtivasPorCurso(int id_curso)
        {
            var unidades = _context.Unidades.Where(p => p.Id_curso.Equals(id_curso) && p.Ativo.Equals(true)).ToList();
            return unidades;
        }

        public List<Unidade> ListaUnidadePorId(int Id)
        {
            return _context.Unidades.Where(p => p.Id_unidade.Equals(Id)).ToList();
        }

        public Unidade RetornaUnidadePorId(int id_unidade)
        {
            return _context.Unidades.FirstOrDefault(p => p.Id_unidade == id_unidade);
        }

        //Salvar ou Alterar uma Unidade
        public void Salvar(Unidade Unidade)
        {
            if (Unidade.Id_unidade == 0)
            {
                //Salvar
                _context.Unidades.Add(Unidade);


            }
            else
            {
                Unidade UnidadeBanco = _context.Unidades.Find(Unidade.Id_unidade);
                if (UnidadeBanco != null)
                {
                    //Alterar
                    UnidadeBanco.Ativo = Unidade.Ativo;
                    UnidadeBanco.Id_curso = Unidade.Id_curso;
                    UnidadeBanco.Nome = Unidade.Nome;
                    UnidadeBanco.Ordem = Unidade.Ordem;
                }
            }

            _context.SaveChanges();

        }


    }
}
