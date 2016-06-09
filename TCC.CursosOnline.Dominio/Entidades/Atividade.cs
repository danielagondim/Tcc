﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Atividade
    {
        public int Id_atividade { get; set; }
        public bool Ativo { get; set; }
        public string Titulo { get; set; }
        public int Id_unidade { get; set; }
        public int Ordem { get; set; }

        public virtual Unidade Unidade { get; set; }
    }
}
