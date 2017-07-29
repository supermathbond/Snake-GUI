namespace Snake
{
    partial class MyForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.butLoad = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.butHigh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 79);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start Game";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // butLoad
            // 
            this.butLoad.Location = new System.Drawing.Point(12, 95);
            this.butLoad.Name = "butLoad";
            this.butLoad.Size = new System.Drawing.Size(220, 79);
            this.butLoad.TabIndex = 1;
            this.butLoad.Text = "Load Game";
            this.butLoad.UseVisualStyleBackColor = true;
            this.butLoad.Click += new System.EventHandler(this.butLoad_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 265);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(220, 79);
            this.button4.TabIndex = 3;
            this.button4.Text = "Exit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // butHigh
            // 
            this.butHigh.Location = new System.Drawing.Point(12, 180);
            this.butHigh.Name = "butHigh";
            this.butHigh.Size = new System.Drawing.Size(220, 79);
            this.butHigh.TabIndex = 4;
            this.butHigh.Text = "High Scores";
            this.butHigh.UseVisualStyleBackColor = true;
            this.butHigh.Click += new System.EventHandler(this.butHigh_Click);
            // 
            // myForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 358);
            this.Controls.Add(this.butHigh);
            this.Controls.Add(this.butLoad);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button4);
            this.Name = "myForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "myForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button butLoad;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button butHigh;
    }
}