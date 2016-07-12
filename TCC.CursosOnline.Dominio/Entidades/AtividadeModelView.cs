using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class AtividadeViewModel
    {
        public int Id { get; set; }

        //Atividade
        
        public string Nome_atividade { get; set; }

       
        //Listas
        public List<Questao> ListaQuestoes { get; set; }
        public IEnumerable ListaOpcoes { get; set; }



    }
}
