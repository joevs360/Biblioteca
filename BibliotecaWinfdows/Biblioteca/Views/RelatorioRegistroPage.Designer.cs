﻿namespace Biblioteca.Views
{
    partial class RelatorioRegistroPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.comboTipo = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labelDateFim = new System.Windows.Forms.Label();
            this.datePickFim = new System.Windows.Forms.DateTimePicker();
            this.datelabelInicio = new System.Windows.Forms.Label();
            this.datePickInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.graficoTemperatura = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.graficoUmidade = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.carregamento1 = new Biblioteca.Views.Carregamento();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graficoTemperatura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoUmidade)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.comboTipo);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.labelDateFim);
            this.panel1.Controls.Add(this.datePickFim);
            this.panel1.Controls.Add(this.datelabelInicio);
            this.panel1.Controls.Add(this.datePickInicio);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 126);
            this.panel1.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Tipo";
            // 
            // comboTipo
            // 
            this.comboTipo.FormattingEnabled = true;
            this.comboTipo.Items.AddRange(new object[] {
            "Dia",
            "Periodo"});
            this.comboTipo.Location = new System.Drawing.Point(15, 69);
            this.comboTipo.Name = "comboTipo";
            this.comboTipo.Size = new System.Drawing.Size(121, 21);
            this.comboTipo.TabIndex = 8;
            this.comboTipo.SelectedIndexChanged += new System.EventHandler(this.comboTipo_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Consultar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonConsultar_Click);
            // 
            // labelDateFim
            // 
            this.labelDateFim.AutoSize = true;
            this.labelDateFim.ForeColor = System.Drawing.Color.White;
            this.labelDateFim.Location = new System.Drawing.Point(359, 54);
            this.labelDateFim.Name = "labelDateFim";
            this.labelDateFim.Size = new System.Drawing.Size(49, 13);
            this.labelDateFim.TabIndex = 6;
            this.labelDateFim.Text = "Data Fim";
            // 
            // datePickFim
            // 
            this.datePickFim.Location = new System.Drawing.Point(362, 70);
            this.datePickFim.Name = "datePickFim";
            this.datePickFim.Size = new System.Drawing.Size(211, 20);
            this.datePickFim.TabIndex = 5;
            // 
            // datelabelInicio
            // 
            this.datelabelInicio.AutoSize = true;
            this.datelabelInicio.ForeColor = System.Drawing.Color.White;
            this.datelabelInicio.Location = new System.Drawing.Point(142, 54);
            this.datelabelInicio.Name = "datelabelInicio";
            this.datelabelInicio.Size = new System.Drawing.Size(58, 13);
            this.datelabelInicio.TabIndex = 4;
            this.datelabelInicio.Text = "Data Inicio";
            // 
            // datePickInicio
            // 
            this.datePickInicio.Location = new System.Drawing.Point(142, 70);
            this.datePickInicio.Name = "datePickInicio";
            this.datePickInicio.Size = new System.Drawing.Size(211, 20);
            this.datePickInicio.TabIndex = 1;
            this.datePickInicio.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Relatório";
            // 
            // graficoTemperatura
            // 
            this.graficoTemperatura.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.graficoTemperatura.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.graficoTemperatura.Legends.Add(legend1);
            this.graficoTemperatura.Location = new System.Drawing.Point(3, 3);
            this.graficoTemperatura.Name = "graficoTemperatura";
            this.graficoTemperatura.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.graficoTemperatura.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(110)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(94)))), ((int)(((byte)(94))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(154)))), ((int)(((byte)(224)))))};
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.LegendText = "Temperatura °C";
            series1.Name = "Temperatura";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Média";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Max";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "Min";
            this.graficoTemperatura.Series.Add(series1);
            this.graficoTemperatura.Series.Add(series2);
            this.graficoTemperatura.Series.Add(series3);
            this.graficoTemperatura.Series.Add(series4);
            this.graficoTemperatura.Size = new System.Drawing.Size(900, 300);
            this.graficoTemperatura.TabIndex = 4;
            this.graficoTemperatura.Text = "Temperatura";
            title1.Name = "Title1";
            title1.Text = "Temperatura °C";
            this.graficoTemperatura.Titles.Add(title1);
            // 
            // graficoUmidade
            // 
            this.graficoUmidade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.graficoUmidade.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.graficoUmidade.Legends.Add(legend2);
            this.graficoUmidade.Location = new System.Drawing.Point(3, 309);
            this.graficoUmidade.Name = "graficoUmidade";
            this.graficoUmidade.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.graficoUmidade.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Blue,
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(94)))), ((int)(((byte)(94))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(154)))), ((int)(((byte)(224)))))};
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.LegendText = "Umidade Relativa (%)";
            series5.Name = "Umidade";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Legend = "Legend1";
            series6.Name = "Média";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Legend = "Legend1";
            series7.Name = "Max";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Legend = "Legend1";
            series8.Name = "Min";
            this.graficoUmidade.Series.Add(series5);
            this.graficoUmidade.Series.Add(series6);
            this.graficoUmidade.Series.Add(series7);
            this.graficoUmidade.Series.Add(series8);
            this.graficoUmidade.Size = new System.Drawing.Size(900, 300);
            this.graficoUmidade.TabIndex = 5;
            this.graficoUmidade.Text = "Umidade";
            title2.Name = "Title1";
            title2.Text = "Umidade (%)";
            this.graficoUmidade.Titles.Add(title2);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.graficoTemperatura);
            this.flowLayoutPanel1.Controls.Add(this.graficoUmidade);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 133);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(909, 616);
            this.flowLayoutPanel1.TabIndex = 20;
            // 
            // carregamento1
            // 
            this.carregamento1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.carregamento1.Location = new System.Drawing.Point(378, 442);
            this.carregamento1.Margin = new System.Windows.Forms.Padding(4);
            this.carregamento1.Name = "carregamento1";
            this.carregamento1.Size = new System.Drawing.Size(160, 148);
            this.carregamento1.TabIndex = 19;
            this.carregamento1.Visible = false;
            // 
            // RelatorioRegistroPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 761);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.carregamento1);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(950, 800);
            this.Name = "RelatorioRegistroPage";
            this.Text = "RelatorioRegistroPage";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graficoTemperatura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graficoUmidade)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker datePickInicio;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelDateFim;
        private System.Windows.Forms.DateTimePicker datePickFim;
        private System.Windows.Forms.Label datelabelInicio;
        private System.Windows.Forms.DataVisualization.Charting.Chart graficoTemperatura;
        private System.Windows.Forms.DataVisualization.Charting.Chart graficoUmidade;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboTipo;
        private Carregamento carregamento1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}