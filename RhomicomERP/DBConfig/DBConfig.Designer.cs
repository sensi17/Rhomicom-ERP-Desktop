namespace DBConfig
{
  partial class DBConfig
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBConfig));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.installPgButton = new System.Windows.Forms.Button();
            this.pgDirButton = new System.Windows.Forms.Button();
            this.pgDirTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.createEmptyButton = new System.Windows.Forms.Button();
            this.rpts1Button = new System.Windows.Forms.Button();
            this.emptyDBNmTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.restoreFileButton = new System.Windows.Forms.Button();
            this.rpts2Button = new System.Windows.Forms.Button();
            this.srcBkpButton = new System.Windows.Forms.Button();
            this.srcFileNmTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.restoreDBNmTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.connectDBButton = new System.Windows.Forms.Button();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dbaseTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pwdTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.unameTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.loadDfltsButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.installFTPButton = new System.Windows.Forms.Button();
            this.baseDirButton = new System.Windows.Forms.Button();
            this.baseDirTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.getLastPatchButton = new System.Windows.Forms.Button();
            this.dbPatchesButton = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.modulesBaughtComboBox = new System.Windows.Forms.ComboBox();
            this.patchDBTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.waitLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.installPgButton);
            this.groupBox1.Controls.Add(this.pgDirButton);
            this.groupBox1.Controls.Add(this.pgDirTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(4, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 65);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Install PostgreSQL Database Server 9.3";
            // 
            // installPgButton
            // 
            this.installPgButton.ForeColor = System.Drawing.Color.Black;
            this.installPgButton.Location = new System.Drawing.Point(336, 16);
            this.installPgButton.Name = "installPgButton";
            this.installPgButton.Size = new System.Drawing.Size(83, 40);
            this.installPgButton.TabIndex = 3;
            this.installPgButton.Text = "INSTALL POSTGRESQL";
            this.installPgButton.UseVisualStyleBackColor = true;
            this.installPgButton.Click += new System.EventHandler(this.installPgButton_Click);
            // 
            // pgDirButton
            // 
            this.pgDirButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pgDirButton.ForeColor = System.Drawing.Color.Black;
            this.pgDirButton.Location = new System.Drawing.Point(307, 31);
            this.pgDirButton.Name = "pgDirButton";
            this.pgDirButton.Size = new System.Drawing.Size(28, 23);
            this.pgDirButton.TabIndex = 2;
            this.pgDirButton.Text = "...";
            this.pgDirButton.UseVisualStyleBackColor = true;
            this.pgDirButton.Click += new System.EventHandler(this.pgDirButton_Click);
            // 
            // pgDirTextBox
            // 
            this.pgDirTextBox.ForeColor = System.Drawing.Color.Black;
            this.pgDirTextBox.Location = new System.Drawing.Point(9, 32);
            this.pgDirTextBox.MaxLength = 200;
            this.pgDirTextBox.Name = "pgDirTextBox";
            this.pgDirTextBox.ReadOnly = true;
            this.pgDirTextBox.Size = new System.Drawing.Size(295, 21);
            this.pgDirTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "PG_RESTORE DIRECTORY:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.createEmptyButton);
            this.groupBox2.Controls.Add(this.rpts1Button);
            this.groupBox2.Controls.Add(this.emptyDBNmTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(4, 311);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(439, 44);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create Empty Database";
            this.groupBox2.Visible = false;
            // 
            // createEmptyButton
            // 
            this.createEmptyButton.ForeColor = System.Drawing.Color.Black;
            this.createEmptyButton.Location = new System.Drawing.Point(300, 12);
            this.createEmptyButton.Name = "createEmptyButton";
            this.createEmptyButton.Size = new System.Drawing.Size(119, 28);
            this.createEmptyButton.TabIndex = 1;
            this.createEmptyButton.Text = "CREATE";
            this.createEmptyButton.UseVisualStyleBackColor = true;
            this.createEmptyButton.Click += new System.EventHandler(this.createEmptyButton_Click);
            // 
            // rpts1Button
            // 
            this.rpts1Button.ForeColor = System.Drawing.Color.Black;
            this.rpts1Button.Location = new System.Drawing.Point(336, 12);
            this.rpts1Button.Name = "rpts1Button";
            this.rpts1Button.Size = new System.Drawing.Size(79, 28);
            this.rpts1Button.TabIndex = 2;
            this.rpts1Button.Text = "LOAD RPTS";
            this.rpts1Button.UseVisualStyleBackColor = true;
            this.rpts1Button.Visible = false;
            this.rpts1Button.Click += new System.EventHandler(this.rpts1Button_Click);
            // 
            // emptyDBNmTextBox
            // 
            this.emptyDBNmTextBox.ForeColor = System.Drawing.Color.Black;
            this.emptyDBNmTextBox.Location = new System.Drawing.Point(100, 18);
            this.emptyDBNmTextBox.MaxLength = 200;
            this.emptyDBNmTextBox.Name = "emptyDBNmTextBox";
            this.emptyDBNmTextBox.Size = new System.Drawing.Size(197, 21);
            this.emptyDBNmTextBox.TabIndex = 0;
            this.emptyDBNmTextBox.Click += new System.EventHandler(this.hostTextBox_Click);
            this.emptyDBNmTextBox.Enter += new System.EventHandler(this.hostTextBox_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Database Name:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.restoreFileButton);
            this.groupBox3.Controls.Add(this.rpts2Button);
            this.groupBox3.Controls.Add(this.srcBkpButton);
            this.groupBox3.Controls.Add(this.srcFileNmTextBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.restoreDBNmTextBox);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(4, 358);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(439, 77);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Create Sample Database";
            this.groupBox3.Visible = false;
            // 
            // restoreFileButton
            // 
            this.restoreFileButton.ForeColor = System.Drawing.Color.Black;
            this.restoreFileButton.Location = new System.Drawing.Point(300, 43);
            this.restoreFileButton.Name = "restoreFileButton";
            this.restoreFileButton.Size = new System.Drawing.Size(119, 28);
            this.restoreFileButton.TabIndex = 1;
            this.restoreFileButton.Text = "CREATE";
            this.restoreFileButton.UseVisualStyleBackColor = true;
            this.restoreFileButton.Click += new System.EventHandler(this.restoreFileButton_Click);
            // 
            // rpts2Button
            // 
            this.rpts2Button.ForeColor = System.Drawing.Color.Black;
            this.rpts2Button.Location = new System.Drawing.Point(336, 43);
            this.rpts2Button.Name = "rpts2Button";
            this.rpts2Button.Size = new System.Drawing.Size(79, 28);
            this.rpts2Button.TabIndex = 149;
            this.rpts2Button.Text = "LOAD RPTS";
            this.rpts2Button.UseVisualStyleBackColor = true;
            this.rpts2Button.Visible = false;
            this.rpts2Button.Click += new System.EventHandler(this.rpts2Button_Click);
            // 
            // srcBkpButton
            // 
            this.srcBkpButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.srcBkpButton.ForeColor = System.Drawing.Color.Black;
            this.srcBkpButton.Location = new System.Drawing.Point(391, 17);
            this.srcBkpButton.Name = "srcBkpButton";
            this.srcBkpButton.Size = new System.Drawing.Size(28, 23);
            this.srcBkpButton.TabIndex = 3;
            this.srcBkpButton.Text = "...";
            this.srcBkpButton.UseVisualStyleBackColor = true;
            this.srcBkpButton.Click += new System.EventHandler(this.srcBkpButton_Click);
            // 
            // srcFileNmTextBox
            // 
            this.srcFileNmTextBox.ForeColor = System.Drawing.Color.Black;
            this.srcFileNmTextBox.Location = new System.Drawing.Point(104, 18);
            this.srcFileNmTextBox.MaxLength = 200;
            this.srcFileNmTextBox.Name = "srcFileNmTextBox";
            this.srcFileNmTextBox.ReadOnly = true;
            this.srcFileNmTextBox.Size = new System.Drawing.Size(288, 21);
            this.srcFileNmTextBox.TabIndex = 2;
            this.srcFileNmTextBox.Click += new System.EventHandler(this.hostTextBox_Click);
            this.srcFileNmTextBox.Enter += new System.EventHandler(this.hostTextBox_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 148;
            this.label4.Text = "Source Backup File:";
            // 
            // restoreDBNmTextBox
            // 
            this.restoreDBNmTextBox.ForeColor = System.Drawing.Color.Black;
            this.restoreDBNmTextBox.Location = new System.Drawing.Point(100, 47);
            this.restoreDBNmTextBox.MaxLength = 200;
            this.restoreDBNmTextBox.Name = "restoreDBNmTextBox";
            this.restoreDBNmTextBox.Size = new System.Drawing.Size(197, 21);
            this.restoreDBNmTextBox.TabIndex = 0;
            this.restoreDBNmTextBox.Click += new System.EventHandler(this.hostTextBox_Click);
            this.restoreDBNmTextBox.Enter += new System.EventHandler(this.hostTextBox_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Database Name:";
            // 
            // closeButton
            // 
            this.closeButton.ForeColor = System.Drawing.Color.Black;
            this.closeButton.Location = new System.Drawing.Point(185, 530);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 28);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "CLOSE";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.statusLabel);
            this.groupBox4.Controls.Add(this.connectDBButton);
            this.groupBox4.Controls.Add(this.portTextBox);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.dbaseTextBox);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.pwdTextBox);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.hostTextBox);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.unameTextBox);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(4, 102);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(439, 100);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Database Server Connection";
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.Color.Red;
            this.statusLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.statusLabel.ForeColor = System.Drawing.Color.White;
            this.statusLabel.Location = new System.Drawing.Point(248, 69);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(88, 25);
            this.statusLabel.TabIndex = 145;
            this.statusLabel.Text = "Not Connected!";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // connectDBButton
            // 
            this.connectDBButton.ForeColor = System.Drawing.Color.Black;
            this.connectDBButton.Location = new System.Drawing.Point(342, 67);
            this.connectDBButton.Name = "connectDBButton";
            this.connectDBButton.Size = new System.Drawing.Size(77, 28);
            this.connectDBButton.TabIndex = 5;
            this.connectDBButton.Text = "CONNECT";
            this.connectDBButton.UseVisualStyleBackColor = true;
            this.connectDBButton.Click += new System.EventHandler(this.connectDBButton_Click);
            // 
            // portTextBox
            // 
            this.portTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.portTextBox.Location = new System.Drawing.Point(63, 66);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(149, 21);
            this.portTextBox.TabIndex = 2;
            this.portTextBox.Click += new System.EventHandler(this.hostTextBox_Click);
            this.portTextBox.Enter += new System.EventHandler(this.hostTextBox_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(7, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Port:";
            // 
            // dbaseTextBox
            // 
            this.dbaseTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dbaseTextBox.Location = new System.Drawing.Point(63, 40);
            this.dbaseTextBox.Name = "dbaseTextBox";
            this.dbaseTextBox.Size = new System.Drawing.Size(149, 21);
            this.dbaseTextBox.TabIndex = 1;
            this.dbaseTextBox.Click += new System.EventHandler(this.hostTextBox_Click);
            this.dbaseTextBox.Enter += new System.EventHandler(this.hostTextBox_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(7, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Database:";
            // 
            // pwdTextBox
            // 
            this.pwdTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pwdTextBox.Location = new System.Drawing.Point(279, 40);
            this.pwdTextBox.Name = "pwdTextBox";
            this.pwdTextBox.PasswordChar = '*';
            this.pwdTextBox.Size = new System.Drawing.Size(140, 21);
            this.pwdTextBox.TabIndex = 4;
            this.pwdTextBox.Click += new System.EventHandler(this.hostTextBox_Click);
            this.pwdTextBox.Enter += new System.EventHandler(this.hostTextBox_Click);
            this.pwdTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pwdTextBox_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(216, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Password:";
            // 
            // hostTextBox
            // 
            this.hostTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.hostTextBox.Location = new System.Drawing.Point(63, 14);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(149, 21);
            this.hostTextBox.TabIndex = 0;
            this.hostTextBox.Click += new System.EventHandler(this.hostTextBox_Click);
            this.hostTextBox.Enter += new System.EventHandler(this.hostTextBox_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(8, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Host:";
            // 
            // unameTextBox
            // 
            this.unameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.unameTextBox.Location = new System.Drawing.Point(279, 14);
            this.unameTextBox.Name = "unameTextBox";
            this.unameTextBox.Size = new System.Drawing.Size(140, 21);
            this.unameTextBox.TabIndex = 3;
            this.unameTextBox.Click += new System.EventHandler(this.hostTextBox_Click);
            this.unameTextBox.Enter += new System.EventHandler(this.hostTextBox_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(216, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "User Name:";
            // 
            // loadDfltsButton
            // 
            this.loadDfltsButton.ForeColor = System.Drawing.Color.Black;
            this.loadDfltsButton.Location = new System.Drawing.Point(3, 4);
            this.loadDfltsButton.Name = "loadDfltsButton";
            this.loadDfltsButton.Size = new System.Drawing.Size(143, 25);
            this.loadDfltsButton.TabIndex = 0;
            this.loadDfltsButton.Text = "LOAD DEFAULT VALUES";
            this.loadDfltsButton.UseVisualStyleBackColor = true;
            this.loadDfltsButton.Click += new System.EventHandler(this.loadDfltsButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.installFTPButton);
            this.groupBox5.Controls.Add(this.baseDirButton);
            this.groupBox5.Controls.Add(this.baseDirTextBox);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(4, 204);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(439, 107);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Database Directory/Patches";
            this.groupBox5.Visible = false;
            // 
            // installFTPButton
            // 
            this.installFTPButton.ForeColor = System.Drawing.Color.Black;
            this.installFTPButton.Location = new System.Drawing.Point(300, 72);
            this.installFTPButton.Name = "installFTPButton";
            this.installFTPButton.Size = new System.Drawing.Size(119, 30);
            this.installFTPButton.TabIndex = 6;
            this.installFTPButton.Text = "INSTALL FTP SERVER";
            this.installFTPButton.UseVisualStyleBackColor = true;
            this.installFTPButton.Click += new System.EventHandler(this.installFTPButton_Click);
            // 
            // baseDirButton
            // 
            this.baseDirButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baseDirButton.ForeColor = System.Drawing.Color.Black;
            this.baseDirButton.Location = new System.Drawing.Point(271, 78);
            this.baseDirButton.Name = "baseDirButton";
            this.baseDirButton.Size = new System.Drawing.Size(28, 23);
            this.baseDirButton.TabIndex = 3;
            this.baseDirButton.Text = "...";
            this.baseDirButton.UseVisualStyleBackColor = true;
            this.baseDirButton.Click += new System.EventHandler(this.baseDirButton_Click);
            // 
            // baseDirTextBox
            // 
            this.baseDirTextBox.ForeColor = System.Drawing.Color.Black;
            this.baseDirTextBox.Location = new System.Drawing.Point(7, 79);
            this.baseDirTextBox.MaxLength = 200;
            this.baseDirTextBox.Name = "baseDirTextBox";
            this.baseDirTextBox.Size = new System.Drawing.Size(266, 21);
            this.baseDirTextBox.TabIndex = 0;
            this.baseDirTextBox.Enter += new System.EventHandler(this.hostTextBox_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(130, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Base Database Directory:";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(7, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(426, 47);
            this.label11.TabIndex = 4;
            this.label11.Text = resources.GetString("label11.Text");
            // 
            // getLastPatchButton
            // 
            this.getLastPatchButton.ForeColor = System.Drawing.Color.Black;
            this.getLastPatchButton.Location = new System.Drawing.Point(127, 59);
            this.getLastPatchButton.Name = "getLastPatchButton";
            this.getLastPatchButton.Size = new System.Drawing.Size(171, 30);
            this.getLastPatchButton.TabIndex = 8;
            this.getLastPatchButton.Text = "GET LAST PATCHE\'S VERSION";
            this.getLastPatchButton.UseVisualStyleBackColor = true;
            this.getLastPatchButton.Click += new System.EventHandler(this.getLastPatchButton_Click);
            // 
            // dbPatchesButton
            // 
            this.dbPatchesButton.ForeColor = System.Drawing.Color.Black;
            this.dbPatchesButton.Location = new System.Drawing.Point(298, 59);
            this.dbPatchesButton.Name = "dbPatchesButton";
            this.dbPatchesButton.Size = new System.Drawing.Size(122, 30);
            this.dbPatchesButton.TabIndex = 7;
            this.dbPatchesButton.Text = "APPLY PATCHES";
            this.dbPatchesButton.UseVisualStyleBackColor = true;
            this.dbPatchesButton.Click += new System.EventHandler(this.dbPatchesButton_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.modulesBaughtComboBox);
            this.groupBox6.Controls.Add(this.patchDBTextBox);
            this.groupBox6.Controls.Add(this.getLastPatchButton);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.dbPatchesButton);
            this.groupBox6.ForeColor = System.Drawing.Color.White;
            this.groupBox6.Location = new System.Drawing.Point(3, 436);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(440, 92);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Applied Patches/HotFixes";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 42);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(139, 13);
            this.label15.TabIndex = 162;
            this.label15.Text = "Modules/Packages Needed:";
            // 
            // modulesBaughtComboBox
            // 
            this.modulesBaughtComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modulesBaughtComboBox.DropDownWidth = 364;
            this.modulesBaughtComboBox.FormattingEnabled = true;
            this.modulesBaughtComboBox.Items.AddRange(new object[] {
            "Person Records Only",
            "Point of Sale Only",
            "Accounting Only",
            "Person Records with Accounting Only",
            "Sales with Accounting Only",
            "Accounting with Payroll Only",
            "Person Records + Hospitality Only",
            "Person Records + Events Only",
            "Basic Modules Only",
            "Basic Modules + Hospitality Only",
            "Basic Modules + Events Only",
            "Basic Modules + Projects Only",
            "Basic Modules + Appointments Only",
            "Basic Modules + PMS Only",
            "Basic Modules + Events + Hospitality Only",
            "Basic Modules - Payroll - Person Records + Events + Hospitality Only",
            "Basic Modules + Payroll - Person Records + Events + Hospitality Only",
            "Basic Modules + Events + PMS Only",
            "Basic Modules + Projects + PMS Only",
            "Basic Modules + Projects + Hospitality Only",
            "Basic Modules + Projects + Events Only",
            "Basic Modules + Events + Hospitality + PMS Only",
            "Basic Modules + Projects + Hospitality + PMS Only",
            "Basic Modules + Events + Projects + Hospitality Only",
            "Basic Modules + Events + Projects + Hospitality + PMS Only",
            "All Modules"});
            this.modulesBaughtComboBox.Location = new System.Drawing.Point(156, 36);
            this.modulesBaughtComboBox.Name = "modulesBaughtComboBox";
            this.modulesBaughtComboBox.Size = new System.Drawing.Size(264, 21);
            this.modulesBaughtComboBox.TabIndex = 161;
            // 
            // patchDBTextBox
            // 
            this.patchDBTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.patchDBTextBox.Location = new System.Drawing.Point(101, 13);
            this.patchDBTextBox.Name = "patchDBTextBox";
            this.patchDBTextBox.Size = new System.Drawing.Size(319, 21);
            this.patchDBTextBox.TabIndex = 21;
            this.patchDBTextBox.Click += new System.EventHandler(this.hostTextBox_Click);
            this.patchDBTextBox.Enter += new System.EventHandler(this.hostTextBox_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(8, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Database Name:";
            // 
            // waitLabel
            // 
            this.waitLabel.BackColor = System.Drawing.Color.Green;
            this.waitLabel.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waitLabel.ForeColor = System.Drawing.Color.White;
            this.waitLabel.Location = new System.Drawing.Point(59, 409);
            this.waitLabel.Name = "waitLabel";
            this.waitLabel.Size = new System.Drawing.Size(328, 52);
            this.waitLabel.TabIndex = 134;
            this.waitLabel.Text = "APPLYING PATCHE(S)...PLEASE WAIT...";
            this.waitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.waitLabel.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DBConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(446, 561);
            this.Controls.Add(this.waitLabel);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.loadDfltsButton);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Configuration for ROMS/REMS V1";
            this.Load += new System.EventHandler(this.DBConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button pgDirButton;
    private System.Windows.Forms.TextBox pgDirTextBox;
    private System.Windows.Forms.Button installPgButton;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button createEmptyButton;
    private System.Windows.Forms.TextBox emptyDBNmTextBox;
    private System.Windows.Forms.Button restoreFileButton;
    private System.Windows.Forms.TextBox restoreDBNmTextBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.TextBox srcFileNmTextBox;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button srcBkpButton;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.GroupBox groupBox4;
    public System.Windows.Forms.TextBox portTextBox;
    private System.Windows.Forms.Label label5;
    public System.Windows.Forms.TextBox dbaseTextBox;
    private System.Windows.Forms.Label label6;
    public System.Windows.Forms.TextBox pwdTextBox;
    private System.Windows.Forms.Label label7;
    public System.Windows.Forms.TextBox hostTextBox;
    private System.Windows.Forms.Label label8;
    public System.Windows.Forms.TextBox unameTextBox;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Button connectDBButton;
    private System.Windows.Forms.Label statusLabel;
    private System.Windows.Forms.Button loadDfltsButton;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.TextBox baseDirTextBox;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Button baseDirButton;
    private System.Windows.Forms.Button rpts1Button;
    private System.Windows.Forms.Button rpts2Button;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Button installFTPButton;
    private System.Windows.Forms.Button dbPatchesButton;
    private System.Windows.Forms.Button getLastPatchButton;
    private System.Windows.Forms.GroupBox groupBox6;
    public System.Windows.Forms.TextBox patchDBTextBox;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label waitLabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox modulesBaughtComboBox;
        private System.Windows.Forms.Timer timer1;
    }
}

