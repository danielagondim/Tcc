using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Usuario
    {
        [Key]
        public int Id_usuario { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Digite o CPF.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Digite o Nome.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o Telefone.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Digite a senha.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public bool Administrador { get; set; }
    }
}
