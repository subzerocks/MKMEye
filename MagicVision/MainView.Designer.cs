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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.image_output = new System.Windows.Forms.PictureBox();
            this.cam = new System.Windows.Forms.PictureBox();
            this.camWindow = new System.Windows.Forms.PictureBox();
            this.logBox = new System.Windows.Forms.TextBox();
            this.startCaptureButton = new System.Windows.Forms.Button();
            this.cardCondition = new System.Windows.Forms.ComboBox();
            this.languageBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.optionsButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.detectedCard = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.image_output)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectedCard)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Original Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(658, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Detection";
            // 
            // image_output
            // 
            this.image_output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.image_output.Location = new System.Drawing.Point(660, 25);
            this.image_output.Name = "image_output";
            this.image_output.Size = new System.Drawing.Size(204, 216);
            this.image_output.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.image_output.TabIndex = 4;
            this.image_output.TabStop = false;
            // 
            // cam
            // 
            this.cam.Location = new System.Drawing.Point(621, 30);
            this.cam.Name = "cam";
            this.cam.Size = new System.Drawing.Size(33, 37);
            this.cam.TabIndex = 7;
            this.cam.TabStop = false;
            this.cam.Visible = false;
            // 
            // camWindow
            // 
            this.camWindow.Location = new System.Drawing.Point(15, 25);
            this.camWindow.Name = "camWindow";
            this.camWindow.Size = new System.Drawing.Size(640, 480);
            this.camWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.camWindow.TabIndex = 15;
            this.camWindow.TabStop = false;
            this.camWindow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.camWindow_MouseClick);
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(14, 517);
            this.logBox.Margin = new System.Windows.Forms.Padding(2);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(642, 100);
            this.logBox.TabIndex = 17;
            this.logBox.Text = "Ready...";
            // 
            // startCaptureButton
            // 
            this.startCaptureButton.BackColor = System.Drawing.Color.GreenYellow;
            this.startCaptureButton.Location = new System.Drawing.Point(764, 517);
            this.startCaptureButton.Margin = new System.Windows.Forms.Padding(2);
            this.startCaptureButton.Name = "startCaptureButton";
            this.startCaptureButton.Size = new System.Drawing.Size(100, 100);
            this.startCaptureButton.TabIndex = 18;
            this.startCaptureButton.Text = "Add to MKM";
            this.startCaptureButton.UseVisualStyleBackColor = false;
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
            this.cardCondition.Location = new System.Drawing.Point(734, 486);
            this.cardCondition.Margin = new System.Windows.Forms.Padding(2);
            this.cardCondition.Name = "cardCondition";
            this.cardCondition.Size = new System.Drawing.Size(62, 21);
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
            this.languageBox.Location = new System.Drawing.Point(734, 461);
            this.languageBox.Margin = new System.Windows.Forms.Padding(2);
            this.languageBox.Name = "languageBox";
            this.languageBox.Size = new System.Drawing.Size(62, 21);
            this.languageBox.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gold;
            this.button1.Location = new System.Drawing.Point(660, 517);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 100);
            this.button1.TabIndex = 21;
            this.button1.Text = "Check on MKM";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // optionsButton
            // 
            this.optionsButton.Location = new System.Drawing.Point(839, 483);
            this.optionsButton.Name = "optionsButton";
            this.optionsButton.Size = new System.Drawing.Size(25, 25);
            this.optionsButton.TabIndex = 22;
            this.optionsButton.UseVisualStyleBackColor = true;
            this.optionsButton.Click += new System.EventHandler(this.optionsButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(661, 489);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Condition";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(661, 464);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Language";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(661, 434);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(103, 13);
            this.nameLabel.TabIndex = 25;
            this.nameLabel.Text = "Detectedcard Name";
            // 
            // detectedCard
            // 
            this.detectedCard.Location = new System.Drawing.Point(660, 248);
            this.detectedCard.Name = "detectedCard";
            this.detectedCard.Size = new System.Drawing.Size(204, 183);
            this.detectedCard.TabIndex = 26;
            this.detectedCard.TabStop = false;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 627);
            this.Controls.Add(this.detectedCard);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.optionsButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.languageBox);
            this.Controls.Add(this.cardCondition);
            this.Controls.Add(this.startCaptureButton);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.camWindow);
            this.Controls.Add(this.cam);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.image_output);
            this.Controls.Add(this.label1);
            this.Name = "MainView";
            this.Text = "MKMVision";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.image_output)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectedCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox image_output;
        private System.Windows.Forms.PictureBox cam;
        private System.Windows.Forms.PictureBox camWindow;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Button startCaptureButton;
        private System.Windows.Forms.ComboBox cardCondition;
        private System.Windows.Forms.ComboBox languageBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button optionsButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.PictureBox detectedCard;
    }
}
