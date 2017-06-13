using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions.Algorithms;

namespace Gamelogic.Grids2.Examples
{
	public static class PrimsAlgorithm
	{
		public static IEnumerable<GridPoint2> GetEdgeFaces(GridPoint2 point)
		{
			var color = point.GetColor(2, 2, 2);

			var faces = new StructList<GridPoint2>();

			switch (color)
			{
				case 0:
					//error!
					break;
				case 2:
					faces.Add(point + RectPoint.North);
					faces.Add(point + RectPoint.South);
					break;
				case 1:
					faces.Add(point + RectPoint.East);
					faces.Add(point + RectPoint.West);
					break;
					/*case 2:
				faces.Add(point + RectPoint.NorthEast);
				faces.Add(point + RectPoint.NorthWest);
				faces.Add(point + RectPoint.SouthEast);
				faces.Add(point + RectPoint.SouthWest);
				break;*/
			}

			return faces;
		}

		public static IEnumerable<GridPoint2> GenerateMazeWalls<TCell>(IGrid<GridPoint2, TCell> grid)
		{
			var walls = grid.CloneStructure<bool>(); //true indicates passable

			foreach (var point in walls.Points)
			{
				walls[point] = point.GetColor(2, 2, 2) == 0;

				//Debug.Log(point);
			}

			var wallPoints = walls
				.Where(pair => pair.cell)
				.Select(pair => pair.point);

			foreach (var point in wallPoints)
			{
				yield return point;
			}

			var wallList = new StructList<GridPoint2>();

			var newMaizePoint = grid.Points.Where(p => p.GetColor(2, 2, 2) == 3).RandomItem();
			var inMaze = new StructList<GridPoint2> { newMaizePoint };

			var edges = newMaizePoint
				.GetVectorNeighbors(RectPoint.OrthogonalDirections)
				.In(grid);

			wallList.AddRange(edges);

			while (wallList.Any())
			{
				var randomWall = wallList.RandomItem();
				var faces = GetEdgeFaces(randomWall).Where(grid.Contains);

				//At least one of the two faces must be in the maze
				if (faces.Any(point => !inMaze.Contains(point)))
				{
					newMaizePoint = faces.First(point => !inMaze.Contains(point));
					inMaze.Add(newMaizePoint);
					walls[randomWall] = true;

					yield return randomWall;

					// Add all edges that are not passages
					edges = newMaizePoint
						.GetVectorNeighbors(RectPoint.OrthogonalDirections)
						.In(grid)
						.Where(edge => !(walls[edge]));

					wallList.AddRange(edges);
				}
				else
				{
					wallList.Remove(randomWall);
				}
			}
		}
	}
}