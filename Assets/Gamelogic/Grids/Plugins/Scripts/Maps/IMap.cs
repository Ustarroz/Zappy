//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;
using Gamelogic.Extensions.Internal;
using UnityEngine;
namespace Gamelogic.Grids
{
	/// <summary>
	/// An IMap maps 2D world coordinates to Grid coordinates and vice versa. 
	/// 
	/// Most of the methods of this class are meant to be chained to the constructor.The last command
	/// in the chain is suaully a conversion to IMap3D, which converts the 2D coordinates to 3D for use 
	/// in the game engine.
	/// 
	/// 
	/// The order of chained calls sometimes make a difference.
	/// 
	/// The standard order is this:
	///		- set grid point transforms
	///		- set cell anchoring
	///		- set world transforms (such as translate, rotate)
	///		- do layout (using WithWindow)
	///		- convert to 3D
	/// 
	/// Transformations only apply to the world points returned and processed by the map, 
	/// the grid contents is not transformed.
	/// 
	/// For example, applying scale to the map, will not scale the cells<emph> physically</emph>, in the sense
	/// that if the grid contains GameObjects, cells will remain the same size. The cells will be logically bigger,
	/// so they will appear further apart from each other.
	/// 
	/// Built-in 2D grids generally have associated built-in maps.See the [Grid Index](http://gamelogic.co.za/grids/quick-start-tutorial/grid-index/) 
	/// for the list.
	/// 
	/// You can also provide your own maps, either as implementations of IMap, or IMap3D.
	/// </summary>
	[Version(1)]
	public interface IMap<TPoint> : IGridToWorldMap<TPoint>
		where TPoint : IGridPoint<TPoint>
	{
		#region Properties

		/// <summary>
		/// Gets a grid point given a world point.
		/// </summary>
		TPoint this[Vector2 point]
		{
			get;
		}

		/// <summary>
		/// The transformation applied to points before they are returned from the map.
		/// </summary>
		Func<TPoint, TPoint> GridPointTransform
		{
			get;
		}

		/// <summary>
		/// The transformation applied to points before they are processed by the map.
		/// </summary>
		Func<TPoint, TPoint> InverseGridPointTransform
		{
			get;
		}
		#endregion

		#region Helper methods for implementations

		/// <summary>
		/// This method maps a world point to a grid point
		/// without making any compensation based on the
		/// anchoring.
		/// 
		/// This method should be used for calculations in maps,
		/// rather than the index methods. (The index methods do 
		/// the anchor compensation.If you use that method
		/// instead of this one internally, you may compensate
		/// more than one time.
		/// </summary>
		TPoint RawWorldToGrid(Vector2 worldPoint);

		/// <summary>
		/// This method maps a grid point to a world point.
		/// </summary>
		Vector2 GridToWorld(TPoint worldPoint);

		Vector2 CalcGridDimensions(IGridSpace<TPoint> grid);
		Vector2 CalcBottomLeft(IGridSpace<TPoint> grid); 
		
		Vector2 CalcAnchorDimensions(IGridSpace<TPoint> grid);
		Vector2 CalcAnchorBottomLeft(IGridSpace<TPoint> grid);
		

		Vector2 GetAnchorTranslation();
		Vector2 GetCellDimensions();
		Vector2 GetCellDimensions(TPoint point);

		#endregion


		#region Fluent Interface Members		
		//These are the methods you can chain together to get a suitable map.

		///<sumary>
		/// Sets the point transforms used on the grid that this map
		/// will used with.
		/// 
		/// Normally, the grid and map should have the same point transforms.
		/// </sumary>
		IMap<TPoint> SetGridPointTransforms(
			Func<TPoint, TPoint> coordinateTransformation,
			Func<TPoint, TPoint> inverseCoordinateTransformation);

		/// <summary>
		/// Returns an IMap that anchors the grid cells to the left.
		/// </summary>
		IMap<TPoint> AnchorCellLeft();

		/// <summary>
		/// Returns an IMap that anchors the grid cells to the right.
		/// </summary>
		IMap<TPoint> AnchorCellRight();

		/// <summary>
		/// Returns an IMap that anchors the grid cells horizontally to the center. 
		/// </summary>
		IMap<TPoint> AnchorCellCenter();

		/// <summary>
		/// Returns an IMap that anchors the grid cells to the top.
		/// </summary>
		IMap<TPoint> AnchorCellTop();

		/// <summary>
		/// Returns an IMap that anchors the grid cells to the bottom.
		/// </summary>
		IMap<TPoint> AnchorCellBottom();

		/// <summary>
		/// Returns an IMap that anchors the grid cells vertically to the middle.
		/// </summary>
		IMap<TPoint> AnchorCellMiddle();

		/// <summary>
		/// Returns an IMap that anchors the grid cells vertically to the top left.
		/// </summary>
		IMap<TPoint> AnchorCellTopLeft();

		/// <summary>
		/// Returns an IMap that anchors the grid cells vertically to the top center.
		/// </summary>
		IMap<TPoint> AnchorCellTopCenter();

		/// <summary>
		/// Returns an IMap that anchors the grid cells vertically to the top right.
		/// </summary>
		IMap<TPoint> AnchorCellTopRight();

		/// <summary>
		/// Returns an IMap that anchors the grid cells to the middle left.
		/// </summary>
		IMap<TPoint> AnchorCellMiddleLeft();

		/// <summary>
		/// Returns an IMap that anchors the grid cells to the middle center.
		/// </summary>
		IMap<TPoint> AnchorCellMiddleCenter();

		/// <summary>
		/// Returns an IMap that anchors the grid cells to the middle right.
		/// </summary>
		IMap<TPoint> AnchorCellMiddleRight();

		/// <summary>
		/// Returns an IMap that anchors the grid cells to the bottom left.
		/// </summary>
		IMap<TPoint> AnchorCellBottomLeft();

		/// <summary>
		/// Returns an IMap that anchors the grid cells to the bottom center.
		/// </summary>
		IMap<TPoint> AnchorCellBottomCenter();

		/// <summary>
		/// Returns an IMap that anchors the grid cells to the bottom right.
		/// </summary>
		IMap<TPoint> AnchorCellBottomRight();

		/// <summary>
		/// Returns an IMap where grid cells are translated by the give amount.
		/// </summary>
		IMap<TPoint> Translate(Vector2 offset);

		/// <summary>
		/// Returns an IMap where grid cells are translated by the give amounts.
		/// </summary>
		IMap<TPoint> Translate(float offsetX, float offsetY);

		/// <summary>
		/// Returns an IMap where grid cells are translated by the give amount.
		/// </summary>
		IMap<TPoint> TranslateX(float offsetX);

		/// <summary>
		/// Returns an IMap where grid cells are translated by the give amount.
		/// </summary>
		IMap<TPoint> TranslateY(float offsetY);

		/// <summary>
		/// Returns an IMap where grid cells are reflected about the Y-point in world space.
		/// </summary>
		IMap<TPoint> ReflectAboutY();

		/// <summary>
		/// Returns an IMap where grid cells are reflected about the X-point in world space.
		/// </summary>
		IMap<TPoint> ReflectAboutX();

		/// <summary>
		/// Returns an IMap where grid cells positioned with x-and y-coordinates (in world space) flipped.
		/// </summary>
		IMap<TPoint> FlipXY();

		/// <summary>
		/// Returns an IMap where grid cells are rotated about the origin by the given angle.
		/// </summary>
		IMap<TPoint> Rotate(float angle);

		/// <summary>
		/// Returns an IMap where grid cells are rotated about the given point by the given angle.
		/// </summary>
		IMap<TPoint> RotateAround(float angle, Vector2 point);

		/// <summary>
		/// Returns an IMap where grid cells are rotated about the origin by 90 degrees.
		/// </summary>
		IMap<TPoint> Rotate90();

		/// <summary>
		/// Returns an IMap where grid cells are scaled by factor.
		/// </summary>
		IMap<TPoint> Scale(float factor);

		IMap<TPoint> Scale(Vector2 factor);

		/// <summary>
		/// Returns an IMap where grid cells are scaled by the factors in each direction.
		/// </summary>
		IMap<TPoint> Scale(float factorX, float factorY);

		/// <summary>
		/// Returns an IMap where grid cells are scaled about the origin by 180 degrees.
		/// </summary>
		IMap<TPoint> Rotate180();

		/// <summary>
		/// Returns a WindowedMap based on this map that can be used to 
		/// lay the grid out in a window.
		/// </summary>
		/// <example>
		/// For example:
		/// <code>
		/// var map = new RectMap(cellDimensions)
		/// .WithWindow(screenRect)
		/// .AlignMiddleCenter();
		/// </code>
		/// </example>
		WindowedMap<TPoint> WithWindow(Rect window);

		/// <summary>
		/// Returns a IMap3D which maps a grid point to Vector3 instead of Vector2.
		/// The vector3 is the same as the Vector2 that this map would return, with the
		/// 
		/// z set to the given value.
		/// </summary>
		IMap3D<TPoint> To3DXY(float z);

		/// <summary>
		/// The same as To3DX(float z), but with z set to 0.
		/// </summary>
		IMap3D<TPoint> To3DXY();

		/// <summary>
		/// Returns a IMap3D which maps a grid point to Vector3 instead of Vector2.
		/// The vector3 is the same as the Vector2 that this map would return,
		/// with z of the Vector3 corresponding to y of the Vector2, and with the
		/// y set to the given value.
		/// </summary>
		IMap3D<TPoint> To3DXZ(float y);

		/// <summary>
		/// The same as To3DZ(float y), but with y set to 0.
		/// </summary>
		IMap3D<TPoint> To3DXZ();
		
		#endregion
		
	}
}