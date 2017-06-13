using UnityEditor;
using UnityEngine;

namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// Class for drawing a field of type <see cref="SpaceMapGraph"/> in the inspector.
	/// </summary>
	/// <seealso cref="GraphPropertyDrawer" />
	[CustomPropertyDrawer(typeof(SpaceMapGraph), true)]
	public class SpaceMapGraphPropertyDrawer : GraphPropertyDrawer
	{
		public override void ShowEditor(Object objectReferenceValue)
		{
			SpaceMapGraphWindow.ShowEditor((SpaceMapGraph)objectReferenceValue);
		}
	}
}