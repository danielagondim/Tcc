﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Inscricao
    {
        [Key]
        public int Id_inscricao { get; set; }
        public bool Ativo { get; set; }
        public int Id_usuario { get; set; }
        public int Id_curso { get; set; }
        public DateTime Data { get; set; }
        public DateTime? Data_resultado { get; set; }
        public int Finalizado { get; set; }
        public decimal Nota_final { get; set; }
        public bool Certificado { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Curso Curso { get; set; }


    }
}
