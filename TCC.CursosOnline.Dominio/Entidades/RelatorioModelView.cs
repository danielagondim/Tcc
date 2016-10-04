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
    public class RelatorioViewModel
    {
        public int Id { get; set; }

        //Cabeçalho
        
        public string Usuario { get; set; }
       


        //Campos do relatório
        public string NomeCurso { get; set; }
        
        public string Andamento { get; set; }

        public int NotaFinal { get; set; }

        public int Finalizado { get; set; }


    }
}
