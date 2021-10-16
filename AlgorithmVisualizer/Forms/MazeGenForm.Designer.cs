
namespace AlgorithmVisualizer.Forms
{
	partial class MazeGenForm
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
			this.lblWidth = new System.Windows.Forms.Label();
			this.lblSpeedBar = new System.Windows.Forms.Label();
			this.heightTxtBox = new System.Windows.Forms.TextBox();
			this.lblHeight = new System.Windows.Forms.Label();
			this.speedBar = new System.Windows.Forms.HScrollBar();
			this.widthTxtBox = new System.Windows.Forms.TextBox();
			this.drawMaze = new System.Windows.Forms.Button();
			this.panelControls = new System.Windows.Forms.Panel();
			this.btnFindPath = new System.Windows.Forms.Button();
			this.panelMain = new System.Windows.Forms.Panel();
			this.panelControls.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblWidth
			// 
			this.lblWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblWidth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.lblWidth.Location = new System.Drawing.Point(206, 5);
			this.lblWidth.Name = "lblWidth";
			this.lblWidth.Size = new System.Drawing.Size(100, 22);
			this.lblWidth.TabIndex = 26;
			this.lblWidth.Text = "Width";
			this.lblWidth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblSpeedBar
			// 
			this.lblSpeedBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSpeedBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.lblSpeedBar.Location = new System.Drawing.Point(306, 5);
			this.lblSpeedBar.Name = "lblSpeedBar";
			this.lblSpeedBar.Size = new System.Drawing.Size(110, 19);
			this.lblSpeedBar.TabIndex = 22;
			this.lblSpeedBar.Text = "Speed";
			this.lblSpeedBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// heightTxtBox
			// 
			this.heightTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.heightTxtBox.Location = new System.Drawing.Point(100, 27);
			this.heightTxtBox.Name = "heightTxtBox";
			this.heightTxtBox.Size = new System.Drawing.Size(100, 23);
			this.heightTxtBox.TabIndex = 23;
			// 
			// lblHeight
			// 
			this.lblHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.lblHeight.Location = new System.Drawing.Point(100, 5);
			this.lblHeight.Name = "lblHeight";
			this.lblHeight.Size = new System.Drawing.Size(100, 22);
			this.lblHeight.TabIndex = 25;
			this.lblHeight.Text = "Height";
			this.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// speedBar
			// 
			this.speedBar.LargeChange = 1;
			this.speedBar.Location = new System.Drawing.Point(309, 27);
			this.speedBar.Maximum = 500;
			this.speedBar.Name = "speedBar";
			this.speedBar.Size = new System.Drawing.Size(107, 20);
			this.speedBar.TabIndex = 21;
			this.speedBar.Value = 100;
			this.speedBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.speedBar_Scroll);
			// 
			// widthTxtBox
			// 
			this.widthTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.widthTxtBox.Location = new System.Drawing.Point(206, 27);
			this.widthTxtBox.Name = "widthTxtBox";
			this.widthTxtBox.Size = new System.Drawing.Size(100, 23);
			this.widthTxtBox.TabIndex = 24;
			// 
			// drawMaze
			// 
			this.drawMaze.BackColor = System.Drawing.Color.Gray;
			this.drawMaze.FlatAppearance.BorderSize = 0;
			this.drawMaze.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.drawMaze.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drawMaze.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.drawMaze.Location = new System.Drawing.Point(12, 7);
			this.drawMaze.Name = "drawMaze";
			this.drawMaze.Size = new System.Drawing.Size(75, 42);
			this.drawMaze.TabIndex = 20;
			this.drawMaze.Text = "Draw Maze";
			this.drawMaze.UseVisualStyleBackColor = false;
			this.drawMaze.Click += new System.EventHandler(this.drawMaze_Click);
			// 
			// panelControls
			// 
			this.panelControls.Controls.Add(this.btnFindPath);
			this.panelControls.Controls.Add(this.drawMaze);
			this.panelControls.Controls.Add(this.widthTxtBox);
			this.panelControls.Controls.Add(this.lblWidth);
			this.panelControls.Controls.Add(this.speedBar);
			this.panelControls.Controls.Add(this.lblSpeedBar);
			this.panelControls.Controls.Add(this.lblHeight);
			this.panelControls.Controls.Add(this.heightTxtBox);
			this.panelControls.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControls.Location = new System.Drawing.Point(0, 0);
			this.panelControls.Name = "panelControls";
			this.panelControls.Size = new System.Drawing.Size(1008, 55);
			this.panelControls.TabIndex = 28;
			// 
			// btnFindPath
			// 
			this.btnFindPath.BackColor = System.Drawing.Color.Gray;
			this.btnFindPath.FlatAppearance.BorderSize = 0;
			this.btnFindPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFindPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFindPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnFindPath.Location = new System.Drawing.Point(431, 7);
			this.btnFindPath.Name = "btnFindPath";
			this.btnFindPath.Size = new System.Drawing.Size(75, 42);
			this.btnFindPath.TabIndex = 27;
			this.btnFindPath.Text = "Find path";
			this.btnFindPath.UseVisualStyleBackColor = false;
			this.btnFindPath.Click += new System.EventHandler(this.btnFindPath_Click);
			// 
			// panelMain
			// 
			this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 55);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(1008, 506);
			this.panelMain.TabIndex = 29;
			// 
			// MazeGenerationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
			this.ClientSize = new System.Drawing.Size(1008, 561);
			this.Controls.Add(this.panelMain);
			this.Controls.Add(this.panelControls);
			this.Name = "MazeGenerationForm";
			this.Text = "Maze genrator";
			this.panelControls.ResumeLayout(false);
			this.panelControls.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblWidth;
		private System.Windows.Forms.Label lblSpeedBar;
		private System.Windows.Forms.TextBox heightTxtBox;
		private System.Windows.Forms.Label lblHeight;
		private System.Windows.Forms.HScrollBar speedBar;
		private System.Windows.Forms.TextBox widthTxtBox;
		private System.Windows.Forms.Button drawMaze;
		private System.Windows.Forms.Panel panelControls;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Button btnFindPath;
	}
}