using UnityEditor;
using UnityEngine;

namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// Class for drawing a <see cref="GridShapeGraph"/> field in the editor.
	/// </summary>
	[CustomPropertyDrawer(typeof(GridShapeGraph))]
	public class ShapeGraphPropertyDrawer : PropertyDrawer
	{
		private const float ExtraHeight = 20;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			//return EditorGUI.GetPropertyHeight(property, label, true);
			//return base.GetPropertyHeight(property, label) + ExtraHeight;

			var gridTypeProperty = property.FindPropertyRelative("gridType");
			var shape1GraphProperty = property.FindPropertyRelative("shape1Graph");
			var shape2GraphProperty = property.FindPropertyRelative("shape2Graph");
			var shape3GraphProperty = property.FindPropertyRelative("shape3Graph");

			float gridTypeHeight = EditorGUI.GetPropertyHeight(gridTypeProperty);
			float shapeGraphHeight = GetShapeGraphHeight(
				gridTypeProperty,
				shape1GraphProperty,
				shape2GraphProperty,
				shape3GraphProperty);

			return gridTypeHeight + shapeGraphHeight;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var gridTypeProperty = property.FindPropertyRelative("gridType");
			var shape1GraphProperty = property.FindPropertyRelative("shape1Graph");
			var shape2GraphProperty = property.FindPropertyRelative("shape2Graph");
			var shape3GraphProperty = property.FindPropertyRelative("shape3Graph");

			float gridTypeHeight = EditorGUI.GetPropertyHeight(gridTypeProperty);

			float shapeGraphHeight = GetShapeGraphHeight(
				gridTypeProperty,
				shape1GraphProperty,
				shape2GraphProperty,
				shape3GraphProperty);

			switch (gridTypeProperty.enumValueIndex)
			{
				case 0:
					shapeGraphHeight = EditorGUI.GetPropertyHeight(shape1GraphProperty);
					break;
				case 1:
					shapeGraphHeight = EditorGUI.GetPropertyHeight(shape2GraphProperty);
					break;
				case 2:
					shapeGraphHeight = EditorGUI.GetPropertyHeight(shape3GraphProperty);
					break;
				default:
					break;
			}

			EditorGUI.BeginProperty(position, label, property);

			//position.height = 16;
			var enumRect = new Rect(position.x, position.y, position.width, gridTypeHeight);
			var exactShapeRect = new Rect(position.x, position.y + gridTypeHeight, position.width, shapeGraphHeight);

			EditorGUI.PropertyField(enumRect, gridTypeProperty);

			switch (gridTypeProperty.enumValueIndex)
			{
				case 0:
					EditorGUI.PropertyField(exactShapeRect, shape1GraphProperty);
					break;
				case 1:
					EditorGUI.PropertyField(exactShapeRect, shape2GraphProperty);
					break;
				case 2:
					EditorGUI.PropertyField(exactShapeRect, shape3GraphProperty);
					break;
				default:
					break;
			}

			EditorGUI.EndProperty();
		}

		private float GetShapeGraphHeight(
			SerializedProperty gridTypeProperty,
			SerializedProperty shape1GraphProperty,
			SerializedProperty shape2GraphProperty,
			SerializedProperty shape3GraphProperty
			)
		{
			float shapeGraphHeight = 0;

			switch (gridTypeProperty.enumValueIndex)
			{
				case 0:
					shapeGraphHeight = EditorGUI.GetPropertyHeight(shape1GraphProperty);
					break;
				case 1:
					shapeGraphHeight = EditorGUI.GetPropertyHeight(shape2GraphProperty);
					break;
				case 2:
					shapeGraphHeight = EditorGUI.GetPropertyHeight(shape3GraphProperty);
					break;
				default:
					break;
			}

			return shapeGraphHeight;
		}
	}
}