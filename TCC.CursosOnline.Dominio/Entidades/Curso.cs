﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Curso
    {
        [Key]
        public int Id_curso { get; set; }

        public bool Ativo { get; set; }

        [Display(Name ="Título do Curso")]
        [Required(ErrorMessage = "Digite o título do curso.")]
        public string Titulo_curso { get; set; }

        [Display(Name = "Categoria")]
        public int Id_categoria { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
