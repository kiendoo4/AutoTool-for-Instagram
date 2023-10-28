using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT2._8
{
    public partial class FormEdit : Form
    {
        public FormEdit()
        {
            InitializeComponent();
            labelValue.Text = Convert.ToString(trackBarSize.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            labelValue.Text = Convert.ToString(trackBarSize.Value);
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            buttonColor.BackColor = colorDialog1.Color;
        }
    }
}
