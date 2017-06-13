//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// This class provides extensions for integers.
	/// </summary>
	[Version(1)]
	static class IntExtensions
	{
		public static bool InHalfOpenInterval(this int n, int closedBottom, int openTop)
		{
			return closedBottom <= n && n < openTop;
		}
	}
}