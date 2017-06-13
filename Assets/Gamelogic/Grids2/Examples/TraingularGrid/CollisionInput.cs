using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class CollisionInput : GridBehaviour<GridPoint2, TileCell>
	{
		public override void InitGrid()
		{
			foreach (var point in Grid.Points)
			{
				var cell = Grid[point];
				var pointCopy = point;
				cell.GetComponent<TriangleCell>().OnClick += () => { OnClick(pointCopy); };
			}
		}

		public void OnClick(GridPoint2 point)
		{
			Debug.Log(point + " clicked.");
		}
	}
}