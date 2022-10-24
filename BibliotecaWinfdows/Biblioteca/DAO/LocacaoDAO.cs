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
    internal class LocacaoDAO
    {
        FirebaseClient fc = FirebaseConfiguracao.fc;

        public async Task<List<Locacao>> GetLocacoesPorLivro(string LivroKey)
        {
            List<Locacao> lista = new List<Locacao>();

            var GetLocacoes = (await fc.Child("Locacao").OnceAsync<Locacao>()).Where(l => l.Object.Livrokey == LivroKey).ToList();
            foreach (var item in GetLocacoes)
            {
                Locacao locacao = item.Object;
                locacao.Key = item.Key;
                lista.Add(locacao);
            }

            return lista;
        }
        public async Task<List<Locacao>> GetLocacoes()
        {
            List<Locacao> lista = new List<Locacao>();

            var GetLocacoes = (await fc.Child("Locacao").OnceAsync<Locacao>()).ToList();
            foreach (var item in GetLocacoes)
            {
                Locacao locacao = item.Object;
                locacao.Key = item.Key;
                lista.Add(locacao);
            }

            return lista;
        }
        public async Task<List<Locacao>> GetLocacoesData(DateTime data)
        {
            List<Locacao> lista = new List<Locacao>();

            var GetLocacoes = (await fc.Child("Locacao").OnceAsync<Locacao>()).Where(l => l.Object.dataInicio.Date == data.Date).ToList();

            foreach (var item in GetLocacoes)
            {
                Locacao locacao = item.Object;
                locacao.Key = item.Key;
                lista.Add(locacao);
            }

            return lista;
        }
        public async Task<int> QuantidadeLocado(string LivroKey)
        {
            return  (await fc.Child("Locacao").OnceAsync<Locacao>()).Where(l => l.Object.Livrokey == LivroKey).Count();
        }
        public async Task<bool> SalvarLocacao(Usuario usuario, Livro livro)
        {
            try
            {
                Locacao locacao = new Locacao();
                locacao.dataInicio = DateTime.Now;
                locacao.Livrokey = livro.Key;
                locacao.UsuarioKey = usuario.Key;

                int qtdLocado = await QuantidadeLocado(livro.Key);

                //Validações

                if (string.IsNullOrEmpty(locacao.Livrokey))
                {
                    MessageBox.Show("Nenhum livro selecionado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(locacao.UsuarioKey))
                {
                    MessageBox.Show("Nenhum usuário selecionado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (livro.QuantidadeTotal <= qtdLocado)
                {
                    MessageBox.Show("O livro está indisponível", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //Salvar
                if (string.IsNullOrEmpty(locacao.Key))
                {
                    await fc.Child("Locacao").PostAsync(locacao);
                }
                else
                {
                    string key = locacao.Key;
                    locacao.Key = null;
                    await fc.Child("Locacao").Child(key).PutAsync(locacao);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> AtualizarLocacaoSemValidacao(Locacao locacao)
        {
            try
            {
                string key = locacao.Key;
                locacao.Key = null;
                await fc.Child("Locacao").Child(key).PutAsync(locacao);
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public async Task<bool> RemoverLocacao(Locacao locacao)
        {
            try
            {
                if (MessageBox.Show("Finalizar a locação", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    await fc.Child("Locacao").Child(locacao.Key).DeleteAsync();
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                return false;
            }

        }
        public async Task<bool> RemoverLocacoesPorLivro(string livrokey)
        {
            try
            {
                bool resp = true;
                var locacoes = await GetLocacoesPorLivro(livrokey);
                foreach (var l in locacoes)
                {
                    if (l.dataDevolucao != null)
                    {
                        await fc.Child("Locacao").Child(l.Key).DeleteAsync();
                    }
                    else
                    {
                        resp = false;
                    }
                }
                if (!resp)
                {
                    MessageBox.Show($"Não é possivel deletar todas locações do livro {Program.livros.FirstOrDefault(l => l.Key == livrokey).Nome} pois algumas estão em aberto!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return resp;
            }
            catch (Exception e)
            {
                MessageBox.Show("Não foi possivel deletar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
    }
}
