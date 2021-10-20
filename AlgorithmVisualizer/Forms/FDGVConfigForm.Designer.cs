
namespace AlgorithmVisualizer.Forms.Dialogs
{
	partial class FDGVConfigForm
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
			this.lblG = new System.Windows.Forms.Label();
			this.hScrollBarG = new System.Windows.Forms.HScrollBar();
			this.hScrollBarMaxParticleSpeed = new System.Windows.Forms.HScrollBar();
			this.lblMaxParticleSpeed = new System.Windows.Forms.Label();
			this.lblMaxCenterPullMag = new System.Windows.Forms.Label();
			this.hScrollBarMaxCenterPullMag = new System.Windows.Forms.HScrollBar();
			this.lblVelDecay = new System.Windows.Forms.Label();
			this.hScrollBarVelDecay = new System.Windows.Forms.HScrollBar();
			this.lblParticleSize = new System.Windows.Forms.Label();
			this.hScrollBarParticleSize = new System.Windows.Forms.HScrollBar();
			this.lblK = new System.Windows.Forms.Label();
			this.hScrollBarK = new System.Windows.Forms.HScrollBar();
			this.lblRestLen = new System.Windows.Forms.Label();
			this.hScrollBarRestLen = new System.Windows.Forms.HScrollBar();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnResetAll = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblG
			// 
			this.lblG.AutoSize = true;
			this.lblG.Location = new System.Drawing.Point(157, 14);
			this.lblG.Name = "lblG";
			this.lblG.Size = new System.Drawing.Size(110, 13);
			this.lblG.TabIndex = 1;
			this.lblG.Text = "Gravitational force (G)";
			// 
			// hScrollBarG
			// 
			this.hScrollBarG.Location = new System.Drawing.Point(13, 12);
			this.hScrollBarG.Maximum = 200;
			this.hScrollBarG.Name = "hScrollBarG";
			this.hScrollBarG.Size = new System.Drawing.Size(138, 20);
			this.hScrollBarG.TabIndex = 2;
			this.hScrollBarG.Value = 100;
			this.hScrollBarG.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarG_Scroll);
			// 
			// hScrollBarMaxParticleSpeed
			// 
			this.hScrollBarMaxParticleSpeed.Location = new System.Drawing.Point(13, 35);
			this.hScrollBarMaxParticleSpeed.Maximum = 200;
			this.hScrollBarMaxParticleSpeed.Name = "hScrollBarMaxParticleSpeed";
			this.hScrollBarMaxParticleSpeed.Size = new System.Drawing.Size(138, 20);
			this.hScrollBarMaxParticleSpeed.TabIndex = 5;
			this.hScrollBarMaxParticleSpeed.Value = 100;
			this.hScrollBarMaxParticleSpeed.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarMaxParticleSpeed_Scroll);
			// 
			// lblMaxParticleSpeed
			// 
			this.lblMaxParticleSpeed.AutoSize = true;
			this.lblMaxParticleSpeed.Location = new System.Drawing.Point(157, 37);
			this.lblMaxParticleSpeed.Name = "lblMaxParticleSpeed";
			this.lblMaxParticleSpeed.Size = new System.Drawing.Size(97, 13);
			this.lblMaxParticleSpeed.TabIndex = 4;
			this.lblMaxParticleSpeed.Text = "Max particle ppeed";
			// 
			// lblMaxCenterPullMag
			// 
			this.lblMaxCenterPullMag.AutoSize = true;
			this.lblMaxCenterPullMag.Location = new System.Drawing.Point(157, 60);
			this.lblMaxCenterPullMag.Name = "lblMaxCenterPullMag";
			this.lblMaxCenterPullMag.Size = new System.Drawing.Size(130, 13);
			this.lblMaxCenterPullMag.TabIndex = 4;
			this.lblMaxCenterPullMag.Text = "max center pull magnitude";
			// 
			// hScrollBarMaxCenterPullMag
			// 
			this.hScrollBarMaxCenterPullMag.Location = new System.Drawing.Point(13, 58);
			this.hScrollBarMaxCenterPullMag.Maximum = 200;
			this.hScrollBarMaxCenterPullMag.Name = "hScrollBarMaxCenterPullMag";
			this.hScrollBarMaxCenterPullMag.Size = new System.Drawing.Size(138, 20);
			this.hScrollBarMaxCenterPullMag.TabIndex = 5;
			this.hScrollBarMaxCenterPullMag.Value = 100;
			this.hScrollBarMaxCenterPullMag.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarMaxCenterPullMag_Scroll);
			// 
			// lblVelDecay
			// 
			this.lblVelDecay.AutoSize = true;
			this.lblVelDecay.Location = new System.Drawing.Point(157, 83);
			this.lblVelDecay.Name = "lblVelDecay";
			this.lblVelDecay.Size = new System.Drawing.Size(76, 13);
			this.lblVelDecay.TabIndex = 4;
			this.lblVelDecay.Text = "Velocity decay";
			// 
			// hScrollBarVelDecay
			// 
			this.hScrollBarVelDecay.Location = new System.Drawing.Point(13, 81);
			this.hScrollBarVelDecay.Maximum = 200;
			this.hScrollBarVelDecay.Name = "hScrollBarVelDecay";
			this.hScrollBarVelDecay.Size = new System.Drawing.Size(138, 20);
			this.hScrollBarVelDecay.TabIndex = 5;
			this.hScrollBarVelDecay.Value = 100;
			this.hScrollBarVelDecay.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarVelDecay_Scroll);
			// 
			// lblParticleSize
			// 
			this.lblParticleSize.AutoSize = true;
			this.lblParticleSize.Location = new System.Drawing.Point(157, 106);
			this.lblParticleSize.Name = "lblParticleSize";
			this.lblParticleSize.Size = new System.Drawing.Size(63, 13);
			this.lblParticleSize.TabIndex = 4;
			this.lblParticleSize.Text = "Particle size";
			// 
			// hScrollBarParticleSize
			// 
			this.hScrollBarParticleSize.Location = new System.Drawing.Point(13, 104);
			this.hScrollBarParticleSize.Maximum = 200;
			this.hScrollBarParticleSize.Name = "hScrollBarParticleSize";
			this.hScrollBarParticleSize.Size = new System.Drawing.Size(138, 20);
			this.hScrollBarParticleSize.TabIndex = 5;
			this.hScrollBarParticleSize.Value = 100;
			this.hScrollBarParticleSize.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarParticleSize_Scroll);
			// 
			// lblK
			// 
			this.lblK.AutoSize = true;
			this.lblK.Location = new System.Drawing.Point(157, 129);
			this.lblK.Name = "lblK";
			this.lblK.Size = new System.Drawing.Size(150, 13);
			this.lblK.TabIndex = 4;
			this.lblK.Text = "Spring proportionality const (K)";
			// 
			// hScrollBarK
			// 
			this.hScrollBarK.Location = new System.Drawing.Point(13, 127);
			this.hScrollBarK.Maximum = 200;
			this.hScrollBarK.Name = "hScrollBarK";
			this.hScrollBarK.Size = new System.Drawing.Size(138, 20);
			this.hScrollBarK.TabIndex = 5;
			this.hScrollBarK.Value = 100;
			this.hScrollBarK.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarK_Scroll);
			// 
			// lblRestLen
			// 
			this.lblRestLen.AutoSize = true;
			this.lblRestLen.Location = new System.Drawing.Point(157, 152);
			this.lblRestLen.Name = "lblRestLen";
			this.lblRestLen.Size = new System.Drawing.Size(103, 13);
			this.lblRestLen.TabIndex = 4;
			this.lblRestLen.Text = "Spring resting length";
			// 
			// hScrollBarRestLen
			// 
			this.hScrollBarRestLen.Location = new System.Drawing.Point(13, 150);
			this.hScrollBarRestLen.Maximum = 200;
			this.hScrollBarRestLen.Name = "hScrollBarRestLen";
			this.hScrollBarRestLen.Size = new System.Drawing.Size(138, 20);
			this.hScrollBarRestLen.TabIndex = 5;
			this.hScrollBarRestLen.Value = 100;
			this.hScrollBarRestLen.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarRestLen_Scroll);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.panel1.Controls.Add(this.btnResetAll);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(317, 44);
			this.panel1.TabIndex = 6;
			// 
			// btnResetAll
			// 
			this.btnResetAll.BackColor = System.Drawing.Color.Gray;
			this.btnResetAll.FlatAppearance.BorderSize = 0;
			this.btnResetAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnResetAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnResetAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnResetAll.Location = new System.Drawing.Point(13, 11);
			this.btnResetAll.Name = "btnResetAll";
			this.btnResetAll.Size = new System.Drawing.Size(72, 22);
			this.btnResetAll.TabIndex = 6;
			this.btnResetAll.Text = "Reset all";
			this.btnResetAll.UseVisualStyleBackColor = false;
			this.btnResetAll.Click += new System.EventHandler(this.btnResetAll_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lblG);
			this.panel2.Controls.Add(this.hScrollBarRestLen);
			this.panel2.Controls.Add(this.hScrollBarG);
			this.panel2.Controls.Add(this.hScrollBarK);
			this.panel2.Controls.Add(this.lblRestLen);
			this.panel2.Controls.Add(this.lblMaxParticleSpeed);
			this.panel2.Controls.Add(this.lblK);
			this.panel2.Controls.Add(this.hScrollBarParticleSize);
			this.panel2.Controls.Add(this.hScrollBarMaxParticleSpeed);
			this.panel2.Controls.Add(this.lblParticleSize);
			this.panel2.Controls.Add(this.hScrollBarVelDecay);
			this.panel2.Controls.Add(this.lblMaxCenterPullMag);
			this.panel2.Controls.Add(this.lblVelDecay);
			this.panel2.Controls.Add(this.hScrollBarMaxCenterPullMag);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 44);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(317, 184);
			this.panel2.TabIndex = 7;
			// 
			// FDGVConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.ClientSize = new System.Drawing.Size(317, 228);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "FDGVConfigForm";
			this.Text = "GraphConfiguratorForm";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label lblG;
		private System.Windows.Forms.HScrollBar hScrollBarG;
		private System.Windows.Forms.HScrollBar hScrollBarMaxParticleSpeed;
		private System.Windows.Forms.Label lblMaxParticleSpeed;
		private System.Windows.Forms.Label lblMaxCenterPullMag;
		private System.Windows.Forms.HScrollBar hScrollBarMaxCenterPullMag;
		private System.Windows.Forms.Label lblVelDecay;
		private System.Windows.Forms.HScrollBar hScrollBarVelDecay;
		private System.Windows.Forms.Label lblParticleSize;
		private System.Windows.Forms.HScrollBar hScrollBarParticleSize;
		private System.Windows.Forms.Label lblK;
		private System.Windows.Forms.HScrollBar hScrollBarK;
		private System.Windows.Forms.Label lblRestLen;
		private System.Windows.Forms.HScrollBar hScrollBarRestLen;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnResetAll;
	}
}