using System;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// An attribute used to decorate behaviour tree nodes. Apply this to custom behaviours
	/// to have them appear in the behaviour tree editor list.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class GraphEditorAttribute : Attribute
	{
		private const string DefaultFolder = "Root";

		public string folder;
		public string name;

		public GraphEditorAttribute(string fullPath)
		{
			var path = fullPath.Split('/');

			if (path.Length == 1)
			{
				folder = DefaultFolder;
				name = fullPath;
			}
			else
			{
				folder = path[0];
				name = path[1];
			}
		}
	}
}