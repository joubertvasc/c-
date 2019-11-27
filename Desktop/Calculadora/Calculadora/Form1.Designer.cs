namespace Calculadora
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbPrimeiroNumero = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSegundoNumero = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbResultado = new System.Windows.Forms.TextBox();
            this.btCalcular = new System.Windows.Forms.Button();
            this.rbSomar = new System.Windows.Forms.RadioButton();
            this.rbSubtrair = new System.Windows.Forms.RadioButton();
            this.rbDividir = new System.Windows.Forms.RadioButton();
            this.rbMultiplicar = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Primeiro número:";
            // 
            // tbPrimeiroNumero
            // 
            this.tbPrimeiroNumero.Location = new System.Drawing.Point(15, 25);
            this.tbPrimeiroNumero.Name = "tbPrimeiroNumero";
            this.tbPrimeiroNumero.Size = new System.Drawing.Size(100, 20);
            this.tbPrimeiroNumero.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Segundo Número:";
            // 
            // tbSegundoNumero
            // 
            this.tbSegundoNumero.Location = new System.Drawing.Point(15, 64);
            this.tbSegundoNumero.Name = "tbSegundoNumero";
            this.tbSegundoNumero.Size = new System.Drawing.Size(100, 20);
            this.tbSegundoNumero.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Resultado:";
            // 
            // tbResultado
            // 
            this.tbResultado.Location = new System.Drawing.Point(15, 147);
            this.tbResultado.Name = "tbResultado";
            this.tbResultado.Size = new System.Drawing.Size(100, 20);
            this.tbResultado.TabIndex = 5;
            // 
            // btCalcular
            // 
            this.btCalcular.Location = new System.Drawing.Point(15, 90);
            this.btCalcular.Name = "btCalcular";
            this.btCalcular.Size = new System.Drawing.Size(75, 23);
            this.btCalcular.TabIndex = 6;
            this.btCalcular.Text = "Calcular";
            this.btCalcular.UseVisualStyleBackColor = true;
            this.btCalcular.Click += new System.EventHandler(this.btCalcular_Click);
            // 
            // rbSomar
            // 
            this.rbSomar.AutoSize = true;
            this.rbSomar.Checked = true;
            this.rbSomar.Location = new System.Drawing.Point(121, 9);
            this.rbSomar.Name = "rbSomar";
            this.rbSomar.Size = new System.Drawing.Size(55, 17);
            this.rbSomar.TabIndex = 7;
            this.rbSomar.TabStop = true;
            this.rbSomar.Text = "Somar";
            this.rbSomar.UseVisualStyleBackColor = true;
            // 
            // rbSubtrair
            // 
            this.rbSubtrair.AutoSize = true;
            this.rbSubtrair.Location = new System.Drawing.Point(121, 32);
            this.rbSubtrair.Name = "rbSubtrair";
            this.rbSubtrair.Size = new System.Drawing.Size(61, 17);
            this.rbSubtrair.TabIndex = 8;
            this.rbSubtrair.TabStop = true;
            this.rbSubtrair.Text = "Subtrair";
            this.rbSubtrair.UseVisualStyleBackColor = true;
            // 
            // rbDividir
            // 
            this.rbDividir.AutoSize = true;
            this.rbDividir.Location = new System.Drawing.Point(121, 55);
            this.rbDividir.Name = "rbDividir";
            this.rbDividir.Size = new System.Drawing.Size(54, 17);
            this.rbDividir.TabIndex = 9;
            this.rbDividir.TabStop = true;
            this.rbDividir.Text = "Dividir";
            this.rbDividir.UseVisualStyleBackColor = true;
            // 
            // rbMultiplicar
            // 
            this.rbMultiplicar.AutoSize = true;
            this.rbMultiplicar.Location = new System.Drawing.Point(121, 78);
            this.rbMultiplicar.Name = "rbMultiplicar";
            this.rbMultiplicar.Size = new System.Drawing.Size(72, 17);
            this.rbMultiplicar.TabIndex = 10;
            this.rbMultiplicar.TabStop = true;
            this.rbMultiplicar.Text = "Multiplicar";
            this.rbMultiplicar.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 174);
            this.Controls.Add(this.rbMultiplicar);
            this.Controls.Add(this.rbDividir);
            this.Controls.Add(this.rbSubtrair);
            this.Controls.Add(this.rbSomar);
            this.Controls.Add(this.btCalcular);
            this.Controls.Add(this.tbResultado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbSegundoNumero);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPrimeiroNumero);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Calculadora do José";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPrimeiroNumero;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSegundoNumero;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbResultado;
        private System.Windows.Forms.Button btCalcular;
        private System.Windows.Forms.RadioButton rbSomar;
        private System.Windows.Forms.RadioButton rbSubtrair;
        private System.Windows.Forms.RadioButton rbDividir;
        private System.Windows.Forms.RadioButton rbMultiplicar;
    }
}

