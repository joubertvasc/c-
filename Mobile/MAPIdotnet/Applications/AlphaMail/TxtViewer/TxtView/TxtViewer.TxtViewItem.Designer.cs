namespace TxtView
{
    partial class TxtViewer
    {
    
        partial class TxtViewItem
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
                this.ParentHandler = null;
                this.ItemSelected = null;
                base.Dispose(disposing);
            }

            #region Component Designer generated code

            /// <summary> 
            /// Required method for Designer support - do not modify 
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
                this.SuspendLayout();
                // 
                // TxtViewItem
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
                this.BackColor = System.Drawing.SystemColors.Window;
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
                this.Name = "TxtViewItem";
                this.Size = new System.Drawing.Size(207, 45);
                this.ResumeLayout(false);

            }

            #endregion

        }
    }
}