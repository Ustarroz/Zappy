using UnityEngine;


namespace Gamelogic.Grids2
{
	/*
		@version1_8
		@ingroup UnityEditorSupport
	*/
	/// <summary>
	/// Represents a cell whose color can be set.
	/// </summary>
	public interface IColorableCell
	{
		/// <summary>
		/// The main color of the tile. This is used to set the color of tiles dynamically.
		/// </summary>
		Color Color { get; set; }
	}
}
