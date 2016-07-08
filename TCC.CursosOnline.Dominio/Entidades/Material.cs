using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Material
    {
        [Key]
        public int Id_materiais { get; set; }

        [Display(Name = "Curso")]
        public int Id_curso { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Digite o Nome do Material Complementar.")]
        public string Nome { get; set; }

        public string Arquivo { get; set; }
       

        public virtual Curso Curso { get; set; }
    }
}
