using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Categoria
    {
        public int Id_categoria { get; set; }
        public bool Ativo { get; set; }
        public string Descricao { get; set; }
    }
}
