using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT2._6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var db = new Database();
            dgvSinhVien.DataSource = db.SelectData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnThemSV_Click(object sender, EventArgs e)
        {
            ThemSV themSV = new ThemSV();
            themSV.Show();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void btnXoaSV_Click(object sender, EventArgs e)
        {
            XoaSV xoaSV = new XoaSV();
            xoaSV.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
