using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class ResultadosRespositorio
    {
        private EfDbContext _context;

        string conexao = WebConfigurationManager.ConnectionStrings["EfDbContext"].ConnectionString;

        public List<Resultado> ListaResultados(int id_inscricao, int id_atividade)
        {
            EfDbContext _context = new EfDbContext();
            return _context.Resultados.Where(p => p.Id_inscricao.Equals(id_inscricao) && p.Id_atividade.Equals(id_atividade)).ToList();

        }

        public int RetornaResultado(int id_inscricao, int id_atividade)
        {
            EfDbContext _context = new EfDbContext();
            return _context.Resultados.FirstOrDefault(p => p.Id_atividade.Equals(id_atividade) && p.Id_inscricao.Equals(id_inscricao)).Id_resultado;
        }

        //Salvar o resultado
        public void Salvar(Resultado result)
        {
            if (result.Id_resultado == 0)
            {
               
                _context = new EfDbContext();
                //Salvar
                _context.Resultados.Add(result);
                _context.SaveChanges();

            }
        }
    }
}
