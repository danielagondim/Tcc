using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using TCC.CursosOnline.Dominio.Entidades;

namespace TCC.CursosOnline.Dominio.Repositorio
{
    public class UsuariosRepositorio
    {
        private readonly EfDbContext _context = new EfDbContext();

        public List<Usuario> ListaTodosUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario ListaUsuarioPorId(Int32 Id){

            return _context.Usuarios.FirstOrDefault(p => p.Id_usuario == Id);
        }

        public Usuario AutenticaUsuario(int id_usuario, string senha)
        {
            return _context.Usuarios.FirstOrDefault(p => p.Id_usuario == id_usuario && p.Senha == senha);
        }

        public Usuario GetUsuarioLogado()
        {
            string _Login = HttpContext.Current.User.Identity.Name;

            if (_Login == "")
            {
                return null;
            }
            else
            {
                return _context.Usuarios.FirstOrDefault(o => o.Id_usuario.ToString() == _Login);
                
            }
        }

        public void Deslogar()
        {
            FormsAuthentication.SignOut();
        }

        //Salvar ou Alterar um Usuário
        public void Salvar(Usuario Usuario)
        {
            if (Usuario.Id_usuario == 0)
            {
                //Salvar
                _context.Usuarios.Add(Usuario);
               

            }
            else
            {
                Usuario UsuarioBanco = _context.Usuarios.Find(Usuario.Id_usuario);
                if (UsuarioBanco != null)
                {
                    //Alterar
                    UsuarioBanco.Ativo = Usuario.Ativo;
                    UsuarioBanco.Administrador = Usuario.Administrador;                  
                    UsuarioBanco.CPF = Usuario.CPF;
                    UsuarioBanco.Nome = Usuario.Nome;
                    UsuarioBanco.Senha = Usuario.Senha;
                    UsuarioBanco.Telefone = Usuario.Telefone;
                    
                }
            }

            _context.SaveChanges();

        }

        //Desativar
        public Usuario Desativar(int Id)
        {
            Usuario UsuarioBanco = _context.Usuarios.Find(Id);

            if (UsuarioBanco != null)
            {
                UsuarioBanco.Ativo = false;
                _context.SaveChanges();

            }

            return UsuarioBanco;
        }
    }
}
