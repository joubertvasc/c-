using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Orcamento2005
{
    public partial class CalculoJuros : Form
    {
        public CalculoJuros()
        {
            InitializeComponent();
        }

        private String PreencheEspacos(string original, int tamanho, Boolean esquerda)
        {
            string x = original;

            for (int i = original.Length; i < tamanho; i++)
            {
                if (esquerda)
                {
                    x = " " + x;
                }
                else
                {
                    x += " ";
                }
            }

            return x;
        }

        private String ValorFormatado(double valor)
        {
            if (valor == 0)
            {
                return "0,00";
            }
            else
            {
                return valor.ToString("###,000.00");
            }
        }

        private void calcular()
        {
            string sCabecalho = " Taxa   Prest. R$     Total R$";
            double prestacao;
            double valor;
            double vezes;
            double taxa;
            string str;
            double per = 100;
            int i;
            double total;

            limpe();

            ///valor = nuValor.Value;
            valor = System.Convert.ToDouble (nuValor.DecimalValue);

            ///12 meses         
            vezes = 12;
            taxa = System.Convert.ToDouble (nuTaxa.DecimalValue);

            lb12.Items.Add(sCabecalho);
            for (i = 0; i < 21; i++)
            {
                prestacao = valor * (((taxa / per) * Math.Pow(1 + (taxa / per), vezes)) / (Math.Pow(1 + (taxa / per), vezes) - 1));
                total = prestacao * 12;
                str = taxa.ToString("0.00") + "% | " +
                      PreencheEspacos(ValorFormatado((double)prestacao), 9, true) + " | " +
                      PreencheEspacos(ValorFormatado((double)total), 10, true);
                taxa = taxa + 0.10;

                lb12.Items.Add(str);
            }

            ///24 meses         
            vezes = 24;
            taxa = Convert.ToDouble(nuTaxa.Text);

            lb24.Items.Add(sCabecalho);
            for (i = 0; i < 21; i++)
            {
                prestacao = valor * (((taxa / per) * Math.Pow(1 + (taxa / per), vezes)) / (Math.Pow(1 + (taxa / per), vezes) - 1));
                total = prestacao * 24;
                str = taxa.ToString("0.00") + "% | " +
                      PreencheEspacos(ValorFormatado((double)prestacao), 9, true) + " | " +
                      PreencheEspacos(ValorFormatado((double)total), 10, true);
                taxa = taxa + 0.10;

                lb24.Items.Add(str);
            }

            ///36 meses         
            vezes = 36;
            taxa = Convert.ToDouble(nuTaxa.Text);

            lb36.Items.Add(sCabecalho);
            for (i = 0; i < 21; i++)
            {
                prestacao = valor * (((taxa / per) * Math.Pow(1 + (taxa / per), vezes)) / (Math.Pow(1 + (taxa / per), vezes) - 1));
                total = prestacao * 36;
                str = taxa.ToString("0.00") + "% | " +
                      PreencheEspacos(ValorFormatado((double)prestacao), 9, true) + " | " +
                      PreencheEspacos(ValorFormatado((double)total), 10, true);
                taxa = taxa + 0.10;

                lb36.Items.Add(str);
            }

            ///48 meses         
            vezes = 48;
            taxa = Convert.ToDouble(nuTaxa.Text);

            lb48.Items.Add(sCabecalho);
            for (i = 0; i < 21; i++)
            {
                prestacao = valor * (((taxa / per) * Math.Pow(1 + (taxa / per), vezes)) / (Math.Pow(1 + (taxa / per), vezes) - 1));
                total = prestacao * 48;
                str = taxa.ToString("0.00") + "% | " +
                      PreencheEspacos(ValorFormatado((double)prestacao), 9, true) + " | " +
                      PreencheEspacos(ValorFormatado((double)total), 10, true);
                taxa = taxa + 0.10;

                lb48.Items.Add(str);
            }

            ///60 meses         
            vezes = 60;
            taxa = Convert.ToDouble(nuTaxa.Text);

            lb60.Items.Add(sCabecalho);
            for (i = 0; i < 21; i++)
            {
                prestacao = valor * (((taxa / per) * Math.Pow(1 + (taxa / per), vezes)) / (Math.Pow(1 + (taxa / per), vezes) - 1));
                total = prestacao * 60;
                str = taxa.ToString("0.00") + "% | " +
                      PreencheEspacos(ValorFormatado((double)prestacao), 9, true) + " | " +
                      PreencheEspacos(ValorFormatado((double)total), 10, true);
                taxa = taxa + 0.10;

                lb60.Items.Add(str);
            }
        }

        private void CalculoJuros_Load(object sender, EventArgs e)
        {
            limpe();
        }

        private void limpe()
        {
            ///Limpando listas...
            this.lb12.Items.Clear();
            this.lb24.Items.Clear();
            this.lb36.Items.Clear();
            this.lb48.Items.Clear();
            this.lb60.Items.Clear();
        }

        private void miPrincipal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void miCalcular_Click(object sender, EventArgs e)
        {
            calcular();
        }

        private void btCalcular_Click(object sender, EventArgs e)
        {
            calcular();
        }

        private void nuValor_GotFocus(object sender, EventArgs e)
        {
            ipCalculoJuros.Enabled = true;
        }

        private void nuValor_LostFocus(object sender, EventArgs e)
        {
            ipCalculoJuros.Enabled = false;
        }
    }
}