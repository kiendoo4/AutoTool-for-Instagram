namespace BT2._9
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            login_button = new Button();
            label5 = new Label();
            pictureBox1 = new PictureBox();
            label4 = new Label();
            button1 = new Button();
            label3 = new Label();
            label2 = new Label();
            password = new TextBox();
            label1 = new Label();
            username = new TextBox();
            checkBox1 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // login_button
            // 
            login_button.Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point);
            login_button.Location = new Point(571, 379);
            login_button.Name = "login_button";
            login_button.Size = new Size(195, 43);
            login_button.TabIndex = 19;
            login_button.Text = "Đăng nhập";
            login_button.UseVisualStyleBackColor = true;
            login_button.Click += login_button_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Tahoma", 38F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(571, 104);
            label5.Name = "label5";
            label5.Size = new Size(344, 77);
            label5.TabIndex = 18;
            label5.Text = "Đăng nhập";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.output_onlinepngtools__1_;
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(61, -39);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(450, 445);
            pictureBox1.TabIndex = 17;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Tahoma", 20F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(127, 419);
            label4.Name = "label4";
            label4.Size = new Size(294, 41);
            label4.TabIndex = 16;
            label4.Text = "Simple Text Editor";
            // 
            // button1
            // 
            button1.Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(880, 443);
            button1.Name = "button1";
            button1.Size = new Size(154, 45);
            button1.TabIndex = 15;
            button1.Text = "Đăng ký";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(571, 447);
            label3.Name = "label3";
            label3.Size = new Size(303, 36);
            label3.TabIndex = 14;
            label3.Text = "Chưa có tài khoản?";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(571, 280);
            label2.Name = "label2";
            label2.Size = new Size(143, 36);
            label2.TabIndex = 13;
            label2.Text = "Mật khẩu";
            // 
            // password
            // 
            password.Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point);
            password.Location = new Point(774, 280);
            password.Name = "password";
            password.PasswordChar = '●';
            password.Size = new Size(314, 43);
            password.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(571, 221);
            label1.Name = "label1";
            label1.Size = new Size(159, 36);
            label1.TabIndex = 11;
            label1.Text = "Tài khoản";
            // 
            // username
            // 
            username.Font = new Font("Consolas", 18F, FontStyle.Regular, GraphicsUnit.Point);
            username.Location = new Point(774, 215);
            username.Name = "username";
            username.Size = new Size(314, 43);
            username.TabIndex = 10;
            username.TextChanged += username_TextChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Consolas", 14F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox1.Location = new Point(833, 349);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(255, 32);
            checkBox1.TabIndex = 20;
            checkBox1.Text = "Hiển thị mật khẩu";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 553);
            Controls.Add(checkBox1);
            Controls.Add(login_button);
            Controls.Add(label5);
            Controls.Add(pictureBox1);
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(password);
            Controls.Add(label1);
            Controls.Add(username);
            Name = "Login";
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button login_button;
        private Label label5;
        private PictureBox pictureBox1;
        private Label label4;
        private Button button1;
        private Label label3;
        private Label label2;
        private TextBox password;
        private Label label1;
        private TextBox username;
        private CheckBox checkBox1;
    }
}