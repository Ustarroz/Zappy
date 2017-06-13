using Gamelogic.Extensions.Editor.Internal;
using UnityEditor;
using UnityEngine;

namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// Shows a read-only input and output rectangle of the colors
	/// that represent the types of parameters that the node
	/// use for input and output.
	/// </summary>
	[CustomPropertyDrawer(typeof(ColorIndicator))]
	public class ColorIndicatorPropertyDrawer : PropertyDrawer
	{
		private readonly Color[] colorList = new Color[2];

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var inputColor = property.FindPropertyRelative("inputColor").colorValue;
			var outputColor = property.FindPropertyRelative("outputColor").colorValue;

			EditorGUI.BeginProperty(position, label, property);

			colorList[0] = inputColor;
			colorList[1] = outputColor;

			EditorUtils.DrawColors(colorList, position);

			EditorGUI.EndProperty();
		}
	}
}