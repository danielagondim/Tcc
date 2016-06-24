using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Curso
    {
        [Key]
        public int Id_curso { get; set; }
        public bool Ativo { get; set; }
        public string Titulo_curso { get; set; }
        public int Id_categoria { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
