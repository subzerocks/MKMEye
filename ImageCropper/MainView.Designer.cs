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
            this.sourceText.Location = new System.Drawing.Point(24, 565);
            this.sourceText.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.sourceText.Name = "sourceText";
            this.sourceText.Size = new System.Drawing.Size(1026, 31);
            this.sourceText.TabIndex = 0;
            this.sourceText.Text = "X:\\cards\\";
            // 
            // targetText
            // 
            this.targetText.Location = new System.Drawing.Point(24, 640);
            this.targetText.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.targetText.Name = "targetText";
            this.targetText.Size = new System.Drawing.Size(1026, 31);
            this.targetText.TabIndex = 1;
            this.targetText.Text = "X:\\crops\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 535);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 610);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Target";
            // 
            // cropButton
            // 
            this.cropButton.BackColor = System.Drawing.Color.Orange;
            this.cropButton.Location = new System.Drawing.Point(24, 692);
            this.cropButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cropButton.Name = "cropButton";
            this.cropButton.Size = new System.Drawing.Size(1030, 90);
            this.cropButton.TabIndex = 4;
            this.cropButton.Text = "Crop Images";
            this.cropButton.UseVisualStyleBackColor = false;
            this.cropButton.Click += new System.EventHandler(this.cropButton_Click);
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(24, 23);
            this.logBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(1026, 502);
            this.logBox.TabIndex = 5;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 800);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.cropButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.targetText);
            this.Controls.Add(this.sourceText);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
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

