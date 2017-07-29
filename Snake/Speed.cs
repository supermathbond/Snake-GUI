using System;
using System.Windows.Forms;

namespace Snake
{
    public partial class Speed : Form
    {
        public Speed()
        {
            InitializeComponent();
            cb1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Snake form1 = new Snake(int.Parse(cb1.SelectedItem.ToString()));
            form1.ShowDialog();
            this.Visible = true;
            this.Activate();
            //form1.Dispose();
        }
    }
}
