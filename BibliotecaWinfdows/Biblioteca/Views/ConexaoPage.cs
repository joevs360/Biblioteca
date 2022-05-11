using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Biblioteca.Models;

namespace Biblioteca.Views
{
    public partial class ConexaoPage : Form
    {
        MainPage mainPage;
        public Porta porta;
        public ConexaoPage(MainPage mainPage, bool primeiro = false)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            timer.Enabled = true;
            this.primeiro = primeiro;


        }
        async void SelecionarDoBanco()
        {
            porta = await Program.Database.GetPorta();
            if (porta != null)
            {
                if(comboPortas.Items.Contains(porta.Texto)){
                    comboPortas.SelectedItem = porta.Texto;
                    conectar();
                }
                
            }
        }
        bool primeiro = true;
        private void atualizaListaCOMs()
        {
            int i;
            bool qtdDiferente;   

            i = 0;
            qtdDiferente = false;
            //Verifica se a quandidade de portas mudou
            if (comboPortas.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (comboPortas.Items[i++].Equals(s) == false)
                    {
                        qtdDiferente = true;
                    }
                }
            }
            else
            {
                qtdDiferente = true;
            }

            if (qtdDiferente == false)
            {
                return;  
            }

            comboPortas.Items.Clear();

            //Adiciona as linsta no comboBox
            foreach (string s in SerialPort.GetPortNames())
            {
                comboPortas.Items.Add(s);
            }
            if (primeiro)
            {
                primeiro = false;
                SelecionarDoBanco();
            }
            VerificarConexao();
        }
        private void VerificarConexao()
        {
            if (mainPage.arduino.serialPorta.IsOpen)
            {
                comboPortas.SelectedItem = mainPage.arduino.serialPorta.PortName;
                btnConectar.Text = "Desconectar";
                comboPortas.Enabled = false;
            }
            else
            {
                comboPortas.Enabled = true;
                btnConectar.Text = "Conectar";
                comboPortas.SelectedIndex = comboPortas.Items.Count - 1;
            }
        }
        void conectar()
        {
            if (mainPage.arduino.serialPorta.IsOpen == false)
            {
                try
                {
                    mainPage.arduino.serialPorta.PortName = comboPortas.Items[comboPortas.SelectedIndex].ToString();
                    mainPage.arduino.serialPorta.Open();

                }
                catch
                {
                    return;
                }
            }
            else
            {

                try
                {
                    mainPage.arduino.serialPorta.Close();

                }
                catch
                {
                    return;
                }

            }
            VerificarConexao();
            if (mainPage.arduino.serialPorta.IsOpen)
                this.Close();
        }
        async Task salvarConexao()
        {
            porta = new Porta();
            porta.Texto = comboPortas.SelectedItem.ToString();
            await Program.Database.SalvarPorta(porta);
        }
        private async void btnConectar_Click(object sender, EventArgs e)
        {
            await salvarConexao();
            conectar();
           
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            atualizaListaCOMs();
        }
    }
}
