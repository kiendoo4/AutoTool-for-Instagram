using System.Security.Cryptography;

namespace BT2._9
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        byte[] aesKey = new byte[16] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
        byte[] aesIV = new byte[16] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00 };

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            this.Hide();
            registration.ShowDialog();
            this.Close();
        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void login_button_Click(object sender, EventArgs e)
        {
            string uname = username.Text;
            string pword = password.Text;
            if (AccountExists(uname, pword))
            {
                MessageBox.Show("Đăng nhập thành công!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool AccountExists(string uname, string pword)
        {
            string[] lines = File.ReadAllLines("user.txt");
            foreach (var line in lines)
            {
                string[] storedUsername = Decrypt(line).Split(' ');
                if (uname == storedUsername[0] && pword == storedUsername[1])
                {
                    return true;
                }
            }
            return false;
        }
        private string Decrypt(string text)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 128;
                aesAlg.Key = aesKey;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.IV = aesIV;
                aesAlg.Padding = PaddingMode.PKCS7;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor();

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(text)))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                password.UseSystemPasswordChar = false;
                password.PasswordChar = '\0';
            }
            else
            {
                password.UseSystemPasswordChar = true;
                password.PasswordChar = '●';
            }
        }
    }
}