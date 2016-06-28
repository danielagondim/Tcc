using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class CursosRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();

        public List<Curso> ListaTodosCursos()
        {
            return _context.Cursos.ToList();
        }

        public Curso ListaCursoPorId(Int32 Id)
        {

            return _context.Cursos.FirstOrDefault(p => p.Id_curso == Id);
        }

        //Salvar ou Alterar um Curso
        public void Salvar(Curso Curso)
        {
            if (Curso.Id_curso == 0)
            {
                //Salvar
                _context.Cursos.Add(Curso);


            }
            else
            {
                Curso CursoBanco = _context.Cursos.Find(Curso.Id_curso);
                if (CursoBanco != null)
                {
                    //Alterar
                    CursoBanco.Ativo = Curso.Ativo;
                    CursoBanco.Id_categoria = Curso.Id_categoria;
                    CursoBanco.Titulo_curso = Curso.Titulo_curso;

                }
            }

            _context.SaveChanges();

        }

        //Desativar
        public Curso Desativar(int Id)
        {
            Curso CursoBanco = _context.Cursos.Find(Id);

            if (CursoBanco != null)
            {
                CursoBanco.Ativo = false;
                _context.SaveChanges();

            }

            return CursoBanco;
        }
    }
}
