using Biblioteca.DAO;
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
    public partial class RelatorioRegistroPage : Form
    {
        public RelatorioRegistroPage()
        {
            InitializeComponent();
            comboTipo.SelectedIndex = 0;
            datePickFim.MaxDate = DateTime.Now;
            datePickInicio.MaxDate = DateTime.Now;

        }

        async void AtualizarGraficos()
        {

            await carregamento1.carregar(true);
            List<Registro> registros = new List<Registro>();

            List<Registro> media = new List<Registro>();
            List<Registro> min = new List<Registro>();
            List<Registro> max = new List<Registro>();

            TimeSpan diferenca = datePickFim.Value.Subtract(datePickInicio.Value);
            bool dia = comboTipo.SelectedIndex == 0 || diferenca.Days == 0;

            graficoTemperatura.Series["Temperatura"].IsVisibleInLegend = dia;
            graficoUmidade.Series["Umidade"].IsVisibleInLegend = dia;

            graficoTemperatura.Series["Média"].IsVisibleInLegend = !dia;
            graficoTemperatura.Series["Max"].IsVisibleInLegend = !dia;
            graficoTemperatura.Series["Min"].IsVisibleInLegend = !dia;

            graficoUmidade.Series["Média"].IsVisibleInLegend = !dia;
            graficoUmidade.Series["Max"].IsVisibleInLegend = !dia;
            graficoUmidade.Series["Min"].IsVisibleInLegend = !dia;

            if (dia)
            {
               registros = await new RegistroDAO().GetRegistro(datePickInicio.Value);

               
            }
            else
            {
              
                for (int i = 0; i < diferenca.Days+1; i++)
                {
                    DateTime data = datePickInicio.Value.AddDays(i).Date;
                    List<Registro> list = await new RegistroDAO().GetRegistro(data);

                    if(list.Count > 0)
                    {
                        min.Add(new Registro() { Temperatura = 1000, Umidade = 1000 });
                        max.Add(new Registro());
                        media.Add(new Registro());
                        media[i].DataHora = data;
                        foreach (var item in list)
                        {
                            if (min[i].Temperatura > item.Temperatura)
                                min[i].Temperatura = item.Temperatura;

                            if (max[i].Temperatura < item.Temperatura)
                                max[i].Temperatura = item.Temperatura;

                            media[i].Temperatura += item.Temperatura;


                            if (min[i].Umidade > item.Umidade)
                                min[i].Umidade = item.Umidade;

                            if (max[i].Umidade < item.Umidade)
                                max[i].Umidade = item.Umidade;

                            media[i].Umidade += item.Umidade;
                        }
                        media[i].Temperatura /= list.Count;
                        media[i].Umidade /= list.Count;
                    }
                    else
                    {
                        min.Add(new Registro() { DataHora = data});
                        max.Add(new Registro() { DataHora = data });
                        media.Add(new Registro() { DataHora = data });
                    }
                   
                }
            }

            graficoTemperatura.Series["Temperatura"].Points.Clear();
            graficoUmidade.Series["Umidade"].Points.Clear();

            graficoTemperatura.Series["Média"].Points.Clear();
            graficoTemperatura.Series["Max"].Points.Clear();
            graficoTemperatura.Series["Min"].Points.Clear();

            graficoUmidade.Series["Média"].Points.Clear();
            graficoUmidade.Series["Max"].Points.Clear();
            graficoUmidade.Series["Min"].Points.Clear();

            if (dia)
            {
                foreach (Registro registro in registros)
                {
                    string texto =  registro.Key.ToString();
                    graficoTemperatura.Series["Temperatura"].Points.AddXY(texto, registro.Temperatura);
                    graficoUmidade.Series["Umidade"].Points.AddXY(texto, registro.Umidade);
                }
            }
            else
            {
                for (int i = 0; i < media.Count; i++)
                {
                    string texto = media[i].DataHora.ToString("dd/MM/yyyy");
                    graficoTemperatura.Series["Média"].Points.AddXY(texto, media[i].Temperatura);
                    graficoTemperatura.Series["Max"].Points.AddXY(texto, max[i].Temperatura);
                    graficoTemperatura.Series["Min"].Points.AddXY(texto, min[i].Temperatura);

                    graficoUmidade.Series["Média"].Points.AddXY(texto, media[i].Umidade);
                    graficoUmidade.Series["Max"].Points.AddXY(texto, max[i].Umidade);
                    graficoUmidade.Series["Min"].Points.AddXY(texto, min[i].Umidade);
                }
            }

            await carregamento1.carregar(false);

        }

        private void comboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool dia = comboTipo.SelectedIndex != 0;
            labelDateFim.Visible = dia;
            datePickFim.Visible = dia;
            datelabelInicio.Text = !dia ? "Dia" : "Data Inicio";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            datePickFim.MinDate = datePickInicio.Value;
        }
        private void buttonConsultar_Click(object sender, EventArgs e)
        {
            AtualizarGraficos();
        }
    }
}
