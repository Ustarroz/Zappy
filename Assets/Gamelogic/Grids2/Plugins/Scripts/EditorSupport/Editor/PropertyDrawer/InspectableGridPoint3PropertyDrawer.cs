using UnityEditor;
using UnityEngine;

namespace Gamelogic.Grids2.Editor
{
	/// <summary>
	/// Class for drawing a <see cref="InspectableGridPoint3"/> in the inspector.
	/// </summary>
	[CustomPropertyDrawer(typeof(InspectableGridPoint3))]
	public class InspectableGridPoint3PropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			EditorGUIUtility.labelWidth = 15;
			EditorGUIUtility.fieldWidth = position.width / 2 - 15;

			position.height = 16;
			var rectWidth = position.width / 3;
			var xRect = new Rect(position.x, position.y, rectWidth - 2, position.height);
			var yRect = new Rect(position.x + rectWidth, position.y, rectWidth - 2, position.height);
			var zRect = new Rect(position.x + rectWidth * 2, position.y, rectWidth - 2, position.height);

			EditorGUI.PropertyField(xRect, property.FindPropertyRelative("x"));
			EditorGUI.PropertyField(yRect, property.FindPropertyRelative("y"));
			EditorGUI.PropertyField(zRect, property.FindPropertyRelative("z"));

			EditorGUI.EndProperty();
		}
	}
}