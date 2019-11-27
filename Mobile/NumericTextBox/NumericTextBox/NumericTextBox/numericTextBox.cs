using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace NumericTextBox
{
    /// <summary>
    /// Summary description for numericTextBox.
    /// </summary>
    public class NumericTextBox : System.Windows.Forms.TextBox
    {
        public enum NTBType
        {
            Numbers = 0,
            Integer = 1
        }

        private NTBType type;

        // Restricts the entry of characters to digits (including hex), the negative sign,
        // the decimal point, and editing keystrokes (backspace).
        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            // Verifica se o caracter é um número
            if (Char.IsDigit(e.KeyChar))
            {
                // Se for, ok
            }

            // Vírgula, testa se pode e se já existe
            else if (System.Convert.ToInt16(e.KeyChar) == 44)
            {
                if (this.type == NTBType.Integer || this.Text.IndexOf(",") > 0)
                {
                    e.Handled = true;
                }
            }
            
            // Negativo, verifica se pode e se já existe
            else if (keyInput.Equals(negativeSign))
            {
                if (this.type == NTBType.Integer || this.Text.IndexOf("-") > 0)
                {
                    e.Handled = true;
                }
            }
            
//            else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
//             keyInput.Equals(negativeSign))
//            {
//                // Decimal separator is OK
//            }
            // Backspace
            else if (e.KeyChar == '\b')
            {
                // Backspace  OK
            }
            else
            {
                // Qualquer outra tecla é inválida
                e.Handled = true;
            }
        }

        public NTBType Type
        {
            get {
                return type;
            } 
            set {
                type = value;
            }
        }

        public int IntValue {
            get {
                if (this.Text != "") {
                    return Int32.Parse(this.Text);
                } else {
                    return 0;
                }
            }
            set {
                this.Text = System.Convert.ToString (value);
            }
        }

        public decimal DecimalValue {
            get {
                if (this.Text != "") {
                    return Decimal.Parse(this.Text);
                } else {
                    return 0;
                }
            }
            set {
                this.Text = System.Convert.ToString(value);
            }
        }
    }
}