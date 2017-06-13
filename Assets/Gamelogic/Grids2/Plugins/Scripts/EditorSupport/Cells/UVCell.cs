using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Similar to a sprite cell, but with custom UV coordinates.
	/// This type of cell is useful when placing a single texture
	/// across multiple cells.
	/// </summary>
	public class UVCell : TileCell
	{
		//[SerializeField] public MapPlane plane = MapPlane.XY;

		[SerializeField]
		private Color color;

		[SerializeField]
		private Texture2D texture;

		[SerializeField]
		private Vector2 textureScale;

		[SerializeField]
		private Vector2 textureOffset;

		[SerializeField]
		[HideInInspector]
		private Material material;

		public Color Color
		{
			get { return color; }

			set
			{
				color = value;
				UpdatePresentation();
			}
		}

		public Material Material
		{
			get { return material; }
		}

		//public Vector2 Dimensions
		//{
		//	get
		//	{
		//		switch (plane)
		//		{
		//			case MapPlane.XY:
		//			default:
		//				return GetComponent<MeshFilter>().sharedMesh.bounds.size.To2DXY();
		//			case MapPlane.XZ:
		//				return GetComponent<MeshFilter>().sharedMesh.bounds.size.To2DXZ();
		//		}
		//	}
		//}

		public void SetTexture(Texture2D texture)
		{
			this.texture = texture;
			UpdatePresentation();
		}

		public void SetUVs(Vector2 offset, Vector2 scale)
		{
			textureOffset = offset;
			textureScale = scale;
			UpdatePresentation();
		}

		private void UpdatePresentation()
		{
			if (material == null)
			{
				material = new Material(GetComponent<Renderer>().sharedMaterial); //only duplicate once
			}

			material.color = color;
			material.mainTexture = texture;
			material.mainTextureOffset = textureOffset;
			material.mainTextureScale = textureScale;

			GetComponent<Renderer>().material = material;
		}

		public void OnDestroy()
		{
			DestroyImmediate(material);
		}
	}
}