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
    public partial class AlunosPage : Form
    {
        MainPage main;
        List<Usuario> listUsuario = new List<Usuario>();
        public AlunosPage(MainPage main, int tipo = 1)
        {
            InitializeComponent();
            this.main = main;
            buscarItens(tipo);
        }
        async void buscarItens(int tipo)
        {
            listUsuario = await Program.Database.GetAllUsuario(tipo);
            listarUsuarios();
        }
        void listarUsuarios(string filtro="")
        {
            listView.Items.Clear();
            foreach (var item in listUsuario.Where(u=>u.Nome.Contains(filtro) || u.RA.Contains(filtro)))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(item.RA);
                lvi.SubItems.Add(item.Nome);
                lvi.SubItems.Add(item.Email);
                lvi.SubItems.Add(item.Telefone);
                listView.Items.Add(lvi);
            }
        }

        private void btnClick(object sender, EventArgs e)
        {
            listarUsuarios(txtBusca.Text);
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            if(txtBusca.Text.Length > 5)
            {
                listarUsuarios(txtBusca.Text);
            }
            else if(txtBusca.Text.Length == 0)
            {
                listarUsuarios();
            }
        }

        

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            CadastroAluno cadastroAluno = new CadastroAluno(main);
            cadastroAluno.ShowDialog();
            List<Usuario> usrs = cadastroAluno.listUsuario;
            if(usrs != null)
            {
                listUsuario = usrs;
                listarUsuarios();
            }
        }

        private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (listView.CheckedItems.Count == 0)
            {
                btnEditar.Visible = false;
                btnRemover.Visible = false;
            }
            else if (listView.CheckedItems.Count > 1)
            {
                btnEditar.Visible = true;
                btnRemover.Visible = false;
            }
            else
            {
                btnEditar.Visible = true;
                btnRemover.Visible = true;
            }
        }
        async Task carregamento(bool carregar, string mensagem = "carregando...")
        {
            txtCarregamento.Text = mensagem;
            panelCarregando.Visible = carregar;
            await Task.Delay(100);
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            buscarUsuario();
        }
        async void buscarUsuario()
        {
            await carregamento(true, "Consultando no banco...");
            string r = listView.CheckedItems[0].SubItems[1].Text;
            Usuario usuario = await Program.Database.GetUsuarioByRA(r);
            if (usuario != null)
            {
                CadastroAluno cadastroAluno = new CadastroAluno(main, usuario);
                await carregamento(true, "Montando tela...");
                cadastroAluno.Show();
                
            }
            await carregamento(false, "Finalizando...");

        }
    }
}
