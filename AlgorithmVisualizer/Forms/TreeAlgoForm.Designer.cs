
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
			this.components = new System.ComponentModel.Container();
			this.panelControls = new System.Windows.Forms.Panel();
			this.btnRemoveNode = new System.Windows.Forms.Button();
			this.btnAddNode = new System.Windows.Forms.Button();
			this.txtBoxNodeValue = new System.Windows.Forms.TextBox();
			this.algoComboBox = new System.Windows.Forms.ComboBox();
			this.lblAlgoComboBox = new System.Windows.Forms.Label();
			this.lblSpeedBar = new System.Windows.Forms.Label();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.speedBar = new System.Windows.Forms.HScrollBar();
			this.btnPauseResume = new System.Windows.Forms.Button();
			this.canvas = new System.Windows.Forms.PictureBox();
			this.canvasContextStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.resetDisplacementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panelControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
			this.canvasContextStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelControls
			// 
			this.panelControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
			this.panelControls.Controls.Add(this.btnRemoveNode);
			this.panelControls.Controls.Add(this.btnAddNode);
			this.panelControls.Controls.Add(this.txtBoxNodeValue);
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
			// btnRemoveNode
			// 
			this.btnRemoveNode.BackColor = System.Drawing.Color.Gray;
			this.btnRemoveNode.FlatAppearance.BorderSize = 0;
			this.btnRemoveNode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRemoveNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRemoveNode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnRemoveNode.Location = new System.Drawing.Point(836, 16);
			this.btnRemoveNode.Name = "btnRemoveNode";
			this.btnRemoveNode.Size = new System.Drawing.Size(74, 23);
			this.btnRemoveNode.TabIndex = 15;
			this.btnRemoveNode.Text = "Remove";
			this.btnRemoveNode.UseVisualStyleBackColor = false;
			this.btnRemoveNode.Click += new System.EventHandler(this.btnRemoveNode_Click);
			// 
			// btnAddNode
			// 
			this.btnAddNode.BackColor = System.Drawing.Color.Gray;
			this.btnAddNode.FlatAppearance.BorderSize = 0;
			this.btnAddNode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAddNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAddNode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnAddNode.Location = new System.Drawing.Point(773, 16);
			this.btnAddNode.Name = "btnAddNode";
			this.btnAddNode.Size = new System.Drawing.Size(60, 23);
			this.btnAddNode.TabIndex = 15;
			this.btnAddNode.Text = "Add";
			this.btnAddNode.UseVisualStyleBackColor = false;
			this.btnAddNode.Click += new System.EventHandler(this.btnAddNode_Click);
			// 
			// txtBoxNodeValue
			// 
			this.txtBoxNodeValue.Location = new System.Drawing.Point(913, 17);
			this.txtBoxNodeValue.Name = "txtBoxNodeValue";
			this.txtBoxNodeValue.Size = new System.Drawing.Size(78, 20);
			this.txtBoxNodeValue.TabIndex = 14;
			// 
			// algoComboBox
			// 
			this.algoComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.algoComboBox.FormattingEnabled = true;
			this.algoComboBox.Items.AddRange(new object[] {
            "Binary search tree",
            "AVL tree"});
			this.algoComboBox.Location = new System.Drawing.Point(79, 15);
			this.algoComboBox.Name = "algoComboBox";
			this.algoComboBox.Size = new System.Drawing.Size(184, 24);
			this.algoComboBox.TabIndex = 2;
			this.algoComboBox.SelectedIndexChanged += new System.EventHandler(this.algoComboBox_SelectedIndexChanged);
			// 
			// lblAlgoComboBox
			// 
			this.lblAlgoComboBox.AutoSize = true;
			this.lblAlgoComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAlgoComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.lblAlgoComboBox.Location = new System.Drawing.Point(9, 19);
			this.lblAlgoComboBox.Name = "lblAlgoComboBox";
			this.lblAlgoComboBox.Size = new System.Drawing.Size(69, 17);
			this.lblAlgoComboBox.TabIndex = 1;
			this.lblAlgoComboBox.Text = "Tree type";
			// 
			// lblSpeedBar
			// 
			this.lblSpeedBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSpeedBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.lblSpeedBar.Location = new System.Drawing.Point(558, 17);
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
			this.btnReset.Location = new System.Drawing.Point(332, 16);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(60, 23);
			this.btnReset.TabIndex = 3;
			this.btnReset.Text = "Reset";
			this.btnReset.UseVisualStyleBackColor = false;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnStart
			// 
			this.btnStart.BackColor = System.Drawing.Color.Gray;
			this.btnStart.FlatAppearance.BorderSize = 0;
			this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnStart.Location = new System.Drawing.Point(269, 16);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(60, 23);
			this.btnStart.TabIndex = 5;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = false;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// speedBar
			// 
			this.speedBar.LargeChange = 1;
			this.speedBar.Location = new System.Drawing.Point(611, 15);
			this.speedBar.Name = "speedBar";
			this.speedBar.Size = new System.Drawing.Size(151, 23);
			this.speedBar.TabIndex = 11;
			this.speedBar.Value = 50;
			this.speedBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.speedBar_Scroll);
			// 
			// btnPauseResume
			// 
			this.btnPauseResume.BackColor = System.Drawing.Color.Gray;
			this.btnPauseResume.Enabled = false;
			this.btnPauseResume.FlatAppearance.BorderSize = 0;
			this.btnPauseResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnPauseResume.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPauseResume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnPauseResume.Location = new System.Drawing.Point(395, 16);
			this.btnPauseResume.Name = "btnPauseResume";
			this.btnPauseResume.Size = new System.Drawing.Size(60, 23);
			this.btnPauseResume.TabIndex = 6;
			this.btnPauseResume.Text = "Pause";
			this.btnPauseResume.UseVisualStyleBackColor = false;
			this.btnPauseResume.Click += new System.EventHandler(this.btnPauseResume_Click);
			// 
			// canvas
			// 
			this.canvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.canvas.Location = new System.Drawing.Point(0, 57);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(1008, 504);
			this.canvas.TabIndex = 0;
			this.canvas.TabStop = false;
			this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
			this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
			this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
			this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
			this.canvas.Resize += new System.EventHandler(this.canvas_Resize);
			// 
			// canvasContextStrip
			// 
			this.canvasContextStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetDisplacementToolStripMenuItem});
			this.canvasContextStrip.Name = "canvasContextStrip";
			this.canvasContextStrip.Size = new System.Drawing.Size(177, 26);
			// 
			// resetDisplacementToolStripMenuItem
			// 
			this.resetDisplacementToolStripMenuItem.Name = "resetDisplacementToolStripMenuItem";
			this.resetDisplacementToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.resetDisplacementToolStripMenuItem.Text = "Reset displacement";
			this.resetDisplacementToolStripMenuItem.Click += new System.EventHandler(this.resetDisplacementToolStripMenuItem_Click);
			// 
			// TreeAlgoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
			this.ClientSize = new System.Drawing.Size(1008, 561);
			this.Controls.Add(this.canvas);
			this.Controls.Add(this.panelControls);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "TreeAlgoForm";
			this.Text = "TreeAlgoForm";
			this.panelControls.ResumeLayout(false);
			this.panelControls.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
			this.canvasContextStrip.ResumeLayout(false);
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
		private System.Windows.Forms.Button btnRemoveNode;
		private System.Windows.Forms.Button btnAddNode;
		private System.Windows.Forms.TextBox txtBoxNodeValue;
		private System.Windows.Forms.PictureBox canvas;
		private System.Windows.Forms.ContextMenuStrip canvasContextStrip;
		private System.Windows.Forms.ToolStripMenuItem resetDisplacementToolStripMenuItem;
	}
}