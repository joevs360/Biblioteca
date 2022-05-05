﻿namespace Biblioteca.Views
{
    partial class CadastroLivro
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.panelUsuario = new System.Windows.Forms.Panel();
            this.panelCarregando = new System.Windows.Forms.Panel();
            this.txtCarregamento = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtISBN = new System.Windows.Forms.TextBox();
            this.txtQtd = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.selectAutor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEditora = new System.Windows.Forms.TextBox();
            this.txtEdicao = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelUsuario.SuspendLayout();
            this.panelCarregando.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQtd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdicao)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(504, 68);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cadastro Livro";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(176, 197);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 4;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // panelUsuario
            // 
            this.panelUsuario.Controls.Add(this.panelCarregando);
            this.panelUsuario.Controls.Add(this.label7);
            this.panelUsuario.Controls.Add(this.txtISBN);
            this.panelUsuario.Controls.Add(this.txtQtd);
            this.panelUsuario.Controls.Add(this.label6);
            this.panelUsuario.Controls.Add(this.selectAutor);
            this.panelUsuario.Controls.Add(this.label5);
            this.panelUsuario.Controls.Add(this.label4);
            this.panelUsuario.Controls.Add(this.txtEditora);
            this.panelUsuario.Controls.Add(this.txtEdicao);
            this.panelUsuario.Controls.Add(this.label2);
            this.panelUsuario.Controls.Add(this.label3);
            this.panelUsuario.Controls.Add(this.txtNome);
            this.panelUsuario.Location = new System.Drawing.Point(12, 74);
            this.panelUsuario.Name = "panelUsuario";
            this.panelUsuario.Size = new System.Drawing.Size(482, 114);
            this.panelUsuario.TabIndex = 3;
            // 
            // panelCarregando
            // 
            this.panelCarregando.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelCarregando.BackColor = System.Drawing.Color.Transparent;
            this.panelCarregando.Controls.Add(this.txtCarregamento);
            this.panelCarregando.Controls.Add(this.pictureBox1);
            this.panelCarregando.Location = new System.Drawing.Point(182, 29);
            this.panelCarregando.Margin = new System.Windows.Forms.Padding(2);
            this.panelCarregando.Name = "panelCarregando";
            this.panelCarregando.Size = new System.Drawing.Size(110, 91);
            this.panelCarregando.TabIndex = 28;
            this.panelCarregando.Visible = false;
            // 
            // txtCarregamento
            // 
            this.txtCarregamento.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtCarregamento.Location = new System.Drawing.Point(0, 51);
            this.txtCarregamento.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtCarregamento.Name = "txtCarregamento";
            this.txtCarregamento.Size = new System.Drawing.Size(110, 40);
            this.txtCarregamento.TabIndex = 18;
            this.txtCarregamento.Text = "Carregando...";
            this.txtCarregamento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::Biblioteca.Properties.Resources.loading;
            this.pictureBox1.Location = new System.Drawing.Point(36, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(294, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Editora";
            // 
            // txtISBN
            // 
            this.txtISBN.Location = new System.Drawing.Point(355, 81);
            this.txtISBN.Name = "txtISBN";
            this.txtISBN.Size = new System.Drawing.Size(100, 20);
            this.txtISBN.TabIndex = 14;
            // 
            // txtQtd
            // 
            this.txtQtd.Location = new System.Drawing.Point(228, 81);
            this.txtQtd.Name = "txtQtd";
            this.txtQtd.Size = new System.Drawing.Size(120, 20);
            this.txtQtd.TabIndex = 13;
            this.txtQtd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(225, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Quantidade";
            // 
            // selectAutor
            // 
            this.selectAutor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.selectAutor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.selectAutor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectAutor.Location = new System.Drawing.Point(9, 81);
            this.selectAutor.Name = "selectAutor";
            this.selectAutor.Size = new System.Drawing.Size(200, 21);
            this.selectAutor.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(6, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Autor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(354, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "ISBN";
            // 
            // txtEditora
            // 
            this.txtEditora.Location = new System.Drawing.Point(297, 29);
            this.txtEditora.Name = "txtEditora";
            this.txtEditora.Size = new System.Drawing.Size(158, 20);
            this.txtEditora.TabIndex = 8;
            // 
            // txtEdicao
            // 
            this.txtEdicao.Location = new System.Drawing.Point(220, 29);
            this.txtEdicao.Name = "txtEdicao";
            this.txtEdicao.Size = new System.Drawing.Size(67, 20);
            this.txtEdicao.TabIndex = 7;
            this.txtEdicao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(217, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Edição";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(3, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Título";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(6, 29);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(203, 20);
            this.txtNome.TabIndex = 4;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(257, 197);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // CadastroLivro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 227);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelUsuario);
            this.MaximumSize = new System.Drawing.Size(520, 266);
            this.MinimumSize = new System.Drawing.Size(520, 266);
            this.Name = "CadastroLivro";
            this.Text = "CadastroLivro";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelUsuario.ResumeLayout(false);
            this.panelUsuario.PerformLayout();
            this.panelCarregando.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQtd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdicao)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Panel panelUsuario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtEdicao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtQtd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox selectAutor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEditora;
        private System.Windows.Forms.Panel panelCarregando;
        private System.Windows.Forms.Label txtCarregamento;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtISBN;
        private System.Windows.Forms.Button btnCancelar;
    }
}