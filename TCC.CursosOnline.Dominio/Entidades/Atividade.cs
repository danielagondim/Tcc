using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Atividade
    {
        [Key]
        public int Id_atividade { get; set; }

        public bool Ativo { get; set; }


        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Titulo { get; set; }

        [Display(Name = "Unidade")]
        public int Id_unidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [Range(1, 99999)]
        public int Ordem { get; set; }

        public virtual Unidade Unidade { get; set; }
    }
}
