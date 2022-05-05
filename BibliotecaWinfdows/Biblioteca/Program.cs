using Biblioteca.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biblioteca.Data;
using System.IO;
using Biblioteca.Models;

namespace Biblioteca
{
    internal static class Program
    {
        //Listas
        public static List<Livro> livros = new List<Livro>();
        public static List<Autor> autores = new List<Autor>();
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainPage());
        }
        static BibliotecaDataBase database;
        public static BibliotecaDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new BibliotecaDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BiblitecaDB.db3"));
                }
                return database;
            }
        }
    }
}
