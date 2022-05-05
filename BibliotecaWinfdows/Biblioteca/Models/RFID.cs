using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class RFID
    {
        [PrimaryKey]
        public string ID { get; set; }
        public int IdUsuario { get; set; }
        public DateTime dataCadastro { get; set; }
    }
    
}
