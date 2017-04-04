namespace MagicVision
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
            this.hashCalcButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.image_output = new System.Windows.Forms.PictureBox();
            this.cam = new System.Windows.Forms.PictureBox();
            this.camWindow = new System.Windows.Forms.PictureBox();
            this.logBox = new System.Windows.Forms.TextBox();
            this.startCaptureButton = new System.Windows.Forms.Button();
            this.cardCondition = new System.Windows.Forms.ComboBox();
            this.languageBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.image_output)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // hashCalcButton
            // 
            this.hashCalcButton.Location = new System.Drawing.Point(1320, 520);
            this.hashCalcButton.Margin = new System.Windows.Forms.Padding(6);
            this.hashCalcButton.Name = "hashCalcButton";
            this.hashCalcButton.Size = new System.Drawing.Size(479, 44);
            this.hashCalcButton.TabIndex = 0;
            this.hashCalcButton.Text = "Calculate Hashes from Ref Images";
            this.hashCalcButton.UseVisualStyleBackColor = true;
            this.hashCalcButton.Click += new System.EventHandler(this.hashCalcButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Original Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1342, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Detected";
            // 
            // image_output
            // 
            this.image_output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.image_output.Location = new System.Drawing.Point(1320, 48);
            this.image_output.Margin = new System.Windows.Forms.Padding(6);
            this.image_output.Name = "image_output";
            this.image_output.Size = new System.Drawing.Size(481, 460);
            this.image_output.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.image_output.TabIndex = 4;
            this.image_output.TabStop = false;
            // 
            // cam
            // 
            this.cam.Location = new System.Drawing.Point(1242, 57);
            this.cam.Margin = new System.Windows.Forms.Padding(6);
            this.cam.Name = "cam";
            this.cam.Size = new System.Drawing.Size(66, 71);
            this.cam.TabIndex = 7;
            this.cam.TabStop = false;
            this.cam.Visible = false;
            // 
            // camWindow
            // 
            this.camWindow.Location = new System.Drawing.Point(30, 48);
            this.camWindow.Margin = new System.Windows.Forms.Padding(6);
            this.camWindow.Name = "camWindow";
            this.camWindow.Size = new System.Drawing.Size(1280, 923);
            this.camWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.camWindow.TabIndex = 15;
            this.camWindow.TabStop = false;
            this.camWindow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.camWindow_MouseClick);
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(29, 994);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(1281, 369);
            this.logBox.TabIndex = 17;
            this.logBox.Text = "Ready...";
            // 
            // startCaptureButton
            // 
            this.startCaptureButton.Location = new System.Drawing.Point(1322, 994);
            this.startCaptureButton.Name = "startCaptureButton";
            this.startCaptureButton.Size = new System.Drawing.Size(479, 369);
            this.startCaptureButton.TabIndex = 18;
            this.startCaptureButton.Text = "Add to MKMInventory";
            this.startCaptureButton.UseVisualStyleBackColor = true;
            this.startCaptureButton.Click += new System.EventHandler(this.startCaptureButton_Click);
            // 
            // cardCondition
            // 
            this.cardCondition.FormattingEnabled = true;
            this.cardCondition.Items.AddRange(new object[] {
            "MT",
            "NM",
            "EX",
            "GD",
            "LP",
            "PL",
            "PO"});
            this.cardCondition.Location = new System.Drawing.Point(1322, 937);
            this.cardCondition.Name = "cardCondition";
            this.cardCondition.Size = new System.Drawing.Size(121, 33);
            this.cardCondition.TabIndex = 19;
            // 
            // languageBox
            // 
            this.languageBox.FormattingEnabled = true;
            this.languageBox.Items.AddRange(new object[] {
            "EN",
            "DE",
            "IT",
            "JP",
            "CH",
            "RU",
            "FR",
            "KO",
            "TW",
            "SP",
            "PT"});
            this.languageBox.Location = new System.Drawing.Point(1322, 883);
            this.languageBox.Name = "languageBox";
            this.languageBox.Size = new System.Drawing.Size(121, 33);
            this.languageBox.TabIndex = 20;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1825, 1375);
            this.Controls.Add(this.languageBox);
            this.Controls.Add(this.cardCondition);
            this.Controls.Add(this.startCaptureButton);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.camWindow);
            this.Controls.Add(this.cam);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.image_output);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hashCalcButton);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainView";
            this.Text = "Magic Vision";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.image_output)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camWindow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button hashCalcButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox image_output;
        private System.Windows.Forms.PictureBox cam;
        private System.Windows.Forms.PictureBox camWindow;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Button startCaptureButton;
        private System.Windows.Forms.ComboBox cardCondition;
        private System.Windows.Forms.ComboBox languageBox;
    }
}
