using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class MeusCursosViewModel
    {
        [Key]
        public int Id_inscricao { get; set; }


        public int Id_curso { get; set; }

        public string Titulo_curso { get; set; }

        public int Id_categoria { get; set; }

        public string descricao_categoria { get; set; }

       

        public DateTime Dt_inscricao { get; set; }

        public DateTime? Dt_resultado { get; set; }

        public string Andamento { get; set; }

        public int NotaFinal { get; set; }

        public int finalizado { get; set; }


        public List<Unidade> ListaUnidades { get; set; }
        public List<Video> ListaVideos { get; set; }
        public List<Atividade> ListaAtividades { get; set; }
      


    }
}
