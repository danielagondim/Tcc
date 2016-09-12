using System;
using System.Collections;
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
    public class RespostasRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();
        string conexao = WebConfigurationManager.ConnectionStrings["EfDbContext"].ConnectionString;

      

        //Salvar ou Alterar uma resposta
        public void Salvar(Resposta resposta)
        {
            if (resposta.Id_resposta == 0)
            {
                //Salvar
                _context.Respostas.Add(resposta);


            }
            else
            {
                Resposta RespostaBanco = _context.Respostas.Find(resposta.Id_resposta);
                if (RespostaBanco != null)
                {
                    //Alterar (só pode altera a opção)
                    RespostaBanco.Id_opcao= resposta.Id_opcao;
                    RespostaBanco.Data = resposta.Data;

                }
            }

            _context.SaveChanges();

        }


    }
}
