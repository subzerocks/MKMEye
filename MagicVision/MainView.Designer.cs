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
            this.treasholdBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.blobHigh = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.blobWidth = new System.Windows.Forms.TextBox();
            this.priceBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.targetPic = new System.Windows.Forms.PictureBox();
            this.addToListButton = new System.Windows.Forms.Button();
            this.scanDataView = new System.Windows.Forms.DataGridView();
            this.nextButton = new System.Windows.Forms.Button();
            this.gp1 = new System.Windows.Forms.GroupBox();
            this.deleteFromListButton = new System.Windows.Forms.Button();
            this.exportCSVButton = new System.Windows.Forms.Button();
            this.exportToMKMButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.image_output)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectedCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanDataView)).BeginInit();
            this.gp1.SuspendLayout();
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
            this.label2.Location = new System.Drawing.Point(556, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Detection";
            // 
            // image_output
            // 
            this.image_output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.image_output.Location = new System.Drawing.Point(8, 404);
            this.image_output.Name = "image_output";
            this.image_output.Size = new System.Drawing.Size(142, 100);
            this.image_output.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.image_output.TabIndex = 4;
            this.image_output.TabStop = false;
            // 
            // cam
            // 
            this.cam.Location = new System.Drawing.Point(517, 30);
            this.cam.Name = "cam";
            this.cam.Size = new System.Drawing.Size(33, 37);
            this.cam.TabIndex = 7;
            this.cam.TabStop = false;
            this.cam.Visible = false;
            // 
            // camWindow
            // 
            this.camWindow.Location = new System.Drawing.Point(9, 25);
            this.camWindow.Name = "camWindow";
            this.camWindow.Size = new System.Drawing.Size(541, 374);
            this.camWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.camWindow.TabIndex = 15;
            this.camWindow.TabStop = false;
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(155, 404);
            this.logBox.Margin = new System.Windows.Forms.Padding(2);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(395, 100);
            this.logBox.TabIndex = 17;
            // 
            // addMKMButton
            // 
            this.addMKMButton.BackColor = System.Drawing.Color.GreenYellow;
            this.addMKMButton.Location = new System.Drawing.Point(216, 512);
            this.addMKMButton.Margin = new System.Windows.Forms.Padding(2);
            this.addMKMButton.Name = "addMKMButton";
            this.addMKMButton.Size = new System.Drawing.Size(100, 100);
            this.addMKMButton.TabIndex = 18;
            this.addMKMButton.Text = "Directly List on MKM for Sale (s)";
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
            this.conditionCombo.Location = new System.Drawing.Point(64, 63);
            this.conditionCombo.Margin = new System.Windows.Forms.Padding(2);
            this.conditionCombo.Name = "conditionCombo";
            this.conditionCombo.Size = new System.Drawing.Size(115, 21);
            this.conditionCombo.TabIndex = 19;
            // 
            // langCombo
            // 
            this.langCombo.FormattingEnabled = true;
            this.langCombo.Location = new System.Drawing.Point(64, 38);
            this.langCombo.Margin = new System.Windows.Forms.Padding(2);
            this.langCombo.Name = "langCombo";
            this.langCombo.Size = new System.Drawing.Size(115, 21);
            this.langCombo.TabIndex = 20;
            // 
            // checkMKMButton
            // 
            this.checkMKMButton.BackColor = System.Drawing.Color.Gold;
            this.checkMKMButton.Location = new System.Drawing.Point(8, 512);
            this.checkMKMButton.Margin = new System.Windows.Forms.Padding(2);
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
            this.label3.Location = new System.Drawing.Point(6, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Condition";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Language";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(556, 311);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(103, 13);
            this.nameLabel.TabIndex = 25;
            this.nameLabel.Text = "Detectedcard Name";
            // 
            // detectedCard
            // 
            this.detectedCard.BackColor = System.Drawing.Color.Black;
            this.detectedCard.Location = new System.Drawing.Point(556, 25);
            this.detectedCard.Name = "detectedCard";
            this.detectedCard.Size = new System.Drawing.Size(188, 271);
            this.detectedCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.detectedCard.TabIndex = 26;
            this.detectedCard.TabStop = false;
            // 
            // pidLabel
            // 
            this.pidLabel.AutoSize = true;
            this.pidLabel.Location = new System.Drawing.Point(556, 332);
            this.pidLabel.Name = "pidLabel";
            this.pidLabel.Size = new System.Drawing.Size(93, 13);
            this.pidLabel.TabIndex = 27;
            this.pidLabel.Text = "Detectedcard PID";
            // 
            // editionLabel
            // 
            this.editionLabel.AutoSize = true;
            this.editionLabel.Location = new System.Drawing.Point(556, 355);
            this.editionLabel.Name = "editionLabel";
            this.editionLabel.Size = new System.Drawing.Size(39, 13);
            this.editionLabel.TabIndex = 29;
            this.editionLabel.Text = "Edition";
            // 
            // treasholdBox
            // 
            this.treasholdBox.Location = new System.Drawing.Point(438, 540);
            this.treasholdBox.Name = "treasholdBox";
            this.treasholdBox.Size = new System.Drawing.Size(100, 20);
            this.treasholdBox.TabIndex = 31;
            this.treasholdBox.Text = "100000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(435, 524);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Detection Treashold";
            // 
            // blobHigh
            // 
            this.blobHigh.Location = new System.Drawing.Point(438, 584);
            this.blobHigh.Name = "blobHigh";
            this.blobHigh.Size = new System.Drawing.Size(39, 20);
            this.blobHigh.TabIndex = 34;
            this.blobHigh.Text = "225";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(435, 568);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Blob Height/Width";
            // 
            // blobWidth
            // 
            this.blobWidth.Location = new System.Drawing.Point(499, 584);
            this.blobWidth.Name = "blobWidth";
            this.blobWidth.Size = new System.Drawing.Size(39, 20);
            this.blobWidth.TabIndex = 36;
            this.blobWidth.Text = "125";
            // 
            // priceBox
            // 
            this.priceBox.Location = new System.Drawing.Point(64, 13);
            this.priceBox.Name = "priceBox";
            this.priceBox.Size = new System.Drawing.Size(115, 20);
            this.priceBox.TabIndex = 37;
            this.priceBox.Text = "1982";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 16);
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
            this.targetPic.Size = new System.Drawing.Size(541, 369);
            this.targetPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.targetPic.TabIndex = 39;
            this.targetPic.TabStop = false;
            // 
            // addToListButton
            // 
            this.addToListButton.BackColor = System.Drawing.Color.Aquamarine;
            this.addToListButton.Location = new System.Drawing.Point(320, 512);
            this.addToListButton.Margin = new System.Windows.Forms.Padding(2);
            this.addToListButton.Name = "addToListButton";
            this.addToListButton.Size = new System.Drawing.Size(100, 100);
            this.addToListButton.TabIndex = 40;
            this.addToListButton.Text = "Add to List (l)";
            this.addToListButton.UseVisualStyleBackColor = false;
            this.addToListButton.Click += new System.EventHandler(this.addToListButton_Click);
            // 
            // scanDataView
            // 
            this.scanDataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scanDataView.Location = new System.Drawing.Point(750, 25);
            this.scanDataView.Name = "scanDataView";
            this.scanDataView.Size = new System.Drawing.Size(465, 479);
            this.scanDataView.TabIndex = 41;
            // 
            // nextButton
            // 
            this.nextButton.BackColor = System.Drawing.Color.LightCoral;
            this.nextButton.Location = new System.Drawing.Point(112, 512);
            this.nextButton.Margin = new System.Windows.Forms.Padding(2);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(100, 100);
            this.nextButton.TabIndex = 30;
            this.nextButton.Text = "Check Next possible Item i.e. other Edition (w)";
            this.nextButton.UseVisualStyleBackColor = false;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // gp1
            // 
            this.gp1.Controls.Add(this.label7);
            this.gp1.Controls.Add(this.priceBox);
            this.gp1.Controls.Add(this.label4);
            this.gp1.Controls.Add(this.langCombo);
            this.gp1.Controls.Add(this.conditionCombo);
            this.gp1.Controls.Add(this.label3);
            this.gp1.Location = new System.Drawing.Point(559, 509);
            this.gp1.Name = "gp1";
            this.gp1.Size = new System.Drawing.Size(185, 102);
            this.gp1.TabIndex = 42;
            this.gp1.TabStop = false;
            // 
            // deleteFromListButton
            // 
            this.deleteFromListButton.Location = new System.Drawing.Point(750, 512);
            this.deleteFromListButton.Name = "deleteFromListButton";
            this.deleteFromListButton.Size = new System.Drawing.Size(112, 100);
            this.deleteFromListButton.TabIndex = 43;
            this.deleteFromListButton.Text = "Delete Entry";
            this.deleteFromListButton.UseVisualStyleBackColor = true;
            this.deleteFromListButton.Click += new System.EventHandler(this.deleteFromListButton_Click);
            // 
            // exportCSVButton
            // 
            this.exportCSVButton.Location = new System.Drawing.Point(868, 512);
            this.exportCSVButton.Name = "exportCSVButton";
            this.exportCSVButton.Size = new System.Drawing.Size(105, 99);
            this.exportCSVButton.TabIndex = 44;
            this.exportCSVButton.Text = "Export CSV";
            this.exportCSVButton.UseVisualStyleBackColor = true;
            this.exportCSVButton.Click += new System.EventHandler(this.exportCSVButton_Click);
            // 
            // exportToMKMButton
            // 
            this.exportToMKMButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.exportToMKMButton.Location = new System.Drawing.Point(1110, 512);
            this.exportToMKMButton.Name = "exportToMKMButton";
            this.exportToMKMButton.Size = new System.Drawing.Size(105, 100);
            this.exportToMKMButton.TabIndex = 45;
            this.exportToMKMButton.Text = "Send List to MKM";
            this.exportToMKMButton.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(747, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 46;
            this.label8.Text = "Storage List";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(425, 509);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(125, 102);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 623);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.exportToMKMButton);
            this.Controls.Add(this.exportCSVButton);
            this.Controls.Add(this.deleteFromListButton);
            this.Controls.Add(this.image_output);
            this.Controls.Add(this.gp1);
            this.Controls.Add(this.scanDataView);
            this.Controls.Add(this.addToListButton);
            this.Controls.Add(this.targetPic);
            this.Controls.Add(this.blobWidth);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.blobHigh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.treasholdBox);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.editionLabel);
            this.Controls.Add(this.pidLabel);
            this.Controls.Add(this.detectedCard);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.checkMKMButton);
            this.Controls.Add(this.addMKMButton);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.camWindow);
            this.Controls.Add(this.cam);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainView";
            this.Text = "MKMEye 0.2b";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.image_output)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detectedCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.targetPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanDataView)).EndInit();
            this.gp1.ResumeLayout(false);
            this.gp1.PerformLayout();
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
        private System.Windows.Forms.TextBox treasholdBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox blobHigh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox blobWidth;
        private System.Windows.Forms.TextBox priceBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox targetPic;
        private System.Windows.Forms.Button addToListButton;
        private System.Windows.Forms.DataGridView scanDataView;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.GroupBox gp1;
        private System.Windows.Forms.Button deleteFromListButton;
        private System.Windows.Forms.Button exportCSVButton;
        private System.Windows.Forms.Button exportToMKMButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
