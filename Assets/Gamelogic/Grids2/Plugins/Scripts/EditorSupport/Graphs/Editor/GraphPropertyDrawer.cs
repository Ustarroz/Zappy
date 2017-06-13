using UnityEngine;
using UnityEditor;

namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// Class for drawing a field of type <see cref="Graph{TNode}"/> in the inspector.
	/// </summary>
	/// <seealso cref="UnityEditor.PropertyDrawer" />
	public class GraphPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			EditorGUIUtility.labelWidth = 15;
			EditorGUIUtility.fieldWidth = position.width / 2 - 15;

			//fixed height so it is not affected for external GetPropertyHeight modificators.
			position.height = 16;
			var xRect = new Rect(position.x, position.y, position.width - 50 - 2, position.height);
			var yRect = new Rect(position.x + position.width - 50, position.y, 50, position.height);

			EditorGUI.PropertyField(xRect, property, new GUIContent());

			if (property.objectReferenceValue == null)
			{

			}
			else
			{
				if (GUI.Button(yRect, "Edit"))
				{
					ShowEditor(property.objectReferenceValue);
				}
			}



			EditorGUI.EndProperty();
		}

		public virtual void ShowEditor(Object objectReferenceValue)
		{

		}
	}
}

