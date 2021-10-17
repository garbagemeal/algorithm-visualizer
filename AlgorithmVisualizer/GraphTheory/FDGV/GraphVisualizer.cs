using System;
using System.Collections.Generic;
using System.Drawing;

using AlgorithmVisualizer.GraphTheory.Utils;
using AlgorithmVisualizer.MathUtils;
using AlgorithmVisualizer.Threading;

namespace AlgorithmVisualizer.GraphTheory.FDGV
{
	public class GraphVisualizer : PauseResumeSleep
	{
		/* Implementation of a force directed graph visualizing algorithm.

		* Multi-graph are unsupported, meaning that parallel edges may not exist.
		* i.e, the edges (0, 1, 5), (0, 1, 7) are parallel edges because both have the
		* same starting and ending nodes, only 1 of the above edges can exist at any
		* given moment in this graph.
		* Note that this property ensures that the graph's edges are unique!
		* 
		* Because of the limitation of not being a multi-graph, and in order to also
		* support "undirected" edges, If there exists and edge (u, v, x),
		* the edge (v, u, y) may exist IFF x = y
		* 
		* The pair of edges (u, v, x), (v, u, x) may be considered as a single undirected
		* edge in some of the algorithms implementations. */
		
		// Mapping node ids to the GNode objects
		protected Dictionary<int, GNode> nodeLookup = new Dictionary<int, GNode>();

		private List<Particle> particles;
		private List<Spring> springs;

		// G is the graphics object of panelMain, GLog is a graphics object of panelLog.
		public Graphics GMain { get; set; }
		public Graphics GLog { get; set; }

		// Center of point of panelMain; used to pull particles to the center
		private Vector centerPos;
		// Find center pos of the panel
		private Vector findCenterPos() => new Vector(panelWidth / 2, panelHeight / 2);

		// Height/width of the main panel (related to GMain)
		private int panelHeight, panelWidth;
		// When updating height/width of the panel the center pos is also updated!
		public int PanelHeight { set { panelHeight = value; centerPos = findCenterPos(); } }
		public int PanelWidth { set { panelWidth = value; centerPos = findCenterPos(); } }

		// Note that the condition: PARTICLE_SIZE <= PARTICLE_SPAWN_OFFSET must hold
		// for the prticles not to spawn outside of the panel (not clip ourside of it)
		protected const int DEFAULT_PARTICLE_SIZE = 30, DEFAULT_SPRING_REST_LEN = 125;
		private const int PARTICLE_SPAWN_OFFSET = 50;

		protected static Random rnd = new Random();
		
		public GraphVisualizer(Graphics gMain, Graphics gLog, int _panelHeight, int _panelWidth)
		{
			GMain = gMain;
			GLog = gLog;
			panelHeight = _panelHeight;
			panelWidth = _panelWidth;
			centerPos = findCenterPos();
			particles = new List<Particle>();
			springs = new List<Spring>();
		}

		#region particle/spring list manipulation
		protected Vector GetRndPosWithinPanel()
		{
			// Note: the position vector returned will be within the panel and also offset
			// from borders by PARTICLE_SPAWN_OFFSET
			int x = rnd.Next(PARTICLE_SPAWN_OFFSET, panelWidth - PARTICLE_SPAWN_OFFSET);
			int y = rnd.Next(PARTICLE_SPAWN_OFFSET, panelHeight - PARTICLE_SPAWN_OFFSET);
			return new Vector(x, y);
		}
		protected Particle GetParticle(int id) =>  nodeLookup[id] as Particle;
		protected void AddParticle(Particle particle) => particles.Add(particle);
		protected void RemoveParticle(int id)
		{
			RemoveSpringsConnectedTo(id);
			particles.Remove(GetParticle(id));
		}
		private Spring GetSpring(Edge edge)
		{
			// Comaptions using u, v, x
			for (int i = 0; i < springs.Count; i++)
				if (springs[i].Equals(edge)) return springs[i];
			return null;
		}
		public void AddSpring(Spring spring) => springs.Add(spring);
		public void RemoveSpring(Spring spring) => springs.Remove(GetSpring(spring));
		private void RemoveSpringsConnectedTo(int id)
		{
			// Remove any spring containing a particle with the given id
			for (int i = 0; i < springs.Count;)
				if (springs[i].ContainsNodeId(id)) springs.RemoveAt(i);
				else i++;
		}
		#endregion

		#region Visuals
		private void ClearPanel() => GMain.Clear(Colors.Undraw);
		protected void ClearVisualization()
		{
			// Clears visuals and spring/particle lists
			springs.Clear();
			particles.Clear();
			ClearPanel();
		}
		public void DrawGraph()
		{
			// Draw the graph by first applying forces
			ClearPanel();
			ApplyForces();
			DrawSprings();
			DrawParticles();

			void ApplyForces()
			{
				foreach (Particle particle in particles)
				{
					// Pull this particle to the center
					particle.PullToCenter(centerPos);
					// Repel all other particles via this particle
					particle.ApplyRepulsiveForces(particles);
				}
				// Apply forces on the particles via the springs
				foreach (Spring spring in springs) spring.ExertForcesOnParticles();
				// Update all particle positions
				foreach (Particle particle in particles)
					particle.UpdatePos(panelHeight, panelWidth);
			}
		}
		public void DrawGraphForceless()
		{
			// Draw the graph without applying forces
			ClearPanel();
			DrawSprings();
			DrawParticles();
		}
		public void ResetGraphColors()
		{
			ResetParticleColors();
			ResetSpringColors();

			void ResetParticleColors()
			{
				foreach (var particle in particles)
				{
					particle.SetDefaultColors();
					particle.Draw(GMain);
				}
			}
			void ResetSpringColors()
			{
				foreach (var spring in springs)
				{
					spring.SetDefaultColors();
					spring.Draw(GMain);
				}
			}
		}
		public void Visualize()
		{
			// Main method used to visualize the graph - Force directed graph drawing
			const int MAX_NUM_ITR = 750;
			const float EPSILON = 0.1f;
			int i = 0;
			// Run the FDGV as long i < MAX_NUM_ITR and
			// the maximal velocity for all particles per iteration > EPSILON
			do
			{
				Particle.MAX_VEL_MAG_PER_ITR = 0;
				DrawGraph();
				i++;
				Sleep(0); // Check for pause event
			} while (i < MAX_NUM_ITR && Particle.MAX_VEL_MAG_PER_ITR > EPSILON);

			// Reset max_vel/itr for next invocation of this method
			Particle.MAX_VEL_MAG_PER_ITR = 0;
		}

		// The following methods assume that the given particle id exists
		private void DrawParticle(int id) => GetParticle(id).Draw(GMain);
		public void DrawParticle(int id, Color innerColor)
		{
			// Set given particle's innerColor and draw it
			GetParticle(id).InnerColor = innerColor;
			DrawParticle(id);
		}
		public void DrawParticle(int id, Color innerColor, Color borderColor)
		{
			// Set given particle's innerColor and borderColor and draw it
			GetParticle(id).InnerColor = innerColor;
			GetParticle(id).BorderColor = borderColor;
			DrawParticle(id);
		}
		private void DrawParticles()
		{
			// Draw each particle
			foreach (Particle particle in particles) particle.Draw(GMain);
		}
		public void ResetParticleColor(int id)
		{
			GetParticle(id).SetDefaultColors();
			DrawParticle(id);
		}
		
		// The following methods assume the given edge is in the graph (can be a colone)
		public void RedrawSpring(Edge edge, Color color, int dir = -1)
		{
			/*
			 * Undraw given edge and reverse edge if exists
			 * Given edge assuemd to be valid (can also be a clone edge)
			 * 
			 * cases for dir (type of draw):
			 * -1 - draw both given and reversed edge if exists
			 *  0 - from ---> to
			 *  1 - from <--- to
			 *  2 - from <--> to
			 */

			// Find given spring and revSpring
			Spring spring = GetSpring(edge), revSpring = GetSpring(Edge.ReversedCopy(edge));
			// Undraw both springs
			spring.Undraw(GMain);
			if (revSpring != null) revSpring.Undraw(GMain);
			// Update color for both springs
			spring.InnerColor = color;
			if (revSpring != null) revSpring.InnerColor = color;

			// Draw according to spec
			if (dir == -1 || dir == 0 || dir == 2) spring.Draw(GMain);
			if ((dir == -1 || dir == 1 || dir == 2) && revSpring != null) revSpring.Draw(GMain);

		}
		private void DrawSprings()
		{
			// Draw all springs
			foreach (Spring spring in springs) spring.Draw(GMain);
		}
		public void ReverseSprings()
		{
			foreach (Spring spring in springs) spring.Reversed = true;
			DrawGraphForceless();
		}
		public void ClearSpringReversedState()
		{
			foreach (var spring in springs) spring.Reversed = false;
		}
		#endregion

		#region Helper methods for panel click events
		public Particle GetClickedParticle(float x, float y)
		{
			// If the given coordinates are within a particle will return
			// a reference to that particle, otherwise returns null
			foreach (Particle particle in particles)
				if (particle.PointIsWithin(x, y)) return particle;
			return null;
		}
		// Toggle the pin status for particle with given id. assumes id exists
		public void ToggleParticlePin(int id) => GetParticle(id).TogglePin();
		public void PinAllParticles()
		{
			// Pin every particle in the particle list
			foreach (Particle particle in particles) particle.Pinned = true;
		}
		public void UnpinAllParticles()
		{
			// Unpin every particle in the particle list
			foreach (Particle particle in particles) particle.Pinned = false;
		}
		#endregion
	}
}