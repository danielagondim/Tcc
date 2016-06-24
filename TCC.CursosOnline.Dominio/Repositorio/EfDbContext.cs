using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.CursosOnline.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class EfDbContext : DbContext
    {
        public DbSet<Andamento> Andamentos { get; set; }
        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Forum_resposta> Forum_respostas { get; set; }
        public DbSet<Forum_topico> Forum_topicos { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }
        public DbSet<Material> Materiais { get; set; }
        public DbSet<Opcao> Opcoes { get; set; }
        public DbSet<Questao> Questoes { get; set; }
        public DbSet<Resposta> Respostas { get; set; }
        public DbSet<Resultado> Resultados { get; set; }
        public DbSet<Unidade> Unidades { get; set; }        
        public DbSet<Usuario> Usuarios { get; set;  }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Andamento>().ToTable("Andamentos");
            modelBuilder.Entity<Atividade>().ToTable("Atividades");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<Curso>().ToTable("Cursos");
            modelBuilder.Entity<Forum_resposta>().ToTable("Forum_Respostas");
            modelBuilder.Entity<Forum_topico>().ToTable("Forum_Topicos");
            modelBuilder.Entity<Inscricao>().ToTable("Incricoes");
            modelBuilder.Entity<Material>().ToTable("Materiais");
            modelBuilder.Entity<Opcao>().ToTable("Opcoes");
            modelBuilder.Entity<Questao>().ToTable("Questoes");
            modelBuilder.Entity<Resposta>().ToTable("Respostas");
            modelBuilder.Entity<Resultado>().ToTable("Resultados");
            modelBuilder.Entity<Unidade>().ToTable("Unidades");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Video>().ToTable("Videos");

        }

    }
}
