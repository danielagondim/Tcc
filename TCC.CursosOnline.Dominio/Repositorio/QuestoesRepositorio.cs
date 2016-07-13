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
    public class QuestoesRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();
        string conexao = WebConfigurationManager.ConnectionStrings["EfDbContext"].ConnectionString;

        public List<Questao> ListaQuestoesPorAtividade(int id_atividade)
        {
            var questoes = _context.Questoes.Where(p => p.Id_atividade.Equals(id_atividade)).ToList();

            return questoes;
                         
                
        }

        public List<Questao> ListaQuestoesPorAtividadeAtivasOrdenadas(int id_atividade)
        {
            var sql = "SELECT QUESTOES.* " +
                       "  FROM QUESTOES " +
                       " WHERE QUESTOES.ID_ATIVIDADE = " + id_atividade +
                       "   AND QUESTOES.ATIVO = 1 " +
                       " ORDER BY QUESTOES.ORDEM ";

            using (var conn = new SqlConnection(conexao))
            {
                var cmd = new SqlCommand(sql, conn);
                List<Questao> dados = new List<Questao>();
                Questao p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            p = new Questao();
                            p.Id_questao = (int)reader["Id_questao"];
                            p.Ativo = (bool)reader["Ativo"];
                            p.Enunciado = (string)reader["Enunciado"].ToString();
                            p.Id_atividade = (int)reader["Id_atividade"];
                            p.Ordem = (int)reader["Ordem"];
                            dados.Add(p);
                        }
                    }
                }
                finally
                {
                    conn.Close();
                }
                return dados;
            }


        }

        public List<Questao> ListaQuestoesPorId(int Id)
        {
            return _context.Questoes.Where(p => p.Id_questao.Equals(Id)).ToList();
        }

        public Questao RetornaQuestãoPorId(int id_questao)
        {
            return _context.Questoes.FirstOrDefault(p => p.Id_questao == id_questao);
        }

        //Salvar ou Alterar uma Questão
        public void Salvar(Questao Questao)
        {
            if (Questao.Id_questao == 0)
            {
                //Salvar
                _context.Questoes.Add(Questao);


            }
            else
            {
                Questao QuestaoBanco = _context.Questoes.Find(Questao.Id_questao);
                if (QuestaoBanco != null)
                {
                    //Alterar
                    QuestaoBanco.Ativo = Questao.Ativo;
                    QuestaoBanco.Enunciado = Questao.Enunciado;
                    QuestaoBanco.Id_atividade = Questao.Id_atividade;
                    QuestaoBanco.Ordem = Questao.Ordem;
                   
                }
            }

            _context.SaveChanges();

        }


    }
}
