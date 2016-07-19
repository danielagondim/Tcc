using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    class InscricoesModelView
    {
        public int Id { get; set; }

        public List<Curso> ListaCursos { get; set; }
    }
}
