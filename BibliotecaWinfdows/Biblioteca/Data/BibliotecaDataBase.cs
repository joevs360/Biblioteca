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
            database.CreateTableAsync<RFID>().Wait();
            database.CreateTableAsync<Usuario>().Wait();
            database.CreateTableAsync<Emprestimo>().Wait();
            database.CreateTableAsync<Livro>().Wait();
        }
        public async Task<List<RFID>> GetRFIDs(int idUsuario)
        {
            return await database.Table<RFID>().Where(r => r.IdUsuario == idUsuario).ToListAsync();
        }
        public async Task<bool> SalvarRFID(RFID rfid)
        {
            try
            {
                //Validações
                if (string.IsNullOrWhiteSpace(rfid.ID))
                {
                    MessageBox.Show("Não é possivel salvar um RFID zerado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (rfid.IdUsuario == 0)
                {
                    MessageBox.Show("Não foi possivel vincular com o id do usuário!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                await database.InsertAsync(rfid);
                return true;
            }
            catch (Exception)
            {
                return false;            }
        }
        //Usuário
        public async Task<Usuario> GetUsuarioByRFID(string id)
        {
            RFID rfid = await database.Table<RFID>().Where(r => r.ID == id).FirstOrDefaultAsync();
            if(rfid == null)
            {
                return await database.Table<Usuario>().Where(u => u.ID== rfid.IdUsuario).FirstOrDefaultAsync();
            }
            return null;
        }
        public async Task<Usuario> GetUsuarioByID(int id)
        {
            return await database.Table<Usuario>().Where(u => u.ID == id).FirstOrDefaultAsync();
        }
        public async Task<Usuario> GetUsuarioByRA(string ra)
        {
            return await database.Table<Usuario>().Where(u => u.RA == ra).FirstOrDefaultAsync();
        }
        public async Task<List<Usuario>> GetAllUsuario(int tipo=1)
        {
            return await database.Table<Usuario>().Where(u=>u.CategoriaUsuarioID == tipo).ToListAsync();
        }
        public async Task<bool> SalvarUsuario(Usuario usuario)
        {
            
           
            try
            {
                //Validações
                if (string.IsNullOrWhiteSpace(usuario.Nome))
                {
                    MessageBox.Show("O campo do nome não pode ser vazio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(usuario.RA))
                {
                    MessageBox.Show("O campo do RA não pode ser vazio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(usuario.Telefone))
                {
                    MessageBox.Show("O campo do telefone não pode ser vazio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(usuario.Email))
                {
                    MessageBox.Show("O campo do e-mail não pode ser vazio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                

                //Cadastrar ou editar
                if (usuario.ID == 0)
                {
                    await database.InsertAsync(usuario);
                    MessageBox.Show("Usuário cadastrado com sucesso!","Aviso");
                }
                else
                {
                    await database.UpdateAsync(usuario);
                    MessageBox.Show("Usuário alterado com sucesso!", "Aviso");
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Não foi possível salvar o usuário:\n"+e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
