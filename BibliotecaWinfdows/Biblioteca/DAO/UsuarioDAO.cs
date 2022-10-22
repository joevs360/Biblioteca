using Biblioteca.Data;
using Biblioteca.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca.DAO
{
    public class UsuarioDAO
    {
        FirebaseClient fc = FirebaseConfiguracao.fc;
        public async Task<Usuario> GetUsuarioByID(string key)
        {
            try
            {
                var GetItem = (await fc.Child("Usuario").Child(key).OnceSingleAsync<Usuario>());
                Usuario usuario = new Usuario();

                usuario = GetItem;
                usuario.Key = key;
                
                return usuario;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Usuario> GetUsuarioByRFID(string rfid)
        {
            try
            {
                var GetItem = (await fc.Child("Usuario").OnceAsync<Usuario>()).Where(u=>u.Object.RFIDs.Exists(r=>r.ID == rfid)).FirstOrDefault();
                Usuario usuario = new Usuario();
                if(GetItem != null)
                {
                    usuario = GetItem.Object;
                    usuario.Key = GetItem.Key;
                }
                else
                {
                    usuario = null;
                }

                return usuario;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> RemoverUsuario(string key)
        {
            try
            {
                var usuario = await GetUsuarioByID(key);
                if (usuario == null)
                {
                    MessageBox.Show("Usuário não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (MessageBox.Show("Deseja deletar o usuário " + usuario.Nome, "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    await fc.Child("Usuario").Child(key).DeleteAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possivel deletar esse usuário!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public async Task<bool> RemoverUsuario(List<string> Keys)
        {
            try
            {
                if (MessageBox.Show($"Deseja deletar o {Keys.Count} usuários ", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (var key in Keys)
                    {
                        await fc.Child("Usuario").Child(key).DeleteAsync();
                    }
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possivel deletar alguns usuários!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public async Task<List<Usuario>> GetAllUsuarios()
        {
            try
            {
                var GetItems = (await fc.Child("Usuario").OnceAsync<Usuario>()).ToList();
                List<Usuario> usuarios = new List<Usuario>();

                foreach(var GetItem in GetItems)
                {
                    var usuario = GetItem.Object;
                    usuario.Key = GetItem.Key;

                    usuarios.Add(usuario);
                }
               

                return usuarios;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> SalvarRFID(RFID rfid)
        {
            if (await GetUsuarioByRFID(rfid.ID) != null)
            {
                MessageBox.Show("Esse RFID já está cadastrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public Usuario RemoverRFID(List<string> rfids, Usuario usuario)
        {
            try
            {
                if (MessageBox.Show($"Deseja deletar {rfids.Count} RFID", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (var id in rfids)
                    {
                        RFID rfid = usuario.RFIDs.FirstOrDefault(r=>r.ID == id) ;
                        usuario.RFIDs.Remove(rfid);
                    }
                    return usuario;
                }
                return usuario;

            }
            catch (Exception)
            {

                throw;
            }
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
                if (string.IsNullOrEmpty(usuario.Key))
                {
                    await fc.Child("Usuario").PostAsync(usuario);
                    MessageBox.Show("Usuário cadastrado com sucesso!", "Aviso");
                }
                else
                {
                    string key = usuario.Key;
                    usuario.Key = null;
                    await fc.Child("Usuario").Child(key).PutAsync(usuario);
                    MessageBox.Show("Usuário alterado com sucesso!", "Aviso");
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Não foi possível salvar o usuário:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
