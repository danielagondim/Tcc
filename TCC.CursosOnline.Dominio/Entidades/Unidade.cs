using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Unidade
    {
        [Key]
        public int Id_unidade { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public int Id_curso { get; set; }
        public int Ordem { get; set; }

        public virtual Curso Curso { get; set; }
    }
}
