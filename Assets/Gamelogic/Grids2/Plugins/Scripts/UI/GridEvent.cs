using System;
using UnityEngine.Events;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Use for displaying events of the GridEventTrigger in the inspector.
	/// </summary>
	[Serializable]
	public class GridEvent : UnityEvent<GridPoint2>
	{
	}
}