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
            if (!string.IsNullOrWhiteSpace(rfid))
               BuscarUsuario(rfid);
        }
        async void BuscarUsuario(string rfid)
        {
            usuario = await Program.Database.GetUsuarioByRFID(rfid);
            ExibirUsuario();
        }
        void ExibirUsuario()
        {
            if (usuario != null)
            {
                txtEmail.Text = usuario.Email;
                txtNome.Text = usuario.Nome;
                txtRA.Text = usuario.RA;
                txtTelefone.Text = usuario.Telefone;
                btnSalvar.Visible = true;
            }
            else
            {
                btnSalvar.Visible = false;
            }
        }
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView.SelectedItems.Clear();
        }
        void EscreverQuantidade()
        {
            txtQuantidade.Text = $"{listLivros.Count} itens";
        }

        private void Adicionar_Click(object sender, EventArgs e)
        {
            ListaLivroPage listaLivroPage = new ListaLivroPage(true, listLivros);
            listaLivroPage.ShowDialog();
            listLivros = listaLivroPage.livrosSelecionados;
            listarLivros();
            EscreverQuantidade();
        }
        void listarLivros()
        {
            listView.Items.Clear();
            foreach (var item in listLivros)
            {
                Autor autor = Program.autores.Where(a => a.Id == item.AutorID).FirstOrDefault();
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems[0].Text=(item.Nome);
                lvi.SubItems.Add(item.Edicao.ToString());
                lvi.SubItems.Add(autor.Nome);
                lvi.SubItems.Add(item.ISBN.ToString());
                listView.Items.Add(lvi);
            }
            EscreverQuantidade();
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            await carregamento1.carregar(true, "Salvando...");
            List<Livro> temp = new List<Livro>();
            foreach (var item in listLivros)
            {
                temp.Add(item);
            }
            foreach (var item in listLivros)
            {
                if (usuario != null)
                {
                   if(await Program.Database.SalvarLocacao(usuario, item))
                    {
                        temp.Remove(item);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Nenhum usuário selecionado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            listLivros = temp;
            if (listLivros.Count == 0)
            {
                this.Close();
            }
            await carregamento1.carregar(false);
        }
    }
}
