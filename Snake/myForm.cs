using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public partial class MyForm : Form
    {
        public MyForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Speed sp = new Speed();
            this.Visible = false;
            sp.ShowDialog();
            sp.Dispose();
            this.Visible = true;
            this.Activate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butLoad_Click(object sender, EventArgs e)
        {

        }

        private void butHowTo_Click(object sender, EventArgs e)
        {

        }

        private void butHigh_Click(object sender, EventArgs e)
        {

        }
    }
}
