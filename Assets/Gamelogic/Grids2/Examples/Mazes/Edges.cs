using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Algorithms;
using UnityEngine;
using System.Text;

namespace Gamelogic.Grids2.Examples
{
	public static class RectFEV
	{
		public enum FEVType 
		{
			Face,
			Vertex,
			HorizontalEdge,
			VerticalEdge
		}

		private static readonly Vector2 EdgeOffset = new Vector2(0.5f, 0);
		private static readonly Vector2 HorizontalEdgeOffset = EdgeOffset;
		private static readonly Vector2 VerticalEdgeOffset = new Vector2(0, 0.5f);

		public static int GetCheckboardColoring(GridPoint2 point)
		{
			return point.GetColor(2, 1, 1);
		}

		public static int Get1234Coloring(GridPoint2 point)
		{
			return point.GetColor(2, 2, 2);
		}

		public static int Get5Coloring(GridPoint2 point)
		{
			return point.GetColor(5, 3, 1);
		}

		public static int Get9Coloring(GridPoint2 point)
		{
			return point.GetColor(3, 0, 3);
		}

		public static int Get9bColoring(GridPoint2 point)
		{
			return point.GetColor(3, 1, 3);
		}

		public static int Get9cColoring(GridPoint2 point)
		{
			return point.GetColor(3, 2, 3);
		}

		public static Vector2 EdgeBetween(GridPoint2 point1, GridPoint2 point2)
		{
			int distance = RectPoint.ManhattanNorm(point1 - point2);

			if (distance != 1)
			{
				throw new InvalidOperationException("The points are not neighbors and don't share an edge.");
			}

			return (point1.ToVector2() + point2.ToVector2())/2;
		}

		public static Vector2 EdgeTowards(GridPoint2 point, GridPoint2 direction)
		{
			return EdgeBetween(point, point + direction);
		}

		public static Vector2 FaceToEdge(Vector2 point)
		{
			var newX = point.x + point.y + 0.5f ;
			var newY = -point.x + point.y - 0.5f;

			return new Vector2(newX, newY);
		}

		public static Vector2 EdgeToFace(Vector2 point)
		{
			var newX = (point.x - point.y - 1)/2;
			var newY = (point.x + point.y)/2;

			return new Vector2(newX, newY);
		}

		public static IMap<Vector2, Vector2> FaceToEdgeMap()
		{
			return Map.Func<Vector2, Vector2>(FaceToEdge, EdgeToFace);
		}

		public static Vector2[] FacesOfEdge(Vector2 edgeInFaceSpace)
		{
			var edgeInEdgeSpace = FaceToEdge(edgeInFaceSpace);
			var discreteEdge = RectPoint.RoundToGridPoint(edgeInEdgeSpace);
			var colorIndex = discreteEdge.GetColor(2, 1, 1);
			
			if (colorIndex == 0)
			{
				return new[]
				{
					edgeInFaceSpace + HorizontalEdgeOffset,
					edgeInFaceSpace - HorizontalEdgeOffset
				};
			}
			else
			{
				return new[]
				{
					edgeInFaceSpace + VerticalEdgeOffset,
					edgeInFaceSpace - VerticalEdgeOffset
				};
			}
		}

		public static Vector2[] EdgesOfFace(Vector2 faceInFaceSpace)
		{
			return new[]
			{
				faceInFaceSpace + HorizontalEdgeOffset,
				faceInFaceSpace + VerticalEdgeOffset,
				faceInFaceSpace - HorizontalEdgeOffset,
				faceInFaceSpace - VerticalEdgeOffset,
			};
		}

		public static bool ContainsEdge(IImplicitShape<GridPoint2> faceShape, GridPoint2 edgeInEdgeSpace)
		{
			var edgeInFaceSpace = EdgeToFace(edgeInEdgeSpace.ToVector2());
			var faces = FacesOfEdge(edgeInFaceSpace).Select<Vector2, GridPoint2>(RectPoint.RoundToGridPoint);

			return faces.Any(faceShape.Contains);
		}

		public static IExplicitShape<GridPoint2> EdgeShape(this IExplicitShape<GridPoint2> faceShape)
		{
			var implicitShape = ImplicitShape.Func<GridPoint2>(edge => ContainsEdge(faceShape, edge));
			var edges = faceShape
				.Points
				.SelectMany(face => EdgesOfFace(face.ToVector2()))
				.Select(edge => RectPoint.RoundToGridPoint(FaceToEdge(edge)))
				.Distinct();

			var bounds = ExplicitShape.GetBounds(edges);

			return implicitShape.ToExplicit(bounds);
		}

		public static Vector2 VertexBetween(GridPoint2 point1, GridPoint2 point2)
		{
			var distance = RectPoint.ChebychevNorm(point1 - point2);

			if (distance != 1)
			{
				throw new InvalidOperationException("The two points must be diagonally opposite");
			}

			var vertex = ((Vector2) (point1 + point2))/2;

			return vertex;
		}

		public static Vector2 VertexTowards(GridPoint2 point1, GridPoint2 direction)
		{
			return VertexBetween(point1, point1 + direction);
		}

		public static Vector2 FaceToVertex(Vector2 face)
		{
			return face + new Vector2(0.5f, 0.5f);
		}

		public static Vector2 VertexToFace(Vector2 edge)
		{
			return edge - new Vector2(0.5f, 0.5f);
		}

		public static IMap<Vector2, Vector2> FaceToVertexMap()
		{
			return Map.Func<Vector2, Vector2>(FaceToVertex, VertexToFace);
		}

		public static Vector2[] FacesOfVertex(Vector2 vertexInFaceSpace)
		{
			return new[]
			{
				vertexInFaceSpace + new Vector2(0.5f, 0.5f),
				vertexInFaceSpace + new Vector2(-0.5f, 0.5f),
				vertexInFaceSpace + new Vector2(-0.5f, -0.5f),
				vertexInFaceSpace + new Vector2(0.5f, -0.5f),
			};
		}

		public static Vector2[] VerticesOfFace(Vector2 faceInFaceSpace)
		{
			return new[]
			{
				faceInFaceSpace + new Vector2(0.5f, 0.5f),
				faceInFaceSpace + new Vector2(-0.5f, 0.5f),
				faceInFaceSpace + new Vector2(-0.5f, -0.5f),
				faceInFaceSpace + new Vector2(0.5f, -0.5f),
			};
		}

		public static bool ContainsVertex(IImplicitShape<GridPoint2> faceShape, GridPoint2 vertexInVertexSpace)
		{
			var vertexInFaceSpace = VertexToFace(vertexInVertexSpace);
			var faces = FacesOfVertex(vertexInFaceSpace).Select<Vector2, GridPoint2>(RectPoint.RoundToGridPoint);

			return faces.Any(faceShape.Contains);
		}

		public static IExplicitShape<GridPoint2> VertexShape(this IExplicitShape<GridPoint2> faceShape)
		{
			var implicitShape = ImplicitShape.Func<GridPoint2>(vertex => ContainsVertex(faceShape, vertex));
			var vertices = faceShape
				.Points
				.SelectMany(face => VerticesOfFace(face.ToVector2()))
				.Select(vertex => RectPoint.RoundToGridPoint(FaceToVertex(vertex)))
				.Distinct();

			var bounds = ExplicitShape.GetBounds(vertices);

			return implicitShape.ToExplicit(bounds);
		}

		public static Vector2 FaceToFEV(Vector2 point)
		{
			return point * 2;
		}

		public static Vector2 FEVToFace(Vector2 point)
		{
			return point/2;
		}

		public static IMap<Vector2, Vector2> FaceToFEVMap()
		{
			return Map.Func<Vector2, Vector2>(FaceToFEV, FEVToFace);
		} 

		public static FEVType GetType(GridPoint2 point)
		{
			var colorIndex = point.GetColor(2, 2, 2);

			switch (colorIndex)
			{
				case 0:
					return FEVType.Face;
				case 1:
					return FEVType.VerticalEdge;
				case 2:
					return FEVType.HorizontalEdge;
				case 3:
					return FEVType.Vertex;

				default:
					throw new InvalidOperationException();
			}
		}

		public static Vector2[] FacesOfFEV(Vector2 fev)
		{
			var fevInFEVSpace = RectPoint.RoundToGridPoint(FaceToFEV(fev));

			var fevType = GetType(fevInFEVSpace);

			switch (fevType)
			{
				case FEVType.Face:
					return new[] {fev};
				case FEVType.Vertex:
					return FacesOfVertex(fev);
				case FEVType.HorizontalEdge:
					return FacesOfEdge(fev);
				case FEVType.VerticalEdge:
					return FacesOfEdge(fev);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public static Vector2[] FEVsOfFace(Vector2 faceInFaceSpace)
		{
			return new[]
			{
				faceInFaceSpace,
				faceInFaceSpace + new Vector2(0.5f, 0),
				faceInFaceSpace + new Vector2(0.5f, 0.5f),
				faceInFaceSpace + new Vector2(0, 0.5f),
				faceInFaceSpace + new Vector2(-0.5f, 0.5f),
				faceInFaceSpace + new Vector2(-0.5f, 0),
				faceInFaceSpace + new Vector2(-0.5f, -0.5f),
				faceInFaceSpace + new Vector2(0f, -0.5f),
				faceInFaceSpace + new Vector2(0.5f, -0.5f),
			};
		}

		public static bool ContainsFEV(IImplicitShape<GridPoint2> faceShape, GridPoint2 fevInFEVSpace)
		{
			var fevInFaceSpace = FEVToFace(fevInFEVSpace);
			var faces = FacesOfFEV(fevInFaceSpace).Select<Vector2, GridPoint2>(RectPoint.RoundToGridPoint);

			return faces.Any(faceShape.Contains);
		}

		public static IExplicitShape<GridPoint2> FEVShape(this IExplicitShape<GridPoint2> faceShape)
		{
			var implicitShape = ImplicitShape.Func<GridPoint2>(fev => ContainsFEV(faceShape, fev));
			var vertices = faceShape
				.Points
				.SelectMany(face => FEVsOfFace(face.ToVector2()))
				.Select(vertex => RectPoint.RoundToGridPoint(FaceToFEV(vertex)))
				.Distinct();

			var bounds = ExplicitShape.GetBounds(vertices);

			return implicitShape.ToExplicit(bounds);
		}

		public enum MazeType
		{
			Open,
			Close
		}

		public static IGrid<GridPoint2, bool> GenerateMaze3<TCell>(IGrid<GridPoint2, TCell> fevGrid)
		{
			var neighborDirections = new GridPoint2[]
			{
				RectPoint.North,
				RectPoint.East,
				RectPoint.South,
				RectPoint.West,
			};

			var maze = fevGrid.CloneStructure<GridPoint2, MazeType>(
				p => Get1234Coloring(p) == 0 ? MazeType.Open : MazeType.Close);

			//var start = GridPoint2.Zero;
			var start = maze.Where(pair => pair.cell == MazeType.Open).RandomItem().point;

			var node = start;
			var path = new List<GridPoint2>();

			path.Add(start);

			var newWalls = start.GetVectorNeighbors(neighborDirections).In(maze).ToList();
			newWalls.Shuffle();
			var walls = new List<GridPoint2>(newWalls);

			//var directionGenerator1 = Generator
			//	.Repeat(new List<int> {1, 1, 1, 1, 2})
			//	.RepeatEach(Generator.UniformRandomInt(1)
			//	.Select(n => n + 1));

			//var directionGenerator2 = Generator
			//	.Repeat(new List<int> {2, 2, 2, 2, 1})
			//	.RepeatEach(Generator.UniformRandomInt(1)
			//	.Select(n => n + 1));

			//var switcher = Generator
			//	.Repeat(new List<int> {0, 1})
			//	.RepeatEach(10000);

			//var directionGenerator = Generator
			//	.Choose(new List<IGenerator<int>> {directionGenerator1, directionGenerator2}, switcher);

			while (walls.Any())
			{
				//var wall = walls.RandomItem();
				//var wall = walls.First();
				//var wall = walls.Last();
				//var wall = walls.NthOrLast(5);
				//var wall = walls.TakeLast(5).First();

				//var wall = walls.GroupBy(w => RectPoint.EuclideanNorm(w - start)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ChebychevNorm(w - start)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ManhattanNorm(w - start)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.KnightNorm(w - start)).MinBy(g => g.Key).RandomItem();

				//var wall = walls.GroupBy(w => RectPoint.EuclideanNorm(w - start)).MaxBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ChebychevNorm(w - start)).MaxBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ManhattanNorm(w - start)).MaxBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.KnightNorm(w - start)).MaxBy(g => g.Key).RandomItem();

				var nodeCopy = node;
				//var wall = walls.GroupBy(w => RectPoint.EuclideanNorm(w - nodeCopy)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ChebychevNorm(w - nodeCopy)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ManhattanNorm(w - nodeCopy)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.KnightNorm(w - nodeCopy)).MinBy(g => g.Key).RandomItem();

				//var wall = walls.GroupBy(w => RectPoint.EuclideanNorm(w - nodeCopy)).MaxBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ChebychevNorm(w - nodeCopy)).MaxBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ManhattanNorm(w - nodeCopy)).MaxBy(g => g.Key).RandomItem();
				var wall = walls.GroupBy(w => RectPoint.KnightNorm(w - nodeCopy)).MaxBy(g => g.Key).RandomItem();

				//Spirally
				//var wall = walls.TakeLast(2).GroupBy(w => RectPoint.KnightNorm(node1 - w)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.TakeLast(2).GroupBy(w => RectPoint.KnightNorm(start - w)).MinBy(g => g.Key).RandomItem();

				//int direction = directionGenerator.Next();
				//var wall = walls.RandomOrDefault(walls.TakeLast(2).RandomItem(), w => Get1234Coloring(w) == direction);
				//var wall = walls.FirstOrRandom(w => Get1234Coloring(w) == 1);

				List<GridPoint2> faces = new List<GridPoint2>();

				if (Get1234Coloring(wall) == 1)
				{
					var face = wall + RectPoint.East;

					if(maze.Contains(face)) faces.Add(face);

					face = wall - RectPoint.East;
					if (maze.Contains(face)) faces.Add(face);
				}
				else
				{
					GLDebug.Assert(Get1234Coloring(wall) == 2, "Must be 2");

					var face = wall + RectPoint.North;

					if (maze.Contains(face)) faces.Add(face);

					face = wall - RectPoint.North;
					if (maze.Contains(face)) faces.Add(face);
				}

				if (faces.Count == 2)
				{
					var unvisited = faces.Where(f => !path.Contains(f)).ToList();

					bool shouldAdd = unvisited.Count == 1;

					if (!shouldAdd)
					{
						shouldAdd = !IsConnected(maze, faces[0], faces[1]);
					}

					if(shouldAdd)
					{
						var nodeToAdd = unvisited.Count == 0 ? faces.First() : unvisited[0];

						maze[wall] = MazeType.Open;
						path.Add(nodeToAdd);
						newWalls = nodeToAdd.GetVectorNeighbors(neighborDirections).In(maze).ToList();
						//newWalls.Shuffle();
						node = nodeToAdd;
						walls.AddRange(newWalls);

						var node0 = RectPoint.ReflectAboutY(nodeToAdd);
						var wall0 = RectPoint.ReflectAboutY(wall);
						shouldAdd = !path.Contains(node0);

						if (!shouldAdd)
						{
							shouldAdd = !IsConnected(maze, node, node0);
						}

						if (shouldAdd)
						{
							maze[wall0] = MazeType.Open;
							path.Add(node0);
						}

						var node1 = RectPoint.ReflectAboutX(nodeToAdd);
						var wall1 = RectPoint.ReflectAboutX(wall);
						shouldAdd = !path.Contains(node1);

						if (!shouldAdd)
						{
							shouldAdd = !IsConnected(maze, node, node1);
						}

						if (shouldAdd)
						{
							maze[wall1] = MazeType.Open;
							path.Add(node1);
						}

						var node2 = -node;
						var wall2 = -wall;

						shouldAdd = !path.Contains(node2);

						if (!shouldAdd)
						{
							shouldAdd = !IsConnected(maze, node, node2);
						}

						if (shouldAdd)
						{
							maze[wall2] = MazeType.Open;
							path.Add(node2);
						}
					}
				}

				walls.Remove(wall);
			}
		
			return maze.CloneStructure<GridPoint2, bool>(p => maze[p] == MazeType.Open);
		}

		public enum Symmetry
		{
			None,
			Reflection,
			Rotation,
			All,
			LongX
		}

		public static IGrid<GridPoint2, bool> GenerateMaze_Reference<TCell>(IGrid<GridPoint2, TCell> fevGrid, Symmetry symmetry)
		{
			var neighborDirections = new GridPoint2[]
			{
				RectPoint.North,
				RectPoint.East,
				RectPoint.South,
				RectPoint.West,
			};

			var maze = fevGrid.CloneStructure<GridPoint2, MazeType>(
				p => Get1234Coloring(p) == 0 ? MazeType.Open : MazeType.Close);

			//var start = GridPoint2.Zero;
			var start = maze.Where(pair => pair.cell == MazeType.Open).RandomItem().point;

			//var node = start;
			var path = new List<GridPoint2>();

			path.Add(start);

			var newWalls = start.GetVectorNeighbors(neighborDirections).In(maze).ToList();
			newWalls.Shuffle();
			var walls = new List<GridPoint2>(newWalls);

			//var directionGenerator1 = Generator
			//	.Repeat(new List<int> { 1, 1, 1, 1, 2 })
			//	.RepeatEach(Generator.UniformRandomInt(1)
			//	.Select(n => n + 1));
			//
			//var directionGenerator2 = Generator
			//	.Repeat(new List<int> { 2, 2, 2, 2, 1 })
			//	.RepeatEach(Generator.UniformRandomInt(1)
			//	.Select(n => n + 1));

			//var switcher = Generator
			//	.Repeat(new List<int> { 0, 1 })
			//	.RepeatEach(40);

			//var directionGenerator = Generator
			//	.Choose(new List<IGenerator<int>> { directionGenerator1, directionGenerator2 }, switcher);

			var centerGenerator =
				Generator
					.ChooseUniformRandom(maze.Points.Where(p => Get1234Coloring(p) == 0).ToList()).RepeatEach(80);
					//.Constant(GridPoint2.Zero);

			while (walls.Any())
			{
				//var wall = walls.RandomItem();
				//var wall = walls.First();
				//var wall = walls.Last();
				//var wall = walls.NthOrLast(5);
				//var wall = walls.TakeLast(5).First();

				//var wall = walls.GroupBy(w => RectPoint.EuclideanNorm(w - start)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ChebychevNorm(w - start)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ManhattanNorm(w - start)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.KnightNorm(w - start)).MinBy(g => g.Key).RandomItem();

				//var wall = walls.GroupBy(w => RectPoint.EuclideanNorm(w - start)).MaxBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ChebychevNorm(w - start)).MaxBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ManhattanNorm(w - start)).MaxBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.KnightNorm(w - start)).MaxBy(g => g.Key).RandomItem();

				//var nodeCopy = node;
				//var wall = walls.GroupBy(w => RectPoint.EuclideanNorm(w - nodeCopy)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ChebychevNorm(w - nodeCopy)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ManhattanNorm(w - nodeCopy)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.KnightNorm(w - nodeCopy)).MinBy(g => g.Key).RandomItem();

				//var wall = walls.GroupBy(w => RectPoint.EuclideanNorm(w - nodeCopy)).MaxBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ChebychevNorm(w - nodeCopy)).MaxBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.ManhattanNorm(w - nodeCopy)).MaxBy(g => g.Key).RandomItem();
				//var wall = walls.GroupBy(w => RectPoint.KnightNorm(w - nodeCopy)).MaxBy(g => g.Key).RandomItem();

				//var centerCopy = centerGenerator.Current;
				//var wall = walls.GroupBy(w => RectPoint.EuclideanNorm(w - centerCopy)).MinBy(g => g.Key).RandomItem();

				//Spirally
				//var wall = walls.TakeLast(2).GroupBy(w => RectPoint.KnightNorm(nodeCopy - w)).MinBy(g => g.Key).RandomItem();
				//var wall = walls.TakeLast(2).GroupBy(w => RectPoint.KnightNorm(start - w)).MinBy(g => g.Key).RandomItem();

				//int direction = directionGenerator.Next();
				//var wall = walls.RandomOrDefault(walls.TakeLast(2).RandomItem(), w => Get1234Coloring(w) == direction);
				//var wall = walls.FirstOrRandom(w => Get1234Coloring(w) == 1);

				centerGenerator.Next();
/*
				var wall = selection == 0 
					? walls.GroupBy(w => RectPoint.EuclideanNorm(w - centerGenerator.Current)).MinBy(g => g.Key).RandomItem() 
					: walls.GroupBy(w => RectPoint.EuclideanNorm(w - centerGenerator.Current)).MaxBy(g => g.Key).RandomItem();
*/				
/*
				var wall = selection == 0
					? walls.GroupBy(w => RectPoint.EuclideanNorm(w - start)).MinBy(g => g.Key).RandomItem()
					: walls.TakeLast(2).GroupBy(w => RectPoint.KnightNorm(nodeCopy - w)).MinBy(g => g.Key).RandomItem();
*/
				var wallPartition = walls.GroupBy(w => w.FloorDiv(new GridPoint2(2, 2)).GetColor(5, 3, 1) % 2 == 1).RandomItem();

				var wall = wallPartition.Key 
					? wallPartition.LastOrDefault(wallPartition.Last(), w => Get1234Coloring(w) == 1) 
					: wallPartition.LastOrDefault(wallPartition.Last(), w => Get1234Coloring(w) == 2);

				TryToAddWall(maze, path, walls, neighborDirections, wall);
				List<GridPoint2> pattern = null;

				if (symmetry != Symmetry.None)
				{
					switch (symmetry)
					{
						case Symmetry.Reflection:
							pattern = GetReflectionalSymmetry(wall, GridPoint2.Zero);
							break;
						case Symmetry.Rotation:
							pattern = GetRotationalSymmetry(wall, GridPoint2.Zero);
							break;
						case Symmetry.All:
							pattern = GetReflectionalSymmetry(wall, GridPoint2.Zero);
							pattern.AddRange(GetRotationalSymmetry(wall, GridPoint2.Zero));
							pattern = pattern.Distinct().ToList();
							break;
					}

					foreach (var newWall in pattern.Where(newWall => maze.Contains(newWall)))
					{
						if (Get1234Coloring(newWall) == 0 || Get1234Coloring(newWall) == 3)
						{
							throw new Exception("Assert wall is a wall");
						}
						if (maze.Contains(wall))
						{
							TryToAddWall(maze, path, walls, neighborDirections, newWall);
						}
					}
				}
			}

			return maze.CloneStructure<GridPoint2, bool>(p => maze[p] == MazeType.Open);
		}

		/// <summary>
		/// Generate Maze generates a maze on a grid using a symmetry mode.
		/// </summary>
		public static IGrid<GridPoint2, MazeType> GenerateMaze(
			IExplicitShape<GridPoint2> fevGrid, 
			Symmetry symmetry)
		{
			// We start the maze and set all the cells as closed
			var maze = fevGrid.ToGrid<MazeType>();
			maze.Fill(p => MazeType.Close);

			ContinueMaze(maze, symmetry);

			return maze;
		}

		/// <summary>
		/// Continue Maze process a maze and and creates the paths between unconnected sets if any potential one is found.
		/// </summary>
		public static void ContinueMaze(
			IGrid<GridPoint2, MazeType> maze, 
			Symmetry symmetry)
		{
			// Get the special cell points called rooms, that are all the cell points in the grid that are separated by 
			// one cell from each other.
			/*
				Rooms on a grid of 9x7 cells:

				- - - - - - - - -
				- R - R - R - R -
				- - - - - - - - -
				- R - R - R - R -
				- - - - - - - - -
				- R - R - R - R -
				- - - - - - - - -
			*/
			var rooms = maze
				.Points
				.Where(IsRoom)
				.ToList();

			if (!rooms.Any()) return;

			// Force Open all the cells that are rooms and leave the rest intact.
			maze.Fill(p => ((Func<GridPoint2, bool>) IsRoom)(p) ? MazeType.Open : maze[p]);

			// Get all the cell points that can make a path between all rooms.
			var path = rooms
				.Where(p =>
					p.GetVectorNeighbors(NeighborDirections)
						.Where(maze.Contains)
						.Any(q => maze[q] == MazeType.Open))
				.ToList();

			var walls = new List<GridPoint2>();

			do
			{
				// While there is walls to check
				while (walls.Any())
				{
					// Get a random wall and try to open it to expand the maze
					var wall = walls.RandomItem();

					// Try to add it to the path (and also remove it from the wall list).
					TryToAddWall(maze, path, walls, NeighborDirections, wall);

					// Do the same for walls generated by the symmetry mode.
					var pattern = GetSymmetryPattern(symmetry, wall);

					foreach (var newWall in pattern.In(maze))
					{
						TryToAddWall(maze, path, walls, NeighborDirections, newWall);
					}

					// An optimization to stop checking walls when all the paths are already connected.
					if (path.Count == rooms.Count)
					{
						walls.Clear();
						break;
					}
				}

				// Get the list of rooms that are connected.
				var connectedRooms = Grids2.Algorithms
					.GetConnectedSet(maze,
						rooms.First(),
						p => p.GetVectorNeighbors(NeighborDirections).Where(maze.Contains).Where(q => maze[q] == MazeType.Open))
					.Where(p => ((Func<GridPoint2, bool>) IsRoom)(p)).ToList();

				// Compare the connected rooms to the unconnected ones.
				var unconnectedRooms = rooms
					.Where(p => !connectedRooms.Contains(p));

				// Get the possible walls from the unconnected rooms.
				var possibleWalls = unconnectedRooms
					.SelectMany(
						room => room.GetVectorNeighbors(NeighborDirections).Where(maze.Contains).Where(wall => maze[wall] == MazeType.Close))
					.ToList();

				// If there is any possible wall, we will start expanding with a random one
				if (possibleWalls.Any())
				{
					walls.Add(possibleWalls.RandomItem());
				}

			} while (walls.Any());
		}


		public static bool IsRoom(GridPoint2 p)
		{
			return Get1234Coloring(p) == 0;
		}

		private static List<GridPoint2> GetSymmetryPattern(Symmetry symmetry, GridPoint2 wall)
		{
			List<GridPoint2> pattern = null;

			switch (symmetry)
			{
				case Symmetry.LongX:
					pattern = GetLongX(wall);
					break;
				case Symmetry.Reflection:
					pattern = GetReflectionalSymmetry(wall, GridPoint2.Zero);
					break;
				case Symmetry.Rotation:
					pattern = GetRotationalSymmetry(wall, GridPoint2.Zero);
					break;
				case Symmetry.All:
					pattern = GetReflectionalSymmetry(wall, GridPoint2.Zero);
					pattern.AddRange(GetRotationalSymmetry(wall, GridPoint2.Zero));
					pattern = pattern.Distinct().ToList();
					break;
				default:
					pattern = new List<GridPoint2>();
					break;
			}
			return pattern;
		}

		private static List<GridPoint2> GetRotationalSymmetry(GridPoint2 point, GridPoint2 center)
		{
			var list = new List<GridPoint2>
			{
				RectPoint.Rotate90(point - center) + center, RectPoint.Rotate180(point - center) + center, RectPoint.Rotate270(point - center) + center
			};


			return list;
		}

		private static List<GridPoint2> GetReflectionalSymmetry(GridPoint2 point, GridPoint2 center)
		{
			var list = new List<GridPoint2>
			{
				RectPoint.ReflectAboutY(point - center) + center, RectPoint.ReflectAboutX(point - center) + center, RectPoint.ReflectAboutX(RectPoint.ReflectAboutY(point - center)) + center
			};

			return list;
		}

		private static List<GridPoint2> GetLongX(GridPoint2 point)
		{
			var list = new List<GridPoint2>
			{
				point + RectPoint.East * 2, point + RectPoint.East *4
			};

			return list;
		} 

		private static void TryToAddWall(
			IGrid<GridPoint2, MazeType> maze, 
			List<GridPoint2> path, 
			List<GridPoint2> walls, 
			GridPoint2[] neighborDirections, 
			GridPoint2 wall)
		{
			// Get the faces associated to the wall.
			var faces = GetFaces(maze, wall).In(maze).ToList();

			if (faces.Count == 2)
			{
				// Check if the faces are connected.
				if (!IsConnected(maze, faces[0], faces[1]))
				{
					// Add the wall to the path
					var unvisited = faces.Where(f => !path.Contains(f)).ToList();

					if (unvisited.Any())
					{
						path.AddRange(unvisited);
						var newWalls = unvisited.SelectMany(n => n.GetVectorNeighbors(neighborDirections).In(maze)).ToList();
						walls.AddRange(newWalls);
					}

					// And Open it
					maze[wall] = MazeType.Open;
				}
			}

			walls.Remove(wall);
		}

		private static List<GridPoint2> GetFaces(IImplicitShape<GridPoint2> maze, GridPoint2 wall)
		{
			var faces = new List<GridPoint2>();

			if (Get1234Coloring(wall) == 1)
			{
				var face = wall + RectPoint.East;

				if (maze.Contains(face)) faces.Add(face);

				face = wall - RectPoint.East;
				if (maze.Contains(face)) faces.Add(face);
			}
			else
			{
				GLDebug.Assert(Get1234Coloring(wall) == 2, "Must be 2");

				var face = wall + RectPoint.North;

				if (maze.Contains(face)) faces.Add(face);

				face = wall - RectPoint.North;
				if (maze.Contains(face)) faces.Add(face);
			}

			return faces;
		}

		private static readonly Wall[] Walls5 =
		{
			new Wall
			{
				direction = WallDirection.North, points = new[]
				{
					RectPoint.North, RectPoint.NorthEast,
				}
			},
			new Wall
			{
				direction = WallDirection.East, points = new[]
				{
					RectPoint.East, RectPoint.SouthEast,
				}
			},
			new Wall
			{
				direction = WallDirection.South, points = new[]
				{
					RectPoint.South, RectPoint.SouthWest,
				}
			},
			new Wall
			{
				direction = WallDirection.West, points = new[]
				{
					RectPoint.West, RectPoint.NorthWest,
				}
			}
		};

		private static readonly Wall[] Walls9 =
		{
			new Wall
			{
				direction = WallDirection.North, points = new[]
				{
					RectPoint.North, RectPoint.NorthWest, 2*RectPoint.North, 2*RectPoint.North + RectPoint.East,
				}
			},
			new Wall
			{
				direction = WallDirection.East, points = new[]
				{
					RectPoint.East, RectPoint.NorthEast, 2*RectPoint.East, 2*RectPoint.East + RectPoint.South,
				}
			},
			new Wall
			{
				direction = WallDirection.South, points = new[]
				{
					RectPoint.South, RectPoint.SouthEast, 2*RectPoint.South, 2*RectPoint.South + RectPoint.West,
				}
			},
			new Wall
			{
				direction = WallDirection.West, points = new[]
				{
					RectPoint.West, RectPoint.SouthWest, 2*RectPoint.West, 2*RectPoint.West + RectPoint.North,
				}
			}
		};

		private static readonly Wall[] Walls9b =
		{
			new Wall
			{
				direction = WallDirection.North, points = new[]
				{
					RectPoint.North, 2*RectPoint.North + RectPoint.East, 2*RectPoint.North,
					//2*RectPoint.North + RectPoint.East,
				}
			},
			new Wall
			{
				direction = WallDirection.East, points = new[]
				{
					RectPoint.East,
					//RectPoint.NorthEast,
					2*RectPoint.East,
					//2*RectPoint.East + RectPoint.South,
				}
			},
			new Wall
			{
				direction = WallDirection.South, points = new[]
				{
					RectPoint.South, RectPoint.SouthWest,
					//2*RectPoint.South,
					2*RectPoint.South + RectPoint.West,
				}
			},
			new Wall
			{
				direction = WallDirection.West, points = new[]
				{
					RectPoint.West,
					//RectPoint.SouthWest,
					2*RectPoint.West,
					//2*RectPoint.West + RectPoint.North,
				}
			}
		};

		private static readonly Wall[] Walls9c =
		{
			new Wall
			{
				direction = WallDirection.North, points = new[]
				{
					RectPoint.North, RectPoint.North + RectPoint.East, 2*RectPoint.North + RectPoint.East, 2*RectPoint.North + 2*RectPoint.East,
				}
			},
			new Wall
			{
				direction = WallDirection.East, points = new[]
				{
					RectPoint.East, 2*RectPoint.East,
				}
			},
			new Wall
			{
				direction = WallDirection.South, points = new[]
				{
					RectPoint.South, RectPoint.SouthWest, 2*RectPoint.South + RectPoint.West, 2*RectPoint.South + 2*RectPoint.West,
				}
			},
			new Wall
			{
				direction = WallDirection.West, points = new[]
				{
					RectPoint.West,
					//RectPoint.SouthWest,
					2*RectPoint.West,
					//2*RectPoint.West + RectPoint.North,
				}
			}
		};

		private static readonly GridPoint2[][] Faces5 =
		{
			new GridPoint2[]
			{
				RectPoint.South, RectPoint.NorthEast
			},
			new GridPoint2[]
			{
				RectPoint.West, RectPoint.SouthEast
			},
			new GridPoint2[]
			{
				RectPoint.North, RectPoint.SouthWest
			},
			new GridPoint2[]
			{
				RectPoint.East, RectPoint.NorthWest
			},
		};

		private static readonly GridPoint2[][] Faces9 =
		{
			new GridPoint2[]
			{
				RectPoint.South, 2*RectPoint.North
			},
			new GridPoint2[]
			{
				RectPoint.West, 2*RectPoint.East
			},
			new GridPoint2[]
			{
				RectPoint.North, 2*RectPoint.South
			},
			new GridPoint2[]
			{
				RectPoint.East, 2*RectPoint.West
			},
		};

		private static readonly GridPoint2[][] Faces9b =
		{
			new GridPoint2[]
			{
				RectPoint.South, 2*RectPoint.North + RectPoint.East
			},
			new GridPoint2[]
			{
				RectPoint.West, 2*RectPoint.East
			},
			new GridPoint2[]
			{
				RectPoint.North, 2*RectPoint.South + RectPoint.West
			},
			new GridPoint2[]
			{
				RectPoint.East, 2*RectPoint.West
			},
		};

		private static readonly GridPoint2[][] Faces9c =
		{
			new GridPoint2[]
			{
				RectPoint.South, 2*RectPoint.North + 2*RectPoint.East
			},
			new GridPoint2[]
			{
				RectPoint.West, 2*RectPoint.East
			},
			new GridPoint2[]
			{
				RectPoint.North, 2*RectPoint.South + 2*RectPoint.West
			},
			new GridPoint2[]
			{
				RectPoint.East, 2*RectPoint.West
			},
		};

		private static readonly GridPoint2[] NeighborDirections = new[]
		{
			RectPoint.North,
			RectPoint.East,
			RectPoint.South,
			RectPoint.West,
		};

		public enum WallDirection
		{
			North,
			East,
			South,
			West
		}

		public class Wall : IEnumerable<GridPoint2>
		{
			public WallDirection direction;
			public GridPoint2[] points;


			public IEnumerator<GridPoint2> GetEnumerator()
			{
				return ((IEnumerable<GridPoint2>) points).GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}

		private static IEnumerable<Wall> GetWalls(Wall[] wallDirections, GridPoint2 point)
		{
			return wallDirections.Select(w => new Wall
			{
				direction = w.direction, points = w.points.Select(c => c + point).ToArray()
			});
		}

		private static IEnumerable<GridPoint2> GetFaces(GridPoint2[][] faceDirections, Wall wall)
		{
			return faceDirections[(int) wall.direction].Select(f => f + wall.points[0]);
		}

		public static IGrid<GridPoint2, bool> GenerateMaze5<TCell>(IGrid<GridPoint2, TCell> fevGrid)
		{
			return GenerateMaze(fevGrid, Get5Coloring, Walls5, Faces5);
		}

		public static IGrid<GridPoint2, bool> GenerateMaze9<TCell>(IGrid<GridPoint2, TCell> fevGrid)
		{
			return GenerateMaze(fevGrid, Get9Coloring, Walls9, Faces9);
		}

		public static IGrid<GridPoint2, bool> GenerateMaze9b<TCell>(IGrid<GridPoint2, TCell> fevGrid)
		{
			return GenerateMaze(fevGrid, Get9bColoring, Walls9b, Faces9b);
		}

		public static IGrid<GridPoint2, bool> GenerateMaze9c<TCell>(IGrid<GridPoint2, TCell> fevGrid)
		{
			return GenerateMaze(fevGrid, Get9cColoring, Walls9c, Faces9c);
		}

		/// <summary>
		/// Returns a random element from a source.
		/// </summary>
		/// <typeparam name="T">The type of items generated from the source.</typeparam>
		/// <param name="source">The list.</param>
		/// <returns>A item randomly selected from the source.</returns>
		public static T RandomItem<T>(this IEnumerable<T> source, float p)
		{
			if (source.Count() == 1) return source.First();

			var r = GLRandom.Range(0, 1f);

			if (r < p) return source.Last();

			return source.ButFirst().RandomItem();
		}

		public static T NthOrLast<T>(this IEnumerable<T> source, int n)
		{
			if (source.Count() <= n + 1) return source.Last();

			return source.Skip(n).First();
		}

		public static IEnumerable<T> SampleRandom<T>(this IEnumerable<T> source)
		{
			return source.SampleRandom(source.Count());
		}

		private static Wall Select(List<Wall> walls, int n)
		{
			IEnumerable<Wall> filteredWalls;

			if (n == 0)
			{
				filteredWalls = walls.Where(w => w.direction == WallDirection.East || w.direction == WallDirection.West);
			}
			else
			{
				filteredWalls = walls.Where(w => w.direction == WallDirection.South || w.direction == WallDirection.North);
			}

			if (filteredWalls.Any()) return filteredWalls.Last();

			return walls.RandomItem();
		}

		private static Wall Select2(List<Wall> walls, int n)
		{
			IEnumerable<Wall> filteredWalls;

			if (n == 0)
			{
				filteredWalls = walls.Where(w => w.direction == WallDirection.East);
			}
			else if (n == 1)
			{
				filteredWalls = walls.Where(w => w.direction == WallDirection.North);
			}
			else if (n == 2)
			{
				filteredWalls = walls.Where(w => w.direction == WallDirection.West);
			}
			else //if (n == 3)
			{
				filteredWalls = walls.Where(w => w.direction == WallDirection.South);
			}

			if (filteredWalls.Any()) return filteredWalls.Last();

			return walls.RandomItem();
		}

		private static T FirstOrRandom<T>(this IEnumerable<T> source, params Func<T, bool>[] predicates)
		{
			return FirstOrDefault(source, source.RandomItem(), predicates);
		}

		private static T FirstOrDefault<T>(this IEnumerable<T> source, T defaultItem, params Func<T, bool>[] predicates)
		{
			foreach (var predicate in predicates)
			{
				var filteredItems = source.Where(predicate);

				if (filteredItems.Any()) return filteredItems.First();
			}

			return defaultItem;
		}

		private static T LastOrDefault<T>(this IEnumerable<T> source, T defaultItem, params Func<T, bool>[] predicates)
		{
			foreach (var predicate in predicates)
			{
				var filteredItems = source.Where(predicate);

				if (filteredItems.Any())
				{
					return filteredItems.Last();
				}
			}

			return defaultItem;
		}

		private static T RandomOrDefault<T>(this IEnumerable<T> source, T defaultItem, params Func<T, bool>[] predicates)
		{
			foreach (var predicate in predicates)
			{
				var filteredItems = source.Where(predicate);

				if (filteredItems.Any()) return filteredItems.RandomItem();
			}

			return defaultItem;
		}

		public static float AverageDistance(IEnumerable<GridPoint2> path, Wall wall)
		{
			return path.Average(p => RectPoint.EuclideanNorm(p - wall.points[0]));
		}

		private static bool IsConnected(IGrid<GridPoint2, MazeType> grid, GridPoint2 point1, GridPoint2 point2)
		{
			var path = Grids2.Algorithms.AStar<GridPoint2>(grid, point1, point2, (p1, p2) => RectPoint.ManhattanNorm(p1 - p2), p => grid[p] == MazeType.Open, RectPoint.GetOrthogonalNeighbors, (p1, p2) => 1);

			return path != null;
		}

		public static IGrid<GridPoint2, bool> GenerateMaze<TCell>(IGrid<GridPoint2, TCell> fevGrid, Func<GridPoint2, int> getColoring, Wall[] wallDirections, GridPoint2[][] faceDirections)
		{
			var border = fevGrid.Points.Where(p => RectPoint.GetOrthogonalNeighbors(p).In(fevGrid).Count() != 4).ToList();
			var maze = fevGrid.CloneStructure<GridPoint2, MazeType>(p => (getColoring(p) == 0 && !border.Contains(p)) ? MazeType.Open : MazeType.Close);

			var start = maze.Where(pair => pair.cell == MazeType.Open).RandomItem().point;

			//start = GridPoint2.Zero;

			var path = new List<GridPoint2>();

			path.Add(start);

			var walls = new List<Wall>(GetWalls(wallDirections, start).Where(w => w.All(c => maze.Contains(c))));
			int i = 0;

			//var selector = Generator.Count(4).Select(x => x%4).RepeatEach(8);
			//var dir = WallDirection.North;
			//var node = start;

			while (walls.Any())
			{
				//Spirally
				//var wall = walls.TakeLast(3).GroupBy(w => RectPoint.KnightNorm(node - w.points[0])).MinBy(g => g.Key).RandomItem();

				var wall = walls.RandomItem();

				var faces = GetFaces(faceDirections, wall).In(maze).ToList();

				if (faces.Count == 2)
				{
					var unvisited = faces.Where(f => !path.Contains(f) && !border.Contains(f)).ToList();

					if (unvisited.Count == 1)
					{
						foreach (var cell in wall)
						{
							maze[cell] = MazeType.Open;
						}
						start = unvisited[0];
						path.Add(start);
						walls.AddRange(GetWalls(wallDirections, start).Where(w => w.All(c => maze.Contains(c))));

						//if (i%4 == 3) node = start;
						/*
						
						var otherWall = GetWalls(wallDirections, -unvisited[0]).ToList()[(0 + (int) wall.direction)%4];

						foreach (var cell in otherWall)
						{
							maze[cell] = MazeType.Open;
						}

						path.Add(-unvisited[0]);
						walls.AddRange(GetWalls(wallDirections, -unvisited[0]).Where(w => w.All(c => maze.Contains(c))));

						var otherWall2 = GetWalls(wallDirections, RectPoint.Rotate90(unvisited[0])).ToList()[(1 + (int)wall.direction) % 4];

						foreach (var cell in otherWall2)
						{
							maze[cell] = MazeType.Open;
						}

						path.Add(RectPoint.Rotate90(unvisited[0]));
						walls.AddRange(GetWalls(wallDirections, RectPoint.Rotate90(unvisited[0])).Where(w => w.All(c => maze.Contains(c))));

						var otherWall3 = GetWalls(wallDirections, RectPoint.Rotate270(unvisited[0])).ToList()[(3 + (int)wall.direction) % 4];

						foreach (var cell in otherWall3)
						{
							maze[cell] = MazeType.Open;
						}
						
						path.Add(RectPoint.Rotate270(unvisited[0]));
						walls.AddRange(GetWalls(wallDirections, RectPoint.Rotate270(unvisited[0])).Where(w => w.All(c => maze.Contains(c))));
						*/
						i++;
					}
				}

				walls.Remove(wall);

				//if(i > 1) break;
			}

			return maze.CloneStructure<GridPoint2, bool>(p => maze[p] == MazeType.Open);
		}
	}
}