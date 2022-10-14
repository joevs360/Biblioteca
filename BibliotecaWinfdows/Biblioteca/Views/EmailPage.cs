using Biblioteca.Services;
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
    public partial class EmailPage : Form
    {
        public EmailPage()
        {
            InitializeComponent();
        }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            await carregamento1.carregar(true, "Enviando...");
            txtEmail.Text.Replace(",", ";");
            List<string> emails = txtEmail.Text.ToLower().Split(';').ToList();

            bool enviado;

            List<string> erro = new List<string>();
            
            foreach (string email in emails) {
                email.Replace(" ", "");
                if (!String.IsNullOrEmpty(email)) {
                    if (!EmailService.EnviaEmail(email, txtAssunto.Text, txtMensagem.Text))
                        erro.Add(email);
                }
            }
            
            if (erro.Count > 0) {
                MessageBox.Show("Não foi possivel enviar todos emails, verifique!","Alguns emails estão com erro");

                txtEmail.Text = String.Empty;

                erro.ForEach(email => {  txtEmail.Text += email+";";});
            }
            else
            {
                Close();
            }
            await carregamento1.carregar(false);
        }
    }
}
