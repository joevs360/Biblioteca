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

namespace Biblioteca.Views
{
    public partial class ConexaoPage : Form
    {
        MainPage mainPage;
        public ConexaoPage(MainPage mainPage)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            timer.Enabled = true;
            
        }
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
                comboPortas.SelectedIndex = 0;
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
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

        private void timer_Tick(object sender, EventArgs e)
        {
            atualizaListaCOMs();
        }
    }
}
