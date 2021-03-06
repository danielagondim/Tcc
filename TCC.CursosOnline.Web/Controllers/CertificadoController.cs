﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Entidades;
using TCC.CursosOnline.Dominio.Repositorio;
using RazorPDF;

namespace TCC.CursosOnline.Web.Controllers
{
    [Authorize]
    public class CertificadoController : Controller
    {
        private MeusCursosRepositorio _repositorio;
     

        // GET: Listar os cursos que o aluno foi aprovado
        public ActionResult Index()
        {
            _repositorio = new MeusCursosRepositorio();
           
            IPrincipal principal = HttpContext.User;

            var listaCursosAprovados = _repositorio.ListaMeusCursosAprovados(principal.Identity.Name.ToString());

            return View(listaCursosAprovados);
        }


        public ActionResult Certificado(int id_curso)
        {
            _repositorio = new MeusCursosRepositorio();
           

            IPrincipal principal = HttpContext.User;

            var dadosCurso = new MeusCursosViewModel();

            dadosCurso = _repositorio.BuscaDadosDoCurso(id_curso.ToString(), principal.Identity.Name.ToString());

            var pdfResult = new PdfResult(dadosCurso, "PDF");

            pdfResult.ViewBag.title = "teste";
          

            return pdfResult;


        }
    }
}