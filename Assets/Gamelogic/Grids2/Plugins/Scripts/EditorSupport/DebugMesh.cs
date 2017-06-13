using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Internal
{
	/// <summary>
	/// A component to draw gizmos at vertices that makes it easier to
	/// debug meshes.
	/// </summary>
	/// <seealso cref="Gamelogic.Extensions.GLMonoBehaviour" />
	public class DebugMesh : GLMonoBehaviour
	{

		public Color color = Color.white;
		public float radius = 0.1f;

		public void OnDrawGizmos()
		{
			Gizmos.color = color;

			foreach (var vert in GetComponent<MeshFilter>().sharedMesh.vertices)
			{
				Gizmos.DrawSphere(transform.TransformPoint(vert), radius);
			}
		}
	}
}