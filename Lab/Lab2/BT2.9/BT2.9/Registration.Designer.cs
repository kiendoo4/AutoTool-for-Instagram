namespace BT2._9
{
    partial class Registration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            accept = new Button();
            label4 = new Label();
            checkpw = new TextBox();
            label3 = new Label();
            password = new TextBox();
            label2 = new Label();
            username = new TextBox();
            label1 = new Label();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // accept
            // 
            accept.Font = new Font("Consolas", 17F, FontStyle.Regular, GraphicsUnit.Point);
            accept.Location = new Point(235, 437);
            accept.Name = "accept";
            accept.Size = new Size(189, 44);
            accept.TabIndex = 15;
            accept.Text = "Xác nhận";
            accept.UseVisualStyleBackColor = true;
            accept.Click += accept_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(235, 325);
            label4.Name = "label4";
            label4.Size = new Size(287, 36);
            label4.TabIndex = 14;
            label4.Text = "Nhập lại mật khẩu";
            // 
            // checkpw
            // 
            checkpw.Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point);
            checkpw.Location = new Point(557, 319);
            checkpw.Name = "checkpw";
            checkpw.PasswordChar = '●';
            checkpw.Size = new Size(446, 43);
            checkpw.TabIndex = 13;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(235, 261);
            label3.Name = "label3";
            label3.Size = new Size(143, 36);
            label3.TabIndex = 12;
            label3.Text = "Mật khẩu";
            // 
            // password
            // 
            password.Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point);
            password.Location = new Point(557, 253);
            password.Name = "password";
            password.PasswordChar = '●';
            password.Size = new Size(446, 43);
            password.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(235, 200);
            label2.Name = "label2";
            label2.Size = new Size(159, 36);
            label2.TabIndex = 10;
            label2.Text = "Tài khoản";
            // 
            // username
            // 
            username.Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point);
            username.Location = new Point(557, 193);
            username.Name = "username";
            username.Size = new Size(446, 43);
            username.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 38F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(235, 91);
            label1.Name = "label1";
            label1.Size = new Size(267, 77);
            label1.TabIndex = 8;
            label1.Text = "Đăng ký";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Consolas", 14F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox1.Location = new Point(747, 387);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(255, 32);
            checkBox1.TabIndex = 16;
            checkBox1.Text = "Hiển thị mật khẩu";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // Registration
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 553);
            Controls.Add(checkBox1);
            Controls.Add(accept);
            Controls.Add(label4);
            Controls.Add(checkpw);
            Controls.Add(label3);
            Controls.Add(password);
            Controls.Add(label2);
            Controls.Add(username);
            Controls.Add(label1);
            Name = "Registration";
            Text = "Registration";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button accept;
        private Label label4;
        private TextBox checkpw;
        private Label label3;
        private TextBox password;
        private Label label2;
        private TextBox username;
        private Label label1;
        private CheckBox checkBox1;
    }
}