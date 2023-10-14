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
    public partial class XoaSV : Form
    {
        public XoaSV()
        {
            InitializeComponent();
        }

        private void btnXoaSV_Click(object sender, EventArgs e)
        {
            var db = new Database();
            try
            {
                db.DeleteData(tbMSSV.Text);
                MessageBox.Show("Xoá dữ liệu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("MSSV không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
