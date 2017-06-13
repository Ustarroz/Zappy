using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class AnimationMapTest : GridBehaviour<GridPoint2, TileCell>
	{
		private GridMap<GridPoint2> map;

		public class AnimationMap : IMap<Vector3, Vector3>
		{
			private readonly float frequency;
			private readonly float amplitude;

			public AnimationMap(float frequency, float amplitude)
			{
				this.frequency = frequency;
				this.amplitude = amplitude;
			}

			public Vector3 Forward(Vector3 input)
			{
				return input*ScaleFactor();
			}

			public Vector3 Reverse(Vector3 output)
			{
				return output/ScaleFactor();
			}

			public float ScaleFactor()
			{
				return (2 + Mathf.Sin(frequency * Time.time)) * amplitude / 2;
			}
		}

		public override void InitGrid()
		{
			MakeMap();
			UpdateCellPositions();
		}

		private void MakeMap()
		{
			var spaceMap = GridMap.SpaceMap.ComposeWith(new AnimationMap(1, 2));
			var roundMap = GridMap.RoundMap;

			map = new GridMap<GridPoint2>(spaceMap, roundMap);
		}

		public void Update()
		{
			if (map == null) return;

			UpdateCellPositions();
		}

		private void UpdateCellPositions()
		{
			foreach (var pair in Grid)
			{
				var cellPosition = map.GridToWorld(pair.point);
				pair.cell.transform.localPosition = cellPosition;
			}
		}
	}
}