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
    public partial class AlunosPage : Form
    {
        MainPage main;
        List<Usuario> listUsuario = new List<Usuario>();
        int qtd;
        public AlunosPage(MainPage main)
        {
            InitializeComponent();
            this.main = main;
            buscarItens();
        }
        async void buscarItens()
        {
            listUsuario = await new UsuarioDAO().GetAllUsuarios();
            listarUsuarios();
        }
        void listarUsuarios(string filtro="")
        {
            listView.Items.Clear();
            foreach (var item in listUsuario.Where(u=>u.Nome.ToUpper().Contains(filtro.ToUpper()) || u.RA.Contains(filtro)))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(item.RA);
                lvi.SubItems.Add(item.Nome);
                lvi.SubItems.Add(item.Email);
                lvi.SubItems.Add(item.Telefone);
                lvi.Name = item.Key;
                listView.Items.Add(lvi);
            }
            qtd = listUsuario.Count;
            EscreverQuantidade();
        }
        void EscreverQuantidade()
        {
            txtQuantidade.Text = $"{listUsuario.Count} itens";
            if(listView.CheckedItems.Count > 0)
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

        private void btnClick(object sender, EventArgs e)
        {
            listarUsuarios(txtBusca.Text);
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            listarUsuarios(txtBusca.Text);
        }

        

        private async void btnAdicionar_Click(object sender, EventArgs e)
        {
            await carregamento.carregar(true, "Aguardando cadastro...");
            CadastroAluno cadastroAluno = new CadastroAluno(main);
            cadastroAluno.ShowDialog();
            List<Usuario> usrs = cadastroAluno.listUsuario;
            if(usrs != null)
            {
                listUsuario = usrs;
                listarUsuarios();
            }
            await carregamento.carregar(false);
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
                btnEditar.Visible = false;
                btnRemover.Visible = true;
            }
            else
            {
                btnEditar.Visible = true;
                btnRemover.Visible = true;
            }
            EscreverQuantidade();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            buscarUsuario();
        }
        async void buscarUsuario()
        {
            await carregamento.carregar(true, "Consultando no banco...");
            string r = listView.CheckedItems[0].Name;
            Usuario usuario = await new UsuarioDAO().GetUsuarioByID(r);
            if (usuario != null)
            {
                await carregamento.carregar(true, "Aguardando alterações...");
                CadastroAluno cadastroAluno = new CadastroAluno(main, usuario);
               
                cadastroAluno.ShowDialog();
                
            }
            await carregamento.carregar(false, "Finalizando...");

        }

        private async void btnRemover_Click(object sender, EventArgs e)
        {
            bool limpar = false;
            await carregamento.carregar(true, "Deletando usuário...");
            if (listView.CheckedItems.Count == 1 )
            {
                limpar = await new UsuarioDAO().RemoverUsuario(listView.CheckedItems[0].Name);
            }
            else
            {
                
                List<string> ras = new List<string>();
                for (int i =0; i< listView.CheckedItems.Count;i++)
                {
                    await carregamento.carregar(true, $"Deletando usuários...\n {i/listView.CheckedItems.Count*100}%");
                    ras.Add(listView.CheckedItems[i].Name);
                }
                limpar = await new UsuarioDAO().RemoverUsuario(ras);

            }           
            await carregamento.carregar(false, "Finalizando...");
            if (limpar)
            {
                for (int i = 0; i < listView.CheckedItems.Count; i++)
                {
                    listUsuario.Remove(listUsuario.Where(u => u.RA == listView.CheckedItems[i].SubItems[1].Text).FirstOrDefault());
                }
            }
            
            listarUsuarios();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView.SelectedItems.Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AlunosPage_Load(object sender, EventArgs e)
        {

        }
    }
}
