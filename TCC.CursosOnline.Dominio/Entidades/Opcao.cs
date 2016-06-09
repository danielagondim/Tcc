using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Opcao
    {
        public int Id_opcao { get; set; }
        public bool Ativo { get; set; }
        public string Descricao { get; set; }
        public int Id_questao { get; set; }
        public int Ordem { get; set; }
        public bool Correta { get; set; }

        public virtual Questao Questao { get; set; }
    }
}
