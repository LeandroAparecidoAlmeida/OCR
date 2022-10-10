namespace RecVogais
{
    partial class TelaPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaPrincipal));
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.botaoClassificar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.botaoTreinar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listaVogais = new System.Windows.Forms.ListBox();
            this.botaoLimpar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.opcaoClassificacao = new System.Windows.Forms.RadioButton();
            this.opcaoTreinamento = new System.Windows.Forms.RadioButton();
            this.barraNotificacao = new System.Windows.Forms.StatusStrip();
            this.textoMensagem = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelVogalIrreconhecivel = new System.Windows.Forms.Label();
            this.barraNotificacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            // 
            // botaoClassificar
            // 
            this.botaoClassificar.Location = new System.Drawing.Point(174, 84);
            this.botaoClassificar.Name = "botaoClassificar";
            this.botaoClassificar.Size = new System.Drawing.Size(100, 23);
            this.botaoClassificar.TabIndex = 2;
            this.botaoClassificar.Text = "Classificar";
            this.botaoClassificar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botaoClassificar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Saída:";
            // 
            // botaoTreinar
            // 
            this.botaoTreinar.Location = new System.Drawing.Point(174, 113);
            this.botaoTreinar.Name = "botaoTreinar";
            this.botaoTreinar.Size = new System.Drawing.Size(100, 23);
            this.botaoTreinar.TabIndex = 7;
            this.botaoTreinar.Text = "Treinar";
            this.botaoTreinar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.botaoTreinar.UseVisualStyleBackColor = true;
            this.botaoTreinar.Click += new System.EventHandler(this.BotaoTreinarClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Entrada (desenhar):";
            // 
            // listaVogais
            // 
            this.listaVogais.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaVogais.FormattingEnabled = true;
            this.listaVogais.ItemHeight = 20;
            this.listaVogais.Items.AddRange(new object[] {
            "a",
            "e",
            "i",
            "o",
            "u"});
            this.listaVogais.Location = new System.Drawing.Point(302, 84);
            this.listaVogais.Name = "listaVogais";
            this.listaVogais.Size = new System.Drawing.Size(110, 104);
            this.listaVogais.TabIndex = 9;
            // 
            // botaoLimpar
            // 
            this.botaoLimpar.Location = new System.Drawing.Point(34, 200);
            this.botaoLimpar.Name = "botaoLimpar";
            this.botaoLimpar.Size = new System.Drawing.Size(114, 23);
            this.botaoLimpar.TabIndex = 10;
            this.botaoLimpar.Text = "Limpar";
            this.botaoLimpar.UseVisualStyleBackColor = true;
            this.botaoLimpar.Click += new System.EventHandler(this.BotaoLimparClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Modo:";
            // 
            // opcaoClassificacao
            // 
            this.opcaoClassificacao.AutoSize = true;
            this.opcaoClassificacao.Checked = true;
            this.opcaoClassificacao.Location = new System.Drawing.Point(63, 10);
            this.opcaoClassificacao.Name = "opcaoClassificacao";
            this.opcaoClassificacao.Size = new System.Drawing.Size(87, 17);
            this.opcaoClassificacao.TabIndex = 12;
            this.opcaoClassificacao.TabStop = true;
            this.opcaoClassificacao.Text = "Classificação";
            this.opcaoClassificacao.UseVisualStyleBackColor = true;
            this.opcaoClassificacao.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // opcaoTreinamento
            // 
            this.opcaoTreinamento.AutoSize = true;
            this.opcaoTreinamento.Location = new System.Drawing.Point(155, 10);
            this.opcaoTreinamento.Name = "opcaoTreinamento";
            this.opcaoTreinamento.Size = new System.Drawing.Size(84, 17);
            this.opcaoTreinamento.TabIndex = 13;
            this.opcaoTreinamento.Text = "Treinamento";
            this.opcaoTreinamento.UseVisualStyleBackColor = true;
            this.opcaoTreinamento.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // barraNotificacao
            // 
            this.barraNotificacao.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textoMensagem});
            this.barraNotificacao.Location = new System.Drawing.Point(0, 254);
            this.barraNotificacao.Name = "barraNotificacao";
            this.barraNotificacao.Size = new System.Drawing.Size(446, 22);
            this.barraNotificacao.TabIndex = 19;
            this.barraNotificacao.Text = "statusStrip";
            // 
            // textoMensagem
            // 
            this.textoMensagem.Name = "textoMensagem";
            this.textoMensagem.Size = new System.Drawing.Size(74, 17);
            this.textoMensagem.Text = "[mensagem]";
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(110, 110);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CaixaPinturaMouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CaixaPinturaMouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CaixaPinturaMouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.opcaoClassificacao);
            this.panel1.Controls.Add(this.opcaoTreinamento);
            this.panel1.Location = new System.Drawing.Point(15, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 40);
            this.panel1.TabIndex = 22;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pictureBox);
            this.panel2.Location = new System.Drawing.Point(35, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(112, 112);
            this.panel2.TabIndex = 23;
            // 
            // labelVogalIrreconhecivel
            // 
            this.labelVogalIrreconhecivel.AutoSize = true;
            this.labelVogalIrreconhecivel.ForeColor = System.Drawing.Color.Red;
            this.labelVogalIrreconhecivel.Location = new System.Drawing.Point(301, 200);
            this.labelVogalIrreconhecivel.Name = "labelVogalIrreconhecivel";
            this.labelVogalIrreconhecivel.Size = new System.Drawing.Size(117, 13);
            this.labelVogalIrreconhecivel.TabIndex = 24;
            this.labelVogalIrreconhecivel.Text = "Vogal não reconhecida";
            // 
            // TelaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 276);
            this.Controls.Add(this.labelVogalIrreconhecivel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barraNotificacao);
            this.Controls.Add(this.botaoLimpar);
            this.Controls.Add(this.listaVogais);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.botaoTreinar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.botaoClassificar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TelaPrincipal";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reconhecimento de Vogais Manuscritas";
            this.barraNotificacao.ResumeLayout(false);
            this.barraNotificacao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        public System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Button botaoClassificar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button botaoTreinar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listaVogais;
        private System.Windows.Forms.Button botaoLimpar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton opcaoClassificacao;
        private System.Windows.Forms.RadioButton opcaoTreinamento;
        private System.Windows.Forms.StatusStrip barraNotificacao;
        private System.Windows.Forms.ToolStripStatusLabel textoMensagem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelVogalIrreconhecivel;
    }
}

