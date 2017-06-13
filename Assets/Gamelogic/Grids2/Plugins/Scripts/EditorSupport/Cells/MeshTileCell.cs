using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Class MeshTileCell. This class cannot be inherited.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.TileCell" />
	/// <seealso cref="Gamelogic.Grids2.IColorableCell" />
	[AddComponentMenu("Gamelogic/Grids 2/Cells/Mesh Tile Cell")]
	public sealed class MeshTileCell : TileCell, IColorableCell
	{
		private MeshRenderer meshRenderer;
		private MeshFilter meshFilter;

		private MeshRenderer MeshRenderer
		{
			get
			{
				if (meshRenderer == null)
				{
					meshRenderer = this.GetRequiredComponent<MeshRenderer>();
				}

				return meshRenderer;
			}
		}

		private MeshFilter MeshFilter
		{
			get
			{
				if (meshFilter == null)
				{
					meshFilter = this.GetRequiredComponent<MeshFilter>();
				}

				return meshFilter;
			}
		}

		public Material SharedMaterial
		{
			get { return MeshRenderer.sharedMaterial; }
			set { MeshRenderer.sharedMaterial = value; }
		}

		public Mesh SharedMesh
		{
			get { return MeshFilter.sharedMesh; }
			set { MeshFilter.sharedMesh = value; }
		}

		public Color Color
		{
			get { return SharedMaterial.color; }
			set
			{
				var material = new Material(MeshRenderer.sharedMaterial) { color = value };
				MeshRenderer.sharedMaterial = material;
			}
		}
	}
}