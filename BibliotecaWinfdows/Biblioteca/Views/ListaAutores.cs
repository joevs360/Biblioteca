﻿using Biblioteca.Models;
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
    public partial class ListaAutores : Form
    {
        Autor autor = new Autor();
        public ListaAutores()
        {
            InitializeComponent();
            if(Program.autores.Count == 0)
            {
                buscarAutores();
            }
            else
            {
                listarAutores();
            }
           
        }
        async Task carregamento(bool carregar, string mensagem = "carregando...")
        {
            txtCarregamento.Text = mensagem;
            panelCarregando.Visible = carregar;
            await Task.Delay(100);
        }

        async void buscarAutores()
        {
            await carregamento(true, "Consultando no banco...");
            Program.autores = await Program.Database.GetAllAutores();
            listarAutores();
            await carregamento(false, "Finalizando...");
        }
        void EscreverQuantidade()
        {
            txtQuantidade.Text = $"{Program.autores.Count} itens";
            if (listView.CheckedItems.Count > 0)
            {
                txtQuantidade.Text += $" | {listView.CheckedItems.Count}";
                if (listView.CheckedItems.Count == 1)
                {
                    txtQuantidade.Text += " selecionado";
                }
                else
                {
                    txtQuantidade.Text += " selecionados";
                }
            }
        }
        void listarAutores(string filtro = "")
        {
            listView.Items.Clear();
            foreach (var item in Program.autores.Where(a => a.Nome.ToUpper().Contains(filtro.ToUpper())))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(item.Id.ToString());
                lvi.SubItems.Add(item.Nome);
                listView.Items.Add(lvi);
            }
            EscreverQuantidade();
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
           listarAutores(txtBusca.Text);
        }

        private async void btnAdicionar_Click(object sender, EventArgs e)
        {
            autor.Nome = txtNome.Text;
            if(await Program.Database.SalvarAutor(autor))
            {
                buscarAutores();
            }
            limpar();
        }
        void limpar()
        {
            txtNome.Text = "";
            autor = new Autor();
            btnAdicionar.Text = "Cadastrar";
        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limpar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnAdicionar.Text = "Alterar";
            autor.Id = int.Parse(listView.CheckedItems[0].SubItems[1].Text);
            autor.Nome = listView.CheckedItems[0].SubItems[2].Text;

            txtNome.Text = autor.Nome;
        }

        private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            btnEditar.Visible = listView.CheckedItems.Count == 1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
