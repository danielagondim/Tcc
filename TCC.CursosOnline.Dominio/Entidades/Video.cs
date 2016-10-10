using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Video
    {
        [Key]
        public int Id_video { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Url { get; set; }

        [Display(Name = "Unidade")]
        public int Id_Unidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(1, 99999)]
        public int Ordem { get; set; }

        public virtual Unidade Unidade { get; set; }
    }
}
