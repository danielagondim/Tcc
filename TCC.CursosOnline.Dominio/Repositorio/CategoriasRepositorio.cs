using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class CategoriasRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();

        public List<Categoria> ListaTodasCategorias()
        {
            return _context.Categorias.ToList();
        }
    }
}
