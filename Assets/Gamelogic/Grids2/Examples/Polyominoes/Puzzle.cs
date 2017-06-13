using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class Puzzle : GridBehaviour<GridPoint2, TileCell>
	{
		public Color[] colors;
		public GameObject currentShapeRoot;
		public PolyominoCell cellPrefab;

		private Polyominoes.Pentomino.Type currentShapeType;
		private ObservedValue<TightShape2> currentShape;
		private GridEventTrigger gridEvents;
		private ObservedValue<GridPoint2> mousePosition;
		private PuzzleGrid<Polyominoes.Pentomino.Type> puzzleGrid;

		private readonly IMap<GridPoint2, GridPoint2> flip = Map.Func<GridPoint2, GridPoint2>(
			p => RectPoint.ReflectAboutY(p),
			p => RectPoint.ReflectAboutY(p));

		private readonly IMap<GridPoint2, GridPoint2> rotate = Map.Func<GridPoint2, GridPoint2>(
			p => RectPoint.Rotate90(p),
			p => RectPoint.Rotate270(p));


		public override void InitGrid()
		{
			gridEvents = GetComponent<GridEventTrigger>();

			Grid.Apply(c => c.GetComponent<PolyominoCell>().Init());

			puzzleGrid = new PuzzleGrid<Polyominoes.Pentomino.Type>(Grid);

			currentShapeType = Polyominoes.Pentomino.Type.F;
			currentShape = new ObservedValue<TightShape2>(new TightShape2(Polyominoes.Pentomino.Shapes[currentShapeType]));

			currentShape.OnValueChange += PaintHighlight;
			currentShape.OnValueChange += UpdateCurrentShape;

			UpdateCurrentShape();
		}

		public void Update()
		{
			if (mousePosition == null)
			{
				mousePosition = new ObservedValue<GridPoint2>(gridEvents.MousePosition);
				mousePosition.OnValueChange += PaintHighlight;
			}

			mousePosition.Value = gridEvents.MousePosition;

			if (currentShape == null) return;

			if (Input.GetMouseButtonDown(0))
			{
				LeftClick();
			}

			if (Input.GetMouseButtonDown(1))
			{
				RightClick();
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				RotateShape();
			}

			if (Input.GetKeyDown(KeyCode.F))
			{
				FlipShape();
			}
		}

		private void RightClick()
		{
			if (Grid.Contains(mousePosition.Value))
			{
				var shape = puzzleGrid.GetShape(mousePosition.Value);
				puzzleGrid.Remove(mousePosition.Value);

				PaintBackground(shape);
			}
		}

		private void LeftClick()
		{
			if (puzzleGrid.Contains(currentShape.Value, mousePosition.Value) &&
				puzzleGrid.IsEmpty(currentShape.Value, mousePosition.Value))
			{
				var placedShape = puzzleGrid.Place(currentShape.Value, mousePosition.Value, currentShapeType);

				PaintShape(placedShape);
			}
		}

		public void OnRotateClick()
		{
			RotateShape();
		}

		public void OnFlipClick()
		{
			FlipShape();
		}

		public void SetShape(Polyominoes.Pentomino.Type type)
		{
			currentShapeType = type;
			currentShape.Value = new TightShape2(Polyominoes.Pentomino.Shapes[type]);
		}

		private void UpdateCurrentShape()
		{
			if (Application.isPlaying)
			{
				currentShapeRoot.transform.DestroyChildren();
			}
			else
			{
				currentShapeRoot.transform.DestroyChildrenImmediate();
			}

			foreach (var point in currentShape.Value.Points)
			{
				var cell = Instantiate(cellPrefab);

				cell.transform.parent = currentShapeRoot.transform;
				cell.transform.localPosition = GridMap.GridToWorld(point);
				cell.Highlight = false;
				cell.FillColor = colors[(int)currentShapeType];
				cell.Filled = true;
			}
		}

		private void RotateShape()
		{
			currentShape.Value = currentShape.Value.Transform(rotate).ToCanonicalPosition();
		}

		private void FlipShape()
		{
			currentShape.Value = currentShape.Value.Transform(flip).ToCanonicalPosition();
		}

		private void PaintBackground(IExplicitShape<GridPoint2> shape)
		{
			foreach (var point in shape.Points)
			{
				var polyominoCell = Grid[point].GetComponent<PolyominoCell>();

				polyominoCell.Filled = false;
			}
		}

		private void PaintShape(IExplicitShape<GridPoint2> placedShape)
		{
			foreach (var point in placedShape.Points)
			{
				var polyominoCell = Grid[point].GetComponent<PolyominoCell>();

				polyominoCell.FillColor = colors[(int)currentShapeType];
				polyominoCell.Filled = true;
			}
		}

		private void PaintHighlight()
		{
			foreach (var point in Grid.Points)
			{
				Grid[point].GetComponent<PolyominoCell>().Highlight = false;
			}

			if (currentShape == null) return;

			foreach (var point in currentShape.Value.Points)
			{
				var tilePoint = point + mousePosition.Value;

				if (Grid.Contains(tilePoint))
				{
					Grid[tilePoint].GetComponent<PolyominoCell>().Highlight = true;
				}
			}
		}
	}
}