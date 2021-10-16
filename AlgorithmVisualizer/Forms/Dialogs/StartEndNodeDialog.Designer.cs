
namespace AlgorithmVisualizer.Forms.Dialogs
{
	partial class StartEndNodeDialog
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
			this.btnOK = new System.Windows.Forms.Button();
			this.lblTo = new System.Windows.Forms.Label();
			this.lblFrom = new System.Windows.Forms.Label();
			this.textBoxTo = new System.Windows.Forms.TextBox();
			this.textBoxFrom = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.BackColor = System.Drawing.Color.Gray;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatAppearance.BorderSize = 0;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.Location = new System.Drawing.Point(49, 64);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 9;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblTo
			// 
			this.lblTo.AutoSize = true;
			this.lblTo.Location = new System.Drawing.Point(5, 41);
			this.lblTo.Name = "lblTo";
			this.lblTo.Size = new System.Drawing.Size(53, 13);
			this.lblTo.TabIndex = 8;
			this.lblTo.Text = "End node";
			// 
			// lblFrom
			// 
			this.lblFrom.AutoSize = true;
			this.lblFrom.Location = new System.Drawing.Point(5, 15);
			this.lblFrom.Name = "lblFrom";
			this.lblFrom.Size = new System.Drawing.Size(56, 13);
			this.lblFrom.TabIndex = 7;
			this.lblFrom.Text = "Start node";
			// 
			// textBoxTo
			// 
			this.textBoxTo.Location = new System.Drawing.Point(67, 38);
			this.textBoxTo.Name = "textBoxTo";
			this.textBoxTo.Size = new System.Drawing.Size(100, 20);
			this.textBoxTo.TabIndex = 6;
			// 
			// textBoxFrom
			// 
			this.textBoxFrom.Location = new System.Drawing.Point(67, 12);
			this.textBoxFrom.Name = "textBoxFrom";
			this.textBoxFrom.Size = new System.Drawing.Size(100, 20);
			this.textBoxFrom.TabIndex = 5;
			// 
			// StartEndNodeDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
			this.ClientSize = new System.Drawing.Size(179, 95);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lblTo);
			this.Controls.Add(this.lblFrom);
			this.Controls.Add(this.textBoxTo);
			this.Controls.Add(this.textBoxFrom);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StartEndNodeDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblTo;
		private System.Windows.Forms.Label lblFrom;
		private System.Windows.Forms.TextBox textBoxTo;
		private System.Windows.Forms.TextBox textBoxFrom;
	}
}