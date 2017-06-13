using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Algorithms;
using UnityEngine;
using UnityEngine.UI;

namespace Gamelogic.Grids2.Examples
{
	public class LightsOutHexCustomNeighbors : GridBehaviour<GridPoint2, TileCell>
	{
		public Color offColor = Color.black;
		public Color onColor = Color.white;

		public List<InspectableGridPoint2> neighborDirections;

		public Text gameOverText;
		public Text moveCountText;

		private const int Symmetry2 = 0;
		private const int Symmetry3 = 1;
		private const int Symmetry6 = 2;

		private IGrid<GridPoint2, bool> dataGrid;
		private ObservedValue<int> moveCount;
		private ObservedValue<bool> gameOver;

		private List<GridPoint2> neighborDirectionsList;

		public override void InitGrid()
		{
			neighborDirectionsList = neighborDirections.Select(p => p.GetGridPoint()).ToList();
			dataGrid = Grid.CloneStructure<GridPoint2, bool>(false);
			Reset();
		}

		public void OnReset()
		{
			Reset();
		}

		public void OnClick(GridPoint2 point)
		{
			if (gameOver.Value)
			{
				return;
			}

			ToggleCellAt(point);
			moveCount.Value++;

			if (dataGrid.Cells.All(c => !c))
			{
				gameOver.Value = true;
			}
		}

		public void Reset()
		{
			foreach (var point in Grid.Points)
			{
				dataGrid[point] = false;
				(Grid[point].GetComponent<SpriteCell>()).Color = offColor;
			}

			moveCount = new ObservedValue<int>(0);
			moveCount.OnValueChange += () => { moveCountText.text = moveCount.Value.ToString(); };

			gameOverText.gameObject.SetActive(false);
			gameOver = new ObservedValue<bool>(false);
			gameOver.OnValueChange += () => { gameOverText.gameObject.SetActive(gameOver.Value); };

			CreatePuzzle();
		}

		private void CreatePuzzle()
		{
			int pattern = Random.Range(0, 4);

			switch (pattern)
			{
				case 0:
					InitPattern1();
					break;
				case 1:
					InitPattern2();
					break;
				case 2:
					InitPattern1();
					InitPattern2();
					break;
				case 3:
					InitPattern3();
					break;
			}

			if (dataGrid.Cells.All(c => !c)) //The game is already solved, so make a new puzzle instead
			{
				CreatePuzzle();
			}
		}

		private void InitPattern2()
		{
			int start = Random.Range(0, 3);
			int end = Random.Range(start, 3);
			int symmetry = Random.Range(0, 3);

			switch (symmetry)
			{
				case Symmetry6:
					for (int i = start; i <= end; i++)
					{
						ToggleCellAt((PointyHexPoint.East + PointyHexPoint.NorthEast) * i);
						ToggleCellAt((PointyHexPoint.West + PointyHexPoint.SouthWest) * i);
						ToggleCellAt((PointyHexPoint.NorthEast + PointyHexPoint.NorthWest) * i);
						ToggleCellAt((PointyHexPoint.SouthWest + PointyHexPoint.SouthEast) * i);
						ToggleCellAt((PointyHexPoint.NorthWest + PointyHexPoint.West) * i);
						ToggleCellAt((PointyHexPoint.SouthEast + PointyHexPoint.East) * i);
					}
					break;
				case Symmetry3:
					for (int i = start; i <= end; i++)
					{
						ToggleCellAt((PointyHexPoint.East + PointyHexPoint.NorthEast) * i);
						ToggleCellAt((PointyHexPoint.SouthWest + PointyHexPoint.SouthEast) * i);
						ToggleCellAt((PointyHexPoint.NorthWest + PointyHexPoint.West) * i);

					}
					break;
				case Symmetry2:
					for (int i = start; i <= end; i++)
					{
						ToggleCellAt((PointyHexPoint.East + PointyHexPoint.NorthEast) * i);
						ToggleCellAt((PointyHexPoint.West + PointyHexPoint.SouthWest) * i);
					}
					break;
			}
		}

		private void InitPattern1()
		{
			int start = Random.Range(0, 5);
			int end = Random.Range(start, 5);
			int symmetry = Random.Range(0, 3);

			switch (symmetry)
			{
				case Symmetry6:
					for (int i = start; i <= end; i++)
					{
						ToggleCellAt(PointyHexPoint.East * i);
						ToggleCellAt(PointyHexPoint.West * i);
						ToggleCellAt(PointyHexPoint.NorthEast * i);
						ToggleCellAt(PointyHexPoint.SouthWest * i);
						ToggleCellAt(PointyHexPoint.NorthWest * i);
						ToggleCellAt(PointyHexPoint.SouthEast * i);
					}
					break;
				case Symmetry3:
					for (int i = start; i <= end; i++)
					{
						ToggleCellAt(PointyHexPoint.East * i);
						ToggleCellAt(PointyHexPoint.SouthWest * i);
						ToggleCellAt(PointyHexPoint.NorthWest * i);

					}
					break;
				case Symmetry2:
					for (int i = start; i <= end; i++)
					{
						ToggleCellAt(PointyHexPoint.East * i);
						ToggleCellAt(PointyHexPoint.West * i);
					}
					break;
			}
		}

		private void InitPattern3()
		{

			var randomPoints = Grid.Points.SampleRandom(2);
			var pattern = new HashSet<GridPoint2>();
			int symmetry = Random.Range(0, 3);

			foreach (
				var pointyHexPoints in
					randomPoints
						.Select(point1 => Grid.Points.Where(p => PointyHexPoint.HexNorm((p - point1)) <= 3).SampleRandom(2))
						.Select(randomGroup => randomGroup as IList<GridPoint2> ?? randomGroup.ToList()))
			{
				pattern.AddRange(pointyHexPoints);

				switch (symmetry)
				{
					case Symmetry6:

						pattern.AddRange(pointyHexPoints.Select<GridPoint2, GridPoint2>(PointyHexPoint.Rotate60));
						pattern.AddRange(pointyHexPoints.Select<GridPoint2, GridPoint2>(PointyHexPoint.Rotate120));
						pattern.AddRange(pointyHexPoints.Select<GridPoint2, GridPoint2>(PointyHexPoint.Rotate180));
						pattern.AddRange(pointyHexPoints.Select<GridPoint2, GridPoint2>(PointyHexPoint.Rotate240));
						pattern.AddRange(pointyHexPoints.Select<GridPoint2, GridPoint2>(PointyHexPoint.Rotate300));

						break;

					case Symmetry3:
						pattern.AddRange(pointyHexPoints.Select<GridPoint2, GridPoint2>(PointyHexPoint.Rotate120));
						pattern.AddRange(pointyHexPoints.Select<GridPoint2, GridPoint2>(PointyHexPoint.Rotate240));

						break;

					case Symmetry2:
						pattern.AddRange(pointyHexPoints.Select<GridPoint2, GridPoint2>(PointyHexPoint.Rotate180));
						break;
				}
			}

			foreach (var point in pattern)
			{
				ToggleCellAt(point);
			}
		}

		private void ToggleCellAt(GridPoint2 centerPoint)
		{
			foreach (var point in centerPoint.GetVectorNeighbors(neighborDirectionsList).In(Grid))
			{
				var cell = Grid[point].GetComponent<SpriteCell>();

				dataGrid[point] = !dataGrid[point];
				cell.Color = dataGrid[point] ? onColor : offColor;
			}
		}
	}
}