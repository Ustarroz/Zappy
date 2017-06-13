//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) Gamelogic (Pty) Ltd            //
//----------------------------------------------//

using System;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Similar to a sprite cell, but with custom UV coordinates.
	/// This type of cell is useful when placing a single texture
	/// across multiple cells.
	/// </summary>
	[Version(1,8)]
	[AddComponentMenu("Gamelogic/Cells/TextureCell")]
	[Obsolete("Use UVCell instead")]
	public class TextureCell : TileCell, IGLScriptableObject
	{
		[SerializeField] private Color color;

		[SerializeField] private bool highlightOn;

		public bool HighlightOn
		{
			get { return highlightOn; }
			set
			{
				highlightOn = value;
				__UpdatePresentation(true);
			}
		}

		public override Color Color
		{
			get { return color; }

			set
			{
				color = value;
				__UpdatePresentation(true);
			}
		}

		public Renderer Renderer
		{
			get
			{
				var textureRenderer = transform.FindChild("Texture").GetComponent<Renderer>();

				if (textureRenderer == null)
				{
					Debug.LogError("The cell needs a child with a Renderer component attached");
				}

				return textureRenderer;
			}
		}

		public override Vector2 Dimensions
		{
			get { return Renderer.transform.localScale; }
		}

		public void Awake()
		{
			highlightOn = false;
		}

		public override void __UpdatePresentation(bool forceUpdate)
		{
			Renderer.material.color = highlightOn ? Color.Lerp(color, Color.white, 0.8f) : color;
		}

		public override void SetAngle(float angle)
		{
			Renderer.transform.SetLocalRotationZ(angle);
		}

		public override void AddAngle(float angle)
		{
			Renderer.transform.RotateAroundZ(angle);
		}

		public void OnClick()
		{
			highlightOn = !highlightOn;
			__UpdatePresentation(true);
		}
	}
}
