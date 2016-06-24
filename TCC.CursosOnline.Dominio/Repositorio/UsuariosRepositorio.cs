using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class UsuariosRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();

        public IEnumerable<Usuario> Usuarios
        {
            get { return _context.Usuarios; }
        }

    }
}
