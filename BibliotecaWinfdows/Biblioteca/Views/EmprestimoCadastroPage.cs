using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biblioteca.Models;

namespace Biblioteca.Views
{
    public partial class EmprestimoCadastroPage : Form
    {
        public Usuario usuario;
        public MainPage main;
        List<Livro> listLivros = new List<Livro>();
        public EmprestimoCadastroPage(MainPage main)
        {
            InitializeComponent();
            this.main = main;
        }

        private void btnLerRFID_Click(object sender, EventArgs e)
        {
            LeituraRfidPage leituraRfidPage = new LeituraRfidPage(main);
            leituraRfidPage.ShowDialog();
            string rfid = leituraRfidPage.retorno;

        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < listLivros.Count; i++)
            {
                listLivros.RemoveAt(listView.CheckedItems[i].Index);
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView.SelectedItems.Clear();
        }

        private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (listView.CheckedItems.Count == 0)
            {
                btnRemover.Visible = false;
            }
            else
            {
                btnRemover.Visible = true;
            }
            EscreverQuantidade();
        }
        void EscreverQuantidade()
        {
            txtQuantidade.Text = $"{listLivros.Count} itens";
            if (listView.CheckedItems.Count > 0)
            {
                txtQuantidade.Text += $" | {listView.CheckedItems.Count}";
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

        private void Adicionar_Click(object sender, EventArgs e)
        {
            ListaLivroPage listaLivroPage = new ListaLivroPage(true, listLivros);
            listaLivroPage.ShowDialog();
            listLivros = listaLivroPage.livrosSelecionados;
            
        }
        void listarLivros()
        {
            listView.Items.Clear();
            foreach (var item in Program.livros)
            {
                Autor autor = Program.autores.Where(a => a.Id == item.AutorID).FirstOrDefault();
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(item.Nome);
                lvi.SubItems.Add(item.Edicao.ToString());
                lvi.SubItems.Add(autor.Nome);
                lvi.SubItems.Add(item.ISBN.ToString());
                listView.Items.Add(lvi);
            }
            EscreverQuantidade();
        }
    }
}
