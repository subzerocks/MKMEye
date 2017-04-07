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
            this.label2.Location = new System.Drawing.Point(1316, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Detection";
            // 
            // image_output
            // 
            this.image_output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.image_output.Location = new System.Drawing.Point(242, 994);
            this.image_output.Margin = new System.Windows.Forms.Padding(6);
            this.image_output.Name = "image_output";
            this.image_output.Size = new System.Drawing.Size(336, 189);
            this.image_output.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.image_output.TabIndex = 4;
            this.image_output.TabStop = false;
            // 
            // cam
            // 
            this.cam.Location = new System.Drawing.Point(1242, 58);
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
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(590, 994);
            this.logBox.Margin = new System.Windows.Forms.Padding(4);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(526, 189);
            this.logBox.TabIndex = 17;
            // 
            // addMKMButton
            // 
            this.addMKMButton.BackColor = System.Drawing.Color.GreenYellow;
            this.addMKMButton.Location = new System.Drawing.Point(1544, 994);
            this.addMKMButton.Margin = new System.Windows.Forms.Padding(4);
            this.addMKMButton.Name = "addMKMButton";
            this.addMKMButton.Size = new System.Drawing.Size(200, 192);
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
            this.conditionCombo.Location = new System.Drawing.Point(1468, 935);
            this.conditionCombo.Margin = new System.Windows.Forms.Padding(4);
            this.conditionCombo.Name = "conditionCombo";
            this.conditionCombo.Size = new System.Drawing.Size(120, 33);
            this.conditionCombo.TabIndex = 19;
            // 
            // langCombo
            // 
            this.langCombo.FormattingEnabled = true;
            this.langCombo.Location = new System.Drawing.Point(1468, 887);
            this.langCombo.Margin = new System.Windows.Forms.Padding(4);
            this.langCombo.Name = "langCombo";
            this.langCombo.Size = new System.Drawing.Size(120, 33);
            this.langCombo.TabIndex = 20;
            // 
            // checkMKMButton
            // 
            this.checkMKMButton.BackColor = System.Drawing.Color.Gold;
            this.checkMKMButton.Location = new System.Drawing.Point(1128, 994);
            this.checkMKMButton.Margin = new System.Windows.Forms.Padding(4);
            this.checkMKMButton.Name = "checkMKMButton";
            this.checkMKMButton.Size = new System.Drawing.Size(200, 192);
            this.checkMKMButton.TabIndex = 21;
            this.checkMKMButton.Text = "Check on MKM (q)";
            this.checkMKMButton.UseVisualStyleBackColor = false;
            this.checkMKMButton.Click += new System.EventHandler(this.checkMKMButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1322, 940);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 25);
            this.label3.TabIndex = 23;
            this.label3.Text = "Condition";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1322, 892);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 25);
            this.label4.TabIndex = 24;
            this.label4.Text = "Language";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(1322, 652);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(202, 25);
            this.nameLabel.TabIndex = 25;
            this.nameLabel.Text = "Detectedcard Name";
            // 
            // detectedCard
            // 
            this.detectedCard.BackColor = System.Drawing.Color.Black;
            this.detectedCard.Location = new System.Drawing.Point(1320, 48);
            this.detectedCard.Margin = new System.Windows.Forms.Padding(6);
            this.detectedCard.Name = "detectedCard";
            this.detectedCard.Size = new System.Drawing.Size(424, 585);
            this.detectedCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.detectedCard.TabIndex = 26;
            this.detectedCard.TabStop = false;
            // 
            // pidLabel
            // 
            this.pidLabel.AutoSize = true;
            this.pidLabel.Location = new System.Drawing.Point(1322, 692);
            this.pidLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.pidLabel.Name = "pidLabel";
            this.pidLabel.Size = new System.Drawing.Size(180, 25);
            this.pidLabel.TabIndex = 27;
            this.pidLabel.Text = "Detectedcard PID";
            // 
            // editionLabel
            // 
            this.editionLabel.AutoSize = true;
            this.editionLabel.Location = new System.Drawing.Point(1322, 737);
            this.editionLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.editionLabel.Name = "editionLabel";
            this.editionLabel.Size = new System.Drawing.Size(78, 25);
            this.editionLabel.TabIndex = 29;
            this.editionLabel.Text = "Edition";
            // 
            // nextButton
            // 
            this.nextButton.BackColor = System.Drawing.Color.LightCoral;
            this.nextButton.Location = new System.Drawing.Point(1336, 994);
            this.nextButton.Margin = new System.Windows.Forms.Padding(4);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(200, 192);
            this.nextButton.TabIndex = 30;
            this.nextButton.Text = "Check Again / Next (w)";
            this.nextButton.UseVisualStyleBackColor = false;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // treasholdBox
            // 
            this.treasholdBox.Location = new System.Drawing.Point(30, 1025);
            this.treasholdBox.Margin = new System.Windows.Forms.Padding(6);
            this.treasholdBox.Name = "treasholdBox";
            this.treasholdBox.Size = new System.Drawing.Size(196, 31);
            this.treasholdBox.TabIndex = 31;
            this.treasholdBox.Text = "100000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 994);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(205, 25);
            this.label5.TabIndex = 32;
            this.label5.Text = "Detection Treashold";
            // 
            // blobHigh
            // 
            this.blobHigh.Location = new System.Drawing.Point(30, 1110);
            this.blobHigh.Margin = new System.Windows.Forms.Padding(6);
            this.blobHigh.Name = "blobHigh";
            this.blobHigh.Size = new System.Drawing.Size(74, 31);
            this.blobHigh.TabIndex = 34;
            this.blobHigh.Text = "125";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 1079);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 25);
            this.label6.TabIndex = 35;
            this.label6.Text = "Blob Height/Width";
            // 
            // blobWidth
            // 
            this.blobWidth.Location = new System.Drawing.Point(152, 1110);
            this.blobWidth.Margin = new System.Windows.Forms.Padding(6);
            this.blobWidth.Name = "blobWidth";
            this.blobWidth.Size = new System.Drawing.Size(74, 31);
            this.blobWidth.TabIndex = 36;
            this.blobWidth.Text = "125";
            // 
            // priceBox
            // 
            this.priceBox.Location = new System.Drawing.Point(1468, 838);
            this.priceBox.Margin = new System.Windows.Forms.Padding(6);
            this.priceBox.Name = "priceBox";
            this.priceBox.Size = new System.Drawing.Size(120, 31);
            this.priceBox.TabIndex = 37;
            this.priceBox.Text = "1982";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1322, 844);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 25);
            this.label7.TabIndex = 38;
            this.label7.Text = "Price";
            // 
            // targetPic
            // 
            this.targetPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.targetPic.Enabled = false;
            this.targetPic.Image = ((System.Drawing.Image)(resources.GetObject("targetPic.Image")));
            this.targetPic.Location = new System.Drawing.Point(18, 58);
            this.targetPic.Margin = new System.Windows.Forms.Padding(6);
            this.targetPic.Name = "targetPic";
            this.targetPic.Size = new System.Drawing.Size(1280, 923);
            this.targetPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.targetPic.TabIndex = 39;
            this.targetPic.TabStop = false;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1774, 1215);
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
            this.Margin = new System.Windows.Forms.Padding(6);
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
