namespace Gamelogic.Grids2
{
	/// <summary>
	/// A grid map is used to convert from grid space to world space.
	/// </summary>
	/// <typeparam name="TDiscreteInputType">The discrete input type. Built-in examples are <see cref="int"/>,
	/// <see cref="GridPoint2"/>, and <see cref="GridPoint3"/>.</typeparam>
	/// <typeparam name="TContinuousInputType">The corresponding continuous type. Built-in
	/// examples are <see cref="float"/>, <see cref="UnityEngine.Vector2"/>, and <see cref="UnityEngine.Vector3"/>/></typeparam>
	/// <typeparam name="TOutput">The output type, typically <see cref="UnityEngine.Vector3"/>.</typeparam>
	/// <seealso cref="Gamelogic.Grids2.IGridMap{TDiscreteType, Vector3, Vector3}" />
	public interface IGridMap<TDiscreteInputType, TContinuousInputType, TOutput>
	{
		/// <summary>
		/// Converts a discrete grid point to world point.
		/// </summary>
		TOutput GridToWorld(TDiscreteInputType input);

		/// <summary>
		/// Converts a discrete grid point to world point.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>TOutput.</returns>
		TOutput GridToWorld(TContinuousInputType input);

		/// <summary>
		/// Converts a world point to a discrete world point.
		/// </summary>
		/// <param name="output">The output.</param>
		/// <returns>TDiscreteInputType.</returns>
		TDiscreteInputType WorldToGridToDiscrete(TOutput output);

		/// <summary>
		/// Converts a world point to a continuous grid point.
		/// </summary>
		/// <param name="output">The output.</param>
		/// <returns>TContinuousInputType.</returns>
		TContinuousInputType WorldToGrid(TOutput output);
	}
}