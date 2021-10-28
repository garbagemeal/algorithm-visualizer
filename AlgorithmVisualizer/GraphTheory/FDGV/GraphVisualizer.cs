using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
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
		
		// Mapping node ids to the GNode objects of instance Particle
		protected Dictionary<int, GNode> nodeLookup = new Dictionary<int, GNode>();

		public List<Particle> particles;
		public List<Spring> springs;

		// Contains the drawing of the graph
		private readonly PictureBox canvas;

		// GLog is a graphics object of panelLog.
		public Graphics GLog { get; set; }

		// Center of point of canvas; used to pull particles to the center
		private Vector centerPos;
		// by default center pull is active, can be disabled.
		public bool CenterPull { get; set; } = true;
		// Find center pos of the canvas
		private Vector findCenterPos() => new Vector(canvas.Width / 2, canvas.Height / 2);

		// When updating height/width of the canvas the center pos is also updated!
		public int CanvasHeight { set { canvas.Height = value; centerPos = findCenterPos(); } }
		public int CanvasWidth { set { canvas.Width = value; centerPos = findCenterPos(); } }

		// Note that the condition: PARTICLE_SIZE <= PARTICLE_SPAWN_OFFSET must hold
		// for the prticles not to spawn outside of the canvas (not clip ourside of it)
		private const int PARTICLE_SPAWN_OFFSET = 50;

		protected static Random rnd = new Random();

		public GraphVisualizer(PictureBox _canvas, Graphics gLog)
		{
			canvas = _canvas;
			centerPos = findCenterPos();
			GLog = gLog;
			particles = new List<Particle>();
			springs = new List<Spring>();

			SetDefaultPhysicsParams();
		}

		#region particle/spring list manipulation
		protected Particle GetParticle(int id) =>  nodeLookup[id] as Particle;
		protected void AddParticle(Particle particle) => particles.Add(particle);
		protected void RemoveParticle(int id)
		{
			RemoveSpringsConnectedTo(id);
			particles.Remove(GetParticle(id));
		}
		private Spring GetSpring(Edge edge)
		{
			// Compare edges using only (from, to, cost)
			for (int i = 0; i < springs.Count; i++)
				if (springs[i].Equals(edge)) return springs[i];
			return null;
		}
		public void AddSpring(Spring spring)
		{
			if (spring != null) springs.Add(spring);
		}
		public void RemoveSpring(Spring spring) => springs.Remove(GetSpring(spring));
		private void RemoveSpringsConnectedTo(int id)
		{
			// Remove any spring containing a particle with the given id
			for (int i = 0; i < springs.Count;)
				if (springs[i].ContainsNodeId(id)) springs.RemoveAt(i);
				else i++;
		}
		protected void ClearParticlesAndSprings()
		{
			springs.Clear();
			particles.Clear();
		}
		#endregion

		#region Visuals
		public void DrawGraph(Graphics g)
		{
			foreach (var particle in particles.ToList()) particle.Draw(g, canvas.Height, canvas.Width);
			foreach (var spring in springs.ToArray()) spring.Draw(g);
		}
		public void ApplyForcesAndUpdatePositions()
		{
			Particle[] particlesCP = particles.ToArray();
			Spring [] springsCP = springs.ToArray();
			foreach (var particle in particlesCP)
			{
				if (CenterPull) particle.PullToCenter(centerPos);
				particle.ApplyRepulsiveForces(particlesCP);
			}
			foreach (var spring in springsCP) spring.ExertForcesOnParticles();
			// Update particle positions using computed forces
			foreach (var particle in particlesCP) particle.UpdatePos(canvas.Height, canvas.Width);
		}

		// The following methods assume that the given particle id exists
		public void MarkParticle(int id, Color innerColor)
		{
			// GetParticle(id).InnerColor = innerColor;
			MarkParticle(id, innerColor, Colors.ParticleBorderColor);
		}
		public void MarkParticle(int id, Color innerColor, Color borderColor)
		{
			Particle particle = GetParticle(id);
			particle.InnerColor = innerColor;
			particle.BorderColor = borderColor;
		}
		public void ResetParticleColors(int id) => GetParticle(id).SetDefaultColors();
		public enum Dir { None = -1, Directed = 0, Reversed = 1, Undirected = 2 }
		public void MarkSpring(Edge edge, Color color, Dir dir = Dir.None)
		{
			/* Update innerColor for a spring matching the given/reversed edge or both
			 * 
			 * Types of updates:
			 * Dir.None - undefined, highlight both if exist
			 * Dir.Directed -   from ---> to
			 * Dir.Reversed -   from <--- to
			 * Dir.Undirected - from <--> to    */

			// Find given spring and revSpring
			Spring spring = GetSpring(edge), revSpring = GetSpring(Edge.ReversedCopy(edge));
			List<Spring> springsToUpdate = new List<Spring>();
			
			// Determine what springs to update
			if (dir == Dir.Directed) springsToUpdate.Add(spring);
			else if (dir == Dir.Reversed) springsToUpdate.Add(revSpring);
			else // dir = Dir.None OR dir = Dir.Undirected
			{
				springsToUpdate.Add(spring);
				springsToUpdate.Add(revSpring);
			}
			springsToUpdate.RemoveAll(item => item == null);
			
			// Update spring colors
			foreach (var s in springsToUpdate) s.InnerColor = color;

				MoveSpringsToStartOrEnd(springsToUpdate);


			void MoveSpringsToStartOrEnd(List<Spring> springsToMove)
			{
				// Remove each spring appearing in 'springsToMove' from 'springs'
				// Given springs assumed non-null
				foreach (Spring springToMove in springsToMove)
				{
					int remCount = springs.RemoveAll(item => item == springToMove);
					if (remCount != 1) throw new Exception("Failed to match given spring!");
				}
				// Add removed springs in 'springsToMove' to the start/end of the spring list
				foreach (Spring springToMove in springsToMove)
				{
					// If color is not 'Colors.Visited' prepend, else append into 'springs'
					if (color != Colors.Visited) AddSpring(springToMove);
					else springs.Insert(0, springToMove);
				}
			}
		}
		public void ReverseSprings()
		{
			foreach (Spring spring in springs) spring.Reversed = true;
		}
		public void ClearVizState()
		{
			// Clear "Reversed" state for all springs
			foreach (var spring in springs) spring.Reversed = false;
			// Reset particle/spring colors to defaults
			foreach (var particle in particles) particle.SetDefaultColors();
			foreach (var spring in springs) spring.SetDefaultColors();
		}
		public static void SetDefaultPhysicsParams()
		{
			Particle.SetDefaultPhysicsParams();
			Spring.SetDefaultPhysicsParams();
		}
		#endregion

		#region Misc
		public Particle GetClickedParticle(float x, float y)
		{
			// Returns the ref of the particle at the given pos, null if no such particle
			foreach (Particle particle in particles)
				if (particle.PointIsWithin(x, y)) return particle;
			return null;
		}
		public void ToggleParticlePin(int id) => GetParticle(id).TogglePin();
		public void PinAllParticles()
		{
			foreach (Particle particle in particles) particle.Pinned = true;
		}
		public void UnpinAllParticles()
		{
			foreach (Particle particle in particles) particle.Pinned = false;
		}
		protected Vector RndPosWithinCanvas()
		{
			// Returns a pos within the canvas and offset from borders by PARTICLE_SPAWN_OFFSET
			int x = rnd.Next(PARTICLE_SPAWN_OFFSET, canvas.Width - PARTICLE_SPAWN_OFFSET);
			int y = rnd.Next(PARTICLE_SPAWN_OFFSET, canvas.Height - PARTICLE_SPAWN_OFFSET);
			return new Vector(x, y);
		}
		#endregion

	}
}