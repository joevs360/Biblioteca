using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Registro
    {
        public float Temperatura { get; set; }
        public float Umidade { get;set; }
        public string Key { get; set; }
        public DateTime DataHora { get; set; }
    }
}
