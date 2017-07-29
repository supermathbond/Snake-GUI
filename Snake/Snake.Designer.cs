namespace Snake
{
    partial class Snake
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
            this.components = new System.ComponentModel.Container();
            this.ticv = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ticv
            // 
            this.ticv.Interval = 1000;
            this.ticv.Tick += new System.EventHandler(this.ticv_Tick);
            // 
            // Snake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(622, 619);
            this.ControlBox = false;
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "Snake";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Snake";
            this.Load += new System.EventHandler(this.Snake_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Snake_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Snake_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer ticv;
    }
}