//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	[AddComponentMenu("Gamelogic/Grids2/Examples/Diffusion")]
	public class DiffusionExample : GridBehaviour<GridPoint2, TileCell>
	{
		public Gradient temperatureGradient = new Gradient
		{
			alphaKeys = new GradientAlphaKey[0],
			colorKeys = new[]
						{
							new GradientColorKey(Color.red, 4),
							new GradientColorKey(Color.yellow, 7),
						}
		};

		private IGrid<GridPoint2, float> gas;
		private GridEventTrigger eventTrigger;

		public void Start()
		{
			gas = Grid.CloneStructure<float>();
			gas.Fill(0);

			eventTrigger = this.GetRequiredComponent<GridEventTrigger>();
		}

		private float CalculateAverage(IEnumerable<GridPoint2> neighbors)
		{
			float sum = neighbors
				.Where(gas.Contains)
				.Select(x => gas[x])
				.Aggregate((p, q) => p + q);

			return sum / (neighbors.Count());
		}

		public void Update()
		{
			ProcessInput();

			Grids2.Algorithms.AggregateNeighborhood(gas, PointyHexPoint.GetOrthogonalNeighbors, CalculateAverage);

			foreach (var point in gas.Points)
			{
				UpdateCell(point);
			}
		}

		private void ProcessInput()
		{
			if (Input.GetMouseButton(0))
			{
				var gridPoint = eventTrigger.MousePosition;

				if (Grid.Contains(gridPoint))
				{
					gas[gridPoint] = 2;
				}
			}

			if (Input.GetMouseButton(1))
			{
				var gridPoint = eventTrigger.MousePosition;

				if (Grid.Contains(gridPoint))
				{
					gas[gridPoint] = 0;
				}
			}
		}

		private void UpdateCell(GridPoint2 point)
		{
			var newColor = temperatureGradient.Evaluate(gas[point]);
			Grid[point].GetComponent<SpriteCell>().Color = newColor;
		}
	}
}
