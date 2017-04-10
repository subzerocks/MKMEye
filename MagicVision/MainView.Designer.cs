namespace MKMEye
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.image_output = new System.Windows.Forms.PictureBox();
            this.cam = new System.Windows.Forms.PictureBox();
            this.camWindow = new System.Windows.Forms.PictureBox();
            this.logBox = new System.Windows.Forms.TextBox();
            this.addMKMButton = new System.Windows.Forms.Button();
            this.conditionCombo = new System.Windows.Forms.ComboBox();
            this.langCombo = new System.Windows.Forms.ComboBox();
            this.checkMKMButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.detectedCard = new System.Windows.Forms.PictureBox();
            this.pidLabel = new System.Windows.Forms.Label();
            this.editionLabel = new System.Windows.Forms.Label();
            this.nextButton = new System.Windows.Forms.Button();
            this.treasholdBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.blobHigh = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.blobWidth = new System.Windows.Forms.TextBox();
            this.priceBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.targetPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.image_output)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectedCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetPic)).BeginInit();
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
            this.image_output.Location = new System.Drawing.Point(121, 517);
            this.image_output.Name = "image_output";
            this.image_output.Size = new System.Drawing.Size(169, 99);
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
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(295, 517);
            this.logBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(265, 100);
            this.logBox.TabIndex = 17;
            // 
            // addMKMButton
            // 
            this.addMKMButton.BackColor = System.Drawing.Color.GreenYellow;
            this.addMKMButton.Location = new System.Drawing.Point(772, 517);
            this.addMKMButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.addMKMButton.Name = "addMKMButton";
            this.addMKMButton.Size = new System.Drawing.Size(100, 100);
            this.addMKMButton.TabIndex = 18;
            this.addMKMButton.Text = "Add to MKM (s)";
            this.addMKMButton.UseVisualStyleBackColor = false;
            this.addMKMButton.Click += new System.EventHandler(this.addMKMButton_Click);
            // 
            // conditionCombo
            // 
            this.conditionCombo.FormattingEnabled = true;
            this.conditionCombo.Items.AddRange(new object[] {
            "MT",
            "NM",
            "EX",
            "GD",
            "LP",
            "PL",
            "PO"});
            this.conditionCombo.Location = new System.Drawing.Point(734, 486);
            this.conditionCombo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.conditionCombo.Name = "conditionCombo";
            this.conditionCombo.Size = new System.Drawing.Size(62, 21);
            this.conditionCombo.TabIndex = 19;
            // 
            // langCombo
            // 
            this.langCombo.FormattingEnabled = true;
            this.langCombo.Location = new System.Drawing.Point(734, 461);
            this.langCombo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.langCombo.Name = "langCombo";
            this.langCombo.Size = new System.Drawing.Size(62, 21);
            this.langCombo.TabIndex = 20;
            // 
            // checkMKMButton
            // 
            this.checkMKMButton.BackColor = System.Drawing.Color.Gold;
            this.checkMKMButton.Location = new System.Drawing.Point(564, 517);
            this.checkMKMButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkMKMButton.Name = "checkMKMButton";
            this.checkMKMButton.Size = new System.Drawing.Size(100, 100);
            this.checkMKMButton.TabIndex = 21;
            this.checkMKMButton.Text = "Check on MKM (q)";
            this.checkMKMButton.UseVisualStyleBackColor = false;
            this.checkMKMButton.Click += new System.EventHandler(this.checkMKMButton_Click);
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
            this.nameLabel.Location = new System.Drawing.Point(661, 339);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(103, 13);
            this.nameLabel.TabIndex = 25;
            this.nameLabel.Text = "Detectedcard Name";
            // 
            // detectedCard
            // 
            this.detectedCard.BackColor = System.Drawing.Color.Black;
            this.detectedCard.Location = new System.Drawing.Point(660, 25);
            this.detectedCard.Name = "detectedCard";
            this.detectedCard.Size = new System.Drawing.Size(212, 304);
            this.detectedCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.detectedCard.TabIndex = 26;
            this.detectedCard.TabStop = false;
            // 
            // pidLabel
            // 
            this.pidLabel.AutoSize = true;
            this.pidLabel.Location = new System.Drawing.Point(661, 360);
            this.pidLabel.Name = "pidLabel";
            this.pidLabel.Size = new System.Drawing.Size(93, 13);
            this.pidLabel.TabIndex = 27;
            this.pidLabel.Text = "Detectedcard PID";
            // 
            // editionLabel
            // 
            this.editionLabel.AutoSize = true;
            this.editionLabel.Location = new System.Drawing.Point(661, 383);
            this.editionLabel.Name = "editionLabel";
            this.editionLabel.Size = new System.Drawing.Size(39, 13);
            this.editionLabel.TabIndex = 29;
            this.editionLabel.Text = "Edition";
            // 
            // nextButton
            // 
            this.nextButton.BackColor = System.Drawing.Color.LightCoral;
            this.nextButton.Location = new System.Drawing.Point(668, 517);
            this.nextButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(100, 100);
            this.nextButton.TabIndex = 30;
            this.nextButton.Text = "Check Again / Next (w)";
            this.nextButton.UseVisualStyleBackColor = false;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // treasholdBox
            // 
            this.treasholdBox.Location = new System.Drawing.Point(15, 533);
            this.treasholdBox.Name = "treasholdBox";
            this.treasholdBox.Size = new System.Drawing.Size(100, 20);
            this.treasholdBox.TabIndex = 31;
            this.treasholdBox.Text = "100000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 517);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Detection Treashold";
            // 
            // blobHigh
            // 
            this.blobHigh.Location = new System.Drawing.Point(15, 577);
            this.blobHigh.Name = "blobHigh";
            this.blobHigh.Size = new System.Drawing.Size(39, 20);
            this.blobHigh.TabIndex = 34;
            this.blobHigh.Text = "125";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 561);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Blob Height/Width";
            // 
            // blobWidth
            // 
            this.blobWidth.Location = new System.Drawing.Point(76, 577);
            this.blobWidth.Name = "blobWidth";
            this.blobWidth.Size = new System.Drawing.Size(39, 20);
            this.blobWidth.TabIndex = 36;
            this.blobWidth.Text = "125";
            // 
            // priceBox
            // 
            this.priceBox.Location = new System.Drawing.Point(734, 436);
            this.priceBox.Name = "priceBox";
            this.priceBox.Size = new System.Drawing.Size(62, 20);
            this.priceBox.TabIndex = 37;
            this.priceBox.Text = "1982";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(661, 439);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Price";
            // 
            // targetPic
            // 
            this.targetPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.targetPic.Enabled = false;
            this.targetPic.Image = ((System.Drawing.Image)(resources.GetObject("targetPic.Image")));
            this.targetPic.Location = new System.Drawing.Point(9, 30);
            this.targetPic.Name = "targetPic";
            this.targetPic.Size = new System.Drawing.Size(640, 480);
            this.targetPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.targetPic.TabIndex = 39;
            this.targetPic.TabStop = false;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 623);
            this.Controls.Add(this.targetPic);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.priceBox);
            this.Controls.Add(this.blobWidth);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.blobHigh);
            this.Controls.Add(this.image_output);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.treasholdBox);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.editionLabel);
            this.Controls.Add(this.pidLabel);
            this.Controls.Add(this.detectedCard);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkMKMButton);
            this.Controls.Add(this.langCombo);
            this.Controls.Add(this.conditionCombo);
            this.Controls.Add(this.addMKMButton);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.camWindow);
            this.Controls.Add(this.cam);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainView";
            this.Text = "MKMEye";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.image_output)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectedCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetPic)).EndInit();
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
        private System.Windows.Forms.Button addMKMButton;
        private System.Windows.Forms.ComboBox conditionCombo;
        private System.Windows.Forms.ComboBox langCombo;
        private System.Windows.Forms.Button checkMKMButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.PictureBox detectedCard;
        private System.Windows.Forms.Label pidLabel;
        private System.Windows.Forms.Label editionLabel;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.TextBox treasholdBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox blobHigh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox blobWidth;
        private System.Windows.Forms.TextBox priceBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox targetPic;
    }
}
