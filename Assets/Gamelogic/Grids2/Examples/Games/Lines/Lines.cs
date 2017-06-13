using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Algorithms;
using UnityEngine;
using UnityEngine.UI;

namespace Gamelogic.Grids2.Examples
{
	public class Lines : GridBehaviour<GridPoint2, TileCell>
	{
		#region Inspector

		public int lineLength = 4;
		public int cellsPerTurn = 5;
		public List<Color> colors;

		public Text linesCountText;
		public Text gameOverText;
		#endregion

		#region Private Fields

		private IGrid<GridPoint2, LinesCell> grid;
		private bool isHoldingCell;
		private LinesCell cellThatIsBeingHeld;

		private ObservedValue<int> linesCount;
		private ObservedValue<bool> gameOver;

		#endregion

		#region Callbacks

		public void OnReset()
		{
			Reset();
		}

		public override void InitGrid()
		{
			grid = Grid.CloneStructure(p => Grid[p].GetRequiredComponent<LinesCell>());
			Reset();
		}

		public static Color GetColor(int color)
		{
			return FindObjectOfType<Lines>().colors[color];
		}

		public void Reset()
		{
			foreach (var cell in grid.Cells)
			{
				cell.SetState(true, -1, false);
			}

			gameOverText.gameObject.SetActive(false);
			gameOver = new ObservedValue<bool>(false);
			gameOver.OnValueChange += () => { gameOverText.gameObject.SetActive(gameOver.Value); };
			linesCount = new ObservedValue<int>(0);
			linesCount.OnValueChange += () => { linesCountText.text = linesCount.Value.ToString(); };

			AddNewCells();
		}

		public void OnClick(GridPoint2 point)
		{
			if (gameOver.Value)
			{
				return;
			}

			var clickedCell = grid[point];

			if (isHoldingCell)
			{
				if (clickedCell.IsEmpty)
				{
					MoveCell(clickedCell);

					if (!ClearLinesAroundPoint(point))
					{
						AddNewCells();
					} //otherwise, give the player a "free" turn.

				}
				else if (clickedCell == cellThatIsBeingHeld)
				{
					DropCell();
				}
			}
			else
			{
				if (!clickedCell.IsEmpty)
				{
					PickUpCell(clickedCell);
				}
			}
		}

		#endregion

		#region Implementation

		private void PickUpCell(LinesCell clickedCell)
		{
			cellThatIsBeingHeld = clickedCell;
			isHoldingCell = true;
			clickedCell.HighlightOn = true;
		}

		private void DropCell()
		{
			cellThatIsBeingHeld.HighlightOn = false;
			cellThatIsBeingHeld = null;
			isHoldingCell = false;
		}

		private void MoveCell(LinesCell clickedCell)
		{
			cellThatIsBeingHeld.HighlightOn = false;
			SwapCells(cellThatIsBeingHeld, clickedCell);
			cellThatIsBeingHeld = null;
			isHoldingCell = false;
		}

		private void AddNewCells()
		{
			var emptyCells = GetEmptyCells();
			var cellsToPlace = emptyCells.SampleRandom(cellsPerTurn);

			foreach (var point in cellsToPlace)
			{
				SetCellToRandom(grid[point]);
				ClearLinesAroundPoint(point);
			}

			emptyCells = GetEmptyCells();

			if (!emptyCells.Any())
			{
				gameOver.Value = true;
			}
		}

		private IEnumerable<GridPoint2> GetEmptyCells()
		{
			return grid.Points.Where(p => grid[p].IsEmpty);
		}

		private void SetCellToRandom(LinesCell cell)
		{
			int newColor = (Random.Range(0, colors.Count));

			cell.SetState(false, newColor, false);
		}

		private static void SwapCells(LinesCell cell1, LinesCell cell2)
		{
			int tempColor = cell1.Type;
			bool tempIsEmpty = cell1.IsEmpty;

			cell1.SetState(cell2.IsEmpty, cell2.Type, false);
			cell2.SetState(tempIsEmpty, tempColor, false);
		}

		private bool ClearLinesAroundPoint(GridPoint2 point)
		{
			bool wasLinesRemoved = false;

			var lines =
				Grids2.Algorithms.GetConnectedLines(
						grid,
						point,
						PointyHexPoint.OrthogonalLines,
						(p1, p2) => grid[p1].Color == grid[p2].Color)
					.Where(line => line.Count() >= lineLength);

			foreach (var line in lines)
			{
				foreach (var linePoint in line)
				{
					grid[linePoint].SetState(true, -1, false);
				}

				linesCount.Value++;

				wasLinesRemoved = true;
			}

			return wasLinesRemoved;
		}

		#endregion
	}
}