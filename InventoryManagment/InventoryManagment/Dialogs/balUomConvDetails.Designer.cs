namespace StoresAndInventoryManager.Forms
{
    partial class balUomConvDetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.uomCancelbutton = new System.Windows.Forms.Button();
            this.ttlQtytextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rsvdQtytextBox = new System.Windows.Forms.TextBox();
            this.avlblQtytextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewUomConversion = new StoresAndInventoryManager.Classes.MyDataGridView();
            this.detListNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detUom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detTotQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detTotEqivBseQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detRsvdQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detEquivRsvdQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detAvlblQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detEquivAvlblQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detItmUomID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detUomId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detSortOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detCnvsnFactor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUomConversion)).BeginInit();
            this.SuspendLayout();
            // 
            // uomCancelbutton
            // 
            this.uomCancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uomCancelbutton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uomCancelbutton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.uomCancelbutton.Location = new System.Drawing.Point(12, 185);
            this.uomCancelbutton.Name = "uomCancelbutton";
            this.uomCancelbutton.Size = new System.Drawing.Size(56, 25);
            this.uomCancelbutton.TabIndex = 148;
            this.uomCancelbutton.Text = "Cancel";
            this.uomCancelbutton.UseVisualStyleBackColor = true;
            this.uomCancelbutton.Click += new System.EventHandler(this.uomCancelbutton_Click);
            // 
            // ttlQtytextBox
            // 
            this.ttlQtytextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ttlQtytextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ttlQtytextBox.Location = new System.Drawing.Point(258, 185);
            this.ttlQtytextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ttlQtytextBox.Name = "ttlQtytextBox";
            this.ttlQtytextBox.ReadOnly = true;
            this.ttlQtytextBox.Size = new System.Drawing.Size(93, 21);
            this.ttlQtytextBox.TabIndex = 149;
            this.ttlQtytextBox.Tag = "po_id";
            this.ttlQtytextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(200, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 151;
            this.label3.Text = "Total Qty:";
            // 
            // rsvdQtytextBox
            // 
            this.rsvdQtytextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rsvdQtytextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rsvdQtytextBox.Location = new System.Drawing.Point(459, 185);
            this.rsvdQtytextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rsvdQtytextBox.Name = "rsvdQtytextBox";
            this.rsvdQtytextBox.ReadOnly = true;
            this.rsvdQtytextBox.Size = new System.Drawing.Size(93, 21);
            this.rsvdQtytextBox.TabIndex = 152;
            this.rsvdQtytextBox.Tag = "po_id";
            this.rsvdQtytextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // avlblQtytextBox
            // 
            this.avlblQtytextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.avlblQtytextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.avlblQtytextBox.Location = new System.Drawing.Point(661, 185);
            this.avlblQtytextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.avlblQtytextBox.Name = "avlblQtytextBox";
            this.avlblQtytextBox.ReadOnly = true;
            this.avlblQtytextBox.Size = new System.Drawing.Size(93, 21);
            this.avlblQtytextBox.TabIndex = 153;
            this.avlblQtytextBox.Tag = "po_id";
            this.avlblQtytextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(375, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 15);
            this.label1.TabIndex = 154;
            this.label1.Text = "Reserved Qty:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(577, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 155;
            this.label2.Text = "Available Qty:";
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
            this.detTotQty,
            this.detTotEqivBseQty,
            this.detRsvdQty,
            this.detEquivRsvdQty,
            this.detAvlblQty,
            this.detEquivAvlblQty,
            this.detItmUomID,
            this.detUomId,
            this.detSortOrder,
            this.detCnvsnFactor});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewUomConversion.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewUomConversion.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewUomConversion.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewUomConversion.Name = "dataGridViewUomConversion";
            this.dataGridViewUomConversion.Size = new System.Drawing.Size(757, 174);
            this.dataGridViewUomConversion.TabIndex = 146;
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
            // detTotQty
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.detTotQty.DefaultCellStyle = dataGridViewCellStyle2;
            this.detTotQty.HeaderText = "TOTAL: UOM_Qty";
            this.detTotQty.Name = "detTotQty";
            this.detTotQty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.detTotQty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // detTotEqivBseQty
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.detTotEqivBseQty.DefaultCellStyle = dataGridViewCellStyle3;
            this.detTotEqivBseQty.HeaderText = "TOTAL: Equivalent Qty";
            this.detTotEqivBseQty.Name = "detTotEqivBseQty";
            // 
            // detRsvdQty
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.detRsvdQty.DefaultCellStyle = dataGridViewCellStyle4;
            this.detRsvdQty.HeaderText = "RESERVED: UOM Qty";
            this.detRsvdQty.Name = "detRsvdQty";
            this.detRsvdQty.ReadOnly = true;
            // 
            // detEquivRsvdQty
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.detEquivRsvdQty.DefaultCellStyle = dataGridViewCellStyle5;
            this.detEquivRsvdQty.HeaderText = "RESERVED: Equivalent Qty";
            this.detEquivRsvdQty.Name = "detEquivRsvdQty";
            // 
            // detAvlblQty
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.detAvlblQty.DefaultCellStyle = dataGridViewCellStyle6;
            this.detAvlblQty.HeaderText = "AVAILABLE: UOM Qty";
            this.detAvlblQty.Name = "detAvlblQty";
            this.detAvlblQty.ReadOnly = true;
            // 
            // detEquivAvlblQty
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.detEquivAvlblQty.DefaultCellStyle = dataGridViewCellStyle7;
            this.detEquivAvlblQty.HeaderText = "AVAILABLE: Equivalent Qty";
            this.detEquivAvlblQty.Name = "detEquivAvlblQty";
            this.detEquivAvlblQty.ReadOnly = true;
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
            // balUomConvDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(180)))));
            this.ClientSize = new System.Drawing.Size(758, 217);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.avlblQtytextBox);
            this.Controls.Add(this.rsvdQtytextBox);
            this.Controls.Add(this.uomCancelbutton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ttlQtytextBox);
            this.Controls.Add(this.dataGridViewUomConversion);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(774, 255);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(774, 255);
            this.Name = "balUomConvDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UOM Conversion Details";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUomConversion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button uomCancelbutton;
        private System.Windows.Forms.TextBox ttlQtytextBox;
        private StoresAndInventoryManager.Classes.MyDataGridView dataGridViewUomConversion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox rsvdQtytextBox;
        private System.Windows.Forms.TextBox avlblQtytextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn detListNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn detUom;
        private System.Windows.Forms.DataGridViewTextBoxColumn detTotQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn detTotEqivBseQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn detRsvdQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn detEquivRsvdQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn detAvlblQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn detEquivAvlblQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn detItmUomID;
        private System.Windows.Forms.DataGridViewTextBoxColumn detUomId;
        private System.Windows.Forms.DataGridViewTextBoxColumn detSortOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn detCnvsnFactor;
    }
}