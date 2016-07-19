using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC.CursosOnline.Dominio.Repositorio;
using TCC.CursosOnline.Dominio.Entidades;
using TCC.CursosOnline.Web.Security;

namespace TCC.CursosOnline.Web.Areas.Admin.Controllers
{
    [PermissoesFiltro(Roles = "Administrador")]
    public class MateriaisController : Controller
    {
        private MateriaisRepositorio _repositorio;
        private CursosRepositorio _repositorio_curso;


        public ActionResult Index(int id_curso)
        {
            _repositorio = new MateriaisRepositorio();
            _repositorio_curso = new CursosRepositorio();

            var materiais = _repositorio.ListaMateriaisPorCurso(id_curso);
            var nome_curso = _repositorio_curso.RetornaCursoPorId(id_curso).Titulo_curso.ToString();

            ViewData["Id_curso"] = id_curso;
            ViewData["Nome_curso"] = nome_curso;

            return View(materiais);

        }

        public ActionResult CadastroMaterial(int id_curso)
        {
            _repositorio_curso = new CursosRepositorio();
            var cursos = _repositorio_curso.ListaCursoPorId(id_curso);
            ViewData["listaCurso"] = new SelectList(cursos, "Id_curso", "Titulo_curso");
            ViewData["Id_curso"] = id_curso;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroMaterial(MaterialModelView Material)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new MateriaisRepositorio();
                Material mat = new Material();
                mat.Ativo = Material.Ativo;
                mat.Id_curso = Material.Id_curso;
                mat.Arquivo = _repositorio.Upload(Material.ArquivoFile);
                mat.Nome = Material.Nome;
                if (mat.Arquivo != null)
                {
                    _repositorio.Salvar(mat);

                    TempData["mensagem"] = "Material cadastrado com sucesso!";

                    return RedirectToAction("Index", new { id_curso = Material.Id_curso });
                }

            }
            _repositorio_curso = new CursosRepositorio();
            var cursos = _repositorio_curso.ListaCursoPorId(Material.Id_curso);
            ViewData["listaCurso"] = new SelectList(cursos, "Id_curso", "Titulo_curso");
            ViewData["Id_curso"] = Material.Id_curso;
            return View(Material);
        }

        public ViewResult EditarMaterial(Int32 id)
        {
            _repositorio = new MateriaisRepositorio();
            Material material = _repositorio.RetornaMaterialPorId(id);
            MaterialModelView mat = new MaterialModelView();

            mat.Id = material.Id_materiais;
            mat.Ativo = material.Ativo;
            mat.Id_curso = material.Id_curso;
            mat.Nome = material.Nome;
            mat.Arquivo = material.Arquivo;

            _repositorio_curso = new CursosRepositorio();
            var cursos = _repositorio_curso.ListaCursoPorId(material.Id_curso);
            ViewData["listaCurso"] = new SelectList(cursos, "Id_curso", "Titulo_curso");
            ViewData["Id_curso"] = material.Id_curso;

            return View(mat);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarMaterial(MaterialModelView Material)
        {
            if (ModelState.IsValid)
            {
                _repositorio = new MateriaisRepositorio();
                Material mat = new Material();
                mat.Id_materiais = Material.Id;
                mat.Ativo = Material.Ativo;
                mat.Id_curso = Material.Id_curso;
                mat.Nome = Material.Nome;
                if (Material.ArquivoFile != null && Material.ArquivoFile.ContentLength > 0)
                {
                    mat.Arquivo = _repositorio.Upload(Material.ArquivoFile);

                }
                else
                {
                    mat.Arquivo = Material.Arquivo;
                }

                if (mat.Arquivo != null)
                {

                    _repositorio.Salvar(mat);

                    TempData["mensagem"] = "MAterial alterado com sucesso!";

                    return RedirectToAction("Index", new { id_curso = Material.Id_curso });
                }
            }

            _repositorio_curso = new CursosRepositorio();
            var cursos = _repositorio_curso.ListaCursoPorId(Material.Id_curso);
            ViewData["listaCurso"] = new SelectList(cursos, "Id_curso", "Titulo_curso");
            ViewData["Id_curso"] = Material.Id_curso;

            return View(Material);
        }
    }
}