﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Forum_topico
    {
        public int Id_topico { get; set; }
        public int Id_usuario { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
    }
}
