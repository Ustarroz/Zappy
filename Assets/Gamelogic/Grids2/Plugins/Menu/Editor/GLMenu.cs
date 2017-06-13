using UnityEditor;

namespace Gamelogic.Extensions.Menu.Editor.Internal
{
	public partial class GLMenu
	{
		[MenuItem("Help/Gamelogic/Grids2/API Documentation")]
		public static void OpenGrids2API()
		{
			OpenUrl("http://www.gamelogic.co.za/documentation/grids2/");
		}
	}
}