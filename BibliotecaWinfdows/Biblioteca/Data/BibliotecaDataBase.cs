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
            database.CreateTableAsync<Livro>().Wait();
            database.CreateTableAsync<Autor>().Wait();
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
        public async Task<List<Locacao>> GetLocacoesPorLivro(int idLivro)
        {
            return await database.Table<Locacao>().Where(l => l.LivroID == idLivro).ToListAsync();
        }
        public async Task<List<Locacao>> GetLocacoes()
        {
            return await database.Table<Locacao>().ToListAsync();
        }
        public async Task<List<Locacao>> GetLocacoesData(DateTime data)
        {
            return await database.Table<Locacao>().Where(l => l.dataInicio.Date == data.Date).ToListAsync();
        }
        public async Task<int> QuantidadeLocado(int idLivro)
        {
           return await database.Table<Locacao>().Where(l => l.LivroID == idLivro).CountAsync();
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
                locacao.LivroID = livro.ID;
                locacao.UsuarioKey = usuario.Key;

                int qtdLocado = await QuantidadeLocado(livro.ID);
                
                //Validações
               
                if (locacao.LivroID==0)
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
        public async Task<bool> RemoverLocacoesPorLivro(int id)
        {
            try
            {
                bool resp = true;
                var locacoes = await GetLocacoesPorLivro(id);
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
                    MessageBox.Show($"Não é possivel deletar todas locações do livro {Program.livros.FirstOrDefault(l=>l.ID==id).Nome} pois algumas estão em aberto!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return resp;
            }
            catch (Exception e)
            {
                MessageBox.Show("Não foi possivel deletar:\n" + e, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
         
        }

        //Autor
        //public async task<list<autor>> getallautores()
        //{
        //    return await database.table<autor>().tolistasync();
        //}
        //public async task<bool> salvarautor(autor autor)
        //{
        //    try
        //    {
        //        if (string.isnullorempty(autor.nome))
        //        {
        //            messagebox.show("nome não pode ser em branco!", "aviso", messageboxbuttons.ok, messageboxicon.error);
        //            return false;
        //        }

        //        //cadastrar ou editar
        //        if (autor.id == 0)
        //        {
        //            await database.insertasync(autor);
        //            messagebox.show("autor cadastrado com sucesso!", "aviso");
        //        }
        //        else
        //        {
        //            await database.updateasync(autor);
        //            messagebox.show("autor alterado com sucesso!", "aviso");
        //        }
        //        return true;
        //    }
        //    catch (exception)
        //    {
        //        messagebox.show("não foi possível salvar o autor", "aviso", messageboxbuttons.ok, messageboxicon.error);
        //        return false;
        //    }

        //}

        //Livro
        public async Task<bool> SalvarLivro(Livro livro)
        {
            try
            {
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
                if(livro.AutorKey == null)
                {
                    MessageBox.Show("Nenhum autor foi selecionado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (livro.ID == 0)
                {
                    await database.InsertAsync(livro);
                }
                else
                {
                    await database.UpdateAsync(livro);
                    MessageBox.Show("Salvo com sucesso!", "Aviso");
                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível salvar o livro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public async Task<Livro> GetLivro(int id)
        {
            return await database.Table<Livro>().FirstAsync(l => l.ID == id);
        }
        public async Task<List<Livro>> GetLivros()
        {
            return await database.Table<Livro>().ToListAsync();
        }
        public async Task<bool> RemoverLivros(List<int> ids)
        {
            if (MessageBox.Show($"Deseja deletar o {ids.Count} livros", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var id in ids)
                {
                   Livro livro = await GetLivro(id);
                    if (livro != null) {
                        //Remover locações 
                         if (await RemoverLocacoesPorLivro(id))
                         {
                           await DeleteAsync(livro);
                         }
                    }
                }
                return true;
            }
            return false;
        }
    }
}
