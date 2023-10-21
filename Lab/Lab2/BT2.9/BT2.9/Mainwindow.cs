using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT2._9
{
    public partial class TextEditor : Form
    {
        public TextEditor()
        {
            InitializeComponent();
            this.Enabled = false;
            Login login = new Login();
            login.Show();
            login.Owner = this;
            login.FormClosed += login_Closed;
        }
        private void login_Closed(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;
        }
        private void TextEditor_Load(object sender, EventArgs e)
        {

        }

        private void ChooseFileText_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Đã chọn file txt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string filePath = openFileDialog.FileName;
                    tenFile.Text = filePath;
                    string fileContent = File.ReadAllText(filePath);
                    richTextBox1.Text = fileContent;
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreateFileText_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    string fileContent = richTextBox1.Text;
                    File.WriteAllText(filePath, fileContent);
                    MessageBox.Show("Đã tạo file", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void SavetxtButton_Click(object sender, EventArgs e)
        {
            if (tenFile.Text == "")
            {
                MessageBox.Show("Chưa chọn file text để thực hiện", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string filePath = tenFile.Text;
                string fileContent = richTextBox1.Text;
                System.IO.File.WriteAllText(filePath, fileContent); 
                MessageBox.Show("Đã lưu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
