using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class EfDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set;  }

    }
}
