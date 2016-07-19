using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TCC.CursosOnline.Dominio;
using TCC.CursosOnline.Web.Security;

namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    [PermissoesFiltro(Roles = "Administrador")]
    public class HomeController : Controller
    {
        
       
        public ActionResult Index()
        {
            return View();
        }
    }
}