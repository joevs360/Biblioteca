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

       
        //Locação
        public async Task<List<Locacao>> GetLocacoesPorLivro(string LivroKey)
        {
            return await database.Table<Locacao>().Where(l => l.Livrokey == LivroKey).ToListAsync();
        }
        public async Task<List<Locacao>> GetLocacoes()
        {
            return await database.Table<Locacao>().ToListAsync();
        }
        public async Task<List<Locacao>> GetLocacoesData(DateTime data)
        {
            return await database.Table<Locacao>().Where(l => l.dataInicio.Date == data.Date).ToListAsync();
        }
        public async Task<int> QuantidadeLocado(string LivroKey)
        {
           return await database.Table<Locacao>().Where(l => l.Livrokey == LivroKey).CountAsync();
        }
        public async Task<bool> AtualizarLocacao(Locacao locacao)
        {
            try
            {
                await database.UpdateAsync(locacao);
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public async Task<bool> SalvarLocacao(Usuario usuario , Livro livro)
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
                await database.InsertAsync(locacao);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoverLocacao(Locacao locacao)
        {
            try
            {
                if(MessageBox.Show("Finalizar a locação", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    await DeleteAsync(locacao);
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
                        await DeleteAsync(l);
                    }
                    else
                    {
                        resp = false;
                    }
                }
                if (!resp)
                {
                    MessageBox.Show($"Não é possivel deletar todas locações do livro {Program.livros.FirstOrDefault(l=>l.Key==livrokey).Nome} pois algumas estão em aberto!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
