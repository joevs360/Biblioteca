using Biblioteca.DAO;
using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca.Views
{
    public partial class CadastroLivro : Form
    {
        Livro livro;
        public CadastroLivro()
        {
            InitializeComponent();
            livro = new Livro();
            Iniciar();
        }
        public CadastroLivro(string key)
        {
            InitializeComponent();
            Iniciar(key);
        }
        async void Iniciar(string key = "")
        {
            await listarAutores();
            if (!string.IsNullOrEmpty(key))
            {
                buscarLivro(key);
            }
           
        }
       
        async void buscarLivro(string key)
        {
            await carregamento.carregar(true, "Buscando...");
            livro = await new LivroDAO().GetLivro(key);
            if(livro == null)
            {
                MessageBox.Show("Livro não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                //Jogar na tela itens do livro
                txtEdicao.Value = livro.Edicao;
                txtISBN.Text = livro.ISBN;
                txtNome.Text = livro.Nome;
                txtQtd.Value = livro.QuantidadeTotal;
                txtEditora.Text = livro.Editora;
                
                selectAutor.SelectedIndex = Program.autores.IndexOf(Program.autores.First(a => a.Key == livro.AutorKey));
            }
           
            await carregamento.carregar(false);
        }
        async Task listarAutores()
        {
            if(Program.autores.Count == 0)
            {
                Program.autores = await new AutorDAO().GetAllAutores();
            }
            foreach (var item in Program.autores)
            {
                selectAutor.Items.Add(item.Nome);
            }
        }
        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            livro.Nome = txtNome.Text;
            livro.Edicao = (int)txtEdicao.Value;
            livro.ISBN = txtISBN.Text;
            livro.QuantidadeTotal = (int)txtQtd.Value;
            livro.Editora = txtEditora.Text;
            if (selectAutor.SelectedIndex >= 0)
            {
                livro.AutorKey = Program.autores[selectAutor.SelectedIndex].Key;
            }
            
            await carregamento.carregar(true, "Salvando alterações...");
            if (await new LivroDAO().SalvarLivro(livro))
            {
                Program.livros = await new LivroDAO().GetLivros();
                this.Close();
            }
            await carregamento.carregar(false);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
