using UnityEditor;
using UnityEngine;

namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// Class for drawing a field of type <see cref="Shape1Graph"/> in the inspector.
	/// </summary>
	/// <seealso cref="GraphPropertyDrawer" />
	[CustomPropertyDrawer(typeof(Shape1Graph), true)]
	public class Shape1GraphPropertyDrawer : GraphPropertyDrawer
	{
		public override void ShowEditor(Object objectReferenceValue)
		{
			Shape1GraphWindow.ShowEditor((Shape1Graph)objectReferenceValue);
		}
	}
}