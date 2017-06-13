using System;
using UnityEngine;

namespace Gamelogic.Grids2.Examples.Algorithms.PathFinding
{
	public class WalkableCell : TileCell
	{
		/// <summary>
		/// The minimum movement cost of any cell.
		/// </summary>
		public const float MinCost = 1.0f;

		[SerializeField]
		private Color color;

		[SerializeField]
		private bool highlightOn = false;

		private float movementCost;

		public float MovementCost
		{
			get { return movementCost; }

			set
			{
				if (movementCost > MinCost)
				{
					throw new ArgumentOutOfRangeException("Movement cost cannot be less than " + MinCost);
				}

				movementCost = value;
			}
		}

		private SpriteRenderer spriteRenderer;
		protected SpriteRenderer SpriteRenderer
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

		/// <summary>
		/// Whether this cell can be traversed or not.
		/// </summary>
		public bool IsWalkable { get; set; }

		public Color Color
		{
			get { return color; }

			set
			{
				color = value;
				UpdatePresentation();
			}
		}

		private void UpdatePresentation()
		{
			SpriteRenderer.color = highlightOn ? Color.Lerp(color, Color.white, 0.8f) : color;
		}
	}
}