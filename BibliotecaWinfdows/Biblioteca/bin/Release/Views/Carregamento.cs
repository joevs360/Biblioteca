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
    public partial class Carregamento : UserControl
    {
        public Carregamento()
        {
            InitializeComponent();
            this.Visible = false;
        }
        public async Task carregar(bool carregar, string mensagem = "carregando...")
        {
            txtCarregamento.Text = mensagem;
            this.Visible = carregar;
            await Task.Delay(100);
        }

    }
}
