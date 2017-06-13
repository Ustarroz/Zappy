//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Methods in an Op (sublcasses of AbstractOp) marked with this 
	/// attribute are automatically added as static methods to the
	/// appropriate grid class for convenience.
	/// </summary>
	[Version(1)]
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class ShapeMethodAttribute : Attribute
	{ }
}