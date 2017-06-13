using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A <see cref="FuncSpaceMapNode"/> that adds a height map to the input Z.
	/// </summary>
	/// <seealso cref="FuncSpaceMapNode" />
	[SpaceMapNode("Misc/Height Map", 3)]
	public class HeightSpaceMapNode : ProjectSpaceMapNode<Vector3, Vector3>
	{
		public Texture2D heightMap;
		public float heightScale;
		public InspectableGridPoint2 offSet;
		public bool randomOffset;

		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			var heightMapData = randomOffset ?
				GridUtils.ImageToGreyScaleGridWithRandomRange(heightMap) :
				GridUtils.ImageToGreyScaleGrid(heightMap, offSet.x, offSet.y);

			var roundMap = Map.ParallelogramWrapXY(heightMap.width, heightMap.height)
				.ComposeWith(Map.RectFloor());

			var map = Map.Height(heightMapData, heightScale, roundMap);

			return input == null ? map : input.ComposeWith(map);
		}
	}
}