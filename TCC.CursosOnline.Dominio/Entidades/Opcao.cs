using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Opcao
    {
        [Key]
        public int Id_opcao { get; set; }
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Digite  a descrição da Opção.")]
        public string Descricao { get; set; }

        [Display(Name = "Questão")]
        public int Id_questao { get; set; }

        [Required(ErrorMessage = "Informe a ordem.")]
        public int Ordem { get; set; }
        public bool Correta { get; set; }

        public virtual Questao Questao { get; set; }
    }
}
