using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCC.CursosOnline.Web.Controllers
{
    [Authorize]
    public class AlunoController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Negado()
        {
            return View();
        }
    }
}