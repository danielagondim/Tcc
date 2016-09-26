using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
   
    public class Forum_resposta
    {
        [Key]
        public int Id_resposta { get; set; }
        public int Id_topico { get; set; }
        public int Id_usuario { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }

        public virtual Forum_topico Forum_topico { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
