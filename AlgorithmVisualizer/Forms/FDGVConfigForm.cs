using System;
using System.Windows.Forms;

using AlgorithmVisualizer.GraphTheory;
using AlgorithmVisualizer.GraphTheory.FDGV;
using AlgorithmVisualizer.MathUtils;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	// FDGV - Force directed graph drawing
	public partial class FDGVConfigForm : Form
	{
		private Graph graph;

		// Particle related data
		private static float G, MaxParticleSpeed, MaxCenterPullMag, VelDecay, ParticleSize;
		// Spring related data
		private static float K, RestLen;

		// All scrolls bars bounded in the domain [0, 191] (slider takes 9 pixels)
		private const int DefaultHScrollVal = 100;
		private static HScrollBar[] scrollBars;
		private static int[] scrollBarPositions;
		// Array of ranges for possible values per parameter, i.e:
		// rangeOut[0] = Range(0, 10) means the value given from scrollbar 0
		// should be scaled to values in the range [0, 10].
		private static readonly Range rangeIn = new Range(0, 191);
		private static readonly Range[] rangeOut = new Range[] {
			new Range(100, 10000), // G
			new Range(0, 100f), // MaxParticleSpeed
			new Range(0, 1), // MaxCenterPullMag
			new Range(0, 0.999f), // VelDecay
			new Range(1, 59), // ParticleSize
			new Range(0.00005f, 0.005f), // K
			new Range(20, 300), // RestLen
		};

		// Avoid setting values more then once even if openning this from not for the
		// first time
		private static bool valuesHaveBeenSet = false;
		public FDGVConfigForm(Graph _graph)
		{
			InitializeComponent();
			CenterToParent();
			
			scrollBars = new HScrollBar[] {
				hScrollBarG,
				hScrollBarMaxParticleSpeed,
				hScrollBarMaxCenterPullMag,
				hScrollBarVelDecay,
				hScrollBarParticleSize,
				hScrollBarK,
				hScrollBarRestLen,
			};
			// If openning not for the first time values are already set - update scroll
			// bar positions, otherwise if openning for the first time load defaults.
			// Note to self: just store spring values in a static var
			if (valuesHaveBeenSet) UpdateScrollBarPositions();
			else
			{
				InitParams();
				int n = scrollBars.Length;
				scrollBarPositions = new int[n];
				for (int i = 0; i < n; i++) scrollBarPositions[i] = DefaultHScrollVal;
			}
			graph = _graph;

		}

		private void InitParams()
		{
			// Load default params for graph drawing forces
			G = Particle.defaultG;
			MaxParticleSpeed = Particle.defaultMaxSpeed;
			MaxCenterPullMag = Particle.defaultMaxCenterPullMag;
			VelDecay = Particle.defaultVelDecay;
			ParticleSize = Particle.defaultSize;
			K = Spring.defaultK;
			RestLen = Spring.defaultRestLen;
			valuesHaveBeenSet = true;
		}

		private void UpdateScrollBarPositions()
		{
			for (int i = 0; i < scrollBars.Length; i++)
				scrollBars[i].Value = scrollBarPositions[i];
		}
		private void ResetScrollBarVals()
		{
			foreach (var scrollBar in scrollBars) scrollBar.Value = DefaultHScrollVal;
		}

		private void btnResetAll_Click(object sender, EventArgs e)
		{
			// Reset all physics related params in the graph and this from to defaults
			InitParams();
			ResetScrollBarVals();
			graph.SetDefaultPhysicsParams();
		}
		private void hScrollBarG_Scroll(object sender, ScrollEventArgs e)
		{
			int newScrollBarPos = scrollBarPositions[0] = hScrollBarG.Value;
			G = Range.Scale(newScrollBarPos, rangeIn, rangeOut[0]);
			Console.WriteLine($"{newScrollBarPos} ||| {G}");
			graph.SetG(G);
		}
		private void hScrollBarMaxParticleSpeed_Scroll(object sender, ScrollEventArgs e)
		{
			int newScrollBarPos = scrollBarPositions[1] = hScrollBarMaxParticleSpeed.Value;
			MaxParticleSpeed = Range.Scale(newScrollBarPos, rangeIn, rangeOut[1]);
			Console.WriteLine($"{newScrollBarPos} ||| {MaxParticleSpeed}");
			graph.SetMaxParticleSpeed(MaxParticleSpeed);
		}
		private void hScrollBarMaxCenterPullMag_Scroll(object sender, ScrollEventArgs e)
		{
			int newScrollBarPos = scrollBarPositions[2] = hScrollBarMaxCenterPullMag.Value;
			MaxCenterPullMag = Range.Scale(newScrollBarPos, rangeIn, rangeOut[2]);
			Console.WriteLine($"{newScrollBarPos} ||| {MaxCenterPullMag}");
			graph.SetMaxCenterPullMag(MaxCenterPullMag);
		}
		private void hScrollBarVelDecay_Scroll(object sender, ScrollEventArgs e)
		{
			int newScrollBarPos = scrollBarPositions[3] = hScrollBarVelDecay.Value;
			VelDecay = Range.Scale(newScrollBarPos, rangeIn, rangeOut[3]);
			Console.WriteLine($"{newScrollBarPos} ||| {VelDecay}");
			graph.SetVelDecay(VelDecay);
		}
		private void hScrollBarParticleSize_Scroll(object sender, ScrollEventArgs e)
		{
			int newScrollBarPos = scrollBarPositions[4] = hScrollBarParticleSize.Value;
			ParticleSize = Range.Scale(newScrollBarPos, rangeIn, rangeOut[4]);
			Console.WriteLine($"{newScrollBarPos} ||| {ParticleSize}");
			graph.SetParticleSize(ParticleSize);
		}
		private void hScrollBarK_Scroll(object sender, ScrollEventArgs e)
		{
			int newScrollBarPos = scrollBarPositions[5] = hScrollBarK.Value;
			K = Range.Scale(newScrollBarPos, rangeIn, rangeOut[5]);
			Console.WriteLine($"{newScrollBarPos} ||| {K}");
			graph.SetK(K);
		}
		private void hScrollBarRestLen_Scroll(object sender, ScrollEventArgs e)
		{
			int newScrollBarPos = scrollBarPositions[6] = hScrollBarRestLen.Value;
			RestLen = Range.Scale(newScrollBarPos, rangeIn, rangeOut[6]);
			Console.WriteLine($"{newScrollBarPos} ||| {RestLen}");
			graph.SetRestLen(RestLen);
		}
	}
}
