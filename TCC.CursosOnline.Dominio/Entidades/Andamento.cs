using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
   
    public class Andamento
    {
        public int Id_andamento { get; set; }
        public int Id_inscricao { get; set; }
        public int Id_video { get; set; }
        public DateTime Data { get; set; }

        public virtual Video Video { get; set; }
        public virtual Inscricao Inscricao { get; set; }
    }
}
