using System;
using System.Windows.Forms;

using AlgorithmVisualizer.GraphTheory;
using AlgorithmVisualizer.GraphTheory.FDGV;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	// FDGV - Force directed graph drawing
	public partial class FDGVConfigForm : Form
	{
		// All scrolls bars bounded in the domain [0, 200]
		private const int DefaultHScrollVal = 100;

		private Graph graph;

		// Particle related data
		float G { get; set; }
		float MaxParticleSpeed { get; set; }
		float MaxCenterPullMag { get; set; }
		float VelDecay { get; set; }
		float ParticleSize { get; set; }
		// Spring related data
		float K { get; set; }
		float RestLen { get; set; }

		private HScrollBar[] scrollBars;

		public FDGVConfigForm(Graph _graph)
		{
			InitializeComponent();
			CenterToParent();
			InitFDGVParams();
			graph = _graph;

			scrollBars = new HScrollBar[] {
				hScrollBarG,
				hScrollBarMaxParticleSpeed,
				hScrollBarMaxCenterPullMag,
				hScrollBarVelDecay,
				hScrollBarParticleSize,
				hScrollBarK,
				hScrollBarRestLen,
			};
		}
		private void InitFDGVParams()
		{
			// Load default params for graph drawing forces
			G = Particle.defaultG;
			MaxParticleSpeed = Particle.defaultMaxSpeed;
			MaxCenterPullMag = Particle.defaultMaxCenterPullMag;
			VelDecay = Particle.defaultVelDecay;
			ParticleSize = Particle.defaultSize;
			K = Spring.defaultK;
			RestLen = Spring.defaultRestLen;
		}
		private void ResetScrollBarVals()
		{
			foreach (var scrollBar in scrollBars) scrollBar.Value = DefaultHScrollVal;
		}
		private void ResetAll()
		{
			// Reset all physics related params in graph
			graph.SetDefaultPhysicsParams();
			InitFDGVParams();
		}

		private void btnResetAll_Click(object sender, EventArgs e)
		{
			ResetAll();
			ResetScrollBarVals();
		}

		private void hScrollBarG_Scroll(object sender, ScrollEventArgs e)
		{
			G = 0.01f * hScrollBarG.Value * Particle.defaultG;
			graph.SetG(G);
		}
		private void hScrollBarMaxParticleSpeed_Scroll(object sender, ScrollEventArgs e)
		{
			MaxParticleSpeed = 0.01f * hScrollBarMaxParticleSpeed.Value * Particle.defaultMaxSpeed;
			graph.SetMaxParticleSpeed(MaxParticleSpeed);
		}
		private void hScrollBarMaxCenterPullMag_Scroll(object sender, ScrollEventArgs e)
		{
			MaxCenterPullMag = 0.01f * hScrollBarMaxCenterPullMag.Value * Particle.defaultMaxCenterPullMag;
			graph.SetMaxCenterPullMag(MaxCenterPullMag);
		}
		private void hScrollBarVelDecay_Scroll(object sender, ScrollEventArgs e)
		{
			VelDecay = 0.01f * hScrollBarVelDecay.Value * Particle.defaultVelDecay;
			graph.SetVelDecay(VelDecay);
		}
		private void hScrollBarParticleSize_Scroll(object sender, ScrollEventArgs e)
		{
			ParticleSize = 0.01f * hScrollBarParticleSize.Value * Particle.defaultSize;
			graph.SetParticleSize(ParticleSize);
		}
		private void hScrollBarK_Scroll(object sender, ScrollEventArgs e)
		{
			K = 0.01f * hScrollBarK.Value * Spring.defaultK;
			graph.SetK(K);
		}
		private void hScrollBarRestLen_Scroll(object sender, ScrollEventArgs e)
		{
			RestLen = 0.01f * hScrollBarRestLen.Value * Spring.defaultRestLen;
			graph.SetRestLen(RestLen);
		}
	}
}
