namespace StoresAndInventoryManager.Forms
{
    partial class uomConversion
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
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
          this.uomCancelbutton = new System.Windows.Forms.Button();
          this.submitCnvrtdQtybutton = new System.Windows.Forms.Button();
          this.ttlQtytextBox = new System.Windows.Forms.TextBox();
          this.cntrltextBox = new System.Windows.Forms.TextBox();
          this.label3 = new System.Windows.Forms.Label();
          this.dataGridViewUomConversion = new StoresAndInventoryManager.Classes.MyDataGridView();
          this.label1 = new System.Windows.Forms.Label();
          this.ttlPriceTextBox = new System.Windows.Forms.TextBox();
          this.detListNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.detUom = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.detQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.detEqvBaseQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.detItmUomID = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.detUomId = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.detSortOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.detCnvsnFactor = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
          ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUomConversion)).BeginInit();
          this.SuspendLayout();
          // 
          // uomCancelbutton
          // 
          this.uomCancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.uomCancelbutton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.uomCancelbutton.ForeColor = System.Drawing.SystemColors.ControlText;
          this.uomCancelbutton.Location = new System.Drawing.Point(60, 199);
          this.uomCancelbutton.Name = "uomCancelbutton";
          this.uomCancelbutton.Size = new System.Drawing.Size(56, 25);
          this.uomCancelbutton.TabIndex = 142;
          this.uomCancelbutton.Text = "Cancel";
          this.uomCancelbutton.UseVisualStyleBackColor = true;
          this.uomCancelbutton.Click += new System.EventHandler(this.uomCancelbutton_Click);
          // 
          // submitCnvrtdQtybutton
          // 
          this.submitCnvrtdQtybutton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.submitCnvrtdQtybutton.ForeColor = System.Drawing.SystemColors.ControlText;
          this.submitCnvrtdQtybutton.Location = new System.Drawing.Point(4, 199);
          this.submitCnvrtdQtybutton.Name = "submitCnvrtdQtybutton";
          this.submitCnvrtdQtybutton.Size = new System.Drawing.Size(56, 25);
          this.submitCnvrtdQtybutton.TabIndex = 141;
          this.submitCnvrtdQtybutton.Text = "Submit";
          this.submitCnvrtdQtybutton.UseVisualStyleBackColor = true;
          this.submitCnvrtdQtybutton.Click += new System.EventHandler(this.submitCnvrtdQtybutton_Click);
          // 
          // ttlQtytextBox
          // 
          this.ttlQtytextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
          this.ttlQtytextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ttlQtytextBox.Location = new System.Drawing.Point(195, 200);
          this.ttlQtytextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
          this.ttlQtytextBox.Name = "ttlQtytextBox";
          this.ttlQtytextBox.ReadOnly = true;
          this.ttlQtytextBox.Size = new System.Drawing.Size(106, 21);
          this.ttlQtytextBox.TabIndex = 143;
          this.ttlQtytextBox.Tag = "po_id";
          this.ttlQtytextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // cntrltextBox
          // 
          this.cntrltextBox.Location = new System.Drawing.Point(241, 200);
          this.cntrltextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
          this.cntrltextBox.Name = "cntrltextBox";
          this.cntrltextBox.ReadOnly = true;
          this.cntrltextBox.Size = new System.Drawing.Size(34, 20);
          this.cntrltextBox.TabIndex = 144;
          this.cntrltextBox.Tag = "po_id";
          // 
          // label3
          // 
          this.label3.AutoSize = true;
          this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label3.ForeColor = System.Drawing.Color.White;
          this.label3.Location = new System.Drawing.Point(126, 203);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(70, 16);
          this.label3.TabIndex = 145;
          this.label3.Text = "Total Qty:";
          // 
          // dataGridViewUomConversion
          // 
          this.dataGridViewUomConversion.AllowUserToAddRows = false;
          this.dataGridViewUomConversion.AllowUserToDeleteRows = false;
          this.dataGridViewUomConversion.AllowUserToResizeRows = false;
          this.dataGridViewUomConversion.BackgroundColor = System.Drawing.Color.White;
          this.dataGridViewUomConversion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
          dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
          dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
          dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
          this.dataGridViewUomConversion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
          this.dataGridViewUomConversion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.dataGridViewUomConversion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.detListNo,
            this.detUom,
            this.detQty,
            this.detEqvBaseQty,
            this.detItmUomID,
            this.detUomId,
            this.detSortOrder,
            this.detCnvsnFactor,
            this.Column1,
            this.Column2,
            this.Column3});
          dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
          dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
          dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
          dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
          this.dataGridViewUomConversion.DefaultCellStyle = dataGridViewCellStyle2;
          this.dataGridViewUomConversion.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
          this.dataGridViewUomConversion.Location = new System.Drawing.Point(1, 2);
          this.dataGridViewUomConversion.Name = "dataGridViewUomConversion";
          this.dataGridViewUomConversion.Size = new System.Drawing.Size(536, 191);
          this.dataGridViewUomConversion.TabIndex = 76;
          this.dataGridViewUomConversion.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUomConversion_CellValueChanged);
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.label1.ForeColor = System.Drawing.Color.White;
          this.label1.Location = new System.Drawing.Point(307, 203);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(79, 16);
          this.label1.TabIndex = 148;
          this.label1.Text = "Total Price:";
          // 
          // ttlPriceTextBox
          // 
          this.ttlPriceTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
          this.ttlPriceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ttlPriceTextBox.Location = new System.Drawing.Point(383, 200);
          this.ttlPriceTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
          this.ttlPriceTextBox.Name = "ttlPriceTextBox";
          this.ttlPriceTextBox.ReadOnly = true;
          this.ttlPriceTextBox.Size = new System.Drawing.Size(154, 21);
          this.ttlPriceTextBox.TabIndex = 146;
          this.ttlPriceTextBox.Tag = "";
          this.ttlPriceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
          // 
          // detListNo
          // 
          this.detListNo.HeaderText = "No.";
          this.detListNo.Name = "detListNo";
          this.detListNo.ReadOnly = true;
          this.detListNo.Width = 30;
          // 
          // detUom
          // 
          this.detUom.HeaderText = "UOM";
          this.detUom.Name = "detUom";
          this.detUom.ReadOnly = true;
          this.detUom.Width = 80;
          // 
          // detQty
          // 
          this.detQty.HeaderText = "Quantity";
          this.detQty.Name = "detQty";
          this.detQty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
          this.detQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
          this.detQty.Width = 80;
          // 
          // detEqvBaseQty
          // 
          this.detEqvBaseQty.HeaderText = "Equivalent Base Qty";
          this.detEqvBaseQty.Name = "detEqvBaseQty";
          this.detEqvBaseQty.ReadOnly = true;
          // 
          // detItmUomID
          // 
          this.detItmUomID.HeaderText = "";
          this.detItmUomID.Name = "detItmUomID";
          this.detItmUomID.ReadOnly = true;
          this.detItmUomID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
          this.detItmUomID.Visible = false;
          this.detItmUomID.Width = 5;
          // 
          // detUomId
          // 
          this.detUomId.HeaderText = "";
          this.detUomId.Name = "detUomId";
          this.detUomId.ReadOnly = true;
          this.detUomId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
          this.detUomId.Visible = false;
          this.detUomId.Width = 5;
          // 
          // detSortOrder
          // 
          this.detSortOrder.HeaderText = "";
          this.detSortOrder.Name = "detSortOrder";
          this.detSortOrder.ReadOnly = true;
          this.detSortOrder.Resizable = System.Windows.Forms.DataGridViewTriState.False;
          this.detSortOrder.Visible = false;
          this.detSortOrder.Width = 5;
          // 
          // detCnvsnFactor
          // 
          this.detCnvsnFactor.HeaderText = "";
          this.detCnvsnFactor.Name = "detCnvsnFactor";
          this.detCnvsnFactor.ReadOnly = true;
          this.detCnvsnFactor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
          this.detCnvsnFactor.Visible = false;
          this.detCnvsnFactor.Width = 5;
          // 
          // Column1
          // 
          this.Column1.HeaderText = "Selling Price";
          this.Column1.Name = "Column1";
          this.Column1.ReadOnly = true;
          this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
          // 
          // Column2
          // 
          this.Column2.HeaderText = "Price Less Tax";
          this.Column2.Name = "Column2";
          this.Column2.ReadOnly = true;
          this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
          this.Column2.Visible = false;
          // 
          // Column3
          // 
          this.Column3.HeaderText = "Total Amount";
          this.Column3.Name = "Column3";
          this.Column3.ReadOnly = true;
          this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
          // 
          // uomConversion
          // 
          this.AcceptButton = this.submitCnvrtdQtybutton;
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
          this.CancelButton = this.uomCancelbutton;
          this.ClientSize = new System.Drawing.Size(539, 227);
          this.Controls.Add(this.ttlPriceTextBox);
          this.Controls.Add(this.ttlQtytextBox);
          this.Controls.Add(this.label3);
          this.Controls.Add(this.uomCancelbutton);
          this.Controls.Add(this.submitCnvrtdQtybutton);
          this.Controls.Add(this.dataGridViewUomConversion);
          this.Controls.Add(this.cntrltextBox);
          this.Controls.Add(this.label1);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "uomConversion";
          this.ShowIcon = false;
          this.ShowInTaskbar = false;
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "Item UOM Conversion";
          this.Load += new System.EventHandler(this.uomConversion_Load);
          ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUomConversion)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private StoresAndInventoryManager.Classes.MyDataGridView dataGridViewUomConversion;
        private System.Windows.Forms.Button uomCancelbutton;
        private System.Windows.Forms.Button submitCnvrtdQtybutton;
        private System.Windows.Forms.TextBox ttlQtytextBox;
        private System.Windows.Forms.TextBox cntrltextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ttlPriceTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn detListNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn detUom;
        private System.Windows.Forms.DataGridViewTextBoxColumn detQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn detEqvBaseQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn detItmUomID;
        private System.Windows.Forms.DataGridViewTextBoxColumn detUomId;
        private System.Windows.Forms.DataGridViewTextBoxColumn detSortOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn detCnvsnFactor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}