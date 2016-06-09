using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    class Usuario
    {
        public int Id_usuario { get; set; }
        public bool Ativo { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public bool Administrador { get; set; }
    }
}
