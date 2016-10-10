using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class MaterialModelView
    {
      
        public int Id { get; set; }

        [Display(Name = "Curso")]
        public int Id_curso { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        public string Arquivo { get; set; }

        public HttpPostedFileBase ArquivoFile { get; set; }

        public virtual Curso Curso { get; set; }
    }
}
