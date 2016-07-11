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

        public List<Categoria> ListaTodasCategoriasAtivas()
        {
            return _context.Categorias.Where(p => p.Ativo.Equals(true)).ToList();
        }


        public List<Categoria> ListaCategoriaPorId(int Id)
        {
            return _context.Categorias.Where(p => p.Id_categoria.Equals(Id)).ToList();
        }

        public Categoria RetornaCategoriaPorId(Int32 Id)
        {

            return _context.Categorias.FirstOrDefault(p => p.Id_categoria == Id);
        }

        //Salvar ou Alterar a Categoria
        public void Salvar(Categoria Categoria)
        {
            if (Categoria.Id_categoria == 0)
            {
                //Salvar
                _context.Categorias.Add(Categoria);


            }
            else
            {
                Categoria CategoriaBanco = _context.Categorias.Find(Categoria.Id_categoria);
                if (CategoriaBanco != null)
                {
                    //Alterar
                    CategoriaBanco.Ativo = Categoria.Ativo;
                    CategoriaBanco.Id_categoria = Categoria.Id_categoria;
                    CategoriaBanco.Descricao = Categoria.Descricao;

                }
            }

            _context.SaveChanges();

        }

        
    }
}
