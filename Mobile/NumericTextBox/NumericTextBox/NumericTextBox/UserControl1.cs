using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NumericTextBox
{
    public class numericTextBox : System.Windows.Forms.TextBox
    {
        public numericTextBox(System.Windows.Forms.TextBox toReplace)
        {
            Setup(toReplace);
        }

        private void Setup(System.Windows.Forms.TextBox toReplace)
        {
            this.Location = toReplace.Location;
            this.Size = toReplace.Size;
            this.MaxLength = toReplace.MaxLength;

            toReplace.Parent.Controls.Add(this);

            toReplace.Dispose();
            toReplace = null;
        }

        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {

            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            e.Handled = !char.IsDigit(e.KeyChar);
        }
    }
}
