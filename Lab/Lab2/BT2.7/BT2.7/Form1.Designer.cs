namespace BT2._7
{
    partial class frmPower
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cdTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radLogOut = new System.Windows.Forms.RadioButton();
            this.radRestart = new System.Windows.Forms.RadioButton();
            this.radShutDown = new System.Windows.Forms.RadioButton();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(100, 23);
            this.lblTitle.TabIndex = 9;
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(255, 9);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(46, 23);
            this.tbTime.TabIndex = 4;
            this.tbTime.Text = "10";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(154, 185);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(147, 62);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel and restart the timer";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nhập thời gian thực hiện chức năng (giây): ";
            // 
            // cdTimer
            // 
            this.cdTimer.Interval = 1000;
            this.cdTimer.Tick += new System.EventHandler(this.cdTimer_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radLogOut);
            this.groupBox1.Controls.Add(this.radRestart);
            this.groupBox1.Controls.Add(this.radShutDown);
            this.groupBox1.Location = new System.Drawing.Point(12, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 56);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Power options:";
            // 
            // radLogOut
            // 
            this.radLogOut.AutoSize = true;
            this.radLogOut.Location = new System.Drawing.Point(223, 22);
            this.radLogOut.Name = "radLogOut";
            this.radLogOut.Size = new System.Drawing.Size(66, 19);
            this.radLogOut.TabIndex = 2;
            this.radLogOut.TabStop = true;
            this.radLogOut.Text = "Log out";
            this.radLogOut.UseVisualStyleBackColor = true;
            // 
            // radRestart
            // 
            this.radRestart.AutoSize = true;
            this.radRestart.Location = new System.Drawing.Point(118, 22);
            this.radRestart.Name = "radRestart";
            this.radRestart.Size = new System.Drawing.Size(61, 19);
            this.radRestart.TabIndex = 1;
            this.radRestart.TabStop = true;
            this.radRestart.Text = "Restart";
            this.radRestart.UseVisualStyleBackColor = true;
            // 
            // radShutDown
            // 
            this.radShutDown.AutoSize = true;
            this.radShutDown.Location = new System.Drawing.Point(6, 22);
            this.radShutDown.Name = "radShutDown";
            this.radShutDown.Size = new System.Drawing.Size(83, 19);
            this.radShutDown.TabIndex = 0;
            this.radShutDown.TabStop = true;
            this.radShutDown.Text = "Shut Down";
            this.radShutDown.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(154, 117);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(147, 62);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "OK";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lblTime
            // 
            this.lblTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTime.Location = new System.Drawing.Point(12, 117);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(136, 123);
            this.lblTime.TabIndex = 10;
            this.lblTime.Text = "10";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmPower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(312, 252);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbTime);
            this.Controls.Add(this.lblTitle);
            this.MaximizeBox = false;
            this.Name = "frmPower";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Power";
            this.Load += new System.EventHandler(this.frmPower_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label lblTitle;
        private TextBox tbTime;
        private Button btnCancel;
        private Label label1;
        private System.Windows.Forms.Timer cdTimer;
        private GroupBox groupBox1;
        private RadioButton radLogOut;
        private RadioButton radRestart;
        private RadioButton radShutDown;
        private Button btnConfirm;
        private Label lblTime;
    }
}