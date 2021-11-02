
namespace AlgorithmVisualizer.Forms.Dialogs
{
	partial class EdgeDialog
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
			this.radioBtnDirected = new System.Windows.Forms.RadioButton();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblCost = new System.Windows.Forms.Label();
			this.lblTo = new System.Windows.Forms.Label();
			this.textBoxCost = new System.Windows.Forms.TextBox();
			this.textBoxTo = new System.Windows.Forms.TextBox();
			this.radioBtnUndirected = new System.Windows.Forms.RadioButton();
			this.lblFrom = new System.Windows.Forms.Label();
			this.textBoxFrom = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// radioBtnDirected
			// 
			this.radioBtnDirected.AutoSize = true;
			this.radioBtnDirected.Checked = true;
			this.radioBtnDirected.Location = new System.Drawing.Point(9, 84);
			this.radioBtnDirected.Name = "radioBtnDirected";
			this.radioBtnDirected.Size = new System.Drawing.Size(65, 17);
			this.radioBtnDirected.TabIndex = 12;
			this.radioBtnDirected.TabStop = true;
			this.radioBtnDirected.Text = "Directed";
			this.radioBtnDirected.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.BackColor = System.Drawing.Color.Gray;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatAppearance.BorderSize = 0;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.Location = new System.Drawing.Point(42, 110);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 11;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblCost
			// 
			this.lblCost.AutoSize = true;
			this.lblCost.Location = new System.Drawing.Point(6, 61);
			this.lblCost.Name = "lblCost";
			this.lblCost.Size = new System.Drawing.Size(28, 13);
			this.lblCost.TabIndex = 10;
			this.lblCost.Text = "Cost";
			// 
			// lblTo
			// 
			this.lblTo.AutoSize = true;
			this.lblTo.Location = new System.Drawing.Point(6, 35);
			this.lblTo.Name = "lblTo";
			this.lblTo.Size = new System.Drawing.Size(20, 13);
			this.lblTo.TabIndex = 9;
			this.lblTo.Text = "To";
			// 
			// textBoxCost
			// 
			this.textBoxCost.Location = new System.Drawing.Point(49, 58);
			this.textBoxCost.Name = "textBoxCost";
			this.textBoxCost.Size = new System.Drawing.Size(100, 20);
			this.textBoxCost.TabIndex = 8;
			// 
			// textBoxTo
			// 
			this.textBoxTo.Location = new System.Drawing.Point(49, 32);
			this.textBoxTo.Name = "textBoxTo";
			this.textBoxTo.Size = new System.Drawing.Size(100, 20);
			this.textBoxTo.TabIndex = 7;
			// 
			// radioBtnUndirected
			// 
			this.radioBtnUndirected.AutoSize = true;
			this.radioBtnUndirected.Location = new System.Drawing.Point(72, 84);
			this.radioBtnUndirected.Name = "radioBtnUndirected";
			this.radioBtnUndirected.Size = new System.Drawing.Size(77, 17);
			this.radioBtnUndirected.TabIndex = 16;
			this.radioBtnUndirected.Text = "Undirected";
			this.radioBtnUndirected.UseVisualStyleBackColor = true;
			// 
			// lblFrom
			// 
			this.lblFrom.AutoSize = true;
			this.lblFrom.Location = new System.Drawing.Point(6, 10);
			this.lblFrom.Name = "lblFrom";
			this.lblFrom.Size = new System.Drawing.Size(30, 13);
			this.lblFrom.TabIndex = 18;
			this.lblFrom.Text = "From";
			// 
			// txtBoxFrom
			// 
			this.textBoxFrom.Enabled = false;
			this.textBoxFrom.Location = new System.Drawing.Point(49, 7);
			this.textBoxFrom.Name = "txtBoxFrom";
			this.textBoxFrom.Size = new System.Drawing.Size(100, 20);
			this.textBoxFrom.TabIndex = 17;
			// 
			// EdgeDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
			this.ClientSize = new System.Drawing.Size(160, 141);
			this.Controls.Add(this.lblFrom);
			this.Controls.Add(this.textBoxFrom);
			this.Controls.Add(this.radioBtnUndirected);
			this.Controls.Add(this.radioBtnDirected);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lblCost);
			this.Controls.Add(this.lblTo);
			this.Controls.Add(this.textBoxCost);
			this.Controls.Add(this.textBoxTo);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EdgeDialog";
			this.Text = "Add/Remove edge";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.RadioButton radioBtnDirected;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblCost;
		private System.Windows.Forms.Label lblTo;
		private System.Windows.Forms.TextBox textBoxCost;
		private System.Windows.Forms.TextBox textBoxTo;
		private System.Windows.Forms.RadioButton radioBtnUndirected;
		private System.Windows.Forms.Label lblFrom;
		private System.Windows.Forms.TextBox textBoxFrom;
	}
}