using System.Windows.Forms;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	public partial class GraphConfiguratorDialog : Form
	{
		// Environment related
		float G { get; set; }

		// Particle related
		int ParticleSize { get; set; }
		bool Pinned { get; set; }
		float G { get; set; }

		// Spring related
		float K { get; set; }
		float RestLen { get; set; }

		public GraphConfiguratorDialog()
		{
			InitializeComponent();
		}

	}
}
