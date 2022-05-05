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
    }
}
