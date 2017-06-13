using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// A cell that is rendered as a sprite.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.TileCell" />
	/// <seealso cref="Gamelogic.Grids2.IColorableCell" />
	public sealed class SpriteCell : TileCell, IColorableCell
	{
		private SpriteRenderer spriteRenderer;

		private SpriteRenderer SpriteRenderer
		{
			get
			{
				if (spriteRenderer == null)
				{
					spriteRenderer = GetComponent<SpriteRenderer>();
				}

				return spriteRenderer;
			}
		}

		public Sprite Sprite
		{
			get { return SpriteRenderer.sprite; }
			set { SpriteRenderer.sprite = value; }
		}

		public Color Color
		{
			get { return SpriteRenderer.color; }
			set { SpriteRenderer.color = value; }
		}
	}
}