namespace BasicPersonData.Dialogs
 {
 partial class addRltvsDiag
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
     this.rltnTypTextBox = new System.Windows.Forms.TextBox();
     this.rltvNameTextBox = new System.Windows.Forms.TextBox();
     this.idNoTextBox = new System.Windows.Forms.TextBox();
     this.cancelButton = new System.Windows.Forms.Button();
     this.okButton = new System.Windows.Forms.Button();
     this.rltnTypButton = new System.Windows.Forms.Button();
     this.idNoButton = new System.Windows.Forms.Button();
     this.label1 = new System.Windows.Forms.Label();
     this.label3 = new System.Windows.Forms.Label();
     this.label2 = new System.Windows.Forms.Label();
     this.SuspendLayout();
     // 
     // rltnTypTextBox
     // 
     this.rltnTypTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
     this.rltnTypTextBox.Location = new System.Drawing.Point(104, 70);
     this.rltnTypTextBox.MaxLength = 100;
     this.rltnTypTextBox.Name = "rltnTypTextBox";
     this.rltnTypTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
     this.rltnTypTextBox.Size = new System.Drawing.Size(139, 20);
     this.rltnTypTextBox.TabIndex = 3;
     // 
     // rltvNameTextBox
     // 
     this.rltvNameTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
     this.rltvNameTextBox.Location = new System.Drawing.Point(86, 34);
     this.rltvNameTextBox.Multiline = true;
     this.rltvNameTextBox.Name = "rltvNameTextBox";
     this.rltvNameTextBox.ReadOnly = true;
     this.rltvNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
     this.rltvNameTextBox.Size = new System.Drawing.Size(189, 29);
     this.rltvNameTextBox.TabIndex = 2;
     // 
     // idNoTextBox
     // 
     this.idNoTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
     this.idNoTextBox.Location = new System.Drawing.Point(86, 6);
     this.idNoTextBox.Name = "idNoTextBox";
     this.idNoTextBox.ReadOnly = true;
     this.idNoTextBox.Size = new System.Drawing.Size(157, 20);
     this.idNoTextBox.TabIndex = 0;
     // 
     // cancelButton
     // 
     this.cancelButton.ForeColor = System.Drawing.Color.Black;
     this.cancelButton.Location = new System.Drawing.Point(141, 94);
     this.cancelButton.Name = "cancelButton";
     this.cancelButton.Size = new System.Drawing.Size(75, 23);
     this.cancelButton.TabIndex = 6;
     this.cancelButton.Text = "Cancel";
     this.cancelButton.UseVisualStyleBackColor = true;
     this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
     // 
     // okButton
     // 
     this.okButton.ForeColor = System.Drawing.Color.Black;
     this.okButton.Location = new System.Drawing.Point(66, 94);
     this.okButton.Name = "okButton";
     this.okButton.Size = new System.Drawing.Size(75, 23);
     this.okButton.TabIndex = 5;
     this.okButton.Text = "OK";
     this.okButton.UseVisualStyleBackColor = true;
     this.okButton.Click += new System.EventHandler(this.okButton_Click);
     // 
     // rltnTypButton
     // 
     this.rltnTypButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
     this.rltnTypButton.ForeColor = System.Drawing.Color.Black;
     this.rltnTypButton.Location = new System.Drawing.Point(249, 69);
     this.rltnTypButton.Name = "rltnTypButton";
     this.rltnTypButton.Size = new System.Drawing.Size(28, 22);
     this.rltnTypButton.TabIndex = 4;
     this.rltnTypButton.Text = "...";
     this.rltnTypButton.UseVisualStyleBackColor = true;
     this.rltnTypButton.Click += new System.EventHandler(this.rltnTypButton_Click);
     // 
     // idNoButton
     // 
     this.idNoButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
     this.idNoButton.ForeColor = System.Drawing.Color.Black;
     this.idNoButton.Location = new System.Drawing.Point(249, 5);
     this.idNoButton.Name = "idNoButton";
     this.idNoButton.Size = new System.Drawing.Size(28, 22);
     this.idNoButton.TabIndex = 1;
     this.idNoButton.Text = "...";
     this.idNoButton.UseVisualStyleBackColor = true;
     this.idNoButton.Click += new System.EventHandler(this.idNoButton_Click);
     // 
     // label1
     // 
     this.label1.ForeColor = System.Drawing.Color.White;
     this.label1.Location = new System.Drawing.Point(6, 8);
     this.label1.Name = "label1";
     this.label1.Size = new System.Drawing.Size(74, 17);
     this.label1.TabIndex = 134;
     this.label1.Text = "ID No:";
     // 
     // label3
     // 
     this.label3.AutoSize = true;
     this.label3.ForeColor = System.Drawing.Color.White;
     this.label3.Location = new System.Drawing.Point(6, 74);
     this.label3.Name = "label3";
     this.label3.Size = new System.Drawing.Size(95, 13);
     this.label3.TabIndex = 136;
     this.label3.Text = "Relationship Type:";
     // 
     // label2
     // 
     this.label2.ForeColor = System.Drawing.Color.White;
     this.label2.Location = new System.Drawing.Point(6, 34);
     this.label2.Name = "label2";
     this.label2.Size = new System.Drawing.Size(78, 33);
     this.label2.TabIndex = 135;
     this.label2.Text = "Relative\'s Full Name:";
     // 
     // addRltvsDiag
     // 
     this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
     this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
     this.BackColor = System.Drawing.Color.LightSlateGray;
     this.ClientSize = new System.Drawing.Size(282, 119);
     this.Controls.Add(this.rltnTypTextBox);
     this.Controls.Add(this.rltvNameTextBox);
     this.Controls.Add(this.idNoTextBox);
     this.Controls.Add(this.cancelButton);
     this.Controls.Add(this.okButton);
     this.Controls.Add(this.rltnTypButton);
     this.Controls.Add(this.idNoButton);
     this.Controls.Add(this.label1);
     this.Controls.Add(this.label3);
     this.Controls.Add(this.label2);
     this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
     this.MaximizeBox = false;
     this.MinimizeBox = false;
     this.Name = "addRltvsDiag";
     this.ShowInTaskbar = false;
     this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
     this.Text = "Add/Edit Relative";
     this.Load += new System.EventHandler(this.addRltvsDiag_Load);
     this.ResumeLayout(false);
     this.PerformLayout();

   }

  #endregion

  public System.Windows.Forms.TextBox rltnTypTextBox;
  public System.Windows.Forms.TextBox rltvNameTextBox;
  public System.Windows.Forms.TextBox idNoTextBox;
  private System.Windows.Forms.Button cancelButton;
  private System.Windows.Forms.Button okButton;
  private System.Windows.Forms.Button rltnTypButton;
  private System.Windows.Forms.Button idNoButton;
  private System.Windows.Forms.Label label1;
  private System.Windows.Forms.Label label3;
  private System.Windows.Forms.Label label2;
  }
 }