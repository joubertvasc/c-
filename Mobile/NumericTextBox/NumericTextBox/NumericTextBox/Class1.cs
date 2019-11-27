using System;
using System.Collections.Generic;
using System.Text;

namespace NumericTextBox
{
    /// <summary>
    /// Summary description for numericTextBox.
    /// </summary>
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
