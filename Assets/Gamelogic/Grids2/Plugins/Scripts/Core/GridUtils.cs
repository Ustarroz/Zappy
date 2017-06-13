using UnityEngine;
using System.Linq;
using Gamelogic.Extensions;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Class with utility methods for grids.
	/// </summary>
	public static class GridUtils
	{
		public static Grid2<float> ImageToGreyScaleGrid(Texture2D texture, int xOffset, int yOffset)
		{
			var dimensions = new GridPoint2(texture.width, texture.height);
			var storageRect = new GridRect(GridPoint2.Zero, dimensions);
			var implicitShape = ImplicitShape.Parallelogram(dimensions);
			var explicitShape = implicitShape.ToExplicit(storageRect);

			var grid = new Grid2<float>(explicitShape);
			var textureData = texture.GetPixels().Select(c => c.grayscale).ToArray();

			grid.Fill(p => textureData[(p.X + xOffset) % texture.width + (texture.width * ((p.Y + yOffset) % texture.height))]);

			return grid;
		}

		public static Grid2<float> ImageToGreyScaleGrid(Texture2D texture)
		{
			return ImageToGreyScaleGrid(texture, xOffset: 0, yOffset: 0);
		}

		public static Grid2<float> ImageToGreyScaleGridWithRandomRange(Texture2D texture)
		{
			var randomX = GLRandom.Range(0, texture.width);
			var randomY = GLRandom.Range(0, texture.height);

			return ImageToGreyScaleGrid(texture, randomX, randomY);
		}

		public static GridPoint2 MousePositionToGrid(Transform root, Camera uiCamera, GridMap<GridPoint2> gridMap)
		{
			var worldPosition = root.InverseTransformPoint(uiCamera.ScreenToWorldPoint(Input.mousePosition));

			return gridMap.WorldToGridToDiscrete(worldPosition);
		}
	}
}