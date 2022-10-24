namespace Biblioteca.Views
{
    partial class LeituraRfidPage
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
            this.btnReiniciar = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.Label();
            this.imgStatus = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReiniciar);
            this.panel1.Controls.Add(this.txtStatus);
            this.panel1.Controls.Add(this.imgStatus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(307, 332);
            this.panel1.TabIndex = 1;
            // 
            // btnReiniciar
            // 
            this.btnReiniciar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnReiniciar.Location = new System.Drawing.Point(0, 304);
            this.btnReiniciar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReiniciar.Name = "btnReiniciar";
            this.btnReiniciar.Size = new System.Drawing.Size(307, 28);
            this.btnReiniciar.TabIndex = 2;
            this.btnReiniciar.Text = "Reiniciar";
            this.btnReiniciar.UseVisualStyleBackColor = true;
            this.btnReiniciar.Visible = false;
            this.btnReiniciar.Click += new System.EventHandler(this.btnReiniciar_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtStatus.Location = new System.Drawing.Point(0, 209);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtStatus.MaximumSize = new System.Drawing.Size(315, 105);
            this.txtStatus.MinimumSize = new System.Drawing.Size(315, 105);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(315, 105);
            this.txtStatus.TabIndex = 1;
            this.txtStatus.Text = "Aguardando a leitura do RFID...";
            this.txtStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imgStatus
            // 
            this.imgStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.imgStatus.Image = global::Biblioteca.Properties.Resources.arduino;
            this.imgStatus.Location = new System.Drawing.Point(0, 0);
            this.imgStatus.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.imgStatus.Name = "imgStatus";
            this.imgStatus.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.imgStatus.Size = new System.Drawing.Size(307, 209);
            this.imgStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgStatus.TabIndex = 0;
            this.imgStatus.TabStop = false;
            // 
            // LeituraRfidPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 332);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(325, 379);
            this.MinimumSize = new System.Drawing.Size(325, 379);
            this.Name = "LeituraRfidPage";
            this.Text = "Leitura RFID";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LeituraRfidPage_FormClosed);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label txtStatus;
        private System.Windows.Forms.Button btnReiniciar;
    }
}