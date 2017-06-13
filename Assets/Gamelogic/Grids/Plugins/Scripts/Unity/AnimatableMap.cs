//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids
{
	/// <summary>
	/// A map that changes over time.
	/// </summary>
	[Version(1)]
	public class AnimatableMap<TPoint> : AbstractMap<TPoint>
		where TPoint : IGridPoint<TPoint>
	{
		readonly IMap<TPoint> baseMap;
		readonly Func<Vector2, float, Vector2> animation;
		readonly Func<Vector2, float, Vector2> inverseAnimation;

		public AnimatableMap(
			Vector2 cellDimensions,
			IMap<TPoint> baseMap,
			Func<Vector2, float, Vector2> animation,
			Func<Vector2, float, Vector2> inverseAnimation) :
			base(cellDimensions)
		{
			this.baseMap = baseMap;
			this.animation = animation;
			this.inverseAnimation = inverseAnimation;
			gridPointTransform = baseMap.GridPointTransform;
			inverseGridPointTransform = baseMap.InverseGridPointTransform;
		}

		public AnimatableMap(
			Vector2 cellDimensions,
			IMap<TPoint> baseMap,
			Func<Vector2, float, Vector2> animation) :
			this(cellDimensions, baseMap, animation, (x, t) => animation(x, -t))
		{ }

		public override TPoint RawWorldToGrid(Vector2 worldPoint)
		{
			return baseMap.RawWorldToGrid(Application.isPlaying ?
				inverseAnimation(worldPoint, Time.time) :
				inverseAnimation(worldPoint, Time.realtimeSinceStartup));
		}

		public override Vector2 GridToWorld(TPoint gridPoint)
		{
			return animation(baseMap.GridToWorld(gridPoint), Application.isPlaying ?
				Time.time :
				Time.realtimeSinceStartup);
		}
	}

	/// <summary>
	/// These functions are defined as extensions so that IMap can remain pure (that is, not
	/// access Time.time).
	/// 
	/// These methods are implemented as extension methods so that they do not
	/// become part of the "pure" interface of IMap. (Time.time is a "non-pure", 
	/// very Unity-specific feature).
	/// </summary>
	public static class MapAnimationExtensions
	{
		/// <summary>
		/// Only use this method if animation(x, -t) is the inverse of animation(x, t).
		/// </summary>
		public static IMap<TPoint> Animate<TPoint>(this IMap<TPoint> map, Func<Vector2, float, Vector2> animation)
			where TPoint : IGridPoint<TPoint>
		{
			return new AnimatableMap<TPoint>(map.GetCellDimensions(), map, animation);
		}

		/// <summary>
		/// Animates this grid using a function animation that takes a point and time. 
		/// The inverse animation is the inverse mapping at time t, that is,
		/// <code>
		/// inverseAmimation(animation(someVector, t), t) == someVector
		/// </code>
		/// </summary>
		/// <example>
		/// Example:
		/// <code>
		/// map = new PointyHexMap(hexDimensions).AnchorCellMiddleCenter()
		///		.WithWindow(ExampleUtils.ScreenRect)
		///		.AlignMiddleCenter(grid) 
		/// //Rotate
		///		.Animate((x, t) =&gt; x.Rotate(45 * t), (x, t) =&gt; x.Rotate(-45 * t)) 
		/// //Translate
		///		.Animate((x, t) =&gt; x + new Vector2(75 * Mathf.Sin(t * 5), 0),
		///			(x, t) =&gt; x - new Vector2(75 * Mathf.Sin(t * 5), 0)) 
		/// //Scale
		///		.Animate((x, t) =&gt; x * (1 + Mathf.Sin(t * 7)),
		///			 (x, t) =&gt; x / (1 + Mathf.Sin(t * 7)));
		/// </code>
		/// </example>
		public static IMap<TPoint> Animate<TPoint>(this IMap<TPoint> map, Func<Vector2, float, Vector2> animation, Func<Vector2, float, Vector2> inverseAnimation)
			where TPoint : IGridPoint<TPoint>
		{
			return new AnimatableMap<TPoint>(map.GetCellDimensions(), map, animation, inverseAnimation);
		}
	}
}
