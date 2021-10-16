
namespace AlgorithmVisualizer.Forms
{
	partial class TreeAlgoForm
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
			this.panelControls = new System.Windows.Forms.Panel();
			this.algoComboBox = new System.Windows.Forms.ComboBox();
			this.lblAlgoComboBox = new System.Windows.Forms.Label();
			this.lblSpeedBar = new System.Windows.Forms.Label();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.speedBar = new System.Windows.Forms.HScrollBar();
			this.btnPauseResume = new System.Windows.Forms.Button();
			this.panelMain = new System.Windows.Forms.Panel();
			this.panelControls.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelControls
			// 
			this.panelControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
			this.panelControls.Controls.Add(this.algoComboBox);
			this.panelControls.Controls.Add(this.lblAlgoComboBox);
			this.panelControls.Controls.Add(this.lblSpeedBar);
			this.panelControls.Controls.Add(this.btnReset);
			this.panelControls.Controls.Add(this.btnStart);
			this.panelControls.Controls.Add(this.speedBar);
			this.panelControls.Controls.Add(this.btnPauseResume);
			this.panelControls.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControls.Location = new System.Drawing.Point(0, 0);
			this.panelControls.Name = "panelControls";
			this.panelControls.Size = new System.Drawing.Size(1008, 57);
			this.panelControls.TabIndex = 18;
			// 
			// algoComboBox
			// 
			this.algoComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.algoComboBox.FormattingEnabled = true;
			this.algoComboBox.Location = new System.Drawing.Point(79, 15);
			this.algoComboBox.Name = "algoComboBox";
			this.algoComboBox.Size = new System.Drawing.Size(184, 24);
			this.algoComboBox.TabIndex = 2;
			// 
			// lblAlgoComboBox
			// 
			this.lblAlgoComboBox.AutoSize = true;
			this.lblAlgoComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAlgoComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.lblAlgoComboBox.Location = new System.Drawing.Point(9, 19);
			this.lblAlgoComboBox.Name = "lblAlgoComboBox";
			this.lblAlgoComboBox.Size = new System.Drawing.Size(67, 17);
			this.lblAlgoComboBox.TabIndex = 1;
			this.lblAlgoComboBox.Text = "Algorithm";
			// 
			// lblSpeedBar
			// 
			this.lblSpeedBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSpeedBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.lblSpeedBar.Location = new System.Drawing.Point(588, 17);
			this.lblSpeedBar.Name = "lblSpeedBar";
			this.lblSpeedBar.Size = new System.Drawing.Size(50, 20);
			this.lblSpeedBar.TabIndex = 13;
			this.lblSpeedBar.Text = "Speed";
			this.lblSpeedBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnReset
			// 
			this.btnReset.BackColor = System.Drawing.Color.Gray;
			this.btnReset.FlatAppearance.BorderSize = 0;
			this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnReset.Location = new System.Drawing.Point(350, 14);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(75, 23);
			this.btnReset.TabIndex = 3;
			this.btnReset.Text = "Reset";
			this.btnReset.UseVisualStyleBackColor = false;
			// 
			// btnStart
			// 
			this.btnStart.BackColor = System.Drawing.Color.Gray;
			this.btnStart.FlatAppearance.BorderSize = 0;
			this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnStart.Location = new System.Drawing.Point(269, 14);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 5;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = false;
			// 
			// speedBar
			// 
			this.speedBar.LargeChange = 1;
			this.speedBar.Location = new System.Drawing.Point(641, 15);
			this.speedBar.Name = "speedBar";
			this.speedBar.Size = new System.Drawing.Size(151, 23);
			this.speedBar.TabIndex = 11;
			this.speedBar.Value = 50;
			// 
			// btnPauseResume
			// 
			this.btnPauseResume.BackColor = System.Drawing.Color.Gray;
			this.btnPauseResume.Enabled = false;
			this.btnPauseResume.FlatAppearance.BorderSize = 0;
			this.btnPauseResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnPauseResume.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPauseResume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnPauseResume.Location = new System.Drawing.Point(431, 14);
			this.btnPauseResume.Name = "btnPauseResume";
			this.btnPauseResume.Size = new System.Drawing.Size(75, 23);
			this.btnPauseResume.TabIndex = 6;
			this.btnPauseResume.Text = "Pause";
			this.btnPauseResume.UseVisualStyleBackColor = false;
			// 
			// panelMain
			// 
			this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 57);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(1008, 504);
			this.panelMain.TabIndex = 19;
			// 
			// TreeAlgoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 561);
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.panelControls);
			this.Name = "TreeAlgoForm";
			this.Text = "TreeAlgoForm";
			this.panelControls.ResumeLayout(false);
			this.panelControls.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelControls;
		private System.Windows.Forms.ComboBox algoComboBox;
		private System.Windows.Forms.Label lblAlgoComboBox;
		private System.Windows.Forms.Label lblSpeedBar;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.HScrollBar speedBar;
		private System.Windows.Forms.Button btnPauseResume;
		private System.Windows.Forms.Panel panelMain;
	}
}