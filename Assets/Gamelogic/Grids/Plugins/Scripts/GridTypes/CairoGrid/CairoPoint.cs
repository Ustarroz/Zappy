//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;
using System.Collections.Generic;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// A coordinate for a cell in a CiaroGrid. The base point is a pointy hex point, and indices run from 0 to 3.
	/// </summary>
	[Version(1, 2)]
	[Experimental]
	public partial struct CairoPoint : 
		ISplicedPoint <CairoPoint, PointyHexPoint>
	{
		#region Constants
		public const int SpliceCount = 4;

		public static readonly IEnumerable<CairoPoint>[] MainDirections =
		{
			new PointList<CairoPoint>
			{
				new CairoPoint(0, 0, 3),
				new CairoPoint(0, 0, 2),
				new CairoPoint(0, 0, 1),
				new CairoPoint(0, -1, 3),
				new CairoPoint(1, -1, 1),
			},

			new PointList<CairoPoint>
			{
				new CairoPoint(0, 0, 1),
				new CairoPoint(-1, 1, -1),
				new CairoPoint(-1, 0, 2),
				new CairoPoint(0, -1, 1),
				new CairoPoint(0, 0, -1),
			},

			new PointList<CairoPoint>
			{
				new CairoPoint(0, 1, -1),
				new CairoPoint(-1, 1, 1),
				new CairoPoint(0, 0, -1),
				new CairoPoint(0, 0, -2),
				new CairoPoint(0, 0, 1),
			},

			new PointList<CairoPoint>
			{
				new CairoPoint(1, 0, -2),
				new CairoPoint(0, 1, -3),
				new CairoPoint(0, 0, -1),
				new CairoPoint(0, 0, -3),
				new CairoPoint(1, -1, -1),
			}
		};
		#endregion

		#region Properties

		/// <summary>
		/// This is a redundant coordinate that is useful for certain algorithms and calculation.
		/// The coordinates of a triangle satsify this identity: X + Y + Z + I = 0.
		/// </summary>
		public int Z
		{
			get
			{
				return (BasePoint.Z - I);
			}
		}
		#endregion

		#region Magnitude
		public int DistanceFrom(CairoPoint other)
		{
			throw new NotImplementedException();
		}
		#endregion
		
		#region Colorings
		public int GetColor12()
		{
			return (BasePoint.GetColor1_3 () * 4 + I);
		}
		#endregion
	}
}