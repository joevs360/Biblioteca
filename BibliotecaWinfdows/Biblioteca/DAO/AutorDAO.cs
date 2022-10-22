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
    public class AutorDAO
    {
        FirebaseClient fc = FirebaseConfiguracao.fc;


        public async Task<Autor> GetAutorByID(string key)
        {
            try
            {
                var GetItem = (await fc.Child("Autor").Child(key).OnceSingleAsync<Autor>());
                Autor autor = new Autor();

                autor = GetItem;
                autor.Key = key;

                return autor;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> RemoverAutor(string key)
        {
            try
            {
                var autor = await GetAutorByID(key);
                if (autor == null)
                {
                    MessageBox.Show("Autor não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (MessageBox.Show("Deseja deletar o autor " + autor.Nome, "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    await fc.Child("Autor").Child(key).DeleteAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possivel deletar esse autor!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
       

        //Listando os autores
        public async Task<List<Autor>> GetAllAutores()
        {
            try
            {
                var GetItems = (await fc.Child("Autor").OnceAsync<Autor>()).ToList();
                List<Autor> autores = new List<Autor>();

                foreach (var GetItem in GetItems)
                {
                    var autor = GetItem.Object;
                    autor.Key = GetItem.Key;

                    autores.Add(autor);
                }


                return autores;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> SalvarAutor(Autor autor)
        {
            try
            {
                //Validações
                if (string.IsNullOrWhiteSpace(autor.Nome))
                {
                    MessageBox.Show("O campo do nome não pode ser vazio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Cadastrar ou editar
                if (string.IsNullOrEmpty(autor.Key))
                {
                    await fc.Child("Autor").PostAsync(autor);
                    MessageBox.Show("Autor cadastrado com sucesso!", "Aviso");
                }
                else
                {
                    string key = autor.Key;
                    autor.Key = null;
                    await fc.Child("Autor").Child(key).PutAsync(autor);
                    MessageBox.Show("Autor alterado com sucesso!", "Aviso");
                }
                return true;
            }

            catch (Exception e)
            {
                MessageBox.Show("Não foi possível salvar o autor:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
