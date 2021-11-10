using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AlgorithmVisualizer.GraphTheory.Utils;
using AlgorithmVisualizer.Utils;
using AlgorithmVisualizer.Threading;
using System.Threading;

namespace AlgorithmVisualizer.GraphTheory.FDGV
{
	public class GraphVisualizer : PauseResumeSleep
	{
		/* Implementation of a force directed graph visualizing (FDGV) algorithm.
		 * Visuals are NOT supported for ALL edge types, see the beginning of 'Graph.cs' 
		 * file for more details on unsupported edge types. */

		// Mapping node ids to the GNode objects of instance Particle
		protected Dictionary<int, GNode> nodeLookup = new Dictionary<int, GNode>();

		private List<Particle> particles;
		private List<Spring> springs;

		// Contains the drawing of the graph
		private readonly PictureBox canvas;

		// GLog is a graphics object of panelLog.
		public Graphics GLog { get; set; }

		// Center of point of canvas; used to pull particles to the center
		public Vector CenterPos { get; private set; }
		// Find and update the center pos of the canvas
		public void UpdateCenterPos() => CenterPos = new Vector(canvas.Width / 2, canvas.Height / 2);
		// by default center pull is active, can be disabled.
		public bool CenterPull { get; set; } = true;

		// Note that the condition: PARTICLE_SIZE <= PARTICLE_SPAWN_OFFSET must hold
		// for the prticles not to spawn outside of the canvas (not clip ourside of it)
		private const int PARTICLE_SPAWN_OFFSET = 50;

		protected static Random rnd = new Random();
		private static Semaphore sem;

		public GraphVisualizer(PictureBox _canvas, Graphics gLog)
		{
			sem = new Semaphore(1, 1);

			canvas = _canvas;
			UpdateCenterPos();
			GLog = gLog;
			particles = new List<Particle>();
			springs = new List<Spring>();

			SetDefaultPhysicsParams();
		}

		#region particle/spring list manipulation
		protected Particle GetParticle(int id)
		{
			sem.WaitOne();
			var p = nodeLookup[id] as Particle;
			sem.Release();
			return p;
		}
		public Particle GetParticle(float x, float y)
		{
			// Returns the ref of the particle at the given pos, null if no such particle.

			sem.WaitOne();
			for (int i = particles.Count - 1; i >= 0; i--)
			{
				if (particles[i].PointIsWithin(x, y))
				{
					sem.Release();
					return particles[i];
				}
			}
			sem.Release();
			return null;
		}
		protected void AddParticle(Particle particle)
		{
			sem.WaitOne();
			particles.Add(particle);
			sem.Release();
		}
		protected void RemoveParticle(int id)
		{
			RemoveSpringsConnectedTo(id);
			var p = GetParticle(id);
			sem.WaitOne();
			particles.Remove(p);
			sem.Release();
		}
		private Spring GetSpring(Edge edge)
		{
			sem.WaitOne();
			// Compare edges using only (from, to, cost)
			for (int i = 0; i < springs.Count; i++)
			{
				if (springs[i].Equals(edge))
				{
					sem.Release();
					return springs[i];
				}
			}
			sem.Release();
			return null;
		}
		public void AddSpring(Spring spring)
		{
			if (spring != null)
			{
				sem.WaitOne();
				springs.Add(spring);
				sem.Release();
			}
		}
		public void RemoveSpring(Spring spring)
		{
			var s = GetSpring(spring);
			sem.WaitOne();
			springs.Remove(s);
			sem.Release();
		}
		private void RemoveSpringsConnectedTo(int id)
		{
			// Remove any spring containing a particle with the given id
			sem.WaitOne();
			for (int i = 0; i < springs.Count;)
			{
				if (springs[i].ContainsNodeId(id)) springs.RemoveAt(i);
				else i++;
			}
			sem.Release();
		}
		protected void ClearParticlesAndSprings()
		{
			sem.WaitOne();
			springs.Clear();
			particles.Clear();
			sem.Release();
		}
		#endregion

		#region Visuals
		public void DrawGraph(Graphics g)
		{
			sem.WaitOne();
			foreach (var spring in springs) spring.Draw(g);
			foreach (var particle in particles) particle.Draw(g, canvas.Height, canvas.Width);
			sem.Release();
		}
		public void ApplyForcesAndUpdatePositions()
		{
			sem.WaitOne();
			foreach (var particle in particles)
			{
				if (CenterPull) particle.PullToCenter(CenterPos);
				particle.ApplyRepulsiveForces(particles);
			}
			foreach (var spring in springs) spring.ExertForcesOnParticles();
			// Update particle positions using computed forces
			foreach (var particle in particles) particle.UpdatePos(canvas.Height, canvas.Width);
			sem.Release();
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
			sem.WaitOne();
			particle.InnerColor = innerColor;
			particle.BorderColor = borderColor;
			sem.Release();
		}
		public void ResetParticleColors(int id)
		{
			var p = GetParticle(id);
			sem.WaitOne();
			p.SetDefaultColors();
			sem.Release();
		}
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
			sem.WaitOne();
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
			sem.Release();


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
					// If color is not 'Colors.Visited' append, else prepend into 'springs'
					if (color != Colors.Visited) springs.Add(springToMove);
					else springs.Insert(0, springToMove);
				}
			}
		}
		public void ReverseSprings()
		{
			sem.WaitOne();
			foreach (Spring spring in springs) spring.Reversed = true;
			sem.Release();
		}
		public void ClearVizState()
		{
			sem.WaitOne();
			// Clear "Reversed" state for all springs
			foreach (var spring in springs) spring.Reversed = false;
			// Reset particle/spring colors to defaults
			foreach (var particle in particles) particle.SetDefaultColors();
			foreach (var spring in springs) spring.SetDefaultColors();
			sem.Release();
		}
		public static void SetDefaultPhysicsParams()
		{
			sem.WaitOne();
			Particle.SetDefaultPhysicsParams();
			Spring.SetDefaultPhysicsParams();
			sem.Release();
		}
		#endregion

		#region Misc
		public void ToggleParticlePin(int id)
		{
			var p = GetParticle(id);
			sem.WaitOne();
			p.TogglePin();
			sem.Release();
		}
		public void PinAllParticles()
		{
			sem.WaitOne();
			foreach (Particle particle in particles) particle.Pinned = true;
			sem.Release();
		}
		public void UnpinAllParticles()
		{
			sem.WaitOne();
			foreach (Particle particle in particles) particle.Pinned = false;
			sem.Release();
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