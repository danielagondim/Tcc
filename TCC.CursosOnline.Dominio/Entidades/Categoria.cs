using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.CursosOnline.Dominio.Entidades
{
    public class Categoria
    {
        [Key]
        public int Id_categoria { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Descricao { get; set; }
    }
}
