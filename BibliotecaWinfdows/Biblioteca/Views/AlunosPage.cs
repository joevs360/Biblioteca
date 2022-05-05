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
            listView.Clear();
            foreach (var item in listUsuario.Where(u=>u.Nome.Contains(filtro) || u.RA.Contains(filtro)))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(item.RA);
                lvi.SubItems.Add(item.Nome);
                lvi.SubItems.Add(item.Telefone);
                lvi.SubItems.Add(item.Email);
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

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                btnEditar.Visible = false;
                btnRemover.Visible = false;
            }
            else if (listView.SelectedItems.Count > 1)
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
    }
}
