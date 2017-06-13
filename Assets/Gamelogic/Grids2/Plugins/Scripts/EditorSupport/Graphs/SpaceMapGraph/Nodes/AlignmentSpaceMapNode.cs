using System;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node for aligning a grid in a rectangle.
	/// </summary>
	/// <seealso cref="ProjectSpaceMapNode{TInput,TOutput}" />
	/// //TODO Does not work
	//[SpaceMapNode("Misc/Alignment Map", 2)]
	public class AlignmentSpaceMapNode : ProjectSpaceMapNode<Vector3, Vector3>
	{
		[Header("Grid Data")]
		public SpriteCell cellPrefab;
		public Bounds screenBounds;
		public GridShapeGraph gridShapeGraph;

		[Header("Grid Alignment")]
		public HorizontalAlignment horizontalAlign;
		public VerticalAlignment verticalAlign;

		[Header("Cell Anchoring")]
		public HorizontalAlignment horizontalAnchor;
		public VerticalAlignment verticalAnchor;

		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			throw new NotImplementedException();
			/*
			var cellDimensions = cellPrefab.Sprite.rect.size;

			
			switch (gridShapeGraph.gridType)
			{
				case GridType.Grid1:
					return
						input.AlignGridInRect<int>(gridShapeGraph.shape1Graph.GetShape(), p => cellDimensions, screenBounds, horizontalAlign,
							verticalAlign)
						     .AnchorPivotInRect<int>(gridShapeGraph.shape1Graph.GetShape(), p => cellDimensions, horizontalAnchor,
							     verticalAnchor);

				case GridType.Grid2:
					return
						input.AlignGridInRect<GridPoint2>(gridShapeGraph.shape2Graph.GetShape(), p => cellDimensions, screenBounds, horizontalAlign,
							verticalAlign)
						     .AnchorPivotInRect<GridPoint2>(gridShapeGraph.shape2Graph.GetShape(), p => cellDimensions, horizontalAnchor,
							     verticalAnchor);

				case GridType.Grid3:
					return
						input.AlignGridInRect<GridPoint3>(gridShapeGraph.shape3Graph.GetShape(), p => cellDimensions, screenBounds, horizontalAlign,
							verticalAlign)
						     .AnchorPivotInRect<GridPoint3>(gridShapeGraph.shape3Graph.GetShape(), p => cellDimensions, horizontalAnchor,
							     verticalAnchor);

				default:
					throw new ArgumentOutOfRangeException();
			}*/
		}
	}
}