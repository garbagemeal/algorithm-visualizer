using System;
using System.Windows.Forms;

using AlgorithmVisualizer.GraphTheory;
using AlgorithmVisualizer.GraphTheory.FDGV;
using AlgorithmVisualizer.Utils;

namespace AlgorithmVisualizer.Forms.Dialogs
{
	public partial class FDGVConfigForm : Form
	{
		/* 
		 * FDGV - Force directed graph drawing
		 * 
		 * rangeIn  - Expected range of values given from any scrollbar
		 * rangeOut - Expected input range per graph drawing parameter, i.e:
		 * rangeOut[0] = Range(0, 10) implies that parameter 0(G) expects values 
		 * in the range [0, 10]. 
		 */
		private static readonly Range rangeIn = new Range(0, 200);
		private static readonly Range[] rangeOut = new Range[] {
			new Range(0, Particle.DefaultG * 10),
			new Range(0, Particle.DefaultMaxSpeed * 10),
			new Range(0, Particle.DefaultMaxCenterPullMag * 10),
			new Range(0.1f, Particle.DefaultVelDecay),
			new Range(20, Particle.DefaultSize * 3),
			new Range(Spring.DefaultK / 10, Spring.DefaultK * 10),
			new Range(30, Spring.DefaultRestLen * 3),
		};

		public FDGVConfigForm(Graph _graph)
		{
			InitializeComponent();
			CenterToParent();

			UpdateScrollBarPositions();
		}

		private void UpdateScrollBarPositions()
		{
			// Update scrollbar positions according to graph drawing param values
			hScrollBarG.Value = (int)Range.Scale(Particle.G, rangeOut[0], rangeIn);
			hScrollBarMaxParticleSpeed.Value = (int)Range.Scale(Particle.MaxSpeed, rangeOut[1], rangeIn);
			hScrollBarMaxCenterPullMag.Value = (int)Range.Scale(Particle.MaxCenterPullMag, rangeOut[2], rangeIn);
			hScrollBarVelDecay.Value = (int)Range.Scale(Particle.VelDecay, rangeOut[3], rangeIn);
			hScrollBarParticleSize.Value = (int)Range.Scale(Particle.Size, rangeOut[4], rangeIn);
			hScrollBarK.Value = (int)Range.Scale(Spring.K, rangeOut[5], rangeIn);
			hScrollBarRestLen.Value = (int)Range.Scale(Spring.RestLen, rangeOut[6], rangeIn);
		}

		private void btnResetAll_Click(object sender, EventArgs e)
		{
			// Set graph drawing params to defaults and update scrollbar positions
			GraphVisualizer.SetDefaultPhysicsParams();
			UpdateScrollBarPositions();
		}
		/* On a scrollbar value change scale the value from a given range to another and
		 * update the parameter for the graph drawing algo */
		private void hScrollBarG_Scroll(object sender, ScrollEventArgs e)
		{
			Particle.G = Range.Scale(hScrollBarG.Value, rangeIn, rangeOut[0]);
		}
		private void hScrollBarMaxParticleSpeed_Scroll(object sender, ScrollEventArgs e)
		{
			Particle.MaxSpeed = Range.Scale(hScrollBarMaxParticleSpeed.Value, rangeIn, rangeOut[1]);
		}
		private void hScrollBarMaxCenterPullMag_Scroll(object sender, ScrollEventArgs e)
		{
			Particle.MaxCenterPullMag = Range.Scale(hScrollBarMaxCenterPullMag.Value, rangeIn, rangeOut[2]);
		}
		private void hScrollBarVelDecay_Scroll(object sender, ScrollEventArgs e)
		{
			Particle.VelDecay = Range.Scale(hScrollBarVelDecay.Value, rangeIn, rangeOut[3]);
		}
		private void hScrollBarParticleSize_Scroll(object sender, ScrollEventArgs e)
		{
			Particle.Size = Range.Scale(hScrollBarParticleSize.Value, rangeIn, rangeOut[4]);
		}
		private void hScrollBarK_Scroll(object sender, ScrollEventArgs e)
		{
			Spring.K = Range.Scale(hScrollBarK.Value, rangeIn, rangeOut[5]);
		}
		private void hScrollBarRestLen_Scroll(object sender, ScrollEventArgs e)
		{
			Spring.RestLen = Range.Scale(hScrollBarRestLen.Value, rangeIn, rangeOut[6]);
		}
	}
}
