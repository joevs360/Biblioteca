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
    public partial class CadastroAluno : Form
    {
        MainPage main;
        Usuario usuario;
        public  List<Usuario> listUsuario;
        public CadastroAluno(MainPage main)
        {
            InitializeComponent();
            this.main = main;
            usuario = new Usuario();
            
        }
        public CadastroAluno(MainPage main, Usuario usuario)
        {
            InitializeComponent();
            this.main = main;
            this.usuario = usuario;
            btnAdicionar.Visible = true;
            ExibirDados();
            listarRFIDS();

        }
        async void listarRFIDS()
        {
            await carregamento.carregar(true, "Buscando...");
            List<RFID> rfids = await Program.Database.GetRFIDs(usuario.ID);
            listView.Items.Clear();
            await carregamento.carregar(true, "Listando...");
            foreach (var item in rfids)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems[0].Text = (item.dataCadastro.ToString("dd/MM/yyyy"));
                lvi.SubItems.Add(item.ID);
                
                listView.Items.Add(lvi);
            }
            await carregamento.carregar(false);
        }

        void ExibirDados()
        {
            txtNome.Text = usuario.Nome;
            txtEmail.Text = usuario.Email;
            txtRA.Text = usuario.RA;
            txtTelefone.Text = usuario.Telefone;
        }
        private void CadastroAluno_Load(object sender, EventArgs e)
        {

        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView.SelectedItems.Count > 0)
            {
                btnRemover.Visible= true;
            }
            else
            {
                btnRemover.Visible = false;
            }
        }

        private async void btnAdicionar_Click(object sender, EventArgs e)
        {
            await carregamento.carregar(true, "Abrindo cadastro...");
            
          
            LeituraRfidPage leituraRfidPage = new LeituraRfidPage(main);
            await carregamento.carregar(true, "Aguardando cadastro...");
            leituraRfidPage.ShowDialog();
            string ID = leituraRfidPage.retorno;

            if (!string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(usuario.RA))
            {
                await carregamento.carregar(true, "Salvando RFID...");
                RFID rfid = new RFID();
                rfid.ID = ID;
                rfid.IdUsuario = usuario.ID;
                rfid.dataCadastro = DateTime.Now;
                await Program.Database.SalvarRFID(rfid);
                listarRFIDS();
            }
            await carregamento.carregar(false);
           
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            //Pegar dados da tela 
            usuario.CategoriaUsuarioID = 1;
            usuario.RA = txtRA.Text;
            usuario.Email = txtEmail.Text;
            usuario.Telefone = txtTelefone.Text;
            usuario.Nome = txtNome.Text;
            //Salvar Aluno
            if (await Program.Database.SalvarUsuario(usuario))
            {
                listUsuario = await Program.Database.GetAllUsuario();
                usuario = listUsuario.Last();
                ExibirDados();
                btnAdicionar.Visible = true;

            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }
        private async void btnRemover_Click(object sender, EventArgs e)
        {
            await carregamento.carregar(true, "Listando...");
            List<string> ids= new List<string>();
            for(int i = 0; i < listView.SelectedItems.Count; i++)
            {
                ids.Add(listView.SelectedItems[i].SubItems[1].Text);
            }
            await carregamento.carregar(true, "Removendo...");
            await Program.Database.RemoveRFID(ids);
            listarRFIDS();
            await carregamento.carregar(false);
        }


    }
}
