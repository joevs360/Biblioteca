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
                Program.livros = await Program.Database.GetLivros();
            }
            if (Program.autores.Count == 0)
            {
                Program.autores = await Program.Database.GetAllAutores();
            }
            listarLivros();
        }
        void EscreverQuantidade()
        {
            txtQuantidade.Text = $"{Program.livros.Count} itens";
            if (livrosSelecionados.Count > 0)
            {
                txtQuantidade.Text += $" | {livrosSelecionados.Count}";
                if (listView.CheckedItems.Count == 1)
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
                int id = int.Parse(listView.CheckedItems[i].SubItems[1].Text);
                var temp = Program.livros.FirstOrDefault(l => l.ID == id);
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
                int id = int.Parse(listView.Items[i].SubItems[1].Text);
                var temp = Program.livros.FirstOrDefault(l => l.ID == id);
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
            || (Program.autores.Where(a=>a.Nome.ToUpper().Contains(filtro.ToUpper())).FirstOrDefault()!=null && u.ID == Program.autores.Where(a => a.Nome.ToUpper().Contains(filtro.ToUpper())).FirstOrDefault().Id)))
            {
                Autor autor = Program.autores.Where(a => a.Id == item.AutorID).FirstOrDefault();
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(item.ID.ToString());
                lvi.SubItems.Add(item.Nome);
                int qtdDisponivel = item.QuantidadeTotal - await Program.Database.QuantidadeLocado(item.ID);
                lvi.SubItems.Add(qtdDisponivel.ToString());
                lvi.SubItems.Add(autor.Nome);
                lvi.SubItems.Add(item.Editora);
                lvi.SubItems.Add(item.Edicao.ToString());
                lvi.SubItems.Add(item.ISBN.ToString());
                lvi.Checked = livrosSelecionados.FirstOrDefault(l=>l.ID == item.ID) != null;
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
            CadastroLivro cadastroLivro = new CadastroLivro(int.Parse(listView.CheckedItems[0].SubItems[1].Text));
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
            List<int> ids= new List<int>();
            for(int i = 0; i < listView.CheckedItems.Count; i++)
            {
                ids.Add(int.Parse(listView.CheckedItems[i].SubItems[1].Text));
            }
            if (await Program.Database.RemoverLivros(ids))
            {
                Program.livros = await Program.Database.GetLivros();
                listarLivros();
                
            }
              
        }
    }
}
