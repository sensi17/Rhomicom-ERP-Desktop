namespace StoresAndInventoryManager.Forms
{
    partial class payables
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(payables));
          this.payApplybutton = new System.Windows.Forms.Button();
          this.label1 = new System.Windows.Forms.Label();
          this.groupBox1 = new System.Windows.Forms.GroupBox();
          this.docDebtnumericUpDown = new System.Windows.Forms.NumericUpDown();
          this.docTtlPaymtnumericUpDown = new System.Windows.Forms.NumericUpDown();
          this.label7 = new System.Windows.Forms.Label();
          this.label3 = new System.Windows.Forms.Label();
          this.docSuppliertextBox = new System.Windows.Forms.TextBox();
          this.docTotalCostnumericUpDown = new System.Windows.Forms.NumericUpDown();
          this.label6 = new System.Windows.Forms.Label();
          this.label5 = new System.Windows.Forms.Label();
          this.docSrcTypetextBox = new System.Windows.Forms.TextBox();
          this.label4 = new System.Windows.Forms.Label();
          this.label2 = new System.Windows.Forms.Label();
          this.docSrcTypeIDtextBox = new System.Windows.Forms.TextBox();
          this.docSrcTypeDtetextBox = new System.Windows.Forms.TextBox();
          this.label9 = new System.Windows.Forms.Label();
          this.groupBox2 = new System.Windows.Forms.GroupBox();
          this.payRmkstextBox = new System.Windows.Forms.TextBox();
          this.payCancelbutton = new System.Windows.Forms.Button();
          this.payAmtnumericUpDown = new System.Windows.Forms.NumericUpDown();
          this.label8 = new System.Windows.Forms.Label();
          this.payDtebutton = new System.Windows.Forms.Button();
          this.imageListPayables = new System.Windows.Forms.ImageList(this.components);
          this.label10 = new System.Windows.Forms.Label();
          this.payDtetextBox = new System.Windows.Forms.TextBox();
          this.listViewPayables = new System.Windows.Forms.ListView();
          this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
          this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
          this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
          this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
          this.revslcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
          this.reverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.groupBox1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.docDebtnumericUpDown)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.docTtlPaymtnumericUpDown)).BeginInit();
          ((System.ComponentModel.ISupportInitialize)(this.docTotalCostnumericUpDown)).BeginInit();
          this.groupBox2.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.payAmtnumericUpDown)).BeginInit();
          this.revslcontextMenuStrip.SuspendLayout();
          this.SuspendLayout();
          // 
          // payApplybutton
          // 
          this.payApplybutton.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.payApplybutton.ForeColor = System.Drawing.SystemColors.ControlText;
          this.payApplybutton.Location = new System.Drawing.Point(204, 136);
          this.payApplybutton.Name = "payApplybutton";
          this.payApplybutton.Size = new System.Drawing.Size(87, 25);
          this.payApplybutton.TabIndex = 119;
          this.payApplybutton.Text = "Apply";
          this.payApplybutton.UseVisualStyleBackColor = true;
          this.payApplybutton.Click += new System.EventHandler(this.payApplybutton_Click);
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
          this.label1.Location = new System.Drawing.Point(41, 67);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(61, 16);
          this.label1.TabIndex = 116;
          this.label1.Text = "Remarks:";
          // 
          // groupBox1
          // 
          this.groupBox1.Controls.Add(this.docDebtnumericUpDown);
          this.groupBox1.Controls.Add(this.docTtlPaymtnumericUpDown);
          this.groupBox1.Controls.Add(this.label7);
          this.groupBox1.Controls.Add(this.label3);
          this.groupBox1.Controls.Add(this.docSuppliertextBox);
          this.groupBox1.Controls.Add(this.docTotalCostnumericUpDown);
          this.groupBox1.Controls.Add(this.label6);
          this.groupBox1.Controls.Add(this.label5);
          this.groupBox1.Controls.Add(this.docSrcTypetextBox);
          this.groupBox1.Controls.Add(this.label4);
          this.groupBox1.Controls.Add(this.label2);
          this.groupBox1.Controls.Add(this.docSrcTypeIDtextBox);
          this.groupBox1.Controls.Add(this.docSrcTypeDtetextBox);
          this.groupBox1.Controls.Add(this.label9);
          this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
          this.groupBox1.Location = new System.Drawing.Point(395, 0);
          this.groupBox1.Name = "groupBox1";
          this.groupBox1.Size = new System.Drawing.Size(397, 187);
          this.groupBox1.TabIndex = 131;
          this.groupBox1.TabStop = false;
          this.groupBox1.Text = "DOCUMENT SOURCE";
          // 
          // docDebtnumericUpDown
          // 
          this.docDebtnumericUpDown.BackColor = System.Drawing.SystemColors.ControlLightLight;
          this.docDebtnumericUpDown.DecimalPlaces = 2;
          this.docDebtnumericUpDown.Enabled = false;
          this.docDebtnumericUpDown.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.docDebtnumericUpDown.Location = new System.Drawing.Point(104, 155);
          this.docDebtnumericUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
          this.docDebtnumericUpDown.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
          this.docDebtnumericUpDown.Name = "docDebtnumericUpDown";
          this.docDebtnumericUpDown.ReadOnly = true;
          this.docDebtnumericUpDown.Size = new System.Drawing.Size(278, 23);
          this.docDebtnumericUpDown.TabIndex = 145;
          this.docDebtnumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.docDebtnumericUpDown.ThousandsSeparator = true;
          // 
          // docTtlPaymtnumericUpDown
          // 
          this.docTtlPaymtnumericUpDown.BackColor = System.Drawing.SystemColors.ControlLightLight;
          this.docTtlPaymtnumericUpDown.DecimalPlaces = 2;
          this.docTtlPaymtnumericUpDown.Enabled = false;
          this.docTtlPaymtnumericUpDown.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.docTtlPaymtnumericUpDown.Location = new System.Drawing.Point(104, 128);
          this.docTtlPaymtnumericUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
          this.docTtlPaymtnumericUpDown.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
          this.docTtlPaymtnumericUpDown.Name = "docTtlPaymtnumericUpDown";
          this.docTtlPaymtnumericUpDown.ReadOnly = true;
          this.docTtlPaymtnumericUpDown.Size = new System.Drawing.Size(278, 23);
          this.docTtlPaymtnumericUpDown.TabIndex = 143;
          this.docTtlPaymtnumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.docTtlPaymtnumericUpDown.ThousandsSeparator = true;
          // 
          // label7
          // 
          this.label7.AutoSize = true;
          this.label7.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
          this.label7.Location = new System.Drawing.Point(7, 131);
          this.label7.Name = "label7";
          this.label7.Size = new System.Drawing.Size(93, 16);
          this.label7.TabIndex = 142;
          this.label7.Text = "Total Payment:";
          // 
          // label3
          // 
          this.label3.AutoSize = true;
          this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
          this.label3.Location = new System.Drawing.Point(41, 77);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(58, 16);
          this.label3.TabIndex = 141;
          this.label3.Text = "Supplier:";
          // 
          // docSuppliertextBox
          // 
          this.docSuppliertextBox.BackColor = System.Drawing.SystemColors.Control;
          this.docSuppliertextBox.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.docSuppliertextBox.Location = new System.Drawing.Point(104, 73);
          this.docSuppliertextBox.Name = "docSuppliertextBox";
          this.docSuppliertextBox.ReadOnly = true;
          this.docSuppliertextBox.Size = new System.Drawing.Size(278, 23);
          this.docSuppliertextBox.TabIndex = 140;
          // 
          // docTotalCostnumericUpDown
          // 
          this.docTotalCostnumericUpDown.BackColor = System.Drawing.SystemColors.ControlLightLight;
          this.docTotalCostnumericUpDown.DecimalPlaces = 2;
          this.docTotalCostnumericUpDown.Enabled = false;
          this.docTotalCostnumericUpDown.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.docTotalCostnumericUpDown.Location = new System.Drawing.Point(104, 101);
          this.docTotalCostnumericUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
          this.docTotalCostnumericUpDown.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
          this.docTotalCostnumericUpDown.Name = "docTotalCostnumericUpDown";
          this.docTotalCostnumericUpDown.ReadOnly = true;
          this.docTotalCostnumericUpDown.Size = new System.Drawing.Size(278, 23);
          this.docTotalCostnumericUpDown.TabIndex = 139;
          this.docTotalCostnumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.docTotalCostnumericUpDown.ThousandsSeparator = true;
          // 
          // label6
          // 
          this.label6.AutoSize = true;
          this.label6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
          this.label6.Location = new System.Drawing.Point(31, 103);
          this.label6.Name = "label6";
          this.label6.Size = new System.Drawing.Size(67, 16);
          this.label6.TabIndex = 138;
          this.label6.Text = "Total Cost:";
          // 
          // label5
          // 
          this.label5.AutoSize = true;
          this.label5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
          this.label5.Location = new System.Drawing.Point(63, 20);
          this.label5.Name = "label5";
          this.label5.Size = new System.Drawing.Size(40, 16);
          this.label5.TabIndex = 137;
          this.label5.Text = "Type:";
          // 
          // docSrcTypetextBox
          // 
          this.docSrcTypetextBox.BackColor = System.Drawing.SystemColors.Control;
          this.docSrcTypetextBox.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.docSrcTypetextBox.Location = new System.Drawing.Point(104, 17);
          this.docSrcTypetextBox.Name = "docSrcTypetextBox";
          this.docSrcTypetextBox.ReadOnly = true;
          this.docSrcTypetextBox.Size = new System.Drawing.Size(278, 23);
          this.docSrcTypetextBox.TabIndex = 136;
          // 
          // label4
          // 
          this.label4.AutoSize = true;
          this.label4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
          this.label4.Location = new System.Drawing.Point(215, 48);
          this.label4.Name = "label4";
          this.label4.Size = new System.Drawing.Size(38, 16);
          this.label4.TabIndex = 135;
          this.label4.Text = "Date:";
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
          this.label2.Location = new System.Drawing.Point(77, 49);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(23, 16);
          this.label2.TabIndex = 133;
          this.label2.Text = "ID:";
          // 
          // docSrcTypeIDtextBox
          // 
          this.docSrcTypeIDtextBox.BackColor = System.Drawing.SystemColors.Control;
          this.docSrcTypeIDtextBox.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.docSrcTypeIDtextBox.Location = new System.Drawing.Point(104, 45);
          this.docSrcTypeIDtextBox.Name = "docSrcTypeIDtextBox";
          this.docSrcTypeIDtextBox.ReadOnly = true;
          this.docSrcTypeIDtextBox.Size = new System.Drawing.Size(105, 23);
          this.docSrcTypeIDtextBox.TabIndex = 132;
          // 
          // docSrcTypeDtetextBox
          // 
          this.docSrcTypeDtetextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.docSrcTypeDtetextBox.Location = new System.Drawing.Point(259, 45);
          this.docSrcTypeDtetextBox.Name = "docSrcTypeDtetextBox";
          this.docSrcTypeDtetextBox.ReadOnly = true;
          this.docSrcTypeDtetextBox.Size = new System.Drawing.Size(123, 22);
          this.docSrcTypeDtetextBox.TabIndex = 131;
          // 
          // label9
          // 
          this.label9.AutoSize = true;
          this.label9.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label9.ForeColor = System.Drawing.SystemColors.ControlLightLight;
          this.label9.Location = new System.Drawing.Point(61, 158);
          this.label9.Name = "label9";
          this.label9.Size = new System.Drawing.Size(38, 16);
          this.label9.TabIndex = 144;
          this.label9.Text = "Debt:";
          // 
          // groupBox2
          // 
          this.groupBox2.Controls.Add(this.payRmkstextBox);
          this.groupBox2.Controls.Add(this.payCancelbutton);
          this.groupBox2.Controls.Add(this.payAmtnumericUpDown);
          this.groupBox2.Controls.Add(this.label8);
          this.groupBox2.Controls.Add(this.payDtebutton);
          this.groupBox2.Controls.Add(this.label10);
          this.groupBox2.Controls.Add(this.payDtetextBox);
          this.groupBox2.Controls.Add(this.payApplybutton);
          this.groupBox2.Controls.Add(this.label1);
          this.groupBox2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
          this.groupBox2.Location = new System.Drawing.Point(395, 192);
          this.groupBox2.Name = "groupBox2";
          this.groupBox2.Size = new System.Drawing.Size(397, 167);
          this.groupBox2.TabIndex = 140;
          this.groupBox2.TabStop = false;
          this.groupBox2.Text = "PAYMENTS";
          // 
          // payRmkstextBox
          // 
          this.payRmkstextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
          this.payRmkstextBox.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.payRmkstextBox.ForeColor = System.Drawing.SystemColors.WindowText;
          this.payRmkstextBox.Location = new System.Drawing.Point(102, 69);
          this.payRmkstextBox.Multiline = true;
          this.payRmkstextBox.Name = "payRmkstextBox";
          this.payRmkstextBox.Size = new System.Drawing.Size(278, 62);
          this.payRmkstextBox.TabIndex = 141;
          // 
          // payCancelbutton
          // 
          this.payCancelbutton.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.payCancelbutton.ForeColor = System.Drawing.SystemColors.ControlText;
          this.payCancelbutton.Location = new System.Drawing.Point(296, 135);
          this.payCancelbutton.Name = "payCancelbutton";
          this.payCancelbutton.Size = new System.Drawing.Size(87, 25);
          this.payCancelbutton.TabIndex = 140;
          this.payCancelbutton.Text = "Cancel";
          this.payCancelbutton.UseVisualStyleBackColor = true;
          this.payCancelbutton.Click += new System.EventHandler(this.payCancelbutton_Click);
          // 
          // payAmtnumericUpDown
          // 
          this.payAmtnumericUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
          this.payAmtnumericUpDown.DecimalPlaces = 2;
          this.payAmtnumericUpDown.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.payAmtnumericUpDown.Location = new System.Drawing.Point(103, 43);
          this.payAmtnumericUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
          this.payAmtnumericUpDown.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
          this.payAmtnumericUpDown.Name = "payAmtnumericUpDown";
          this.payAmtnumericUpDown.Size = new System.Drawing.Size(278, 23);
          this.payAmtnumericUpDown.TabIndex = 139;
          this.payAmtnumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
          this.payAmtnumericUpDown.ThousandsSeparator = true;
          // 
          // label8
          // 
          this.label8.AutoSize = true;
          this.label8.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
          this.label8.Location = new System.Drawing.Point(43, 45);
          this.label8.Name = "label8";
          this.label8.Size = new System.Drawing.Size(56, 16);
          this.label8.TabIndex = 138;
          this.label8.Text = "Amount:";
          // 
          // payDtebutton
          // 
          this.payDtebutton.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.payDtebutton.ForeColor = System.Drawing.SystemColors.ControlText;
          this.payDtebutton.ImageKey = "calendar.ico";
          this.payDtebutton.ImageList = this.imageListPayables;
          this.payDtebutton.Location = new System.Drawing.Point(207, 16);
          this.payDtebutton.Name = "payDtebutton";
          this.payDtebutton.Size = new System.Drawing.Size(25, 25);
          this.payDtebutton.TabIndex = 137;
          this.payDtebutton.UseVisualStyleBackColor = true;
          this.payDtebutton.Click += new System.EventHandler(this.payDtebutton_Click);
          // 
          // imageListPayables
          // 
          this.imageListPayables.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPayables.ImageStream")));
          this.imageListPayables.TransparentColor = System.Drawing.Color.Transparent;
          this.imageListPayables.Images.SetKeyName(0, "calendar.ico");
          this.imageListPayables.Images.SetKeyName(1, "Credit cards.ico");
          // 
          // label10
          // 
          this.label10.AutoSize = true;
          this.label10.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label10.ForeColor = System.Drawing.SystemColors.ControlLightLight;
          this.label10.Location = new System.Drawing.Point(60, 20);
          this.label10.Name = "label10";
          this.label10.Size = new System.Drawing.Size(38, 16);
          this.label10.TabIndex = 135;
          this.label10.Text = "Date:";
          // 
          // payDtetextBox
          // 
          this.payDtetextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.payDtetextBox.Location = new System.Drawing.Point(104, 17);
          this.payDtetextBox.Name = "payDtetextBox";
          this.payDtetextBox.ReadOnly = true;
          this.payDtetextBox.Size = new System.Drawing.Size(105, 22);
          this.payDtetextBox.TabIndex = 131;
          // 
          // listViewPayables
          // 
          this.listViewPayables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader2});
          this.listViewPayables.ContextMenuStrip = this.revslcontextMenuStrip;
          this.listViewPayables.FullRowSelect = true;
          this.listViewPayables.GridLines = true;
          this.listViewPayables.Location = new System.Drawing.Point(1, 2);
          this.listViewPayables.Name = "listViewPayables";
          this.listViewPayables.Size = new System.Drawing.Size(392, 368);
          this.listViewPayables.TabIndex = 141;
          this.listViewPayables.UseCompatibleStateImageBehavior = false;
          this.listViewPayables.View = System.Windows.Forms.View.Details;
          // 
          // columnHeader1
          // 
          this.columnHeader1.Text = "No";
          this.columnHeader1.Width = 40;
          // 
          // columnHeader3
          // 
          this.columnHeader3.Text = "Date";
          this.columnHeader3.Width = 120;
          // 
          // columnHeader4
          // 
          this.columnHeader4.Text = "Payment Amount";
          this.columnHeader4.Width = 100;
          // 
          // columnHeader2
          // 
          this.columnHeader2.Text = "Remarks";
          this.columnHeader2.Width = 500;
          // 
          // revslcontextMenuStrip
          // 
          this.revslcontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reverseToolStripMenuItem});
          this.revslcontextMenuStrip.Name = "revslcontextMenuStrip";
          this.revslcontextMenuStrip.Size = new System.Drawing.Size(115, 26);
          // 
          // reverseToolStripMenuItem
          // 
          this.reverseToolStripMenuItem.Image = global::StoresAndInventoryManager.Properties.Resources.reverse;
          this.reverseToolStripMenuItem.Name = "reverseToolStripMenuItem";
          this.reverseToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
          this.reverseToolStripMenuItem.Text = "Reverse";
          this.reverseToolStripMenuItem.Click += new System.EventHandler(this.reverseToolStripMenuItem_Click);
          // 
          // payables
          // 
          this.AcceptButton = this.payApplybutton;
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
          this.ClientSize = new System.Drawing.Size(811, 372);
          this.Controls.Add(this.listViewPayables);
          this.Controls.Add(this.groupBox2);
          this.Controls.Add(this.groupBox1);
          this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "payables";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "Payables";
          this.Load += new System.EventHandler(this.payables_Load);
          this.groupBox1.ResumeLayout(false);
          this.groupBox1.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.docDebtnumericUpDown)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.docTtlPaymtnumericUpDown)).EndInit();
          ((System.ComponentModel.ISupportInitialize)(this.docTotalCostnumericUpDown)).EndInit();
          this.groupBox2.ResumeLayout(false);
          this.groupBox2.PerformLayout();
          ((System.ComponentModel.ISupportInitialize)(this.payAmtnumericUpDown)).EndInit();
          this.revslcontextMenuStrip.ResumeLayout(false);
          this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button payApplybutton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown docTotalCostnumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox docSrcTypetextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox docSrcTypeIDtextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox docSrcTypeDtetextBox;
        private System.Windows.Forms.Button payDtebutton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown payAmtnumericUpDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox payDtetextBox;
        private System.Windows.Forms.Button payCancelbutton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox docSuppliertextBox;
        private System.Windows.Forms.TextBox payRmkstextBox;
        private System.Windows.Forms.ImageList imageListPayables;
        private System.Windows.Forms.ListView listViewPayables;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.NumericUpDown docDebtnumericUpDown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown docTtlPaymtnumericUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ContextMenuStrip revslcontextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem reverseToolStripMenuItem;
    }
}