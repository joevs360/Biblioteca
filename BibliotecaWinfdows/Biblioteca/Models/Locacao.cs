using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Locacao
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Livrokey { get; set; }
        public string UsuarioKey { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime dataDevolucao { get; set; }
        public bool notificado { get; set; }
    }
}
