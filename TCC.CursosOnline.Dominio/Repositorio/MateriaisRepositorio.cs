using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class MateriaisRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();


        public List<Material> ListaMateriaisPorCurso(int id_curso)
        {
            var materiais = _context.Materiais.Where(p => p.Id_curso.Equals(id_curso)).ToList();

            return materiais;


        }

        public Material RetornaMaterialPorId(int id_material)
        {
            return _context.Materiais.FirstOrDefault(p => p.Id_materiais == id_material);
        }

        //Salvar ou Alterar um Material Complementar
        public void Salvar(Material Material)
        {
            if (Material.Id_materiais == 0)
            {
                _context.Materiais.Add(Material);

            }
            else
            {
                Material MaterialBanco = _context.Materiais.Find(Material.Id_materiais);
                if (MaterialBanco != null)
                {
                    //Alterar
                    MaterialBanco.Ativo = Material.Ativo;
                    MaterialBanco.Id_curso = Material.Id_curso;
                    MaterialBanco.Nome = Material.Nome;
                    MaterialBanco.Arquivo = Material.Arquivo;
                }
            }

            _context.SaveChanges();

        }

        public string Upload(HttpPostedFileBase arquivofile)
        {
            try
            {
                if (arquivofile != null && arquivofile.ContentLength > 0)
                {
                    var nomeArquivo = AlteraNomeArquivo(Path.GetFileName(arquivofile.FileName));
                    var caminho = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content\\Arquivos");
                    caminho = System.IO.Path.Combine(caminho, nomeArquivo);
                    arquivofile.SaveAs(caminho);
                    var link = ConfigurationManager.AppSettings["Diretorioarquivos"].ToString() + nomeArquivo;

                    return link;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string AlteraNomeArquivo(string nome)
        {
            var novo = nome.ToLower().Replace(" ", "-");
            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(novo);
            novo = System.Text.Encoding.UTF8.GetString(bytes);
            novo = System.Guid.NewGuid().ToString() + "-" + novo;
            return novo;
        }
    }
}
