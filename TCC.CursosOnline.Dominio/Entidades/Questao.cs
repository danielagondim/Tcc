using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Questao
    {
        [Key]
        public int Id_questao { get; set; }
        public bool Ativo { get; set; }

     
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Enunciado { get; set; }

        [Display(Name = "Atividade")]
        public int Id_atividade { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        [Range(1, 99999)]
        public int Ordem { get; set; }


        public virtual Atividade Atividade { get; set; }
    }
}
