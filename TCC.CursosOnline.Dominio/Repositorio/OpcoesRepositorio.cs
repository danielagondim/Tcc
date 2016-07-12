using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class OpcoesRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();


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
