
namespace AlgorithmVisualizer.Forms
{
	partial class MainUIForm
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
			this.panelSidemenu = new System.Windows.Forms.Panel();
			this.btnGraphAlgos = new System.Windows.Forms.Button();
			this.btnMazeGenerator = new System.Windows.Forms.Button();
			this.btnArrayAlgos = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelLog = new System.Windows.Forms.Panel();
			this.panelChildForm = new System.Windows.Forms.Panel();
			this.btnTreeAlgos = new System.Windows.Forms.Button();
			this.panelSidemenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelSidemenu
			// 
			this.panelSidemenu.AutoScroll = true;
			this.panelSidemenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
			this.panelSidemenu.Controls.Add(this.btnTreeAlgos);
			this.panelSidemenu.Controls.Add(this.btnGraphAlgos);
			this.panelSidemenu.Controls.Add(this.btnMazeGenerator);
			this.panelSidemenu.Controls.Add(this.btnArrayAlgos);
			this.panelSidemenu.Controls.Add(this.panel1);
			this.panelSidemenu.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelSidemenu.Location = new System.Drawing.Point(0, 0);
			this.panelSidemenu.Name = "panelSidemenu";
			this.panelSidemenu.Size = new System.Drawing.Size(200, 681);
			this.panelSidemenu.TabIndex = 0;
			// 
			// btnGraphAlgos
			// 
			this.btnGraphAlgos.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnGraphAlgos.FlatAppearance.BorderSize = 0;
			this.btnGraphAlgos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnGraphAlgos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGraphAlgos.ForeColor = System.Drawing.Color.White;
			this.btnGraphAlgos.Location = new System.Drawing.Point(0, 182);
			this.btnGraphAlgos.Name = "btnGraphAlgos";
			this.btnGraphAlgos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.btnGraphAlgos.Size = new System.Drawing.Size(200, 41);
			this.btnGraphAlgos.TabIndex = 3;
			this.btnGraphAlgos.Text = "Graph algorithms";
			this.btnGraphAlgos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnGraphAlgos.UseVisualStyleBackColor = true;
			this.btnGraphAlgos.Click += new System.EventHandler(this.btnGraphAlgos_Click);
			// 
			// btnMazeGenerator
			// 
			this.btnMazeGenerator.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnMazeGenerator.FlatAppearance.BorderSize = 0;
			this.btnMazeGenerator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnMazeGenerator.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnMazeGenerator.ForeColor = System.Drawing.Color.White;
			this.btnMazeGenerator.Location = new System.Drawing.Point(0, 141);
			this.btnMazeGenerator.Name = "btnMazeGenerator";
			this.btnMazeGenerator.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.btnMazeGenerator.Size = new System.Drawing.Size(200, 41);
			this.btnMazeGenerator.TabIndex = 2;
			this.btnMazeGenerator.Text = "Maze generator";
			this.btnMazeGenerator.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnMazeGenerator.UseVisualStyleBackColor = true;
			this.btnMazeGenerator.Click += new System.EventHandler(this.btnMazeGenerator_Click);
			// 
			// btnArrayAlgos
			// 
			this.btnArrayAlgos.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnArrayAlgos.FlatAppearance.BorderSize = 0;
			this.btnArrayAlgos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnArrayAlgos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnArrayAlgos.ForeColor = System.Drawing.Color.White;
			this.btnArrayAlgos.Location = new System.Drawing.Point(0, 100);
			this.btnArrayAlgos.Name = "btnArrayAlgos";
			this.btnArrayAlgos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.btnArrayAlgos.Size = new System.Drawing.Size(200, 41);
			this.btnArrayAlgos.TabIndex = 1;
			this.btnArrayAlgos.Text = "Array algorithms";
			this.btnArrayAlgos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnArrayAlgos.UseVisualStyleBackColor = true;
			this.btnArrayAlgos.Click += new System.EventHandler(this.btnArrayAlgos_Click);
			// 
			// panel1
			// 
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 100);
			this.panel1.TabIndex = 0;
			// 
			// panelLog
			// 
			this.panelLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
			this.panelLog.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelLog.Location = new System.Drawing.Point(200, 542);
			this.panelLog.Name = "panelLog";
			this.panelLog.Size = new System.Drawing.Size(1064, 139);
			this.panelLog.TabIndex = 1;
			this.panelLog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelLog_MouseDown);
			this.panelLog.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelLog_MouseMove);
			this.panelLog.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelLog_MouseUp);
			// 
			// panelChildForm
			// 
			this.panelChildForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.panelChildForm.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelChildForm.Location = new System.Drawing.Point(200, 0);
			this.panelChildForm.Name = "panelChildForm";
			this.panelChildForm.Size = new System.Drawing.Size(1064, 542);
			this.panelChildForm.TabIndex = 2;
			// 
			// btnTreeAlgos
			// 
			this.btnTreeAlgos.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnTreeAlgos.FlatAppearance.BorderSize = 0;
			this.btnTreeAlgos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTreeAlgos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTreeAlgos.ForeColor = System.Drawing.Color.White;
			this.btnTreeAlgos.Location = new System.Drawing.Point(0, 223);
			this.btnTreeAlgos.Name = "btnTreeAlgos";
			this.btnTreeAlgos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.btnTreeAlgos.Size = new System.Drawing.Size(200, 41);
			this.btnTreeAlgos.TabIndex = 4;
			this.btnTreeAlgos.Text = "Tree algorithms";
			this.btnTreeAlgos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnTreeAlgos.UseVisualStyleBackColor = true;
			this.btnTreeAlgos.Click += new System.EventHandler(this.btnTreeAlgos_Click);
			// 
			// MainUIForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1264, 681);
			this.Controls.Add(this.panelChildForm);
			this.Controls.Add(this.panelLog);
			this.Controls.Add(this.panelSidemenu);
			this.MinimumSize = new System.Drawing.Size(1280, 720);
			this.Name = "MainUIForm";
			this.Text = "Algorithm visualizer";
			this.panelSidemenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelSidemenu;
		private System.Windows.Forms.Button btnMazeGenerator;
		private System.Windows.Forms.Button btnArrayAlgos;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panelLog;
		private System.Windows.Forms.Panel panelChildForm;
		private System.Windows.Forms.Button btnGraphAlgos;
		private System.Windows.Forms.Button btnTreeAlgos;
	}
}