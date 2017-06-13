//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	[RequireComponent(typeof(SpriteCell))]
	public class LinesCell : GLMonoBehaviour
	{
		private bool highlightOn;

		public bool IsEmpty { get; private set; }

		public int Type { get; private set; }

		public void SetState(bool isEmpty, int type, bool highlight)
		{
			IsEmpty = isEmpty;
			Type = isEmpty ? -1 : type;

			Color = (isEmpty ? UnityEngine.Color.white : Lines.GetColor(type));
			HighlightOn = highlight;
		}

		public Color Color
		{
			get { return GetComponent<SpriteCell>().Color; }
			set { GetComponent<SpriteCell>().Color = value; }
		}

		public bool HighlightOn
		{
			get { return highlightOn; }

			set
			{
				highlightOn = value;
				transform.GetChild(0).gameObject.SetActive(value);
			}
		}



	}
}