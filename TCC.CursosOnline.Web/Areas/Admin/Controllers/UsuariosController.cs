﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Admin/Usuarios
        public ActionResult Index()
        {
            return View();
        }
    }
}