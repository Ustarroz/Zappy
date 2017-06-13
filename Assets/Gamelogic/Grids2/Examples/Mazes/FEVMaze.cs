using System;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	/// <summary>
	/// FEVMaze is a special behavior that treats a RectGrid as a maze, which cells have a meaning 
	/// depending on their positioning in the grid.
	/// 
	/// Rect Grid:
	/// <code>
	/// . . . . . . . . .
	/// . . . . . . . . .
	/// . . . . . . . . .
	/// . . . . . . . . .
	/// . . . . . . . . .
	/// . . . . . . . . .
	/// . . . . . . . . .
	/// </code>
	/// 
	/// Seen as FEVMaze (Face, Edge, Vertices):
	/// <code>
	/// V E V E V E V E V
	/// E F E F E F E F E
	/// V E V E V E V E V
	/// E F E F E F E F E
	/// V E V E V E V E V
	/// E F E F E F E F E
	/// V E V E V E V E V
	/// </code>
	/// 
	/// In the FEVMaze, Face is a Room, Edge is a Wall, and Vertices are there but are not treated by the algorithm.
	/// </summary>
	public class FEVMaze : GridBehaviour<GridPoint2, TileCell>
	{
		public enum MazeAlgorithm
		{
			Simple,
			TwoStep,
			Product
		}

		public Color openColor = Color.white;
		public Color closedColor = Color.black;

		public RectFEV.Symmetry symmetry;
		public MazeAlgorithm algorithm;

		[Header("Two Step")]
		public int startRadius = 4;
		public int factor;

		[Header("Product")]
		public int smallGridRadius;
		public int repeatGridRadius;

		public override void InitGrid()
		{
			// We are going to store the grid in this variable
			IGrid<GridPoint2, RectFEV.MazeType> maze;

			// And create the maze depending on the algorithm selected
			switch (algorithm)
			{
				case MazeAlgorithm.Simple:
					maze = SimpleGrid();
					break;
				case MazeAlgorithm.TwoStep:
					maze = TwoStep();
					break;
				case MazeAlgorithm.Product:
					maze = ProductGrid();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			// Finally, we colorize the cells depending on the state of it.
			foreach (var point in Grid.Points)
			{
				Grid[point].GetComponent<SpriteCell>().Color = (maze.Contains(point) && maze[point] == RectFEV.MazeType.Open) ? openColor : closedColor;

				Grid[point].transform.localScale = Vector3.one * 0.95f;
			}
		}

		/// <summary>
		/// A simple maze creates a maze in a simple grid structure.
		/// </summary>
		private IGrid<GridPoint2, RectFEV.MazeType> SimpleGrid()
		{
			var maze = RectFEV.GenerateMaze(Grid, symmetry);

			return maze;
		}

		/// <summary>
		/// A Two step maze creates the maze in two steps by startRadius and then increases it by factor.
		/// </summary>
		private IGrid<GridPoint2, RectFEV.MazeType> TwoStep()
		{
			var origin = new GridPoint2(1 - startRadius, 1 - startRadius);
			var size = new GridPoint2(2 * startRadius - 1, 2 * startRadius - 1);

			var smallGridShape = ImplicitShape.Square(GridPoint2.Zero, startRadius).ToExplicit(new GridRect(origin, size));

			var maze = RectFEV.GenerateMaze(smallGridShape, symmetry);

			maze = ExpandMaze(maze, factor);

			return maze;
		}

		/// <summary>
		/// A Product maze creates small mazes and disposition them on the points of the repeat grid.
		/// </summary>
		private IGrid<GridPoint2, RectFEV.MazeType> ProductGrid()
		{
			var repeatShape = ImplicitShape.Square(GridPoint2.Zero, repeatGridRadius).ToExplicit(new GridRect(new GridPoint2(1 - repeatGridRadius, 1 - repeatGridRadius), new GridPoint2(2*repeatGridRadius - 1, 2*repeatGridRadius - 1)));

			var smallGridShape = ImplicitShape.Square(GridPoint2.Zero, smallGridRadius).ToExplicit(new GridRect(new GridPoint2(1 - smallGridRadius, 1 - smallGridRadius), new GridPoint2(2*smallGridRadius - 1, 2*smallGridRadius - 1)));

			var bigMaze = Grid.CloneStructure<GridPoint2, RectFEV.MazeType>(p => RectFEV.MazeType.Close);

			foreach (var iter in repeatShape.Points)
			{
				var maze = RectFEV.GenerateMaze(smallGridShape, iter.GetColor(2, 1, 1) == 0 ? RectFEV.Symmetry.Reflection : RectFEV.Symmetry.Rotation);

				foreach (var point in maze.Points)
				{
					var newPoint = iter*new GridPoint2(2*smallGridRadius - 2, 2*smallGridRadius - 2) + point;

					if (maze.Contains(point) && bigMaze.Contains(newPoint))
					{
						bigMaze[newPoint] = maze[point] == RectFEV.MazeType.Open ? RectFEV.MazeType.Open : RectFEV.MazeType.Close;
					}
				}
			}

			RectFEV.ContinueMaze(bigMaze, symmetry);

			return bigMaze;
		}

		/// <summary>
		/// Expands the maze size by a factor and then connects all new rooms that appeared from the expansion.
		/// </summary>
		private IGrid<GridPoint2, RectFEV.MazeType> ExpandMaze(IGrid<GridPoint2, RectFEV.MazeType> maze, int factor)
		{
			int r1 = (maze.Bounds.Size.X + 1)/2 * factor;

			var one = -GridPoint2.One;
			var newShape = ImplicitShape.Func<GridPoint2>(p =>
				maze.Contains((p - one).FloorDiv(factor * GridPoint2.One)) &&
				maze[(p - one).FloorDiv(factor * GridPoint2.One)] == RectFEV.MazeType.Open).ToExplicit(
				new GridRect(new GridPoint2(1 - r1, 1 - r1), new GridPoint2(2 * r1 - 1, 2 * r1 - 1)));

			var maze2 = RectFEV.GenerateMaze(newShape, symmetry);


			var maze3 = maze2.Bounds.ToGrid<RectFEV.MazeType>();

			foreach (var point in maze3.Points)
			{
				maze3[point] = maze2.Contains(point) && maze2[point] == RectFEV.MazeType.Open
					? RectFEV.MazeType.Open
					: RectFEV.MazeType.Close;
			}

			RectFEV.ContinueMaze(maze3, symmetry);
			return maze3;
		}
	}
}