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
    public class OpcoesRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();
        string conexao = WebConfigurationManager.ConnectionStrings["EfDbContext"].ConnectionString;

       

        public List<Opcao> ListaOpcoesPorAtividade(int id_atividade)
        {
            var sql = "SELECT OPCOES.* " +
                      "  FROM OPCOES INNER JOIN QUESTOES ON QUESTOES.ID_QUESTAO = OPCOES.ID_QUESTAO " +
                      " WHERE QUESTOES.ID_ATIVIDADE = " + id_atividade +
                      "   AND QUESTOES.ATIVO = 1 " +
                      "   AND OPCOES.ATIVO = 1 " +
                      " ORDER BY OPCOES.ORDEM ";

            using (var conn = new SqlConnection(conexao))
            {
                var cmd = new SqlCommand(sql, conn);
                List<Opcao> dados = new List<Opcao>();
                Opcao p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            p = new Opcao();
                            p.Ativo = (bool)reader["Ativo"];
                            p.Id_opcao = (int)reader["Id_opcao"];
                            p.Id_questao = (int)reader["Id_Questao"];
                            p.Descricao = (string)reader["Descricao"];
                            p.Ordem = (int)reader["Ordem"];
                            p.Correta = (bool)reader["Correta"];
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

        public List<Opcao> ListaOpcoesPorQuestao(int id_questao)
        {
            var opcoes = _context.Opcoes.Where(p => p.Id_questao.Equals(id_questao)).ToList();

            return opcoes;


        }

        


        public Opcao RetornaOpcaoPorId(int id_opcao)
        {
            return _context.Opcoes.FirstOrDefault(p => p.Id_opcao == id_opcao);
        }

        //Salvar ou Alterar uma opcao
        public void Salvar(Opcao opcao)
        {
            if (opcao.Id_opcao == 0)
            {
                //Salvar
                _context.Opcoes.Add(opcao);


            }
            else
            {
                Opcao OpcaoBanco = _context.Opcoes.Find(opcao.Id_opcao);
                if (OpcaoBanco != null)
                {
                    //Alterar
                    OpcaoBanco.Ativo = opcao.Ativo;
                    OpcaoBanco.Correta = opcao.Correta;
                    OpcaoBanco.Descricao = opcao.Descricao;
                    OpcaoBanco.Id_questao = opcao.Id_questao;
                    OpcaoBanco.Ordem = opcao.Ordem;

                }
            }

            _context.SaveChanges();

        }


    }
}
