using System;
using System.Collections.Generic;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A class for mapping colors to types; used to draw a color indicator in the graph editor.
	/// </summary>
	static class TypeColorMap
	{
		/*
		/// <summary>
		/// Maps colors to types for drawing color indicators in graph editors.
		/// </summary>
		private static readonly Dictionary<Type, Color> Map = new Dictionary<Type, Color>
		{
			{typeof(int), ColorFromInt(133, 219, 233) },
			{typeof(GridPoint2), ColorFromInt(228, 120, 129) },
			{typeof(GridPoint3), ColorFromInt(255, 215, 87) },

			{typeof(float), ColorFromInt(42, 192, 217)},
			{typeof(Vector2), ColorFromInt(215, 55, 82) },
			{typeof(Vector3), ColorFromInt(247, 188, 0) },
		};*/

		private static Color ColorFromInt(int r, int g, int b)
		{
			return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
		}
	}

	/// <summary>
	/// Represents a node of a <see cref="Graph"/>.
	/// </summary>
	/// <seealso cref="UnityEngine.ScriptableObject" />
	/// <remarks>All graph nodes must extend from this type.</remarks>
	public class BaseNode : ScriptableObject
	{
		#region Fields

		/// <summary>
		/// Allows the node to be active or deactive.
		/// When deactive it will return its inputs without alter them
		/// </summary>
		[HideInInspector]
		public bool enable = true;

		// TODO: Make this private and provide a readonly property
		/// <exclude />
		[HideInInspector]
		public int id;

		/// <summary>
		/// The rectangle this node occupies when displayed visually.
		/// </summary>
		[HideInInspector]
		public Rect rect = new Rect(50, 50, 250, 0);
		#endregion

		#region Properties

		/// <summary>
		/// Gets the id of this node.
		/// </summary>
		public int Id
		{
			get { return id; }
		}

		/// <summary>
		/// A list of nodes that are the inputs for this node.
		/// </summary>
		[Abstract]
		public virtual IEnumerable<BaseNode> Inputs
		{
			get { throw new NotImplementedException("Node of type " + GetType() + "should override this property"); }
		}

		#endregion

		#region Public Methods

		public BaseNode() { }

		/// <summary>
		/// Adds a node to this nodes inputs.
		/// </summary>
		/// <param name="input"></param>
		[Abstract]
		public virtual void AddNodeInput(BaseNode input)
		{
			throw new NotImplementedException("Node of type " + GetType() + "should override this method");
		}

		/// <summary>
		/// Removes the given node from the list of input nodes of this node.
		/// </summary>
		/// <param name="input"></param>
		[Abstract]
		public virtual void RemoveNodeInput(BaseNode input)
		{
			throw new NotImplementedException("Node of type " + GetType() + "should override this method");
		}

		/// <summary>
		/// Updates a nodes outputs without recomputing internal (possibly random) values.
		/// </summary>
		[Abstract]
		public virtual void UpdateStatic()
		{
			throw new NotImplementedException("Node of type " + GetType() + "should override this method");
		}

		/// <summary>
		/// Recomputes a nodes internal values that are independent of the inputs.
		/// </summary>
		[Abstract]
		public virtual void Recompute()
		{
			throw new NotImplementedException("Node of type " + GetType() + "should override this method");
		}
		#endregion

		#region Message Handlers
		public void OnEnable()
		{
			hideFlags = HideFlags.HideInHierarchy;
			//name = "(" + id + ") " + GetType().Name;
		}
		#endregion
	}
}