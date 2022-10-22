using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Livro
    {
        [PrimaryKey, AutoIncrement]
        public string Key { get; set; }
        public string Nome { get; set; }
        public string AutorKey { get; set; }
        public string ISBN { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public int QuantidadeTotal { get; set; } // A quantidade disponivel vai ser a subtração desse valor pela quandidade de livro emprestada

        
    }
}
