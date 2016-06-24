using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Resposta
    {
        [Key]
        public int Id_resposta { get; set; }
        public int Id_resultado { get; set; }
        public int Id_questao { get; set; }
        public int Id_opcao { get; set; }
        public DateTime Data { get; set; }

        public virtual Resultado Resultado { get; set; }
        public virtual Questao Questao { get; set; }
        public virtual Opcao Opcao { get; set; }
    }
}
