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
    public partial class ListaLivroPage : Form
    {
        bool isSelecionar;
        public List<Livro> livrosSelecionados;
        public ListaLivroPage(bool isSelecionar, List<Livro> livrosSelecionados = null)
        {
            InitializeComponent();
            if(livrosSelecionados != null)
            {
                this.livrosSelecionados = livrosSelecionados;
            }
            else
            {
                this.livrosSelecionados = new List<Livro>();
            }
            this.isSelecionar = isSelecionar;
            if (isSelecionar)
            {
                btnAdicionar.Visible = false;
                btnSelecionar.Visible = true;
            }
            iniciarBanco();
        }

        async void iniciarBanco()
        {
            if(Program.livros.Count  == 0)
            {
                Program.livros = await new LivroDAO().GetLivros();
            }
            if (Program.autores.Count == 0)
            {
                Program.autores = await new AutorDAO().GetAllAutores();
            }
            listarLivros();
        }
        void EscreverQuantidade()
        {
            txtQuantidade.Text = $"{Program.livros.Count} itens";
            if (livrosSelecionados.Count > 0)
            {
                txtQuantidade.Text += $" | {livrosSelecionados.Count}";
                if (livrosSelecionados.Count == 1)
                {
                    txtQuantidade.Text += " selecionado";
                }
                else
                {
                    txtQuantidade.Text += " selecionados";
                }
            }

        }
        private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!isSelecionar)
            {
                if (listView.CheckedItems.Count == 0)
                {
                    btnEditar.Visible = false;
                    btnRemover.Visible = false;
                }
                else if (listView.CheckedItems.Count > 1)
                {
                    btnEditar.Visible = false;
                    btnRemover.Visible = true;
                }
                else
                {
                    btnEditar.Visible = true;
                    btnRemover.Visible = true;
                }
            }
            
            for (int i = 0; i < listView.CheckedItems.Count; i++)
            {
                string key = listView.CheckedItems[i].Name;
                var temp = Program.livros.FirstOrDefault(l => l.Key == key);
                if (listView.CheckedItems[i].Checked)
                {
                    if (temp != null && !livrosSelecionados.Contains(temp))
                    {
                        livrosSelecionados.Add(temp);
                    }
                }
            }
            for(int i = 0; i < listView.Items.Count; i++)
            {
                string key = listView.Items[i].Name;
                var temp = Program.livros.FirstOrDefault(l => l.Key == key);
                if (!listView.Items[i].Checked)
                {
                    if (livrosSelecionados.Contains(temp))
                    {
                        livrosSelecionados.Remove(temp);
                    }
                }
            }
            EscreverQuantidade();
        }
       
        async void listarLivros(string filtro = "")
        {
            listView.Items.Clear();
            foreach (var item in Program.livros.Where(u => u.Nome.ToUpper().Contains(filtro.ToUpper()) 
            || (Program.autores.Where(a=>a.Nome.ToUpper().Contains(filtro.ToUpper())).FirstOrDefault()!=null && u.AutorKey == Program.autores.Where(a => a.Nome.ToUpper().Contains(filtro.ToUpper())).FirstOrDefault().Key)))
            {
                Autor autor = Program.autores.Where(a => a.Key == item.AutorKey).FirstOrDefault();
                ListViewItem lvi = new ListViewItem();
                lvi.Name = item.Key;
                lvi.SubItems.Add(item.Nome);
                int qtdDisponivel = item.QuantidadeTotal - await new LocacaoDAO().QuantidadeLocado(item.Key);
                lvi.SubItems.Add(qtdDisponivel.ToString());
                lvi.SubItems.Add(autor.Nome);
                lvi.SubItems.Add(item.Editora);
                lvi.SubItems.Add(item.Edicao.ToString());
                lvi.SubItems.Add(item.ISBN.ToString());
                lvi.Checked = livrosSelecionados.FirstOrDefault(l=>l.Key == item.Key) != null;
                listView.Items.Add(lvi);
            }
            EscreverQuantidade();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            CadastroLivro cadastroLivro = new CadastroLivro();
            cadastroLivro.ShowDialog();
            listarLivros();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            CadastroLivro cadastroLivro = new CadastroLivro(listView.CheckedItems[0].Name);
            cadastroLivro.ShowDialog();
            listarLivros();
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            listarLivros(txtBusca.Text);
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView.SelectedItems.Clear();
        }

        private async void btnRemover_Click(object sender, EventArgs e)
        {
            List<string> Keys= new List<string>();
            for(int i = 0; i < listView.CheckedItems.Count; i++)
            {
                Keys.Add(listView.CheckedItems[i].Name);
            }
            if (await new LivroDAO().RemoverLivros(Keys))
            {
                Program.livros = await new LivroDAO().GetLivros();
                listarLivros();
                
            }
              
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
