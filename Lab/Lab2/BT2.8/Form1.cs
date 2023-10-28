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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Tạo vị trí, kích thước và màu ngẫu nhiên cho Graphic
            Random random = new Random();
            
            int x1 = random.Next(0, this.ClientSize.Width);
            int y1 = random.Next(0, this.ClientSize.Height);
            int x2 = random.Next(0, this.ClientSize.Width);
            int y2 = random.Next(0, this.ClientSize.Height);
            int size = random.Next(0, 10);
            Color color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
            //Chọn hình vẽ
            if (comboBox1.Text == "Đường thẳng")
            {
                Graphics g = this.CreateGraphics();
                g.DrawLine(new Pen(color, size), new Point(x1, y1), new Point(x2, y2));
                g.Dispose();
            }
            else if (comboBox1.Text == "Hình chữ nhật") {
                //New install
                Rectangle Rec = new Rectangle(x1, y1, x2, y2);
                Graphics g = this.CreateGraphics();
                g.DrawRectangle(new Pen(color, size), Rec);

                FormEdit edit = new FormEdit();
                edit.ShowDialog();
                DialogResult result = edit.DialogResult;

                if (result == DialogResult.OK)
                {
                    g.DrawRectangle(new Pen(this.BackColor, size), Rec);
                    Rec.Location = new Point(Convert.ToInt32(edit.textBoxX.Text), Convert.ToInt32(edit.textBoxY.Text));
                    Rec.Size = new Size(Convert.ToInt32(edit.textBoxChieuDai.Text), Convert.ToInt32(edit.textBoxChieuRong.Text));
                    g.DrawRectangle(new Pen(edit.buttonColor.BackColor, edit.trackBarSize.Value), Rec);

                }
                //
                g.Dispose();
            }
            else if (comboBox1.Text == "Hình elip")
            {
                //new install
                Rectangle Rec = new Rectangle(x1, y1, x2, y2);
                Graphics g = this.CreateGraphics();
                g.DrawEllipse(new Pen(color, size), Rec);

                FormEdit edit = new FormEdit();
                edit.ShowDialog();
                DialogResult result = edit.DialogResult;

                if (result == DialogResult.OK)
                {
                    g.DrawEllipse(new Pen(this.BackColor, size), Rec);
                    Rec.Location = new Point(Convert.ToInt32(edit.textBoxX.Text), Convert.ToInt32(edit.textBoxY.Text));
                    Rec.Size = new Size(Convert.ToInt32(edit.textBoxChieuDai.Text), Convert.ToInt32(edit.textBoxChieuRong.Text));
                    g.DrawEllipse(new Pen(edit.buttonColor.BackColor, edit.trackBarSize.Value), Rec);

                }
                //
            }
            else
            {
                Graphics g = this.CreateGraphics();
                int x3 = random.Next(0, this.ClientSize.Width);
                int y3 = random.Next(0, this.ClientSize.Height);
                Point[] points = {
                new Point(x1, y1),
                new Point(x2, y2),
                new Point(x3, y3)
                };

                g.DrawPolygon(new Pen(color, size), points);
                g.Dispose();
            }
        }
    }
}
