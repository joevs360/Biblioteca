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
        }
        void ExibirDados()
        {
            txtNome.Text = usuario.Nome;
            txtEmail.Text = usuario.Telefone;
            txtRA.Text = usuario.RA;
            txtTelefone.Text = usuario.Telefone;
        }
        private void CadastroAluno_Load(object sender, EventArgs e)
        {

        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView.SelectedItems.Count == 0)
            {
                btnEditar.Visible = false;
                btnRemover.Visible = false;
            }
            else if(listView.SelectedItems.Count > 1)
            {
                btnEditar.Visible = true;
                btnRemover.Visible=false;
            }
            else
            {
                btnEditar.Visible = true;
                btnRemover.Visible = true;
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            LeituraRfidPage leituraRfidPage = new LeituraRfidPage(main);
            leituraRfidPage.ShowDialog();
            string ID = leituraRfidPage.retorno;

            if (!string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(usuario.RA))
            {
                RFID rfid = new RFID();
                rfid.ID = ID;
                rfid.IdUsuario = usuario.ID;

            }
            

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

        private void btnRemover_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
