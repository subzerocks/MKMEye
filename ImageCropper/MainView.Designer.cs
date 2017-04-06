namespace ImageCropper
{
    partial class MainView
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
            this.sourceText = new System.Windows.Forms.TextBox();
            this.targetText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cropButton = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sourceText
            // 
            this.sourceText.Location = new System.Drawing.Point(12, 294);
            this.sourceText.Name = "sourceText";
            this.sourceText.Size = new System.Drawing.Size(515, 20);
            this.sourceText.TabIndex = 0;
            this.sourceText.Text = "E:\\originals";
            // 
            // targetText
            // 
            this.targetText.Location = new System.Drawing.Point(12, 333);
            this.targetText.Name = "targetText";
            this.targetText.Size = new System.Drawing.Size(515, 20);
            this.targetText.TabIndex = 1;
            this.targetText.Text = "E:\\crops";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 317);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Target";
            // 
            // cropButton
            // 
            this.cropButton.BackColor = System.Drawing.Color.Orange;
            this.cropButton.Location = new System.Drawing.Point(12, 360);
            this.cropButton.Name = "cropButton";
            this.cropButton.Size = new System.Drawing.Size(515, 47);
            this.cropButton.TabIndex = 4;
            this.cropButton.Text = "Crop Images";
            this.cropButton.UseVisualStyleBackColor = false;
            this.cropButton.Click += new System.EventHandler(this.cropButton_Click);
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(12, 12);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(515, 263);
            this.logBox.TabIndex = 5;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 416);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.cropButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.targetText);
            this.Controls.Add(this.sourceText);
            this.Name = "MainView";
            this.Text = "ArtCropper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sourceText;
        private System.Windows.Forms.TextBox targetText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cropButton;
        private System.Windows.Forms.TextBox logBox;
    }
}

