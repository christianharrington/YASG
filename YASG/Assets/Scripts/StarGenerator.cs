using UnityEngine;
using System.Collections.Generic;
using System;

public class StarGenerator : MonoBehaviour {

	public GameObject Star;

	// Use this for initialization
	void Start () {
		//Instantiate (Star, new Vector3 (x, 0, z), Quaternion.identity);
		IList<Vector2> starPositions = UniformPoissonDiskSampler.SampleRectangle (new Vector2 (-100, -100), new Vector2 (100, 100), 4f);
		foreach (Vector2 p in starPositions) {
			GameObject s = Instantiate (Star, new Vector3 (p.x, 0, p.y), Quaternion.identity) as GameObject;
			float r = UnityEngine.Random.Range(0.01f, 0.1f);
			Vector3 scale = new Vector3(r, r, r);
			s.transform.localScale += scale;
		}
	}

	public static class UniformPoissonDiskSampler
	{
		public const int DefaultPointsPerIteration = 30;
		
		static readonly float SquareRootTwo = (float) Math.Sqrt(2);
		
		struct Settings
		{
			public Vector2 TopLeft, LowerRight, Center;
			public Vector2 Dimensions;
			public float? RejectionSqDistance;
			public float MinimumDistance;
			public float CellSize;
			public int GridWidth, GridHeight;
		}
		
		struct State
		{
			public Vector2?[,] Grid;
			public List<Vector2> ActivePoints, Points;
		}
		
		public static List<Vector2> SampleCircle(Vector2 center, float radius, float minimumDistance)
		{
			return SampleCircle(center, radius, minimumDistance, DefaultPointsPerIteration);
		}
		public static List<Vector2> SampleCircle(Vector2 center, float radius, float minimumDistance, int pointsPerIteration)
		{
			return Sample(center - new Vector2(radius, radius), center + new Vector2(radius, radius), radius, minimumDistance, pointsPerIteration);
		}
		
		public static List<Vector2> SampleRectangle(Vector2 topLeft, Vector2 lowerRight, float minimumDistance)
		{
			return SampleRectangle(topLeft, lowerRight, minimumDistance, DefaultPointsPerIteration);
		}
		public static List<Vector2> SampleRectangle(Vector2 topLeft, Vector2 lowerRight, float minimumDistance, int pointsPerIteration)
		{
			return Sample(topLeft, lowerRight, null, minimumDistance, pointsPerIteration);
		}
		
		static List<Vector2> Sample(Vector2 topLeft, Vector2 lowerRight, float? rejectionDistance, float minimumDistance, int pointsPerIteration)
		{
			var settings = new Settings
			{
				TopLeft = topLeft, LowerRight = lowerRight,
				Dimensions = lowerRight - topLeft,
				Center = (topLeft + lowerRight) / 2,
				CellSize = minimumDistance / SquareRootTwo,
				MinimumDistance = minimumDistance,
				RejectionSqDistance = rejectionDistance == null ? null : rejectionDistance * rejectionDistance
			};
			settings.GridWidth = (int) (settings.Dimensions.x / settings.CellSize) + 1;
			settings.GridHeight = (int) (settings.Dimensions.y / settings.CellSize) + 1;
			
			var state = new State
			{
				Grid = new Vector2?[settings.GridWidth, settings.GridHeight],
				ActivePoints = new List<Vector2>(),
				Points = new List<Vector2>()
			};
			
			AddFirstPoint(ref settings, ref state);
			
			while (state.ActivePoints.Count != 0)
			{
				var listIndex = RandomHelper.Random.Next(state.ActivePoints.Count);
				
				var point = state.ActivePoints[listIndex];
				var found = false;
				
				for (var k = 0; k < pointsPerIteration; k++)
					found |= AddNextPoint(point, ref settings, ref state);
				
				if (!found)
					state.ActivePoints.RemoveAt(listIndex);
			}
			
			return state.Points;
		}
		
		static void AddFirstPoint(ref Settings settings, ref State state)
		{
			var added = false;
			while (!added)
			{
				double d = RandomHelper.Random.NextDouble();
				double xr = settings.TopLeft.x + settings.Dimensions.x * d;
				
				d = RandomHelper.Random.NextDouble();
				double yr = settings.TopLeft.y + settings.Dimensions.y * d;
				
				Vector2 p = new Vector2((float) xr, (float) yr);
				if (settings.RejectionSqDistance != null && Math.Pow(Vector2.Distance(settings.Center, p), 2) > settings.RejectionSqDistance)
					continue;
				added = true;
				
				var index = Denormalize(p, settings.TopLeft, settings.CellSize);
				
				state.Grid[(int) index.x, (int) index.y] = p;
				
				state.ActivePoints.Add(p);
				state.Points.Add(p);
			} 
		}
		
		static bool AddNextPoint(Vector2 point, ref Settings settings, ref State state)
		{
			var found = false;
			var q = GenerateRandomAround(point, settings.MinimumDistance);
			
			if (q.x >= settings.TopLeft.x && q.x < settings.LowerRight.x && 
			    q.y > settings.TopLeft.y && q.y < settings.LowerRight.y &&
			    (settings.RejectionSqDistance == null || Math.Pow(Vector2.Distance(settings.Center, q), 2) <= settings.RejectionSqDistance))
			{
				var qIndex = Denormalize(q, settings.TopLeft, settings.CellSize);
				var tooClose = false;
				
				for (var i = (int)Math.Max(0, qIndex.x - 2); i < Math.Min(settings.GridWidth, qIndex.x + 3) && !tooClose; i++)
					for (var j = (int)Math.Max(0, qIndex.y - 2); j < Math.Min(settings.GridHeight, qIndex.y + 3) && !tooClose; j++)
						if (state.Grid[i, j].HasValue && Vector2.Distance(state.Grid[i, j].Value, q) < settings.MinimumDistance)
							tooClose = true;
				
				if (!tooClose)
				{
					found = true;
					state.ActivePoints.Add(q);
					state.Points.Add(q);
					state.Grid[(int)qIndex.x, (int)qIndex.y] = q;
				}
			}
			return found;
		}
		
		static Vector2 GenerateRandomAround(Vector2 center, float minimumDistance)
		{
			var d = RandomHelper.Random.NextDouble();
			var radius = minimumDistance + minimumDistance * d;
			
			d = RandomHelper.Random.NextDouble();
			var angle = MathHelper.TwoPi * d;
			
			var newX = radius * Math.Sin(angle);
			var newY = radius * Math.Cos(angle);
			
			return new Vector2((float) (center.x + newX), (float) (center.y + newY));
		}
		
		static Vector2 Denormalize(Vector2 point, Vector2 origin, double cellSize)
		{
			return new Vector2((int) ((point.x - origin.x) / cellSize), (int) ((point.y - origin.y) / cellSize));
		}
	}
	
	public static class RandomHelper
	{
		public static readonly System.Random Random = new System.Random();
	}
	
	public static class MathHelper
	{
		public const float Pi = (float)Math.PI;
		public const float HalfPi = (float)(Math.PI / 2);
		public const float TwoPi = (float)(Math.PI * 2);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
