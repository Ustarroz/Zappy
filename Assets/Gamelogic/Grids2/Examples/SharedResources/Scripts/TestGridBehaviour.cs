using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class TestGridBehaviour : GridBehaviour<GridPoint2, SpriteCell>
	{
		public Color color1;
		public Color color2;
		public int columnSize;

		private IGrid<GridPoint2, bool> cellStates;
		public List<InspectableGridPoint2> neighborDirections;

		public override void InitGrid()
		{
			cellStates = Grid.CloneStructure(false);

			var firstColor = color1;
			var secondColor = color2;
			var counter = 0;

			foreach (var point in Grid.Points)
			{

				if (counter == columnSize)
				{
					var swapColor = firstColor;
					firstColor = secondColor;
					secondColor = swapColor;
					counter = 0;
				}

				UpdateColor(point, counter % 2 == 0 ? firstColor : secondColor);
				counter++;
			}
		}

		private void UpdateColor(GridPoint2 point)
		{
			UpdateColor(point, cellStates[point] ? color1 : color2);
		}

		private void UpdateColor(GridPoint2 point, Color color)
		{
			Grid[point].GetComponent<SpriteRenderer>().color = color;
		}

		public void OnClick(GridPoint2 point)
		{
			var neighbors = point
				.GetVectorNeighbors(neighborDirections.Select(p => p.GetGridPoint()))
				.In(cellStates);

			foreach (var neighbor in neighbors)
			{
				cellStates[neighbor] = !cellStates[neighbor];
				UpdateColor(neighbor);
			}
		}
	}
}