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
using System.Windows.Input;

namespace Biblioteca.DAO
{
    public class LivroDAO
    {
        FirebaseClient fc = FirebaseConfiguracao.fc;


        public async Task<Livro> GetLivroByID(string key)
        {
            try
            {
                var GetItem = (await fc.Child("Livro").Child(key).OnceSingleAsync<Livro>());
                Livro livro = new Livro();

                livro = GetItem;
                livro.Key = key;

                return livro;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<Livro>> GetLivros()
        {
            var GetLivros = (await fc.Child("Livro").OnceAsync<Livro>()).ToList();
            List<Livro> livros = new List<Livro>();
            foreach (var item in GetLivros)
            {
                Livro livro = item.Object;
                livro.Key = item.Key;
                livros.Add(livro);
            }
            return livros;
        }
        public async Task<Livro> GetLivro(string key)
        {
            Livro livro = await fc.Child("Livro").Child(key).OnceSingleAsync<Livro>();
            livro.Key = key;

            return livro;
        }

        public async Task<bool> RemoverLivros(List<String> keys)
        {
            if (MessageBox.Show($"Deseja deletar o {keys.Count} livros", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var Key in keys)
                {
                    Livro livro = await GetLivro(Key);
                    if (livro != null)
                    {
                        //Remover locações 
                        if (await new LocacaoDAO().RemoverLocacoesPorLivro(Key))
                        {
                             await fc.Child("Livro").Child(Key).DeleteAsync();
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public async Task<bool> SalvarLivro(Livro livro)
        {
            try
            {
                //Validações
                if (string.IsNullOrWhiteSpace(livro.Nome))
                {
                    MessageBox.Show("O título não pode ser em branco", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (livro.Edicao <= 0)
                {
                    MessageBox.Show("A edição tem que ser maior que zero", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (livro.AutorKey == null)
                {
                    MessageBox.Show("Nenhum autor foi selecionado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }



                //Cadastrar ou editar
                if (string.IsNullOrEmpty(livro.Key))
                {
                    await fc.Child("Livro").PostAsync(livro);
                    MessageBox.Show("Livro cadastrado com sucesso!", "Aviso");
                }
                else
                {
                    string key = livro.Key;
                    livro.Key = null;
                    await fc.Child("Livro").Child(key).PutAsync(livro);
                    MessageBox.Show("Livro alterado com sucesso!", "Aviso");
                }
                return true;
            }

            catch (Exception e)
            {
                MessageBox.Show("Não foi possível salvar o livro\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



        }
    }
}
