
namespace AlgorithmVisualizer.Forms
{
	partial class ArrayAlgoForm
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
			this.lblAlgoComboBox = new System.Windows.Forms.Label();
			this.algoComboBox = new System.Windows.Forms.ComboBox();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnViz = new System.Windows.Forms.Button();
			this.btnPauseResume = new System.Windows.Forms.Button();
			this.lblArrSizeBar = new System.Windows.Forms.Label();
			this.lblSpeedBar = new System.Windows.Forms.Label();
			this.arrSizeBar = new System.Windows.Forms.HScrollBar();
			this.speedBar = new System.Windows.Forms.HScrollBar();
			this.panelControls = new System.Windows.Forms.Panel();
			this.btnDetails = new System.Windows.Forms.Button();
			this.panelMain = new System.Windows.Forms.Panel();
			this.panelControls.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblAlgoComboBox
			// 
			this.lblAlgoComboBox.AutoSize = true;
			this.lblAlgoComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAlgoComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.lblAlgoComboBox.Location = new System.Drawing.Point(9, 17);
			this.lblAlgoComboBox.Name = "lblAlgoComboBox";
			this.lblAlgoComboBox.Size = new System.Drawing.Size(67, 17);
			this.lblAlgoComboBox.TabIndex = 1;
			this.lblAlgoComboBox.Text = "Algorithm";
			// 
			// algoComboBox
			// 
			this.algoComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.algoComboBox.FormattingEnabled = true;
			this.algoComboBox.Location = new System.Drawing.Point(76, 15);
			this.algoComboBox.Name = "algoComboBox";
			this.algoComboBox.Size = new System.Drawing.Size(143, 24);
			this.algoComboBox.TabIndex = 2;
			this.algoComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
			// 
			// btnReset
			// 
			this.btnReset.BackColor = System.Drawing.Color.Gray;
			this.btnReset.FlatAppearance.BorderSize = 0;
			this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnReset.Location = new System.Drawing.Point(387, 15);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(75, 23);
			this.btnReset.TabIndex = 3;
			this.btnReset.Text = "Reset";
			this.btnReset.UseVisualStyleBackColor = false;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnViz
			// 
			this.btnViz.BackColor = System.Drawing.Color.Gray;
			this.btnViz.FlatAppearance.BorderSize = 0;
			this.btnViz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnViz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnViz.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnViz.Location = new System.Drawing.Point(225, 15);
			this.btnViz.Name = "btnViz";
			this.btnViz.Size = new System.Drawing.Size(75, 23);
			this.btnViz.TabIndex = 5;
			this.btnViz.Text = "Visualize";
			this.btnViz.UseVisualStyleBackColor = false;
			this.btnViz.Click += new System.EventHandler(this.btnViz_Click);
			// 
			// btnPauseResume
			// 
			this.btnPauseResume.BackColor = System.Drawing.Color.Gray;
			this.btnPauseResume.Enabled = false;
			this.btnPauseResume.FlatAppearance.BorderSize = 0;
			this.btnPauseResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnPauseResume.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPauseResume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnPauseResume.Location = new System.Drawing.Point(306, 15);
			this.btnPauseResume.Name = "btnPauseResume";
			this.btnPauseResume.Size = new System.Drawing.Size(75, 23);
			this.btnPauseResume.TabIndex = 6;
			this.btnPauseResume.Text = "Pause";
			this.btnPauseResume.UseVisualStyleBackColor = false;
			this.btnPauseResume.Click += new System.EventHandler(this.btnPauseResume_Click);
			// 
			// lblArrSizeBar
			// 
			this.lblArrSizeBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblArrSizeBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.lblArrSizeBar.Location = new System.Drawing.Point(686, 15);
			this.lblArrSizeBar.Name = "lblArrSizeBar";
			this.lblArrSizeBar.Size = new System.Drawing.Size(73, 21);
			this.lblArrSizeBar.TabIndex = 14;
			this.lblArrSizeBar.Text = "Array size";
			this.lblArrSizeBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblSpeedBar
			// 
			this.lblSpeedBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSpeedBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.lblSpeedBar.Location = new System.Drawing.Point(465, 15);
			this.lblSpeedBar.Name = "lblSpeedBar";
			this.lblSpeedBar.Size = new System.Drawing.Size(64, 23);
			this.lblSpeedBar.TabIndex = 13;
			this.lblSpeedBar.Text = "Speed";
			this.lblSpeedBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// arrSizeBar
			// 
			this.arrSizeBar.LargeChange = 1;
			this.arrSizeBar.Location = new System.Drawing.Point(762, 15);
			this.arrSizeBar.Maximum = 1000;
			this.arrSizeBar.Minimum = 10;
			this.arrSizeBar.Name = "arrSizeBar";
			this.arrSizeBar.Size = new System.Drawing.Size(149, 23);
			this.arrSizeBar.TabIndex = 12;
			this.arrSizeBar.Value = 100;
			this.arrSizeBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.arrSizeBar_Scroll);
			// 
			// speedBar
			// 
			this.speedBar.LargeChange = 1;
			this.speedBar.Location = new System.Drawing.Point(532, 15);
			this.speedBar.Name = "speedBar";
			this.speedBar.Size = new System.Drawing.Size(151, 23);
			this.speedBar.TabIndex = 11;
			this.speedBar.Value = 95;
			this.speedBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.speedBar_Scroll);
			// 
			// panelControls
			// 
			this.panelControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
			this.panelControls.Controls.Add(this.btnDetails);
			this.panelControls.Controls.Add(this.algoComboBox);
			this.panelControls.Controls.Add(this.lblArrSizeBar);
			this.panelControls.Controls.Add(this.lblAlgoComboBox);
			this.panelControls.Controls.Add(this.lblSpeedBar);
			this.panelControls.Controls.Add(this.btnReset);
			this.panelControls.Controls.Add(this.arrSizeBar);
			this.panelControls.Controls.Add(this.btnViz);
			this.panelControls.Controls.Add(this.speedBar);
			this.panelControls.Controls.Add(this.btnPauseResume);
			this.panelControls.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControls.Location = new System.Drawing.Point(0, 0);
			this.panelControls.Name = "panelControls";
			this.panelControls.Size = new System.Drawing.Size(1008, 57);
			this.panelControls.TabIndex = 15;
			// 
			// btnDetails
			// 
			this.btnDetails.BackColor = System.Drawing.Color.Gray;
			this.btnDetails.FlatAppearance.BorderSize = 0;
			this.btnDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnDetails.Location = new System.Drawing.Point(921, 15);
			this.btnDetails.Name = "btnDetails";
			this.btnDetails.Size = new System.Drawing.Size(75, 23);
			this.btnDetails.TabIndex = 16;
			this.btnDetails.Text = "Details";
			this.btnDetails.UseVisualStyleBackColor = false;
			this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
			// 
			// panelMain
			// 
			this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 57);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(1008, 504);
			this.panelMain.TabIndex = 16;
			// 
			// ArrayAlgoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 561);
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.panelControls);
			this.Name = "ArrayAlgoForm";
			this.Text = "Array algorithms";
			this.panelControls.ResumeLayout(false);
			this.panelControls.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label lblAlgoComboBox;
		private System.Windows.Forms.ComboBox algoComboBox;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnViz;
		private System.Windows.Forms.Button btnPauseResume;
		private System.Windows.Forms.Label lblArrSizeBar;
		private System.Windows.Forms.Label lblSpeedBar;
		private System.Windows.Forms.HScrollBar arrSizeBar;
		private System.Windows.Forms.HScrollBar speedBar;
		private System.Windows.Forms.Panel panelControls;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Button btnDetails;
	}
}

