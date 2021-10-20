
namespace AlgorithmVisualizer.Forms.Dialogs
{
	partial class PresetDialog
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
			this.btnLoadPreset = new System.Windows.Forms.Button();
			this.listView = new System.Windows.Forms.ListView();
			this.btnRemovePreset = new System.Windows.Forms.Button();
			this.btnSaveNewPreset = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnLoadPreset
			// 
			this.btnLoadPreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadPreset.BackColor = System.Drawing.Color.Gray;
			this.btnLoadPreset.FlatAppearance.BorderSize = 0;
			this.btnLoadPreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLoadPreset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnLoadPreset.Location = new System.Drawing.Point(44, 428);
			this.btnLoadPreset.Name = "btnLoadPreset";
			this.btnLoadPreset.Size = new System.Drawing.Size(100, 23);
			this.btnLoadPreset.TabIndex = 12;
			this.btnLoadPreset.Text = "Load selected";
			this.btnLoadPreset.UseVisualStyleBackColor = false;
			this.btnLoadPreset.Click += new System.EventHandler(this.btnLoadPreset_Click);
			// 
			// listView
			// 
			this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.listView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.listView.HideSelection = false;
			this.listView.Location = new System.Drawing.Point(12, 12);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(371, 410);
			this.listView.TabIndex = 13;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseClick);
			// 
			// btnRemovePreset
			// 
			this.btnRemovePreset.BackColor = System.Drawing.Color.Gray;
			this.btnRemovePreset.FlatAppearance.BorderSize = 0;
			this.btnRemovePreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRemovePreset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnRemovePreset.Location = new System.Drawing.Point(147, 428);
			this.btnRemovePreset.Name = "btnRemovePreset";
			this.btnRemovePreset.Size = new System.Drawing.Size(100, 23);
			this.btnRemovePreset.TabIndex = 15;
			this.btnRemovePreset.Text = "Remove selected";
			this.btnRemovePreset.UseVisualStyleBackColor = false;
			this.btnRemovePreset.Click += new System.EventHandler(this.btnRemovePreset_Click);
			// 
			// btnSaveNewPreset
			// 
			this.btnSaveNewPreset.BackColor = System.Drawing.Color.Gray;
			this.btnSaveNewPreset.FlatAppearance.BorderSize = 0;
			this.btnSaveNewPreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSaveNewPreset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnSaveNewPreset.Location = new System.Drawing.Point(250, 428);
			this.btnSaveNewPreset.Name = "btnSaveNewPreset";
			this.btnSaveNewPreset.Size = new System.Drawing.Size(100, 23);
			this.btnSaveNewPreset.TabIndex = 16;
			this.btnSaveNewPreset.Text = "New preset";
			this.btnSaveNewPreset.UseVisualStyleBackColor = false;
			this.btnSaveNewPreset.Click += new System.EventHandler(this.btnSaveNewPreset_Click);
			// 
			// PresetDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
			this.ClientSize = new System.Drawing.Size(395, 463);
			this.Controls.Add(this.btnSaveNewPreset);
			this.Controls.Add(this.btnRemovePreset);
			this.Controls.Add(this.listView);
			this.Controls.Add(this.btnLoadPreset);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MinimizeBox = false;
			this.Name = "PresetDialog";
			this.Text = "Select a preset";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnLoadPreset;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.Button btnRemovePreset;
		private System.Windows.Forms.Button btnSaveNewPreset;
	}
}