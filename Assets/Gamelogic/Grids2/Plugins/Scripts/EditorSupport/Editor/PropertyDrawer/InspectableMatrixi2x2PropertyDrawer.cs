using UnityEditor;
using UnityEngine;

namespace Gamelogic.Grids2.Editor
{
	/// <summary>
	/// Class for drawing a <see cref="InspectableMatrixi2x2"/> in the inspector.
	/// </summary>
	[CustomPropertyDrawer(typeof(InspectableMatrixi2x2))]
	public class InspectableMatrixi2x2PropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			EditorGUIUtility.labelWidth = 15;
			EditorGUIUtility.fieldWidth = position.width / 2 - 15;

			//position.height = 16;
			var rectWidth = position.width / 2;
			var rectHeight = position.height / 2;

			var aRect = new Rect(position.x, position.y, rectWidth - 2, rectHeight);
			var bRect = new Rect(position.x + rectWidth, position.y, rectWidth - 2, rectHeight);

			var cRect = new Rect(position.x, position.y + rectHeight, rectWidth - 2, rectHeight);
			var dRect = new Rect(position.x + rectWidth, position.y + rectHeight, rectWidth - 2, rectHeight);

			EditorGUI.PropertyField(aRect, property.FindPropertyRelative("a"));
			EditorGUI.PropertyField(bRect, property.FindPropertyRelative("b"));

			EditorGUI.PropertyField(cRect, property.FindPropertyRelative("c"));
			EditorGUI.PropertyField(dRect, property.FindPropertyRelative("d"));

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property, label) * 2;
		}
	}
}