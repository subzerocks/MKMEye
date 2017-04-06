namespace ImageDBBuilder
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
            this.buildButton = new System.Windows.Forms.Button();
            this.Label = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.TextBox();
            this.pathBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buildButton
            // 
            this.buildButton.BackColor = System.Drawing.Color.DarkOrange;
            this.buildButton.Location = new System.Drawing.Point(12, 399);
            this.buildButton.Name = "buildButton";
            this.buildButton.Size = new System.Drawing.Size(572, 53);
            this.buildButton.TabIndex = 1;
            this.buildButton.Text = "Build new pHash Image DB";
            this.buildButton.UseVisualStyleBackColor = false;
            this.buildButton.Click += new System.EventHandler(this.buildButton_Click);
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Location = new System.Drawing.Point(13, 376);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(29, 13);
            this.Label.TabIndex = 2;
            this.Label.Text = "Path";
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(12, 25);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(572, 342);
            this.logBox.TabIndex = 3;
            // 
            // pathBox
            // 
            this.pathBox.Location = new System.Drawing.Point(48, 373);
            this.pathBox.Name = "pathBox";
            this.pathBox.Size = new System.Drawing.Size(391, 20);
            this.pathBox.TabIndex = 0;
            this.pathBox.Text = "C:\\mtgcropimages\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(572, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "*** ONLY USE THIS TOOL IF YOU KNOW WHAT YOU ARE DOING OR YOU WILL DESTROY YOUR PH" +
    "ASH DB ***";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(445, 371);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(139, 23);
            this.browseButton.TabIndex = 5;
            this.browseButton.Text = "Browse Directory";
            this.browseButton.UseVisualStyleBackColor = true;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 464);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.buildButton);
            this.Controls.Add(this.pathBox);
            this.Name = "MainView";
            this.Text = "ImageDBBuilder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buildButton;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.TextBox pathBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button browseButton;
    }
}

