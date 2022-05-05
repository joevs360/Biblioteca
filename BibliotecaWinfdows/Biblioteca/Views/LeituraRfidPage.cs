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
    public partial class LeituraRfidPage : Form
    {
        MainPage main;
        public string retorno;
        public LeituraRfidPage(MainPage main)
        {
            InitializeComponent();
            this.main = main;
            lerRFID();
        }
        private void mudarStatus(string status, Image image, bool btnR = false)
        {
            txtStatus.Text = status;
            imgStatus.Image = image;
            btnReiniciar.Visible = btnR;
        }
        private async void lerRFID()
        {
            if (await testarArduino())
            {
                //Envia para o arduino iniciar a leitura
                await Task.Delay(100);
                main.arduino.enviarDados("[LI]");
                mudarStatus("Aguardando a leitura do RFID...", Properties.Resources.rfidgif);

                if (await main.lerDados(10000))
                {
                    retorno = main.UltimoDado;
                }
                else
                {
                    mudarStatus("Tempo maximo excedido", Properties.Resources.erro,true);
                }
                
                //Envia para o arduino fechar a leitura
                main.arduino.enviarDados("[LF]");
            }
        }

       
        private async Task<bool> testarArduino()
        {
            mudarStatus("Verificando conexão com o arduino...", Properties.Resources.arduinogif);
            bool resp = false;
            //Envia o inicio do teste
            string teste = "[TI]"+ DateTime.Now.ToString();
            main.arduino.enviarDados(teste);
            //Aguarda a resposta do teste e se é valido 
            teste += "-OK";
            if (await main.lerDados(5000) && teste.Contains(main.UltimoDado.Replace("\n","").Replace("\r", "")))
            {
                resp = true;
            }
            else
            {
                mudarStatus("Não foi possível verificar a conexão\nDado Recebido: "+main.UltimoDado, Properties.Resources.erro, true);
                ConexaoPage conexao = new ConexaoPage(main);
                conexao.Visible = true;
            }
          

            return resp;
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            btnReiniciar.Visible = false;
            lerRFID();
        }

        private void LeituraRfidPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Envia para o arduino fechar a leitura
            main.arduino.enviarDados("[LF]");
            
        }
    }
}
