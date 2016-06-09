using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.CursosOnline.Dominio.Repositorio;
using System.ComponentModel.DataAnnotations;


namespace TCC.CursosOnline.Dominio.Entidades
{
   
    public class Andamento
    {
        private EfDbContext db = new EfDbContext();

        [Key]
        public int Id_andamento { get; set; }

        public int Id_inscricao { get; set; }
        public int Id_video { get; set; }
        public DateTime Data { get; set; }

        public virtual Video Video { get; set; }
        public virtual Inscricao Inscricao { get; set; }
    }
}
