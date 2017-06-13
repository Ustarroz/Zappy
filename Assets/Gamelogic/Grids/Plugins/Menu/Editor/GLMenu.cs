using UnityEditor;

namespace Gamelogic.Extensions.Menu.Editor.Internal
{
	public partial class GLMenu
	{
		[MenuItem("Help/Gamelogic/Grids/API Documentation")]
		public static void OpenGridsAPI()
		{
			OpenUrl("http://www.gamelogic.co.za/documentation/grids/");
		}
	}
}