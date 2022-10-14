using Biblioteca.Models;
using Biblioteca.Services;
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
        Locacao LocacaoSelecionada;
        int diaMaximo = 5;
        List<Locacao> LocacoesBD;
        public MainPage()
        {
            InitializeComponent();
            arduino.serialPorta = serial;

            carregamento1.Tag = "em aberto";
            carregamento2.Tag = "vencidas";
            IniciarBanco();
        }
        async void IniciarBanco()
        {
            
            if (Program.livros.Count == 0)
            {
                await carregamento1.carregar(true, $"Buscando livros...");
                Program.livros = await Program.Database.GetLivros();
            }
            if (Program.autores.Count == 0)
            {
                await carregamento1.carregar(true, $"Buscando autores...");
                Program.autores = await Program.Database.GetAllAutores();
            }
            BuscarBanco();
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
            UltimoDado = RxString.Replace("\n", "").Replace("\r", "");
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
            EmprestimoCadastroPage emprestimo = new EmprestimoCadastroPage(this);
            emprestimo.ShowDialog();
            BuscarBanco();
        }


        private void AlunosClick(object sender, EventArgs e)
        {
            AlunosPage alunosPage = new AlunosPage(this);
            alunosPage.ShowDialog();
        }

        private void LivrosClick(object sender, EventArgs e)
        {
            ListaLivroPage listaLivroPage = new ListaLivroPage(false);
            listaLivroPage.ShowDialog();
        }
        private void AutoresClick(object sender, EventArgs e)
        {
            ListaAutores listaAutores = new ListaAutores();
            listaAutores.ShowDialog();
        }
        private void RelatorioClick(object sender, EventArgs e)
        {
            RelatorioRegistroPage relatorioRegistroPage = new RelatorioRegistroPage();
            relatorioRegistroPage.ShowDialog();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void alunosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        
        async void BuscarBanco()
        {
            await carregamento1.carregar(true, $"Buscando locações no banco de dados...");
            await carregamento2.carregar(true, $"Buscando locações no banco de dados...");
            LocacoesBD = await Program.Database.GetLocacoes();
            var atrasados = LocacoesBD.Where(l => l.dataInicio.AddDays(diaMaximo) <= DateTime.Now).ToList();
            ListarLocacoes(listView1, LocacoesBD.Where(l => l.dataInicio.AddDays(diaMaximo) > DateTime.Now).ToList(), carregamento1);
            ListarLocacoes(listView2, atrasados, carregamento2);
            await carregamento1.carregar(false, $"Buscando locações no banco de dados...");
            atrasados.ForEach(async l =>
            {
                if (!l.notificado)
                {
                    Usuario usuario = await Program.Database.GetUsuarioByID(l.UsuarioID);
                    Livro livro = await Program.Database.GetLivro(l.LivroID);
                    //Enviar emails 
                    string mensagem = $@"
Olá {usuario.Nome}

A devolução do livro deveria ocorrer até o dia {l.dataInicio.AddDays(diaMaximo).ToString("dd/MM/YYYY")}, porém ainda não consta em nosso sistema, por favor verifique

Titulo: {livro.Nome}

Você deve fazer a renovação até o dia {l.dataInicio.AddDays(diaMaximo + 2).ToString("dd/MM/YYYY")}, ou devolvê-lo em nosso balcão.

Caso a biblioteca não esteja aberta no dia indicado, você deve comparecer no primeiro dia de funcionamento após essa data.

Esta é uma mensagem automática. Por favor, não responda!
";

                    if(EmailService.EnviaEmail(usuario.Email, "Atraso na devolução do livro", mensagem))
                    {
                        l.notificado = true;
                        //atualizar notificação
                        Program.Database.AtualizarLocacao(l);
                    }

                   
                }
            });
            await carregamento2.carregar(true, $"Enviando notificações..");
            await carregamento2.carregar(false, $"Buscando locações no banco de dados...");
        }

        async void ListarLocacoes(ListView list, List<Locacao> locacoes,Carregamento carregamento)
        {
            int qtd = locacoes.Count;
            list.Items.Clear();
            if (qtd > 0)
            {
                for (int i = 0; i < qtd; i++)
                {
                    var item = locacoes[i];
                    await carregamento.carregar(true, $"Carregando locação {carregamento.Tag}: {i + 1} de {qtd}");
                    Livro livro = Program.livros.Where(l => l.ID == item.LivroID).FirstOrDefault();
                    Usuario aluno = await Program.Database.GetUsuarioByID(item.UsuarioID);

                    if (livro == null)
                        livro = new Livro();
                    if (aluno == null)
                        aluno = new Usuario();


                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems[0].Text=aluno.RA.ToString();
                    lvi.SubItems.Add(aluno.Nome);
                    lvi.SubItems.Add(livro.Nome);
                    lvi.SubItems.Add(item.dataInicio.ToString("dd/MM/yyyy"));
                    if (list.Name.Contains('2'))
                    {
                        lvi.SubItems.Add(item.dataInicio.AddDays(diaMaximo).ToString("dd/MM/yyyy"));
                    }

                    lvi.Tag = item;

                    list.Items.Add(lvi);
                }
                await carregamento.carregar(false, $"Finalizando...");
            } 
            
        }
        private void listAbertasView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count == 1)
            {
                
                btnFinalizar.Visible = true;
                LocacaoSelecionada = listView1.SelectedItems[0].Tag as Locacao;
                listView2.SelectedItems.Clear();
            }
            else
            {
                btnFinalizar.Visible = false;
            }
            
        }

        async void DeletarLocacao(Locacao locacao)
        {
            await carregamento1.carregar(false, $"Finalizando a locação");
            await carregamento2.carregar(false, $"Finalizando a locação");
            if( await Program.Database.RemoverLocacao(locacao))
            {
                BuscarBanco();
            }
            await carregamento1.carregar(false, $"Finalizando...");
            await carregamento2.carregar(false, $"Finalizando...");
            
        }
     

        
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            DeletarLocacao(LocacaoSelecionada);
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 1)
            {

                btnFinalizar.Visible = true;
                LocacaoSelecionada = listView2.SelectedItems[0].Tag as Locacao;
                listView1.SelectedItems.Clear();
            }
            else
            {
                btnFinalizar.Visible = false;
            }
        }



        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {

                btnFinalizar.Visible = true;
                LocacaoSelecionada = listView1.SelectedItems[0].Tag as Locacao;
                listView2.SelectedItems.Clear();
            }
            else
            {
                btnFinalizar.Visible = false;
            }
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmailPage emailPage = new EmailPage();
            emailPage.ShowDialog();
        }
    }
}
