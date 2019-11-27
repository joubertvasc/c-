using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btCalcular_Click(object sender, EventArgs e)
        {
            if (rbSomar.Checked)
                tbResultado.Text = System.Convert.ToString(System.Convert.ToDecimal(tbPrimeiroNumero.Text) + System.Convert.ToDecimal(tbSegundoNumero.Text));
            else if (rbSubtrair.Checked)
                tbResultado.Text = System.Convert.ToString(System.Convert.ToDecimal(tbPrimeiroNumero.Text) - System.Convert.ToDecimal(tbSegundoNumero.Text));
            else if (rbMultiplicar.Checked)
                tbResultado.Text = System.Convert.ToString(System.Convert.ToDecimal(tbPrimeiroNumero.Text) * System.Convert.ToDecimal(tbSegundoNumero.Text));
            else if (rbDividir.Checked)
                tbResultado.Text = System.Convert.ToString(System.Convert.ToDecimal(tbPrimeiroNumero.Text) / System.Convert.ToDecimal(tbSegundoNumero.Text));
        }
    }
}
