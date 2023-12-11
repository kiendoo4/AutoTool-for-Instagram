namespace SpaceShooter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureShip = new System.Windows.Forms.PictureBox();
            this.labelPoint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureShip)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureShip
            // 
            this.pictureShip.BackColor = System.Drawing.Color.Transparent;
            this.pictureShip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureShip.BackgroundImage")));
            this.pictureShip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureShip.Location = new System.Drawing.Point(527, 535);
            this.pictureShip.Name = "pictureShip";
            this.pictureShip.Size = new System.Drawing.Size(50, 50);
            this.pictureShip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureShip.TabIndex = 0;
            this.pictureShip.TabStop = false;
            // 
            // labelPoint
            // 
            this.labelPoint.AutoSize = true;
            this.labelPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPoint.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelPoint.Location = new System.Drawing.Point(32, 30);
            this.labelPoint.Name = "labelPoint";
            this.labelPoint.Size = new System.Drawing.Size(64, 69);
            this.labelPoint.TabIndex = 1;
            this.labelPoint.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.labelPoint);
            this.Controls.Add(this.pictureShip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Space shooter";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureShip)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureShip;
        private System.Windows.Forms.Label labelPoint;
    }
}

