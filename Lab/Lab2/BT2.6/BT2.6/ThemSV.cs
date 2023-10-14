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
    public partial class ThemSV : Form
    {
        private string gt;
        public ThemSV()
        {
            InitializeComponent();
        }

        private void ThemSV_Load(object sender, EventArgs e)
        {
            radNam.Checked = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var db = new Database();
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"MSSV", tbMSSV.Text },
                {"Ho", tbHo.Text},
                {"Ten", tbTen.Text },
                {"GioiTinh", gt },
                {"NgaySinh", dtpNgaySinh.Text },
                {"Khoa", tbKhoa.Text },
                {"Lop", tbLop.Text },
                {"SDT", tbSDT.Text }
            };
            try
            {
                db.InsertData(data);
                MessageBox.Show("Thêm/sửa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
        }

        private void lblGioiTinh_Click(object sender, EventArgs e)
        {

        }

        private void radNam_CheckedChanged(object sender, EventArgs e)
        {
            if (radNam.Checked) gt = "Nam";
        }

        private void radNu_CheckedChanged(object sender, EventArgs e)
        {
            if (radNu.Checked) gt = "Nu";
        }

        private void radUnk_CheckedChanged(object sender, EventArgs e)
        {
            if (radUnk.Checked) gt = "Unk";
        }

        private void chkNgaySinh_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
