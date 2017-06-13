//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids
{
	/// <summary>
	/// A cell represented by a mesh, such as used by the polar grids.
	/// </summary>
	[Version(1,8)]
	[AddComponentMenu("Gamelogic/Cells/MeshTileCell")]
	public class MeshTileCell : TileCell
	{
		private bool on;

		[SerializeField]
		private Color color;
		
		[SerializeField]
		private Color highlightColor;

		public override Color Color
		{
			get { return color; }

			set
			{
				color = value;
				highlightColor = Color.Lerp(value, Color.white, 0.5f);

				__UpdatePresentation();
			}
		}

		public override Vector2 Dimensions
		{
			get { return GetComponent<MeshFilter>().sharedMesh.bounds.size.To2DXY(); }
		}

		public override void __UpdatePresentation(bool forceUpdate)
		{
			if (forceUpdate) __UpdatePresentation();
		}

		public override void SetAngle(float angle)
		{
			transform.RotateAroundZ(angle);
		}

		public override void AddAngle(float angle)
		{
			transform.RotateAroundZ(transform.localEulerAngles.z + angle);
		}

		private void __UpdatePresentation()
		{
			var mesh = GetComponent<MeshFilter>().sharedMesh;
			var colors = new Color[mesh.vertexCount];

			for (int i = 0; i < colors.Length; i++)
			{
				colors[i] = HighlightOn ? highlightColor : color;
			}

			mesh.colors = colors;
		}		

		public bool HighlightOn
		{
			get { return on; }

			set
			{
				on = value;

				__UpdatePresentation();
			}
		}
	}
}