using UnityEditor;
using UnityEngine;

namespace Gamelogic.Grids2.Editor
{
	/// <summary>
	/// Class for drawing a <see cref="InspectableMatrixf3x3"/> in the inspector.
	/// </summary>
	[CustomPropertyDrawer(typeof(InspectableMatrixi3x3))]
	public class InspectableMatrixi3x3PropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			EditorGUIUtility.labelWidth = 15;
			EditorGUIUtility.fieldWidth = position.width / 2 - 15;

			//position.height = 16;
			var rectWidth = position.width / 3;
			var rectHeight = position.height / 3;

			var aRect = new Rect(position.x, position.y, rectWidth - 2, rectHeight);
			var bRect = new Rect(position.x + rectWidth, position.y, rectWidth - 2, rectHeight);
			var cRect = new Rect(position.x + rectWidth * 2, position.y, rectWidth - 2, rectHeight);

			var dRect = new Rect(position.x, position.y + rectHeight, rectWidth - 2, rectHeight);
			var eRect = new Rect(position.x + rectWidth, position.y + rectHeight, rectWidth - 2, rectHeight);
			var fRect = new Rect(position.x + rectWidth * 2, position.y + rectHeight, rectWidth - 2, rectHeight);

			var gRect = new Rect(position.x, position.y + 2 * rectHeight, rectWidth - 2, rectHeight);
			var hRect = new Rect(position.x + rectWidth, position.y + 2 * rectHeight, rectWidth - 2, rectHeight);
			var iRect = new Rect(position.x + rectWidth * 2, position.y + 2 * rectHeight, rectWidth - 2, rectHeight);

			EditorGUI.PropertyField(aRect, property.FindPropertyRelative("a"));
			EditorGUI.PropertyField(bRect, property.FindPropertyRelative("b"));
			EditorGUI.PropertyField(cRect, property.FindPropertyRelative("c"));

			EditorGUI.PropertyField(dRect, property.FindPropertyRelative("d"));
			EditorGUI.PropertyField(eRect, property.FindPropertyRelative("e"));
			EditorGUI.PropertyField(fRect, property.FindPropertyRelative("f"));

			EditorGUI.PropertyField(gRect, property.FindPropertyRelative("g"));
			EditorGUI.PropertyField(hRect, property.FindPropertyRelative("h"));
			EditorGUI.PropertyField(iRect, property.FindPropertyRelative("i"));

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property, label) * 3;
		}
	}
}