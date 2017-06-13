//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) Gamelogic (Pty) Ltd            //
//----------------------------------------------//

using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids
{
	/// <summary>
	/// A TileCell is a cell that is represented by a single Unity object. 
	/// 
	/// They can be anything, including Sprites, Models, or compound objects, 
	/// as long as they can be instantiated, and each corresponds to exactly one
	/// cell in a grid.They are used with TileGrid builders.
	/// </summary>
	[Version(1,8)]
	public abstract class TileCell : GLMonoBehaviour, IColorableCell
	{
		[SerializeField]
		private Vector3 centerOffset = Vector3.zero;

		/// <summary>
		/// The visual center of a cell.
		/// 
		/// This is useful for cells that do not have their actual pivot
		/// at the "expected" place, such as mesh tile cells in polar grids.
		/// </summary>
		public Vector3 Center
		{
			get {return transform.TransformPoint(__CenterOffset); }
		}

		/// <summary>
		/// Should be set by the grid creator to a value that can serve as the 
		/// center of the cell(if the cell is at the origin).
		/// </summary>
		public Vector3 __CenterOffset
		{
			get { return centerOffset; }
			set { centerOffset = value; }
		}

		public abstract Color Color { get; set; }

		/// <summary>
		/// The 2D dimensions of the tile. For 3D objects, this is the 
		/// dimensions along the same plane as the 2D grid.
		/// </summary>
		public abstract Vector2 Dimensions { get; }

		/// <summary>
		/// This methods is called by the editor, and is to update the 
		/// cell representation to it's current state.
		/// 
		/// (Typically, this is necessary for serialized fields that
		/// affect the presentation.For example, the color may be saved,
		/// but the actual object may reset it's color).
		/// </summary>
		public abstract void __UpdatePresentation(bool forceUpdate);

		/// <summary>
		/// Sets the angle of the tile. If the tile is in the XY plane, the angle 
		/// will be the Z angle.
		/// </summary>
		/// <param name="angle">In degrees.</param>
		public abstract void SetAngle(float angle);

		/// <summary>
		/// Adds the given angle to the current angle of the tile. If the tile is 
		/// in the XY plane, it will be added to the Z angle.
		/// </summary>
		public abstract void AddAngle(float angle);


		#region Gizmos

		/// <summary>
		/// Draws the Gizmos for this tile: a label with the tile name.
		/// 
		/// It is often convenient, for this reason, to make the tile name
		/// the coordinate of the tile.
		/// </summary>
		public virtual void OnDrawGizmos()
		{
			//TODO: make this switch off
			//GLGizmos.Label(Center, name);
		}

		#endregion

	}
}