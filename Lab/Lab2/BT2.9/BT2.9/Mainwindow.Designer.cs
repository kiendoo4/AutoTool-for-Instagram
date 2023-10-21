namespace BT2._9
{
    partial class TextEditor
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
            richTextBox1 = new RichTextBox();
            ChooseFileText = new Button();
            CreateTextButton = new Button();
            SavetxtButton = new Button();
            label2 = new Label();
            tenFile = new Label();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox1.Location = new Point(379, 78);
            richTextBox1.Margin = new Padding(3, 2, 3, 2);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(936, 459);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // ChooseFileText
            // 
            ChooseFileText.Font = new Font("Tahoma", 20F, FontStyle.Regular, GraphicsUnit.Point);
            ChooseFileText.Location = new Point(97, 163);
            ChooseFileText.Margin = new Padding(3, 2, 3, 2);
            ChooseFileText.Name = "ChooseFileText";
            ChooseFileText.Size = new Size(216, 40);
            ChooseFileText.TabIndex = 2;
            ChooseFileText.Text = "Chọn file text";
            ChooseFileText.UseVisualStyleBackColor = true;
            ChooseFileText.Click += ChooseFileText_Click;
            // 
            // CreateTextButton
            // 
            CreateTextButton.Font = new Font("Tahoma", 20F, FontStyle.Regular, GraphicsUnit.Point);
            CreateTextButton.Location = new Point(97, 218);
            CreateTextButton.Margin = new Padding(3, 2, 3, 2);
            CreateTextButton.Name = "CreateTextButton";
            CreateTextButton.Size = new Size(216, 40);
            CreateTextButton.TabIndex = 3;
            CreateTextButton.Text = "Tạo file text";
            CreateTextButton.UseVisualStyleBackColor = true;
            CreateTextButton.Click += CreateFileText_Click;
            // 
            // SavetxtButton
            // 
            SavetxtButton.Font = new Font("Tahoma", 20F, FontStyle.Regular, GraphicsUnit.Point);
            SavetxtButton.Location = new Point(97, 272);
            SavetxtButton.Margin = new Padding(3, 2, 3, 2);
            SavetxtButton.Name = "SavetxtButton";
            SavetxtButton.Size = new Size(216, 40);
            SavetxtButton.TabIndex = 4;
            SavetxtButton.Text = "Lưu file text";
            SavetxtButton.UseVisualStyleBackColor = true;
            SavetxtButton.Click += SavetxtButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Tahoma", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(379, 43);
            label2.Name = "label2";
            label2.Size = new Size(115, 24);
            label2.TabIndex = 5;
            label2.Text = "Đường dẫn:";
            // 
            // tenFile
            // 
            tenFile.AutoSize = true;
            tenFile.Font = new Font("Tahoma", 15F, FontStyle.Regular, GraphicsUnit.Point);
            tenFile.Location = new Point(500, 43);
            tenFile.Name = "tenFile";
            tenFile.Size = new Size(0, 24);
            tenFile.TabIndex = 12;
            // 
            // TextEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1348, 565);
            Controls.Add(tenFile);
            Controls.Add(label2);
            Controls.Add(SavetxtButton);
            Controls.Add(CreateTextButton);
            Controls.Add(ChooseFileText);
            Controls.Add(richTextBox1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "TextEditor";
            Text = "TextEditor";
            Load += TextEditor_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button ChooseFileText;
        private Button CreateTextButton;
        private Button SavetxtButton;
        private Label label2;
        private Label tenFile;
    }
}