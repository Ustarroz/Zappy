using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// This is a simple event trigger for 2D grids. To use it, add it on your grid builder, and
	/// link in the methods you want to trigger in the respective fields.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.GridBehaviour{Gamelogic.Grids2.GridPoint2, Gamelogic.Grids2.TileCell}" />
	public class GridEventTrigger : GridBehaviour<GridPoint2, TileCell>
	{
		#region Inspector
		[SerializeField]
		private Camera uiCamera;

		[SerializeField]
		private GridEvent onLeftMouseButtonDown;

		[SerializeField]
		private GridEvent onRightMouseButtonDown;
		#endregion

		#region Properties		
		/// <summary>
		/// Gets the mouse position as a grid point.
		/// </summary>
		/// <value>The mouse position.</value>
		public GridPoint2 MousePosition
		{
			get
			{
				return GridUtils.MousePositionToGrid(transform, uiCamera, GridMap);
			}
		}

		public Camera UICamera
		{
			get { return uiCamera; }
			set { uiCamera = value; }
		}

		public GridEvent OnLeftMouseButtonDown
		{
			get
			{
				if (onLeftMouseButtonDown == null)
				{
					onLeftMouseButtonDown = new GridEvent();
				}

				return onLeftMouseButtonDown;
			}
		}

		public GridEvent OnRightMouseButtonDown
		{
			get
			{
				if (onRightMouseButtonDown == null)
				{
					onRightMouseButtonDown = new GridEvent();
				}

				return onRightMouseButtonDown;
			}
		}

		#endregion

		#region Messages
		public void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (Grid.Contains(MousePosition))
				{
					if (onLeftMouseButtonDown != null)
					{
						onLeftMouseButtonDown.Invoke(MousePosition);
					}
				}
			}

			if (Input.GetMouseButtonDown(1))
			{
				if (Grid.Contains(MousePosition))
				{
					if (onRightMouseButtonDown != null)
					{
						onRightMouseButtonDown.Invoke(MousePosition);
					}
				}
			}
		}

		#endregion
	}
}