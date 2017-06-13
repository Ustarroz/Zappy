// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) Gamelogic (Pty) Ltd            //
//----------------------------------------------//

using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Represents a cell whose color can be set. 
	/// </summary>
	[Version(1,8)]
	public interface IColorableCell : ICell
	{
		/// <summary>
		/// The main color of the tile. 
		/// 
		/// This is used to set the color of tiles dynamically.
		/// </summary>
		Color Color { get; set; }
	}
}