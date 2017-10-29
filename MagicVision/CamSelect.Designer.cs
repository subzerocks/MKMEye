namespace MKMEye
{
    partial class CamSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CamSelect));
            this.camsAvailable = new System.Windows.Forms.ComboBox();
            this.selectCamButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // camsAvailable
            // 
            this.camsAvailable.FormattingEnabled = true;
            this.camsAvailable.Location = new System.Drawing.Point(18, 39);
            this.camsAvailable.Name = "camsAvailable";
            this.camsAvailable.Size = new System.Drawing.Size(278, 21);
            this.camsAvailable.TabIndex = 0;
            // 
            // selectCamButton
            // 
            this.selectCamButton.Enabled = false;
            this.selectCamButton.Location = new System.Drawing.Point(18, 66);
            this.selectCamButton.Name = "selectCamButton";
            this.selectCamButton.Size = new System.Drawing.Size(278, 23);
            this.selectCamButton.TabIndex = 2;
            this.selectCamButton.Text = "Select";
            this.selectCamButton.UseVisualStyleBackColor = true;
            this.selectCamButton.Click += new System.EventHandler(this.selectCamButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(281, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Please select the cam which should be used for scanning:";
            // 
            // CamSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 101);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectCamButton);
            this.Controls.Add(this.camsAvailable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CamSelect";
            this.Text = "Select Camera";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox camsAvailable;
        private System.Windows.Forms.Button selectCamButton;
        private System.Windows.Forms.Label label2;
    }
}