//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2014 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Classes that can be edited in the inspector using a custum editor should implement this 
	/// interface to allow the editor to update the presentation when necessary.
	/// </summary>
	[Version(1,8)]
	public interface IGLScriptableObject
	{		
		/// <param name="forceUpdate">When true, the object must update its presentation state with its logical state.
		/// When false, it can do so depending on the update settings of the object.</param>
		void __UpdatePresentation(bool forceUpdate);
	}
}
