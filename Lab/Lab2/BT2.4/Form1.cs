using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT2._4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Paint += new PaintEventHandler(this.Form1_Paint);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Paint(object sender, EventArgs e)
        {
            Random random = new Random();

            int x = random.Next(this.Width);
            int y = random.Next(this.Height);

            Graphics g = this.CreateGraphics();

            g.DrawString("Paint Event", new Font("Arial", 20), Brushes.Black, x, y);

            g.Dispose();
        }
    }
}
