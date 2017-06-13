using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;

namespace Gamelogic.Grids2.Experimental
{
	/// <summary>
	/// A grid behaviour that can be used with a mesh grid builder to set vertex colors based on a color function.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.GridBehaviour{Gamelogic.Grids2.GridPoint2, Gamelogic.Grids2.MeshCell}" />
	public sealed class VertexColors : GridBehaviour<GridPoint2, MeshCell>
	{
		#region Inspector Fields

		[SerializeField]
		private ColorFunction colorFunction = new ColorFunction { x0 = 1, x1 = 1, y1 = 1 };

		[SerializeField]
		private ColorList colors = new ColorList { Color.white };

		#endregion

		#region Public Properties

		public ColorFunction ColorFunction
		{
			get { return colorFunction; }
		}

		public IList<Color> Colors
		{
			get { return colors; }
		}

		#endregion

		#region Public Methods

		public override void InitGrid()
		{
			var meshFilter = GetComponent<MeshFilter>();
			var mesh = meshFilter.sharedMesh;
			var vertexCount = mesh.vertices.Length;
			var gridCount = Grid.Points.Count();
			var vertexColors = new List<Color>();
			int colorCount = vertexCount / gridCount;

			foreach (var point in Grid.Points)
			{
				var colorIndex = point.GetColor(colorFunction.x0, colorFunction.x1, colorFunction.y1);

				for (int i = 0; i < colorCount; i++)
				{
					vertexColors.Add(colors[colorIndex]);
				}
			}

			mesh.colors = vertexColors.ToArray();
		}

		#endregion
	}
}