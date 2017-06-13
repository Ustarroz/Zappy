using UnityEditor;
using UnityEngine;

namespace Gamelogic.Grids2.Editor
{
	/// <summary>
	/// Class for drawing a <see cref="InspectableGridPoint2"/> in the inspector.
	/// </summary>
	[CustomPropertyDrawer(typeof(InspectableGridPoint2))]
	public class InspectableGridPoint2PropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			EditorGUIUtility.labelWidth = 15;
			EditorGUIUtility.fieldWidth = position.width / 2 - 15;

			var halfWidth = position.width / 2;
			var xRect = new Rect(position.x, position.y, halfWidth - 2, position.height);
			var yRect = new Rect(position.x + halfWidth, position.y, halfWidth - 2, position.height);

			EditorGUI.PropertyField(xRect, property.FindPropertyRelative("x"));
			EditorGUI.PropertyField(yRect, property.FindPropertyRelative("y"));

			EditorGUI.EndProperty();
		}
	}
}