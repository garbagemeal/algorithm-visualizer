
namespace AlgorithmVisualizer.Forms.Dialogs
{
	partial class VertexDialog
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
			this.textBoxID = new System.Windows.Forms.TextBox();
			this.textBoxData = new System.Windows.Forms.TextBox();
			this.lblID = new System.Windows.Forms.Label();
			this.lblData = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxID
			// 
			this.textBoxID.Location = new System.Drawing.Point(50, 12);
			this.textBoxID.Name = "textBoxID";
			this.textBoxID.Size = new System.Drawing.Size(100, 20);
			this.textBoxID.TabIndex = 0;
			// 
			// textBoxData
			// 
			this.textBoxData.Location = new System.Drawing.Point(50, 38);
			this.textBoxData.Name = "textBoxData";
			this.textBoxData.Size = new System.Drawing.Size(100, 20);
			this.textBoxData.TabIndex = 1;
			// 
			// lblID
			// 
			this.lblID.AutoSize = true;
			this.lblID.Location = new System.Drawing.Point(9, 15);
			this.lblID.Name = "lblID";
			this.lblID.Size = new System.Drawing.Size(18, 13);
			this.lblID.TabIndex = 2;
			this.lblID.Text = "ID";
			// 
			// lblData
			// 
			this.lblData.AutoSize = true;
			this.lblData.Location = new System.Drawing.Point(9, 41);
			this.lblData.Name = "lblData";
			this.lblData.Size = new System.Drawing.Size(30, 13);
			this.lblData.TabIndex = 3;
			this.lblData.Text = "Data";
			// 
			// btnOK
			// 
			this.btnOK.BackColor = System.Drawing.Color.Gray;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatAppearance.BorderSize = 0;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.Location = new System.Drawing.Point(40, 68);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// VertexDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
			this.ClientSize = new System.Drawing.Size(162, 99);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lblData);
			this.Controls.Add(this.lblID);
			this.Controls.Add(this.textBoxData);
			this.Controls.Add(this.textBoxID);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "VertexDialog";
			this.Text = "Add a vertex";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxID;
		private System.Windows.Forms.TextBox textBoxData;
		private System.Windows.Forms.Label lblID;
		private System.Windows.Forms.Label lblData;
		private System.Windows.Forms.Button btnOK;
	}
}