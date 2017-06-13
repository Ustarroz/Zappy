using UnityEditor;
using UnityEngine;

namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// Class for drawing a field of type <see cref="Shape2Graph"/> in the inspector.
	/// </summary>
	/// <seealso cref="GraphPropertyDrawer" />
	[CustomPropertyDrawer(typeof(Shape2Graph), true)]
	public class Shape2GraphPropertyDrawer : GraphPropertyDrawer
	{
		public override void ShowEditor(Object objectReferenceValue)
		{
			Shape2GraphWindow.ShowEditor((Shape2Graph)objectReferenceValue);
		}
	}
}