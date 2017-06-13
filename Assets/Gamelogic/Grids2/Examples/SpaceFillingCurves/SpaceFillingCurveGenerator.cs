using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions.Algorithms;
using UnityEngine.Assertions;

namespace Gamelogic.Grids2.Examples
{
	public static class SpaceFillingCurveGenerator
	{
		#region  Types
		/*
		 * Implementation Notes. 
		 * 
		 * The curve generators (not the index generator) 
		 * work on the following principle: the generator maintains a list of
		 * points, and generates points from this list. When all points in the list
		 * have been generated, the list is expanded by a processes that 
		 * uses all the exisiting points. The current list is essentially duplicated a number of times,
		 * and each duplicate is transformed by some rules. The type of curve that results depends
		 * on the first iteration, the number of duplicates, and the rules of transformation 
		 * used for each duplicate.
		 * 
		 * The index generator shows a principle of using a counter to generate recursive-type
		 * sequences; the implementation is more interesting than the actual usefulness of the 
		 * generator. It uses a list of counts for each index, and won't generate an index more
		 * than three times. Each time the count reaches three, the next index not generated three times
		 * is generated, and if no such index exists, a new index is generated for the first time. The counter
		 * is reset to 0 after it hits 3.
		 * 
		 * The sequence starts 
		 * 
		 * 0 0 0 
		 * 
		 * Now 0 has been generated 3 times. There is no index not generated 3 times, so we generate 1
		 * for the first time. The 0-index counter is set to 0, so the sequence continues
		 * 
		 * 1 0 0 0 
		 * 
		 * Now 0 has been generated 3 times again. The next index (1) has only been generated 1, so we 
		 * can generate it again. The sequence continues:
		 * 
		 * 1 0 0 0 1 0 0 0
		 * 
		 * Now 0 has been generated 3 times after the last 0-index counter reset. The next index (1) has also
		 * been generated 3 times. So there is no index not generated 3 times (since the last respective counter resets),
		 * so we generate 2 for the first time. Both the 1 and 0 index counters are reset to 0. So the sequence continues
		 * 
		 * 2 0 0 0 1 0 0 0 1 0 0 0 1 0 0 0 
		 * 2 0 0 0 1 0 0 0 1 0 0 0 1 0 0 0 
		 * 2 0 0 0 1 0 0 0 1 0 0 0 1 0 0 0 
		 * 
		 * Now 2 has been generated 3 times, no other index has been generated 3 times since last counter rests, so
		 * we generate 3 for the first time, and reset all counters.
		 * 
		 * The sequence continues 
		 * 
		 * 3 0 0 0 1 0 0 0 1 0 0 0 1 0 0 0 
		 * 2 0 0 0 1 0 0 0 1 0 0 0 1 0 0 0 
		 * 2 0 0 0 1 0 0 0 1 0 0 0 1 0 0 0 
		 * 2 0 0 0 1 0 0 0 1 0 0 0 1 0 0 0 
		 * 3 0 0 0 ...
		 * 
		 * And so on.
		 */

		private class HilbertIndexGenerator : Generator.AbstractGenerator<int>
		{
			#region Private Fields

			private int index;
			private int current;
			private List<int> count;

			#endregion

			#region Public Properties

			public override int Current
			{
				get { return current; }
			}

			#endregion

			#region Constructors

			public HilbertIndexGenerator()
			{
				count = new List<int> {0};
				MoveNext();
			}

			#endregion

			#region Public Methods

			public override IGenerator<int> CloneAndRestart()
			{
				return new HilbertIndexGenerator();
			}

			public override void MoveNext()
			{
				while (count[index] >= 3)
				{
					count[index] = 0;
					index++;

					Assert.IsTrue(index <= count.Count);

					if (index == count.Count)
					{
						count.Add(0);
					}
				}

				count[index]++;
				current = index; 
				index = 0;
			}

			#endregion
		}

		private class HilbertGenerator : Generator.AbstractGenerator<GridPoint2>
		{
			#region Private Fields

			private int depth;
			private int width;
			private int index;
			private List<GridPoint2> points;
			private GridPoint2 current;

			#endregion

			#region Public Properties

			public override GridPoint2 Current
			{
				get { return current; }
			}

			#endregion

			#region Constructors

			public HilbertGenerator()
			{
				points = new List<GridPoint2>
				{
					GridPoint2.Zero,
					RectPoint.East,
					RectPoint.NorthEast,
					RectPoint.North
				};

				index = 0;

				current = points[0];
				depth = 1;
				width = 1;

			}

			#endregion

			#region Public Methods

			public override void MoveNext()
			{
				index++;

				if (index >= points.Count)
				{
					points = Expand(points);

					width = width * 4 + 3;
					depth = (width + 1)/2;
				}

				current = points[index];
			}

			public override IGenerator<GridPoint2> CloneAndRestart()
			{
				return new HilbertGenerator();
			}

			#endregion

			#region Private Members

			private IEnumerable<GridPoint2> Rotate1(IEnumerable<GridPoint2> points)
			{
				return points.Select(p => Rotate1(p));
			}

			private IEnumerable<GridPoint2> Rotate2(IEnumerable<GridPoint2> points)
			{
				return points.Select(p => Rotate2(p));
			}

			private IEnumerable<GridPoint2> Rotate3(IEnumerable<GridPoint2> points)
			{
				return points.Select(p => Rotate3(p));
			}

			private GridPoint2 Rotate1(GridPoint2 point)
			{
				var offset = new GridPoint2(width, width);

				return (RectPoint.Rotate90(2 * point - offset) + offset) / 2;
			}

			private GridPoint2 Rotate2(GridPoint2 point)
			{
				var offset = new GridPoint2(width, width);

				return (RectPoint.Rotate180(2 * point - offset) + offset) / 2;
			}

			private GridPoint2 Rotate3(GridPoint2 point)
			{
				var offset = new GridPoint2(width, width);

				return (RectPoint.Rotate270(2 * point - offset) + offset) / 2;
			}

			private IEnumerable<GridPoint2> Offset(IEnumerable<GridPoint2> points, GridPoint2 offset)
			{
				return points.Select(p => p + offset);
			}

			private List<GridPoint2> Expand(List<GridPoint2> points)
			{
				var newPoints = new List<GridPoint2>();

				newPoints.AddRange(points);
				newPoints.AddRange(Transform(points, 1, new GridPoint2(0, 2) * depth, true));
				newPoints.AddRange(Transform(points, 1, new GridPoint2(2, 2) * depth, true));
				newPoints.AddRange(Transform(points, 2, new GridPoint2(2, 0) * depth, false));

				newPoints.AddRange(Transform(points, 1, new GridPoint2(4, 0) * depth, true));
				newPoints.AddRange(Transform(points, 0, new GridPoint2(6, 0) * depth, false));
				newPoints.AddRange(Transform(points, 0, new GridPoint2(6, 2) * depth, false));
				newPoints.AddRange(Transform(points, 3, new GridPoint2(4, 2) * depth, true));

				newPoints.AddRange(Transform(points, 1, new GridPoint2(4, 4) * depth, true));
				newPoints.AddRange(Transform(points, 0, new GridPoint2(6, 4) * depth, false));
				newPoints.AddRange(Transform(points, 0, new GridPoint2(6, 6) * depth, false));
				newPoints.AddRange(Transform(points, 3, new GridPoint2(4, 6) * depth, true));

				newPoints.AddRange(Transform(points, 2, new GridPoint2(2, 6) * depth, false));
				newPoints.AddRange(Transform(points, 3, new GridPoint2(2, 4) * depth, true));
				newPoints.AddRange(Transform(points, 3, new GridPoint2(0, 4) * depth, true));
				newPoints.AddRange(Transform(points, 0, new GridPoint2(0, 6) * depth, false));

				return newPoints;
			}

			private IEnumerable<GridPoint2> Transform(IEnumerable<GridPoint2> points, int rotate, GridPoint2 offset,
				bool reverse)
			{
				IEnumerable<GridPoint2> newPoints;

				switch (rotate)
				{
					case 0:
						newPoints = points;
						break;

					case 1:
						newPoints = Rotate1(points);
						break;

					case 2:
						newPoints = Rotate2(points);
						break;

					case 3:
					default:
						newPoints = Rotate3(points);
						break;
				}

				newPoints = Offset(newPoints, offset);

				if (reverse)
				{
					return newPoints.Reverse();
				}

				return newPoints;
			}

			#endregion
		}

		private class PeanoGenerator : Generator.AbstractGenerator<GridPoint2>
		{
			#region Private Fields

			private int depth;
			private int center;
			private int index;
			private List<GridPoint2> points;
			private GridPoint2 current;

			#endregion

			#region Public Properties

			public override GridPoint2 Current
			{
				get { return current; }
			}

			#endregion

			#region Constructors

			public PeanoGenerator()
			{
				points = new List<GridPoint2>
				{
					new GridPoint2(0, 0),
					new GridPoint2(0, 1),
					new GridPoint2(0, 2),

					new GridPoint2(1, 2),
					new GridPoint2(1, 1),
					new GridPoint2(1, 0),

					new GridPoint2(2, 0),
					new GridPoint2(2, 1),
					new GridPoint2(2, 2),
				};

				index = 0;

				current = points[0];
				depth = 3;
				center = 1;
			}

			#endregion

			#region Public Methods

			public override void MoveNext()
			{
				index++;

				if (index >= points.Count)
				{
					points = Expand(points);

					center = 3 * center + 1;
					depth = depth * 3;
				}

				current = points[index];
			}

			public override IGenerator<GridPoint2> CloneAndRestart()
			{
				return new PeanoGenerator();
			}

			#endregion

			#region Private Members

			private IEnumerable<GridPoint2> Reflect(IEnumerable<GridPoint2> points)
			{
				return points.Select(p => Reflect(p));
			}

			private GridPoint2 Reflect(GridPoint2 point)
			{
				var offset = new GridPoint2(center, center);

				return RectPoint.ReflectAboutX(point - offset) + offset;
			}


			private IEnumerable<GridPoint2> Offset(IEnumerable<GridPoint2> points, GridPoint2 offset)
			{
				return points.Select(p => p + offset);
			}

			private List<GridPoint2> Expand(List<GridPoint2> points)
			{
				var newPoints = new List<GridPoint2>();

				newPoints.AddRange(points);
				newPoints.AddRange(Transform(points, true, new GridPoint2(0, 1) * depth, true));
				newPoints.AddRange(Transform(points, false, new GridPoint2(0, 2) * depth, false));

				newPoints.AddRange(Transform(points, true, new GridPoint2(1, 2) * depth, false));
				newPoints.AddRange(Transform(points, false, new GridPoint2(1, 1) * depth, true));
				newPoints.AddRange(Transform(points, true, new GridPoint2(1, 0) * depth, false));

				newPoints.AddRange(Transform(points, false, new GridPoint2(2, 0) * depth, false));
				newPoints.AddRange(Transform(points, true, new GridPoint2(2, 1) * depth, true));
				newPoints.AddRange(Transform(points, false, new GridPoint2(2, 2) * depth, false));

				return newPoints;
			}

			private IEnumerable<GridPoint2> Transform(
				IEnumerable<GridPoint2> points,
				bool reflect,
				GridPoint2 offset,
				bool reverse)
			{
				IEnumerable<GridPoint2> newPoints;

				if (reflect)
				{
					newPoints = Reflect(points);
				}
				else
				{
					newPoints = points;
				}

				newPoints = Offset(newPoints, offset);

				if (reverse)
				{
					return newPoints.Reverse();
				}

				return newPoints;
			}

			#endregion
		}

		private class GosperGenerator : Generator.AbstractGenerator<GridPoint2>
		{
			#region Private Fields

			private int depth;
			private int index;

			private List<GridPoint2> points = new List<GridPoint2>
			{
				new GridPoint2(0, 0),
				new GridPoint2(1, 0),
				new GridPoint2(0, 1),
				new GridPoint2(-1, 1),
				new GridPoint2(-1, 2),
				new GridPoint2(0, 2),
				new GridPoint2(1, 1),
			};

			private List<GridPoint2> offsets = new List<GridPoint2>
			{
				new GridPoint2(0, 0),
				new GridPoint2(1, 0),
				new GridPoint2(0, 1),
				new GridPoint2(-1, 1),
				new GridPoint2(-1, 2),
				new GridPoint2(0, 2),
				new GridPoint2(1, 1),
			};

			private List<bool> reverse = new List<bool>
			{
				false,
				true,
				true,
				false,
				false,
				false,
				true
			};

			private List<int> rotationIndex = new List<int>
			{
				0,
				4,
				0,
				2,
				0,
				0,
				2
			};

			GridPoint2 current;

			Matrixi22 rotation = new Matrixi22(2, 1, -1, 3);

			#endregion

			#region Public Properties

			public override GridPoint2 Current
			{
				get { return current; }
			}

			#endregion

			#region Constructors

			public GosperGenerator()
			{
				index = 0;
				current = points[0];
				depth = 0;
			}

			#endregion

			#region Public Methods

			public override void MoveNext()
			{
				index++;

				if (index >= points.Count)
				{
					depth++;
					points = Expand(points);
				}

				current = points[index];
			}

			public override IGenerator<GridPoint2> CloneAndRestart()
			{
				return new GosperGenerator();
			}

			#endregion

			#region Private Members

			private IEnumerable<GridPoint2> Transform(IEnumerable<GridPoint2> points, GridPoint2 offset, int rotationIndex,
				bool reverse)
			{
				var list = new List<GridPoint2>();
				var newOffset = offset;

				for (var i = 0; i < depth; i++)
				{
					newOffset = newOffset.Mul(rotation);
				}

				var center = GridPoint2.Zero;

				for (var i = 0; i < depth; i++)
				{
					center = center.Mul(rotation);
					center += new GridPoint2(0, 1);
				}

				foreach (var point in points)
				{
					var newPoint = point - center;

					for (var i = 0; i < rotationIndex; i++)
					{
						newPoint = PointyHexPoint.Rotate60(newPoint);
					}

					newPoint += newOffset + center;

					list.Add(newPoint);
				}

				if (reverse)
				{
					list.Reverse();
				}

				return list;
			}

			private List<GridPoint2> Expand(List<GridPoint2> points)
			{
				var newPoints = new List<GridPoint2>();

				newPoints.AddRange(points); //this corresponds to i = 0

				for (var i = 1; i < 7; i++)
				{
					newPoints.AddRange(Transform(points, offsets[i], rotationIndex[i], reverse[i]));
				}

				return newPoints;
			}

			#endregion
		}

		#endregion

		#region Public methods		
		/// <summary>
		/// Returns a new generator that generates the sequence
		/// 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 1.
		/// </summary>
		public static IGenerator<int> HilbertIndex()
		{
			return new HilbertIndexGenerator();
		}

		/// <summary>
		/// Returns a new generator that returns points of a Hilbert curve approximation.
		/// </summary>
		/// <returns>IGenerator&lt;GridPoint2&gt;.</returns>
		public static IGenerator<GridPoint2> Hilbert()
		{
			return new HilbertGenerator();
		}

		/// <summary>
		/// Returns a new generator that returns points of a Peano curve approximation.
		/// </summary>
		/// <returns>IGenerator&lt;GridPoint2&gt;.</returns>
		public static IGenerator<GridPoint2> Peano()
		{
			return new PeanoGenerator();
		}

		/// <summary>
		/// Returns a new generator that returns points of a Gosper curve approximation.
		/// </summary>
		/// <returns>IGenerator&lt;GridPoint2&gt;.</returns>
		public static IGenerator<GridPoint2> Gosper()
		{
			return new GosperGenerator();
		}
		#endregion
	}
}