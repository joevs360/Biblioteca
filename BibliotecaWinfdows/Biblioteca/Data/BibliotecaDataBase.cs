using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Biblioteca.Models;
using System.Windows.Forms;

namespace Biblioteca.Data
{
    internal class BibliotecaDataBase
    {
        readonly SQLiteAsyncConnection database;

        public BibliotecaDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Locacao>().Wait();
            database.CreateTableAsync<Porta>().Wait();
        }
        //Delete geral
        private async Task<int> DeleteAsync(object obj)
        {
            return await database.DeleteAsync(obj);
        }
        //Porta
        public async Task<Porta> GetPorta()
        {
            return await database.Table<Porta>().FirstOrDefaultAsync();
        }
        public async Task<bool> SalvarPorta(Porta porta)
        {
            try
            {
                var portaDoBanco = await GetPorta();
                if (portaDoBanco == null)
                {
                    await database.InsertAsync(porta);
                    return true;
                }
                else
                {
                    portaDoBanco.Texto = porta.Texto;
                    await database.UpdateAsync(portaDoBanco);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
