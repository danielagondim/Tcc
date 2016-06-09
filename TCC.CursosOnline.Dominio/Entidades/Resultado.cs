using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Resultado
    {
        public int Id_resultado { get; set; }
        public int Id_inscricao { get; set; }
        public int Id_atividade { get; set; }
        public DateTime Data { get; set; }

        public virtual Inscricao Incricao { get; set; }
        public virtual Atividade Atividade { get; set; }
    }
}
