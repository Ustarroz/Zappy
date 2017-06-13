using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class PrimsAlgorithmTest : GridBehaviour<GridPoint2, TileCell>
	{
		public override void InitGrid()
		{
			var walls = PrimsAlgorithm.GenerateMazeWalls(Grid);

			foreach (var wall in walls)
			{
				Grid[wall].GetComponent<SpriteCell>().Color = Color.black;
			}
		}
	}
}