using UnityEngine;

namespace Gamelogic.Grids2.Experimental
{
	public class TriangleGrid : GridBehaviour<GridPoint2, TileCell>
	{
		public override void InitGrid()
		{
			foreach (var pointPair in Grid)
			{
				int colorIndex = pointPair.point.GetColor(3, 1, 1);

				switch (colorIndex)
				{
					case 1:
						pointPair.cell.transform.rotation = Quaternion.Euler(0, 0, 180);
						break;
					case 0:
						pointPair.cell.GetComponent<Renderer>().enabled = false;
						break;
				}
			}
		}
	}
}