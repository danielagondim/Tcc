using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TCC.CursosOnline.Dominio.Entidades;
using TCC.CursosOnline.Dominio.Repositorio;

namespace TCC.CursosOnline.Web.Controllers
{
    public class AutenticacaoController : Controller
    {
       private  UsuariosRepositorio _repositorio;
      
        public ActionResult Login(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View(new Usuario());


        }
        [HttpPost]
        public ActionResult Login(Usuario usuario, string returnUrl)
        {
            _repositorio = new UsuariosRepositorio();

            if (usuario.Id_usuario.ToString() != null && usuario.Senha != null )
            {
                Usuario usu = _repositorio.AutenticaUsuario(usuario.Id_usuario, usuario.Senha);

                if (usu != null)
                {

                    FormsAuthentication.SetAuthCookie(usu.Id_usuario.ToString(), false);

                    if (returnUrl == null)
                    {
                        if (usu.Administrador == true)
                        {
                            return Redirect("Admin/Home");
                        }
                        else
                        {
                            return Redirect("Home");
                        }
                    }
                    else
                    {

                        return Redirect(returnUrl);
                    }
                    

                }
                else
                {
                    ModelState.AddModelError("", "Usuário não Cadastrado!");

                }
  
            }
            return View();
        }
    }
}