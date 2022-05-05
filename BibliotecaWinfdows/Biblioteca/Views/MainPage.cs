using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca.Views
{
    public partial class MainPage : Form
    {
        public Arduino arduino = new Arduino();
        public string UltimoDado;
        public MainPage()
        {
            InitializeComponent();
            arduino.serialPorta = serial;
        }

        private void serialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConexaoPage conexaoPage = new ConexaoPage(this);
            conexaoPage.Show();
        }

        private void MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Quando o programa é fechado.
            //Fecha a conexao com a porta serial se ela estiver aberta
            if (arduino.serialPorta.IsOpen == true)
                arduino.serialPorta.Close();
        }
        
     
        private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string RxString = arduino.serialPorta.ReadExisting();
            UltimoDado = RxString;
            if (RxString.Contains('\n'))
            {
                //Envia para o arduino fechar a leitura
                arduino.enviarDados("[LF]");
            }

        }

        public async Task<bool> lerDados(int tempMax)
        {
            int temp = 0;
            int v = 1000;
            UltimoDado = "";
            while (string.IsNullOrEmpty(UltimoDado) && temp <= tempMax)
            {
                await Task.Delay(v);
                temp += v;
            }

            return !string.IsNullOrEmpty(UltimoDado);
        }


        private void locacoes_Click(object sender, EventArgs e)
        {
            //Teste
            EmprestimoCadastroPage emprestimo = new EmprestimoCadastroPage(this);
            emprestimo.Show();
        }


        private void AlunosClick(object sender, EventArgs e)
        {
            AlunosPage alunosPage = new AlunosPage(this);
            alunosPage.Show();
        }

       
    }
}
