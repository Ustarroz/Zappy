using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class PolyominoCell : GLMonoBehaviour
	{
		public SpriteRenderer highlightSprite;
		public SpriteRenderer fillSprite;

		private bool highlight;
		private bool filled;

		public Color FillColor
		{
			set { fillSprite.color = value; }
		}

		public bool Highlight
		{
			get { return highlight; }

			set
			{
				highlight = value;

				highlightSprite.gameObject.SetActive(highlight);
			}
		}

		public bool Filled
		{
			get { return filled; }

			set
			{
				filled = value;

				fillSprite.gameObject.SetActive(filled);
			}
		}

		public void Init()
		{
			Filled = false;
			Highlight = false;
		}
	}
}