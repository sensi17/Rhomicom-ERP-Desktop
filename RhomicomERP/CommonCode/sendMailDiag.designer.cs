namespace CommonCode
{
  partial class sendMailDiag
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sendMailDiag));
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.toTextBox = new System.Windows.Forms.TextBox();
      this.ccTextBox = new System.Windows.Forms.TextBox();
      this.subjTextBox = new System.Windows.Forms.TextBox();
      this.attchMntsTextBox = new System.Windows.Forms.TextBox();
      this.browseButton = new System.Windows.Forms.Button();
      this.bccTextBox = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.mailLabel = new System.Windows.Forms.Label();
      this.cstmrSiteButton = new System.Windows.Forms.Button();
      this.cstmrButton = new System.Windows.Forms.Button();
      this.cstmrSiteTextBox = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.cstmrNmTextBox = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.cstmrIDTextBox = new System.Windows.Forms.TextBox();
      this.cstmrSiteIDTextBox = new System.Windows.Forms.TextBox();
      this.grpComboBox = new System.Windows.Forms.ComboBox();
      this.grpNmTextBox = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.grpNmButton = new System.Windows.Forms.Button();
      this.label11 = new System.Windows.Forms.Label();
      this.grpNmIDTextBox = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.sendIndvdllyCheckBox = new System.Windows.Forms.CheckBox();
      this.button5 = new System.Windows.Forms.Button();
      this.bccButton = new System.Windows.Forms.Button();
      this.ccButton = new System.Windows.Forms.Button();
      this.toButton = new System.Windows.Forms.Button();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.bodyTextBox = new RicherTextBox.RicherTextBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.msgTypComboBox = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(8, 139);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(23, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "To:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.ForeColor = System.Drawing.Color.White;
      this.label3.Location = new System.Drawing.Point(8, 222);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(23, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Cc:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.ForeColor = System.Drawing.Color.White;
      this.label4.Location = new System.Drawing.Point(8, 17);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(47, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "Subject:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.ForeColor = System.Drawing.Color.White;
      this.label5.Location = new System.Drawing.Point(8, 41);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(72, 13);
      this.label5.TabIndex = 4;
      this.label5.Text = "Attachments:";
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(222, 437);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 11;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Location = new System.Drawing.Point(160, 437);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(62, 23);
      this.okButton.TabIndex = 10;
      this.okButton.Text = "Send";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // toTextBox
      // 
      this.toTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.toTextBox.Location = new System.Drawing.Point(105, 135);
      this.toTextBox.MaxLength = 2000000000;
      this.toTextBox.Multiline = true;
      this.toTextBox.Name = "toTextBox";
      this.toTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.toTextBox.Size = new System.Drawing.Size(314, 81);
      this.toTextBox.TabIndex = 13;
      this.toTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toTextBox_KeyDown);
      // 
      // ccTextBox
      // 
      this.ccTextBox.Location = new System.Drawing.Point(105, 218);
      this.ccTextBox.MaxLength = 2000000000;
      this.ccTextBox.Multiline = true;
      this.ccTextBox.Name = "ccTextBox";
      this.ccTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.ccTextBox.Size = new System.Drawing.Size(314, 52);
      this.ccTextBox.TabIndex = 14;
      this.ccTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toTextBox_KeyDown);
      // 
      // subjTextBox
      // 
      this.subjTextBox.Location = new System.Drawing.Point(105, 13);
      this.subjTextBox.Name = "subjTextBox";
      this.subjTextBox.Size = new System.Drawing.Size(314, 21);
      this.subjTextBox.TabIndex = 15;
      // 
      // attchMntsTextBox
      // 
      this.attchMntsTextBox.Location = new System.Drawing.Point(105, 39);
      this.attchMntsTextBox.Multiline = true;
      this.attchMntsTextBox.Name = "attchMntsTextBox";
      this.attchMntsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.attchMntsTextBox.Size = new System.Drawing.Size(246, 40);
      this.attchMntsTextBox.TabIndex = 16;
      this.attchMntsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toTextBox_KeyDown);
      // 
      // browseButton
      // 
      this.browseButton.ForeColor = System.Drawing.Color.Black;
      this.browseButton.Location = new System.Drawing.Point(357, 41);
      this.browseButton.Name = "browseButton";
      this.browseButton.Size = new System.Drawing.Size(62, 38);
      this.browseButton.TabIndex = 17;
      this.browseButton.Text = "Browse...";
      this.browseButton.UseVisualStyleBackColor = true;
      this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
      // 
      // bccTextBox
      // 
      this.bccTextBox.Location = new System.Drawing.Point(105, 273);
      this.bccTextBox.MaxLength = 2000000000;
      this.bccTextBox.Multiline = true;
      this.bccTextBox.Name = "bccTextBox";
      this.bccTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.bccTextBox.Size = new System.Drawing.Size(314, 66);
      this.bccTextBox.TabIndex = 20;
      this.bccTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toTextBox_KeyDown);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.ForeColor = System.Drawing.Color.White;
      this.label7.Location = new System.Drawing.Point(8, 277);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(27, 13);
      this.label7.TabIndex = 19;
      this.label7.Text = "Bcc:";
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // mailLabel
      // 
      this.mailLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.mailLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
      this.mailLabel.Font = new System.Drawing.Font("Times New Roman", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.mailLabel.ForeColor = System.Drawing.Color.White;
      this.mailLabel.Location = new System.Drawing.Point(231, 228);
      this.mailLabel.Name = "mailLabel";
      this.mailLabel.Size = new System.Drawing.Size(627, 60);
      this.mailLabel.TabIndex = 67;
      this.mailLabel.Text = "Sending Email.....Please Wait.....";
      this.mailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.mailLabel.Visible = false;
      // 
      // cstmrSiteButton
      // 
      this.cstmrSiteButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cstmrSiteButton.ForeColor = System.Drawing.Color.Black;
      this.cstmrSiteButton.Location = new System.Drawing.Point(256, 110);
      this.cstmrSiteButton.Name = "cstmrSiteButton";
      this.cstmrSiteButton.Size = new System.Drawing.Size(28, 23);
      this.cstmrSiteButton.TabIndex = 199;
      this.cstmrSiteButton.Text = "...";
      this.cstmrSiteButton.UseVisualStyleBackColor = true;
      this.cstmrSiteButton.Click += new System.EventHandler(this.cstmrSiteButton_Click);
      // 
      // cstmrButton
      // 
      this.cstmrButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cstmrButton.ForeColor = System.Drawing.Color.Black;
      this.cstmrButton.Location = new System.Drawing.Point(256, 86);
      this.cstmrButton.Name = "cstmrButton";
      this.cstmrButton.Size = new System.Drawing.Size(28, 23);
      this.cstmrButton.TabIndex = 197;
      this.cstmrButton.Text = "...";
      this.cstmrButton.UseVisualStyleBackColor = true;
      this.cstmrButton.Click += new System.EventHandler(this.cstmrButton_Click);
      // 
      // cstmrSiteTextBox
      // 
      this.cstmrSiteTextBox.Location = new System.Drawing.Point(105, 111);
      this.cstmrSiteTextBox.MaxLength = 200;
      this.cstmrSiteTextBox.Name = "cstmrSiteTextBox";
      this.cstmrSiteTextBox.ReadOnly = true;
      this.cstmrSiteTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.cstmrSiteTextBox.Size = new System.Drawing.Size(151, 21);
      this.cstmrSiteTextBox.TabIndex = 198;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.ForeColor = System.Drawing.Color.White;
      this.label8.Location = new System.Drawing.Point(8, 115);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(82, 13);
      this.label8.TabIndex = 204;
      this.label8.Text = "Workplace Site:";
      // 
      // cstmrNmTextBox
      // 
      this.cstmrNmTextBox.Location = new System.Drawing.Point(105, 87);
      this.cstmrNmTextBox.MaxLength = 200;
      this.cstmrNmTextBox.Name = "cstmrNmTextBox";
      this.cstmrNmTextBox.ReadOnly = true;
      this.cstmrNmTextBox.Size = new System.Drawing.Size(151, 21);
      this.cstmrNmTextBox.TabIndex = 196;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.ForeColor = System.Drawing.Color.White;
      this.label9.Location = new System.Drawing.Point(8, 91);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(91, 13);
      this.label9.TabIndex = 203;
      this.label9.Text = "Workplace Name:";
      // 
      // cstmrIDTextBox
      // 
      this.cstmrIDTextBox.Location = new System.Drawing.Point(203, 87);
      this.cstmrIDTextBox.MaxLength = 200;
      this.cstmrIDTextBox.Name = "cstmrIDTextBox";
      this.cstmrIDTextBox.ReadOnly = true;
      this.cstmrIDTextBox.Size = new System.Drawing.Size(30, 21);
      this.cstmrIDTextBox.TabIndex = 205;
      this.cstmrIDTextBox.TabStop = false;
      this.cstmrIDTextBox.Text = "-1";
      // 
      // cstmrSiteIDTextBox
      // 
      this.cstmrSiteIDTextBox.Location = new System.Drawing.Point(203, 111);
      this.cstmrSiteIDTextBox.MaxLength = 200;
      this.cstmrSiteIDTextBox.Name = "cstmrSiteIDTextBox";
      this.cstmrSiteIDTextBox.ReadOnly = true;
      this.cstmrSiteIDTextBox.Size = new System.Drawing.Size(30, 21);
      this.cstmrSiteIDTextBox.TabIndex = 206;
      this.cstmrSiteIDTextBox.TabStop = false;
      this.cstmrSiteIDTextBox.Text = "-1";
      // 
      // grpComboBox
      // 
      this.grpComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.grpComboBox.FormattingEnabled = true;
      this.grpComboBox.Items.AddRange(new object[] {
            "Everyone",
            "Divisions/Groups",
            "Grade",
            "Job",
            "Position",
            "Site/Location",
            "Person Type",
            "Single Person",
            "Companies/Institutions"});
      this.grpComboBox.Location = new System.Drawing.Point(105, 39);
      this.grpComboBox.Name = "grpComboBox";
      this.grpComboBox.Size = new System.Drawing.Size(178, 21);
      this.grpComboBox.TabIndex = 193;
      this.grpComboBox.SelectedIndexChanged += new System.EventHandler(this.grpComboBox_SelectedIndexChanged);
      // 
      // grpNmTextBox
      // 
      this.grpNmTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.grpNmTextBox.Location = new System.Drawing.Point(105, 63);
      this.grpNmTextBox.Multiline = true;
      this.grpNmTextBox.Name = "grpNmTextBox";
      this.grpNmTextBox.Size = new System.Drawing.Size(151, 21);
      this.grpNmTextBox.TabIndex = 194;
      this.grpNmTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toTextBox_KeyDown);
      // 
      // label10
      // 
      this.label10.ForeColor = System.Drawing.Color.White;
      this.label10.Location = new System.Drawing.Point(8, 41);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(72, 17);
      this.label10.TabIndex = 202;
      this.label10.Text = "Group Type:";
      // 
      // grpNmButton
      // 
      this.grpNmButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grpNmButton.ForeColor = System.Drawing.Color.Black;
      this.grpNmButton.Location = new System.Drawing.Point(256, 63);
      this.grpNmButton.Name = "grpNmButton";
      this.grpNmButton.Size = new System.Drawing.Size(28, 21);
      this.grpNmButton.TabIndex = 195;
      this.grpNmButton.Text = "...";
      this.grpNmButton.UseVisualStyleBackColor = true;
      this.grpNmButton.Click += new System.EventHandler(this.grpNmButton_Click);
      // 
      // label11
      // 
      this.label11.ForeColor = System.Drawing.Color.White;
      this.label11.Location = new System.Drawing.Point(8, 65);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(72, 17);
      this.label11.TabIndex = 200;
      this.label11.Text = "Group Name:";
      // 
      // grpNmIDTextBox
      // 
      this.grpNmIDTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grpNmIDTextBox.ForeColor = System.Drawing.Color.Black;
      this.grpNmIDTextBox.Location = new System.Drawing.Point(228, 63);
      this.grpNmIDTextBox.Name = "grpNmIDTextBox";
      this.grpNmIDTextBox.ReadOnly = true;
      this.grpNmIDTextBox.Size = new System.Drawing.Size(27, 21);
      this.grpNmIDTextBox.TabIndex = 201;
      this.grpNmIDTextBox.TabStop = false;
      this.grpNmIDTextBox.Text = "-1";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.msgTypComboBox);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.sendIndvdllyCheckBox);
      this.groupBox1.Controls.Add(this.button5);
      this.groupBox1.Controls.Add(this.bccButton);
      this.groupBox1.Controls.Add(this.ccButton);
      this.groupBox1.Controls.Add(this.toButton);
      this.groupBox1.Controls.Add(this.grpNmTextBox);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.cstmrSiteButton);
      this.groupBox1.Controls.Add(this.cstmrButton);
      this.groupBox1.Controls.Add(this.toTextBox);
      this.groupBox1.Controls.Add(this.cstmrSiteTextBox);
      this.groupBox1.Controls.Add(this.ccTextBox);
      this.groupBox1.Controls.Add(this.label8);
      this.groupBox1.Controls.Add(this.label7);
      this.groupBox1.Controls.Add(this.cstmrNmTextBox);
      this.groupBox1.Controls.Add(this.bccTextBox);
      this.groupBox1.Controls.Add(this.label9);
      this.groupBox1.Controls.Add(this.grpNmIDTextBox);
      this.groupBox1.Controls.Add(this.cstmrIDTextBox);
      this.groupBox1.Controls.Add(this.label11);
      this.groupBox1.Controls.Add(this.cstmrSiteIDTextBox);
      this.groupBox1.Controls.Add(this.grpNmButton);
      this.groupBox1.Controls.Add(this.grpComboBox);
      this.groupBox1.Controls.Add(this.label10);
      this.groupBox1.ForeColor = System.Drawing.Color.White;
      this.groupBox1.Location = new System.Drawing.Point(3, 2);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(425, 343);
      this.groupBox1.TabIndex = 208;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Destination Group/Addresses";
      // 
      // sendIndvdllyCheckBox
      // 
      this.sendIndvdllyCheckBox.Location = new System.Drawing.Point(287, 17);
      this.sendIndvdllyCheckBox.Name = "sendIndvdllyCheckBox";
      this.sendIndvdllyCheckBox.Size = new System.Drawing.Size(134, 43);
      this.sendIndvdllyCheckBox.TabIndex = 211;
      this.sendIndvdllyCheckBox.Text = "Send Mails to Email Addresses Individually and NOT in Groups!";
      this.sendIndvdllyCheckBox.UseVisualStyleBackColor = true;
      // 
      // button5
      // 
      this.button5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button5.ForeColor = System.Drawing.Color.Black;
      this.button5.Location = new System.Drawing.Point(290, 87);
      this.button5.Name = "button5";
      this.button5.Size = new System.Drawing.Size(129, 46);
      this.button5.TabIndex = 210;
      this.button5.Text = "Auto-Load Qualifying Emails";
      this.button5.UseVisualStyleBackColor = true;
      this.button5.Click += new System.EventHandler(this.button5_Click);
      // 
      // bccButton
      // 
      this.bccButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.bccButton.ForeColor = System.Drawing.Color.Black;
      this.bccButton.Location = new System.Drawing.Point(72, 272);
      this.bccButton.Name = "bccButton";
      this.bccButton.Size = new System.Drawing.Size(32, 26);
      this.bccButton.TabIndex = 209;
      this.bccButton.Text = "-->";
      this.bccButton.UseVisualStyleBackColor = true;
      this.bccButton.Click += new System.EventHandler(this.bccButton_Click);
      // 
      // ccButton
      // 
      this.ccButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ccButton.ForeColor = System.Drawing.Color.Black;
      this.ccButton.Location = new System.Drawing.Point(72, 217);
      this.ccButton.Name = "ccButton";
      this.ccButton.Size = new System.Drawing.Size(32, 26);
      this.ccButton.TabIndex = 208;
      this.ccButton.Text = "-->";
      this.ccButton.UseVisualStyleBackColor = true;
      this.ccButton.Click += new System.EventHandler(this.ccButton_Click);
      // 
      // toButton
      // 
      this.toButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toButton.ForeColor = System.Drawing.Color.Black;
      this.toButton.Location = new System.Drawing.Point(72, 134);
      this.toButton.Name = "toButton";
      this.toButton.Size = new System.Drawing.Size(32, 26);
      this.toButton.TabIndex = 207;
      this.toButton.Text = "-->";
      this.toButton.UseVisualStyleBackColor = true;
      this.toButton.Click += new System.EventHandler(this.toButton_Click);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Controls.Add(this.subjTextBox);
      this.groupBox2.Controls.Add(this.attchMntsTextBox);
      this.groupBox2.Controls.Add(this.browseButton);
      this.groupBox2.ForeColor = System.Drawing.Color.White;
      this.groupBox2.Location = new System.Drawing.Point(3, 347);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(425, 85);
      this.groupBox2.TabIndex = 209;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Subject/Attachments";
      // 
      // bodyTextBox
      // 
      this.bodyTextBox.AlignCenterVisible = true;
      this.bodyTextBox.AlignLeftVisible = true;
      this.bodyTextBox.AlignRightVisible = true;
      this.bodyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.bodyTextBox.BoldVisible = true;
      this.bodyTextBox.BulletsVisible = true;
      this.bodyTextBox.ChooseFontVisible = true;
      this.bodyTextBox.FindReplaceVisible = true;
      this.bodyTextBox.FontColorVisible = true;
      this.bodyTextBox.FontFamilyVisible = true;
      this.bodyTextBox.FontSizeVisible = true;
      this.bodyTextBox.ForeColor = System.Drawing.Color.Black;
      this.bodyTextBox.GroupAlignmentVisible = true;
      this.bodyTextBox.GroupBoldUnderlineItalicVisible = true;
      this.bodyTextBox.GroupFontColorVisible = true;
      this.bodyTextBox.GroupFontNameAndSizeVisible = true;
      this.bodyTextBox.GroupIndentationAndBulletsVisible = true;
      this.bodyTextBox.GroupInsertVisible = true;
      this.bodyTextBox.GroupSaveAndLoadVisible = true;
      this.bodyTextBox.GroupZoomVisible = true;
      this.bodyTextBox.INDENT = 10;
      this.bodyTextBox.IndentVisible = true;
      this.bodyTextBox.InsertPictureVisible = true;
      this.bodyTextBox.ItalicVisible = true;
      this.bodyTextBox.LoadVisible = true;
      this.bodyTextBox.Location = new System.Drawing.Point(3, 10);
      this.bodyTextBox.Name = "bodyTextBox";
      this.bodyTextBox.OutdentVisible = true;
      this.bodyTextBox.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset204 Microsoft" +
          " Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs18\\par\r\n}\r\n";
      this.bodyTextBox.SaveVisible = true;
      this.bodyTextBox.SeparatorAlignVisible = true;
      this.bodyTextBox.SeparatorBoldUnderlineItalicVisible = true;
      this.bodyTextBox.SeparatorFontColorVisible = true;
      this.bodyTextBox.SeparatorFontVisible = true;
      this.bodyTextBox.SeparatorIndentAndBulletsVisible = true;
      this.bodyTextBox.SeparatorInsertVisible = true;
      this.bodyTextBox.SeparatorSaveLoadVisible = true;
      this.bodyTextBox.Size = new System.Drawing.Size(640, 520);
      this.bodyTextBox.TabIndex = 210;
      this.bodyTextBox.ToolStripVisible = true;
      this.bodyTextBox.UnderlineVisible = true;
      this.bodyTextBox.WordWrapVisible = true;
      this.bodyTextBox.ZoomFactorTextVisible = true;
      this.bodyTextBox.ZoomInVisible = true;
      this.bodyTextBox.ZoomOutVisible = true;
      // 
      // groupBox3
      // 
      this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox3.Controls.Add(this.bodyTextBox);
      this.groupBox3.ForeColor = System.Drawing.Color.White;
      this.groupBox3.Location = new System.Drawing.Point(430, 2);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(646, 533);
      this.groupBox3.TabIndex = 211;
      this.groupBox3.TabStop = false;
      // 
      // msgTypComboBox
      // 
      this.msgTypComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.msgTypComboBox.FormattingEnabled = true;
      this.msgTypComboBox.Items.AddRange(new object[] {
            "Email",
            "SMS",
            "Local Inbox (System)"});
      this.msgTypComboBox.Location = new System.Drawing.Point(105, 15);
      this.msgTypComboBox.Name = "msgTypComboBox";
      this.msgTypComboBox.Size = new System.Drawing.Size(178, 21);
      this.msgTypComboBox.TabIndex = 212;
      // 
      // label1
      // 
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(8, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(96, 17);
      this.label1.TabIndex = 213;
      this.label1.Text = "Message Type:";
      // 
      // sendMailDiag
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.LightSlateGray;
      this.ClientSize = new System.Drawing.Size(1077, 538);
      this.Controls.Add(this.mailLabel);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "sendMailDiag";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "SEND MAIL MESSAGE";
      this.Load += new System.EventHandler(this.sendMailDiag_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button browseButton;
    public System.Windows.Forms.TextBox toTextBox;
    public System.Windows.Forms.TextBox ccTextBox;
    public System.Windows.Forms.TextBox subjTextBox;
    public System.Windows.Forms.TextBox attchMntsTextBox;
    public System.Windows.Forms.TextBox bccTextBox;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.Label mailLabel;
    private System.Windows.Forms.Button cstmrSiteButton;
    private System.Windows.Forms.Button cstmrButton;
    private System.Windows.Forms.TextBox cstmrSiteTextBox;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox cstmrNmTextBox;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox cstmrIDTextBox;
    private System.Windows.Forms.TextBox cstmrSiteIDTextBox;
    public System.Windows.Forms.ComboBox grpComboBox;
    public System.Windows.Forms.TextBox grpNmTextBox;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Button grpNmButton;
    private System.Windows.Forms.Label label11;
    public System.Windows.Forms.TextBox grpNmIDTextBox;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button bccButton;
    private System.Windows.Forms.Button ccButton;
    private System.Windows.Forms.Button toButton;
    private System.Windows.Forms.Button button5;
    private RicherTextBox.RicherTextBox bodyTextBox;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.CheckBox sendIndvdllyCheckBox;
    public System.Windows.Forms.ComboBox msgTypComboBox;
    private System.Windows.Forms.Label label1;
  }
}