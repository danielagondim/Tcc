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

        [Required(ErrorMessage = "Digite o nome do vídeo.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite a URL do vídeo.")]
        public string Url { get; set; }

        [Display(Name = "Unidade")]
        public int Id_Unidade { get; set; }

        [Required(ErrorMessage = "Digite a ordem vídeo.")]
        public int Ordem { get; set; }

        public virtual Unidade Unidade { get; set; }
    }
}
