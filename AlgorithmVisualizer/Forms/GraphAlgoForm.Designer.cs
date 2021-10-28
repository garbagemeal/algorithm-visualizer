
namespace AlgorithmVisualizer.Forms
{
	partial class GraphAlgoForm
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
			this.vertexContextStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addEdgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeEdgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toggleVertexPinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panelControls = new System.Windows.Forms.Panel();
			this.btnPreferences = new System.Windows.Forms.Button();
			this.btnClearState = new System.Windows.Forms.Button();
			this.btnDetails = new System.Windows.Forms.Button();
			this.btnPresets = new System.Windows.Forms.Button();
			this.algoComboBox = new System.Windows.Forms.ComboBox();
			this.lblAlgoComboBox = new System.Windows.Forms.Label();
			this.lblSpeedBar = new System.Windows.Forms.Label();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.speedBar = new System.Windows.Forms.HScrollBar();
			this.btnPauseResume = new System.Windows.Forms.Button();
			this.canvasContextStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pinAllVerticesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.unpinAllVerticesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.togglePhysicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toggleCenterPullToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.canvas = new System.Windows.Forms.PictureBox();
			this.vertexContextStrip.SuspendLayout();
			this.panelControls.SuspendLayout();
			this.canvasContextStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
			this.SuspendLayout();
			// 
			// vertexContextStrip
			// 
			this.vertexContextStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEdgeToolStripMenuItem,
            this.removeEdgeToolStripMenuItem,
            this.removeVertexToolStripMenuItem,
            this.toggleVertexPinToolStripMenuItem});
			this.vertexContextStrip.Name = "panelMainContextMenuStrip";
			this.vertexContextStrip.Size = new System.Drawing.Size(165, 92);
			// 
			// addEdgeToolStripMenuItem
			// 
			this.addEdgeToolStripMenuItem.Name = "addEdgeToolStripMenuItem";
			this.addEdgeToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.addEdgeToolStripMenuItem.Text = "Add edge";
			this.addEdgeToolStripMenuItem.Click += new System.EventHandler(this.addEdgeToolStripMenuItem_Click);
			// 
			// removeEdgeToolStripMenuItem
			// 
			this.removeEdgeToolStripMenuItem.Name = "removeEdgeToolStripMenuItem";
			this.removeEdgeToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.removeEdgeToolStripMenuItem.Text = "Remove edge";
			this.removeEdgeToolStripMenuItem.Click += new System.EventHandler(this.removeEdgeToolStripMenuItem_Click);
			// 
			// removeVertexToolStripMenuItem
			// 
			this.removeVertexToolStripMenuItem.Name = "removeVertexToolStripMenuItem";
			this.removeVertexToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.removeVertexToolStripMenuItem.Text = "Remove vertex";
			this.removeVertexToolStripMenuItem.Click += new System.EventHandler(this.removeVertexToolStripMenuItem_Click);
			// 
			// toggleVertexPinToolStripMenuItem
			// 
			this.toggleVertexPinToolStripMenuItem.Name = "toggleVertexPinToolStripMenuItem";
			this.toggleVertexPinToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.toggleVertexPinToolStripMenuItem.Text = "Toggle vertex pin";
			this.toggleVertexPinToolStripMenuItem.Click += new System.EventHandler(this.toggleVertexPinToolStripMenuItem_Click);
			// 
			// panelControls
			// 
			this.panelControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
			this.panelControls.Controls.Add(this.btnPreferences);
			this.panelControls.Controls.Add(this.btnClearState);
			this.panelControls.Controls.Add(this.btnDetails);
			this.panelControls.Controls.Add(this.btnPresets);
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
			this.panelControls.TabIndex = 17;
			// 
			// btnPreferences
			// 
			this.btnPreferences.BackColor = System.Drawing.Color.Gray;
			this.btnPreferences.FlatAppearance.BorderSize = 0;
			this.btnPreferences.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnPreferences.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPreferences.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnPreferences.Location = new System.Drawing.Point(665, 16);
			this.btnPreferences.Name = "btnPreferences";
			this.btnPreferences.Size = new System.Drawing.Size(95, 23);
			this.btnPreferences.TabIndex = 18;
			this.btnPreferences.Text = "Preferences";
			this.btnPreferences.UseVisualStyleBackColor = false;
			this.btnPreferences.Click += new System.EventHandler(this.btnPreferences_Click);
			// 
			// btnClearState
			// 
			this.btnClearState.BackColor = System.Drawing.Color.Gray;
			this.btnClearState.FlatAppearance.BorderSize = 0;
			this.btnClearState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClearState.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClearState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnClearState.Location = new System.Drawing.Point(512, 16);
			this.btnClearState.Name = "btnClearState";
			this.btnClearState.Size = new System.Drawing.Size(85, 23);
			this.btnClearState.TabIndex = 17;
			this.btnClearState.Text = "Clear state";
			this.btnClearState.UseVisualStyleBackColor = false;
			this.btnClearState.Click += new System.EventHandler(this.btnClearState_Click);
			// 
			// btnDetails
			// 
			this.btnDetails.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnDetails.BackColor = System.Drawing.Color.Gray;
			this.btnDetails.FlatAppearance.BorderSize = 0;
			this.btnDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnDetails.Location = new System.Drawing.Point(935, 16);
			this.btnDetails.Name = "btnDetails";
			this.btnDetails.Size = new System.Drawing.Size(61, 23);
			this.btnDetails.TabIndex = 15;
			this.btnDetails.Text = "Details";
			this.btnDetails.UseVisualStyleBackColor = false;
			this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
			// 
			// btnPresets
			// 
			this.btnPresets.BackColor = System.Drawing.Color.Gray;
			this.btnPresets.FlatAppearance.BorderSize = 0;
			this.btnPresets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnPresets.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPresets.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.btnPresets.Location = new System.Drawing.Point(599, 16);
			this.btnPresets.Name = "btnPresets";
			this.btnPresets.Size = new System.Drawing.Size(64, 23);
			this.btnPresets.TabIndex = 14;
			this.btnPresets.Text = "Presets";
			this.btnPresets.UseVisualStyleBackColor = false;
			this.btnPresets.Click += new System.EventHandler(this.btnPresets_Click);
			// 
			// algoComboBox
			// 
			this.algoComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.algoComboBox.FormattingEnabled = true;
			this.algoComboBox.Location = new System.Drawing.Point(77, 15);
			this.algoComboBox.Name = "algoComboBox";
			this.algoComboBox.Size = new System.Drawing.Size(243, 24);
			this.algoComboBox.TabIndex = 2;
			this.algoComboBox.SelectedIndexChanged += new System.EventHandler(this.algoComboBox_SelectedIndexChanged);
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
			// lblSpeedBar
			// 
			this.lblSpeedBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSpeedBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
			this.lblSpeedBar.Location = new System.Drawing.Point(765, 17);
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
			this.btnReset.Location = new System.Drawing.Point(450, 16);
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
			this.btnStart.Location = new System.Drawing.Point(322, 16);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(60, 23);
			this.btnStart.TabIndex = 5;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = false;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// speedBar
			// 
			this.speedBar.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.speedBar.LargeChange = 1;
			this.speedBar.Location = new System.Drawing.Point(819, 16);
			this.speedBar.Name = "speedBar";
			this.speedBar.Size = new System.Drawing.Size(113, 23);
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
			this.btnPauseResume.Location = new System.Drawing.Point(384, 16);
			this.btnPauseResume.Name = "btnPauseResume";
			this.btnPauseResume.Size = new System.Drawing.Size(64, 23);
			this.btnPauseResume.TabIndex = 6;
			this.btnPauseResume.Text = "Pause";
			this.btnPauseResume.UseVisualStyleBackColor = false;
			this.btnPauseResume.Click += new System.EventHandler(this.btnPauseResume_Click);
			// 
			// canvasContextStrip
			// 
			this.canvasContextStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVertexToolStripMenuItem,
            this.pinAllVerticesToolStripMenuItem,
            this.unpinAllVerticesToolStripMenuItem,
            this.togglePhysicsToolStripMenuItem,
            this.toggleCenterPullToolStripMenuItem});
			this.canvasContextStrip.Name = "panelMainE";
			this.canvasContextStrip.Size = new System.Drawing.Size(169, 114);
			// 
			// addVertexToolStripMenuItem
			// 
			this.addVertexToolStripMenuItem.Name = "addVertexToolStripMenuItem";
			this.addVertexToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.addVertexToolStripMenuItem.Text = "Add vertex";
			this.addVertexToolStripMenuItem.Click += new System.EventHandler(this.addVertexToolStripMenuItem_Click);
			// 
			// pinAllVerticesToolStripMenuItem
			// 
			this.pinAllVerticesToolStripMenuItem.Name = "pinAllVerticesToolStripMenuItem";
			this.pinAllVerticesToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.pinAllVerticesToolStripMenuItem.Text = "Pin all vertices";
			this.pinAllVerticesToolStripMenuItem.Click += new System.EventHandler(this.pinAllVerticesToolStripMenuItem_Click);
			// 
			// unpinAllVerticesToolStripMenuItem
			// 
			this.unpinAllVerticesToolStripMenuItem.Name = "unpinAllVerticesToolStripMenuItem";
			this.unpinAllVerticesToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.unpinAllVerticesToolStripMenuItem.Text = "Unpin all vertices";
			this.unpinAllVerticesToolStripMenuItem.Click += new System.EventHandler(this.unpinAllVerticesToolStripMenuItem_Click);
			// 
			// togglePhysicsToolStripMenuItem
			// 
			this.togglePhysicsToolStripMenuItem.Name = "togglePhysicsToolStripMenuItem";
			this.togglePhysicsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.togglePhysicsToolStripMenuItem.Text = "Toggle Physics";
			this.togglePhysicsToolStripMenuItem.Click += new System.EventHandler(this.togglePhysicsToolStripMenuItem_Click);
			// 
			// toggleCenterPullToolStripMenuItem
			// 
			this.toggleCenterPullToolStripMenuItem.Name = "toggleCenterPullToolStripMenuItem";
			this.toggleCenterPullToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.toggleCenterPullToolStripMenuItem.Text = "Toggle center pull";
			this.toggleCenterPullToolStripMenuItem.Click += new System.EventHandler(this.toggleCenterPullToolStripMenuItem_Click);
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
			this.canvas.MouseLeave += new System.EventHandler(this.canvas_MouseLeave);
			this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
			this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
			// 
			// GraphAlgoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1008, 561);
			this.Controls.Add(this.canvas);
			this.Controls.Add(this.panelControls);
			this.ForeColor = System.Drawing.Color.White;
			this.KeyPreview = true;
			this.Name = "GraphAlgoForm";
			this.Text = "Graph algorithms";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GraphAlgoForm_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GraphAlgoForm_KeyDown);
			this.Resize += new System.EventHandler(this.GraphAlgoForm_Resize);
			this.vertexContextStrip.ResumeLayout(false);
			this.panelControls.ResumeLayout(false);
			this.panelControls.PerformLayout();
			this.canvasContextStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
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
		private System.Windows.Forms.ContextMenuStrip vertexContextStrip;
		private System.Windows.Forms.ToolStripMenuItem removeVertexToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addEdgeToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip canvasContextStrip;
		private System.Windows.Forms.ToolStripMenuItem addVertexToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeEdgeToolStripMenuItem;
		private System.Windows.Forms.Button btnPresets;
		private System.Windows.Forms.ToolStripMenuItem toggleVertexPinToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pinAllVerticesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem unpinAllVerticesToolStripMenuItem;
		private System.Windows.Forms.Button btnDetails;
		private System.Windows.Forms.Button btnClearState;
		private System.Windows.Forms.ToolStripMenuItem togglePhysicsToolStripMenuItem;
		private System.Windows.Forms.PictureBox canvas;
		private System.Windows.Forms.ToolStripMenuItem toggleCenterPullToolStripMenuItem;
		private System.Windows.Forms.Button btnPreferences;
	}
}