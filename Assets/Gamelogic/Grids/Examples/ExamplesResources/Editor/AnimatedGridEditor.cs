using UnityEditor;

namespace Gamelogic.Grids.Examples.Editor
{
	[CustomEditor(typeof (AnimatedGrid))]
	public class AnimatedGridEditor : Extensions.Editor.Internal.GLEditor<AnimatedGrid>
	{
		public void OnEnable()
		{
			EditorApplication.update += UpdateAnimation;
		}

		public void OnDisable()
		{
			EditorApplication.update -= UpdateAnimation;
		}

		private void UpdateAnimation()
		{
			if (Target.enabled /*&& !EditorApplication.isPlaying*/)
			{
				Target.Animate();
			}
		}
	}
}