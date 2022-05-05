using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Unique]
        public string RA { get; set; }
        public string Nome { get; set; }
        public string Email  { get; set; }
        public string Telefone { get; set; }
        public int CategoriaUsuarioID { get; set; }
        /*
         * 1-Aluno
         * 2-Professor
         */
        
    }
}
