using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics.Tracing;

namespace BT2._9
{
    public partial class Registration : Form
    {
        //set cứng bộ aesKey và aesIV
        byte[] aesKey = new byte[16] { 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
        byte[] aesIV = new byte[16] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00 };
        public Registration()
        {
            InitializeComponent();
           // this.FormClosing += CloseRegistration;
        }
        //private void CloseRegistration(object sender, FormClosingEventArgs e)
        //{
        //    if (e.CloseReason == CloseReason.UserClosing)
        //    {
        //        Login login = new Login();
        //        login.ShowDialog();
        //    }
        //}

        private void accept_Click(object sender, EventArgs e)
        {
            string uname = username.Text;
            string pword = password.Text;
            string ckpw = checkpw.Text;
            if (!UsernameExists(uname))
            {
                if (ckpw != pword) { MessageBox.Show("Mật khẩu và xác nhận không trùng nhau, hãy nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else
                {
                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string encryptedData = Encrypt(uname + " " + pword);
                    File.AppendAllText("user.txt", encryptedData + Environment.NewLine);
                    this.Close();
                }
            }
            else
                MessageBox.Show("Tên đăng nhập đã tồn tại, vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool UsernameExists(string username)
        {
            string[] lines = File.ReadAllLines("user.txt");
            foreach (var line in lines)
            {
                string[] storedUsername = Decrypt(line).Split(' ');
                if (username == storedUsername[0])
                {
                    return true;
                }
            }
            return false;
        }
        private string Encrypt(string text)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 128;
                aesAlg.Key = aesKey;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.IV = aesIV;
                aesAlg.Padding = PaddingMode.PKCS7;
                byte[] ciphertext;
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor())
                {
                    byte[] plaintextBytes = Encoding.UTF8.GetBytes(text);
                    ciphertext = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
                    return Convert.ToBase64String(ciphertext);
                }
            }
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
                checkpw.UseSystemPasswordChar = false;
                password.PasswordChar = '\0';
                checkpw.PasswordChar = '\0';
            }
            else
            {
                password.UseSystemPasswordChar = true;
                checkpw.UseSystemPasswordChar = true;
                password.PasswordChar = '●';
                checkpw.PasswordChar = '●';
            }

        }
    }
}
