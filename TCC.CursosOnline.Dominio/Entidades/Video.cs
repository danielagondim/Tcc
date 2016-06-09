using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Video
    {
        public int Id_video { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Url { get; set; }
        public int Id_Unidade { get; set; }
        public int Ordem { get; set; }

        public virtual Unidade Unidade { get; set; }
    }
}
