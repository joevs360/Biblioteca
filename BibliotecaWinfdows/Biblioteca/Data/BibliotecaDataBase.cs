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

        //RFID
        public async Task<RFID> GetRFID(string id)
        {
            return await database.Table<RFID>().FirstOrDefaultAsync(r => r.ID == id);
        }

        public async Task<bool> RemoveRFID(List<string> ids)
        {
            try
            {
                if(MessageBox.Show($"Deseja deletar {ids.Count} RFID" , "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (var id in ids)
                    {
                        RFID rfid = await GetRFID(id);
                        if (rfid != null)
                        {
                            await DeleteAsync(rfid);
                        }
                    }
                    return true;
                }
                return false;

            }
            catch (Exception)
            {

                throw;
            }
           
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
               if(await GetRFID(rfid.ID) == null)
                {
                    await database.InsertAsync(rfid);
                    return true;
                }
                else
                {
                    MessageBox.Show("Esse RFID já está cadastrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception)
            {
                return false;            }
        }

        //Usuário
        public async Task<Usuario> GetUsuarioByRFID(string id)
        {
            try
            {
                RFID rfid = await database.Table<RFID>().Where(r => r.ID == id).FirstOrDefaultAsync();
                if (rfid != null)
                {
                    var user = await database.Table<Usuario>().Where(u => u.ID == rfid.IdUsuario).FirstOrDefaultAsync();
                    if(user == null)
                    {
                        MessageBox.Show("Usuário não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    return user;
                }
                MessageBox.Show("RFID não cadastrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro: "+ e, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
           
        }
        public async Task<Usuario> GetUsuarioByID(int id)
        {
            return await database.Table<Usuario>().Where(u => u.ID == id).FirstOrDefaultAsync();
        }
        public async Task<Usuario> GetUsuarioByRA(string ra)
        {
            return await database.Table<Usuario>().Where(u => u.RA == ra).FirstOrDefaultAsync();
        }
        public async Task<bool> RemoverUsuario(string ra)
        {
            try
            {
                var usuario = await GetUsuarioByRA(ra);
                if(usuario == null)
                {
                    MessageBox.Show("Usuário não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (MessageBox.Show("Deseja deletar o usuário " + usuario.Nome, "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    await DeleteAsync(usuario);
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
        public async Task<bool> RemoverUsuario(List<string> ras)
        {
            try
            {
                if (MessageBox.Show($"Deseja deletar o {ras.Count} usuários ", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (var ra in ras)
                    {
                        var usuario = await GetUsuarioByRA(ra);
                        if (usuario != null)
                        {
                            await database.DeleteAsync(usuario);
                        }
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
        public async Task<bool> SalvarLocacao(Usuario usuario , Livro livro)
        {
            try
            {
                Locacao locacao = new Locacao();
                locacao.dataInicio = DateTime.Now;
                locacao.LivroID = livro.ID;
                locacao.UsuarioID = usuario.ID;

                int qtdLocado = await QuantidadeLocado(livro.ID);
                
                //Validações
               
                if (locacao.LivroID==0)
                {
                    MessageBox.Show("Nenhum livro selecionado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (locacao.UsuarioID == 0)
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
        public async Task<List<Autor>> GetAllAutores()
        {
            return await database.Table<Autor>().ToListAsync();
        }
        public async Task<bool> SalvarAutor(Autor autor)
        {
            try
            {
                if (string.IsNullOrEmpty(autor.Nome))
                {
                    MessageBox.Show("Nome não pode ser em branco!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Cadastrar ou editar
                if (autor.Id == 0)
                {
                    await database.InsertAsync(autor);
                    MessageBox.Show("Autor cadastrado com sucesso!", "Aviso");
                }
                else
                {
                    await database.UpdateAsync(autor);
                    MessageBox.Show("Autor alterado com sucesso!", "Aviso");
                }
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível salvar o autor","Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
        }

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
                if(livro.AutorID == 0)
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
