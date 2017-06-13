//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) Gamelogic (Pty) Ltd            //
//----------------------------------------------//

using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// A common interface for all cells.
	/// 
	/// This class does not specify any methods, but is used to
	/// identify cells so they can be handled in a special way by
	/// the editor.
	/// </summary>
	[Version(1,8)]
	public interface ICell
	{}
}