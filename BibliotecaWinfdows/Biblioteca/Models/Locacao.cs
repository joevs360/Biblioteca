using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Locacao
    {
        public int LivroID { get; set; }
        public int UsuarioID { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime dataDevolucao { get; set; }
    }
}
