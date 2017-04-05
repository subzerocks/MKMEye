namespace MagicVision
{
    partial class OptionsView
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
            this.updateDatabase = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // updateDatabase
            // 
            this.updateDatabase.Location = new System.Drawing.Point(12, 199);
            this.updateDatabase.Name = "updateDatabase";
            this.updateDatabase.Size = new System.Drawing.Size(260, 50);
            this.updateDatabase.TabIndex = 0;
            this.updateDatabase.Text = "Update Image Database";
            this.updateDatabase.UseVisualStyleBackColor = true;
            this.updateDatabase.Click += new System.EventHandler(this.updateDatabase_Click);
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(12, 13);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(260, 180);
            this.logBox.TabIndex = 1;
            // 
            // OptionsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.updateDatabase);
            this.Name = "OptionsView";
            this.Text = "OptionsView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button updateDatabase;
        private System.Windows.Forms.TextBox logBox;
    }
}