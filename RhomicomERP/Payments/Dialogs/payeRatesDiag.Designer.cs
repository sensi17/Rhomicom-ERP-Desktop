namespace InternalPayments.Dialogs
{
    partial class payeRatesDiag
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ratesDataGridView = new System.Windows.Forms.DataGridView();
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.testPayeButton = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.toolStrip12 = new System.Windows.Forms.ToolStrip();
            this.addButton = new System.Windows.Forms.ToolStripButton();
            this.deleteDetButton = new System.Windows.Forms.ToolStripButton();
            this.vwSQLDetButton = new System.Windows.Forms.ToolStripButton();
            this.rcHstryDetButton = new System.Windows.Forms.ToolStripButton();
            this.rfrshDetButton = new System.Windows.Forms.ToolStripButton();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ratesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.toolStrip12.SuspendLayout();
            this.SuspendLayout();
            // 
            // ratesDataGridView
            // 
            this.ratesDataGridView.AllowUserToAddRows = false;
            this.ratesDataGridView.AllowUserToDeleteRows = false;
            this.ratesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ratesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ratesDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.ratesDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ratesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ratesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column10,
            this.Column11,
            this.Column6,
            this.Column4});
            this.ratesDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ratesDataGridView.Location = new System.Drawing.Point(1, 31);
            this.ratesDataGridView.Name = "ratesDataGridView";
            this.ratesDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.ratesDataGridView.Size = new System.Drawing.Size(462, 294);
            this.ratesDataGridView.TabIndex = 1;
            this.ratesDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ratesDataGridView_CellValueChanged);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(300, 329);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(380, 329);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // testPayeButton
            // 
            this.testPayeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.testPayeButton.Location = new System.Drawing.Point(115, 329);
            this.testPayeButton.Name = "testPayeButton";
            this.testPayeButton.Size = new System.Drawing.Size(112, 23);
            this.testPayeButton.TabIndex = 4;
            this.testPayeButton.Text = "TEST PAYE TAX";
            this.testPayeButton.UseVisualStyleBackColor = true;
            this.testPayeButton.Click += new System.EventHandler(this.testPayeButton_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Location = new System.Drawing.Point(1, 330);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(114, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.ThousandsSeparator = true;
            // 
            // toolStrip12
            // 
            this.toolStrip12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip12.AutoSize = false;
            this.toolStrip12.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip12.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButton,
            this.deleteDetButton,
            this.vwSQLDetButton,
            this.rcHstryDetButton,
            this.rfrshDetButton});
            this.toolStrip12.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip12.Location = new System.Drawing.Point(0, 3);
            this.toolStrip12.Margin = new System.Windows.Forms.Padding(3);
            this.toolStrip12.Name = "toolStrip12";
            this.toolStrip12.Size = new System.Drawing.Size(463, 25);
            this.toolStrip12.Stretch = true;
            this.toolStrip12.TabIndex = 9;
            this.toolStrip12.TabStop = true;
            this.toolStrip12.Text = "ToolStrip2";
            // 
            // addButton
            // 
            this.addButton.ForeColor = System.Drawing.Color.Black;
            this.addButton.Image = global::InternalPayments.Properties.Resources.plus_32;
            this.addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(51, 22);
            this.addButton.Text = "ADD";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // deleteDetButton
            // 
            this.deleteDetButton.ForeColor = System.Drawing.Color.Black;
            this.deleteDetButton.Image = global::InternalPayments.Properties.Resources.delete;
            this.deleteDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteDetButton.Name = "deleteDetButton";
            this.deleteDetButton.Size = new System.Drawing.Size(66, 22);
            this.deleteDetButton.Text = "DELETE";
            this.deleteDetButton.Click += new System.EventHandler(this.deleteDetButton_Click);
            // 
            // vwSQLDetButton
            // 
            this.vwSQLDetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vwSQLDetButton.ForeColor = System.Drawing.Color.Black;
            this.vwSQLDetButton.Image = global::InternalPayments.Properties.Resources.sql_icon_by_raisch_d3ax2ih;
            this.vwSQLDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vwSQLDetButton.Name = "vwSQLDetButton";
            this.vwSQLDetButton.Size = new System.Drawing.Size(23, 22);
            this.vwSQLDetButton.Text = "View SQL";
            this.vwSQLDetButton.Click += new System.EventHandler(this.vwSQLDetButton_Click);
            // 
            // rcHstryDetButton
            // 
            this.rcHstryDetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rcHstryDetButton.ForeColor = System.Drawing.Color.Black;
            this.rcHstryDetButton.Image = global::InternalPayments.Properties.Resources.statistics_32;
            this.rcHstryDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rcHstryDetButton.Name = "rcHstryDetButton";
            this.rcHstryDetButton.Size = new System.Drawing.Size(23, 22);
            this.rcHstryDetButton.Text = "Record History";
            this.rcHstryDetButton.Click += new System.EventHandler(this.rcHstryDetButton_Click);
            // 
            // rfrshDetButton
            // 
            this.rfrshDetButton.ForeColor = System.Drawing.Color.Black;
            this.rfrshDetButton.Image = global::InternalPayments.Properties.Resources.refresh;
            this.rfrshDetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rfrshDetButton.Name = "rfrshDetButton";
            this.rfrshDetButton.Size = new System.Drawing.Size(23, 22);
            this.rfrshDetButton.Click += new System.EventHandler(this.rfrshDetButton_Click);
            // 
            // Column10
            // 
            this.Column10.FillWeight = 5F;
            this.Column10.HeaderText = "rate_id";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Visible = false;
            this.Column10.Width = 45;
            // 
            // Column11
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column11.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column11.FillWeight = 5F;
            this.Column11.HeaderText = "Level/Order No.";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column6
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column6.FillWeight = 150F;
            this.Column6.HeaderText = "Taxable Amount";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column4
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column4.FillWeight = 90F;
            this.Column4.HeaderText = "Tax Rate (in Fractions or Decimal)";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column4.Width = 138;
            // 
            // payeRatesDiag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 356);
            this.Controls.Add(this.toolStrip12);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.testPayeButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.ratesDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "payeRatesDiag";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PAYE Tax Rates";
            this.Load += new System.EventHandler(this.payeRatesDiag_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ratesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.toolStrip12.ResumeLayout(false);
            this.toolStrip12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ratesDataGridView;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button testPayeButton;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ToolStrip toolStrip12;
        private System.Windows.Forms.ToolStripButton addButton;
        private System.Windows.Forms.ToolStripButton deleteDetButton;
        private System.Windows.Forms.ToolStripButton vwSQLDetButton;
        private System.Windows.Forms.ToolStripButton rcHstryDetButton;
        private System.Windows.Forms.ToolStripButton rfrshDetButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}