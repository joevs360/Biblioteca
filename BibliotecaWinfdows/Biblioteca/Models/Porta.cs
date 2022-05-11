using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Porta
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Texto { get; set; }
    }
}
