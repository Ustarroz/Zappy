using System;
using Gamelogic.Extensions;

namespace Gamelogic.Grids2.Examples
{
	public class TriangleCell : GLMonoBehaviour
	{
		public event Action OnClick;

		public void OnMouseDown()
		{
			if (OnClick != null)
			{
				OnClick();
			}
		}
	}
}