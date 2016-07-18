using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TCC.CursosOnline.Dominio;

namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [PermissoesFiltro(Role = "Administrador")]
        public ActionResult Index()
        {
            return View();
        }
    }
}