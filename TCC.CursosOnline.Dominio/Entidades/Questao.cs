using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Questao
    {
        public int Id_questao { get; set; }
        public bool Ativo { get; set; }
        public string Enunciado { get; set; }
        public int Id_atividade { get; set; }
        public int Ordem { get; set; }

        public virtual Atividade Atividade { get; set; }
    }
}
