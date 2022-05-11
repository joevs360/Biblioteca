namespace Biblioteca.Views
{
    partial class CadastroAluno
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
            this.btnSalvar = new System.Windows.Forms.Button();
            this.panelUsuario = new System.Windows.Forms.Panel();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtRA = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.colData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRFID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.carregamento = new Biblioteca.Views.Carregamento();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnRemover = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelUsuario.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.btnSalvar);
            this.panel1.Controls.Add(this.panelUsuario);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(519, 197);
            this.panel1.TabIndex = 3;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(16, 158);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 28);
            this.btnSalvar.TabIndex = 4;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // panelUsuario
            // 
            this.panelUsuario.Controls.Add(this.txtTelefone);
            this.panelUsuario.Controls.Add(this.label3);
            this.panelUsuario.Controls.Add(this.label5);
            this.panelUsuario.Controls.Add(this.label2);
            this.panelUsuario.Controls.Add(this.txtEmail);
            this.panelUsuario.Controls.Add(this.txtRA);
            this.panelUsuario.Controls.Add(this.label4);
            this.panelUsuario.Controls.Add(this.txtNome);
            this.panelUsuario.Location = new System.Drawing.Point(16, 68);
            this.panelUsuario.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelUsuario.Name = "panelUsuario";
            this.panelUsuario.Size = new System.Drawing.Size(487, 81);
            this.panelUsuario.TabIndex = 3;
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(340, 44);
            this.txtTelefone.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(113, 22);
            this.txtTelefone.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(4, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Nome:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(299, 48);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "Tel:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(336, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "RA:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(57, 44);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(232, 22);
            this.txtEmail.TabIndex = 6;
            // 
            // txtRA
            // 
            this.txtRA.Location = new System.Drawing.Point(377, 12);
            this.txtRA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRA.Name = "txtRA";
            this.txtRA.Size = new System.Drawing.Size(76, 22);
            this.txtRA.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(4, 48);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Email:";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(57, 12);
            this.txtNome.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(269, 22);
            this.txtNome.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cadastro Aluno";
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colData,
            this.colRFID});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(11, 47);
            this.listView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(496, 228);
            this.listView.TabIndex = 4;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // colData
            // 
            this.colData.Text = "Data";
            this.colData.Width = 140;
            // 
            // colRFID
            // 
            this.colRFID.Text = "RFID";
            this.colRFID.Width = 225;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.carregamento);
            this.panel2.Controls.Add(this.btnAdicionar);
            this.panel2.Controls.Add(this.btnRemover);
            this.panel2.Controls.Add(this.listView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 197);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.panel2.Size = new System.Drawing.Size(519, 289);
            this.panel2.TabIndex = 5;
            // 
            // carregamento
            // 
            this.carregamento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.carregamento.Location = new System.Drawing.Point(161, 119);
            this.carregamento.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.carregamento.Name = "carregamento";
            this.carregamento.Size = new System.Drawing.Size(224, 98);
            this.carregamento.TabIndex = 9;
            this.carregamento.Visible = false;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(11, 11);
            this.btnAdicionar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(100, 28);
            this.btnAdicionar.TabIndex = 8;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Visible = false;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // btnRemover
            // 
            this.btnRemover.Location = new System.Drawing.Point(119, 11);
            this.btnRemover.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(100, 28);
            this.btnRemover.TabIndex = 6;
            this.btnRemover.Text = "Remover";
            this.btnRemover.UseVisualStyleBackColor = true;
            this.btnRemover.Visible = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // CadastroAluno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 486);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(533, 499);
            this.Name = "CadastroAluno";
            this.Text = "CadastroAluno";
            this.Load += new System.EventHandler(this.CadastroAluno_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelUsuario.ResumeLayout(false);
            this.panelUsuario.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelUsuario;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtRA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader colData;
        private System.Windows.Forms.ColumnHeader colRFID;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Button btnRemover;
        private Carregamento carregamento;
    }
}