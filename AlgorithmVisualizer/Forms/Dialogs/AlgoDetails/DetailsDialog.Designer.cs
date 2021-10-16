
namespace AlgorithmVisualizer.Forms.Dialogs.AlgoDetails
{
	partial class DetailsDialog
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
			this.richTextBox = new System.Windows.Forms.RichTextBox();
			this.btnExplanation = new System.Windows.Forms.Button();
			this.btnImplementation = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// richTextBox
			// 
			this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.richTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.richTextBox.Location = new System.Drawing.Point(12, 48);
			this.richTextBox.Name = "richTextBox";
			this.richTextBox.Size = new System.Drawing.Size(819, 570);
			this.richTextBox.TabIndex = 0;
			this.richTextBox.Text = "";
			// 
			// btnExplanation
			// 
			this.btnExplanation.BackColor = System.Drawing.Color.Gray;
			this.btnExplanation.FlatAppearance.BorderSize = 0;
			this.btnExplanation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExplanation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExplanation.Location = new System.Drawing.Point(12, 13);
			this.btnExplanation.Name = "btnExplanation";
			this.btnExplanation.Size = new System.Drawing.Size(104, 29);
			this.btnExplanation.TabIndex = 1;
			this.btnExplanation.Text = "Explanation";
			this.btnExplanation.UseVisualStyleBackColor = false;
			this.btnExplanation.Click += new System.EventHandler(this.btnExplanation_Click);
			// 
			// btnImplementation
			// 
			this.btnImplementation.BackColor = System.Drawing.Color.Gray;
			this.btnImplementation.FlatAppearance.BorderSize = 0;
			this.btnImplementation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnImplementation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnImplementation.Location = new System.Drawing.Point(122, 13);
			this.btnImplementation.Name = "btnImplementation";
			this.btnImplementation.Size = new System.Drawing.Size(130, 29);
			this.btnImplementation.TabIndex = 2;
			this.btnImplementation.Text = "Implementation";
			this.btnImplementation.UseVisualStyleBackColor = false;
			this.btnImplementation.Click += new System.EventHandler(this.btnImplementation_Click);
			// 
			// DetailsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.ClientSize = new System.Drawing.Size(843, 630);
			this.Controls.Add(this.btnImplementation);
			this.Controls.Add(this.btnExplanation);
			this.Controls.Add(this.richTextBox);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.Name = "DetailsDialog";
			this.Text = "Details";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBox;
		private System.Windows.Forms.Button btnExplanation;
		private System.Windows.Forms.Button btnImplementation;
	}
}