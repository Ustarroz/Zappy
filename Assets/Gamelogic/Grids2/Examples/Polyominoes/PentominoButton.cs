using Gamelogic.Extensions;

namespace Gamelogic.Grids2.Examples
{
	public class PentominoButton : GLMonoBehaviour
	{
		public Polyominoes.Pentomino.Type type;

		public void OnClick()
		{
			FindObjectOfType<Puzzle>().SetShape(type);
		}
	}
}