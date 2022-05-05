using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Autor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome{ get; set; }
    }
}
