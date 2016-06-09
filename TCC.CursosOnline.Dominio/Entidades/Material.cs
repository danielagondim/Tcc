using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Material
    {
        public int Id_materiais { get; set; }
        public int Id_curso { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Arquivo { get; set; }

        public virtual Curso Curso { get; set; }
    }
}
