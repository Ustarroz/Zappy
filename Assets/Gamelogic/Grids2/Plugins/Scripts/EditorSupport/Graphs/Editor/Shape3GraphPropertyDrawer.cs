using UnityEditor;
using UnityEngine;

namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// Class for drawing a field of type <see cref="Shape3Graph"/> in the inspector.
	/// </summary>
	/// <seealso cref="GraphPropertyDrawer" />
	[CustomPropertyDrawer(typeof(Shape3Graph), true)]
	public class Shape3GraphPropertyDrawer : GraphPropertyDrawer
	{
		public override void ShowEditor(Object objectReferenceValue)
		{
			Shape3GraphWindow.ShowEditor((Shape3Graph)objectReferenceValue);
		}
	}
}