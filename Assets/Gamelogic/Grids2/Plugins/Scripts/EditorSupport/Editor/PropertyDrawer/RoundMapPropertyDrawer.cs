using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Gamelogic.Grids2.Editor
{
	/// <summary>
	/// Property drawer for the RoundType enum.
	/// </summary>
	/// <seealso cref="UnityEditor.PropertyDrawer" />
	[CustomPropertyDrawer(typeof(RoundType))]
	public class RoundMapPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			var labelRect = new Rect(position)
			{
				width = EditorGUIUtility.labelWidth
			};

			EditorGUI.LabelField(labelRect, label);

			var contentRect = new Rect(position)
			{
				x = position.x + EditorGUIUtility.labelWidth,
				width = position.width - EditorGUIUtility.labelWidth
			};

			var allValues = Enum.GetValues(typeof(RoundType))
				.Cast<RoundType>()
				.ToArray();

			var gridType = Enum.GetValues(typeof(GridType))
					.Cast<GridType>()
					.ToArray()[property.serializedObject.FindProperty("gridShapeGraph").FindPropertyRelative("gridType").enumValueIndex];

			if (allValues[property.enumValueIndex].GetDimension() != gridType.GetDimension())
			{
				for (int i = 0; i < allValues.Length; i++)
				{
					if (allValues[i].GetDimension() == gridType.GetDimension())
					{
						property.enumValueIndex = i;
						property.serializedObject.ApplyModifiedProperties();
						break;
					}
				}
			}

			if (EditorGUI.DropdownButton(
				contentRect,
				new GUIContent(property.enumDisplayNames[property.enumValueIndex]),
				FocusType.Passive))
			{
				GenericMenu menu = new GenericMenu();
				for (int i = 0; i < allValues.Length; i++)
				{
					var value = allValues[i];

					if (value.GetDimension() == gridType.GetDimension())
					{
						var selectedValueIndex = property.enumValueIndex;
						int iCopy = i;
						menu.AddItem(
							new GUIContent(property.enumDisplayNames[i]),
							i == selectedValueIndex,
							() =>
							{
								property.enumValueIndex = iCopy;
								property.serializedObject.ApplyModifiedProperties();
							});
					}
				}

				menu.DropDown(contentRect);
			}
			EditorGUI.EndProperty();


		}
	}
}