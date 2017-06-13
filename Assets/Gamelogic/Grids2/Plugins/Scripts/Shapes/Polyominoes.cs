using System.Collections.Generic;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Contains functions for making polyomino shape grids.
	/// </summary>
	[Version(2, 3)]
	public static class Polyominoes
	{
		/// <summary>
		/// The monomino grid shape (a single square).
		/// </summary>
		public static readonly IExplicitShape<GridPoint2> Monomino = ExplicitShape.Bitmask(new[] { "1" });

		/// <summary>
		/// The domino grid shape (two connected squares).
		/// </summary>
		public static readonly IExplicitShape<GridPoint2> Domino = ExplicitShape.Bitmask(new[] { "11" });

		/// <summary>
		/// Contains functions for making tromino shapes (three connected squares).
		/// </summary>
		public class Tromino
		{
			/// <summary>
			/// Represents the tromino type by their standard names.
			/// </summary>
			public enum Type
			{
				/// <summary>
				/// The I tromino (three squares in a row).
				/// </summary>
				I,

				/// <summary>
				/// The L tromino (three squares connected in a L-shape).
				/// </summary>
				L
			}

			private static readonly string[] IMask = new[] { "1", "1", "1" };
			private static readonly string[] LMask = new[] { "10", "11" };

			/// <summary>
			/// The I tromino shape.
			/// </summary>
			public static readonly IExplicitShape<GridPoint2> IShape = ExplicitShape.Bitmask(IMask);

			/// <summary>
			/// The L tromino shape.
			/// </summary>
			public static readonly IExplicitShape<GridPoint2> LShape = ExplicitShape.Bitmask(LMask);

			/// <summary>
			/// The dictionary that maps tromino Types with their associated grid shapes.
			/// </summary>
			public static readonly Dictionary<Type, IExplicitShape<GridPoint2>> Shapes = new Dictionary<Type, IExplicitShape<GridPoint2>>
			{
				{Type.I, IShape },
				{Type.L, LShape }
			};
		}

		/// <summary>
		/// Contains functions for making tetromino shapes (four connected squares).
		/// </summary>
		public class Tetromino
		{
			/// <summary>
			/// Represents the tetromino types by their standard names.
			/// </summary>
			public enum Type
			{
				I,
				O,
				Z,
				T,
				L
			}

			private static readonly string[] IMask = new[] { "1", "1", "1", "1" };
			private static readonly string[] LMask = new[] { "10", "10", "11" };
			private static readonly string[] OMask = new[] { "11", "11" };
			private static readonly string[] ZMask = new[] { "110", "011" };
			private static readonly string[] TMask = new[] { "111", "010" };

			public static readonly IExplicitShape<GridPoint2> IShape = ExplicitShape.Bitmask(IMask);
			public static readonly IExplicitShape<GridPoint2> LShape = ExplicitShape.Bitmask(LMask);
			public static readonly IExplicitShape<GridPoint2> OShape = ExplicitShape.Bitmask(OMask);
			public static readonly IExplicitShape<GridPoint2> ZShape = ExplicitShape.Bitmask(ZMask);
			public static readonly IExplicitShape<GridPoint2> TShape = ExplicitShape.Bitmask(TMask);

			/// <summary>
			/// The dictionary that maps tetromino Types with their associated grid shapes.
			/// </summary>
			public static readonly Dictionary<Type, IExplicitShape<GridPoint2>> Shapes = new Dictionary<Type, IExplicitShape<GridPoint2>>
			{
				{Type.I, IShape },
				{Type.L, LShape },
				{Type.O, OShape },
				{Type.Z, ZShape },
				{Type.T, TShape },
			};
		}

		/// <summary>
		/// Contains functions for making pentomino shapes (five connected squares).
		/// </summary>
		public class Pentomino
		{

			/// <summary>
			/// Represents the pentomino types by their standard names.
			/// </summary>
			public enum Type
			{
				F,
				I,
				L,
				N,
				P,
				T,
				U,
				V,
				W,
				X,
				Y,
				Z
			}

			private static readonly string[] FMask = new[] { "011", "110", "010" };
			private static readonly string[] IMask = new[] { "1", "1", "1", "1", "1" };
			private static readonly string[] LMask = new[] { "10", "10", "10", "11" };
			private static readonly string[] NMask = new[] { "1100", "0111" };
			private static readonly string[] PMask = new[] { "11", "11", "10" };
			private static readonly string[] TMask = new[] { "111", "010", "010" };
			private static readonly string[] UMask = new[] { "101", "111" };
			private static readonly string[] VMask = new[] { "100", "100", "111" };
			private static readonly string[] WMask = new[] { "100", "110", "011" };
			private static readonly string[] XMask = new[] { "010", "111", "010" };
			private static readonly string[] YMask = new[] { "0010", "1111" };
			private static readonly string[] ZMask = new[] { "110", "010", "011" };

			public static readonly IExplicitShape<GridPoint2> FShape = ExplicitShape.Bitmask(FMask);
			public static readonly IExplicitShape<GridPoint2> IShape = ExplicitShape.Bitmask(IMask);
			public static readonly IExplicitShape<GridPoint2> LShape = ExplicitShape.Bitmask(LMask);
			public static readonly IExplicitShape<GridPoint2> NShape = ExplicitShape.Bitmask(NMask);
			public static readonly IExplicitShape<GridPoint2> PShape = ExplicitShape.Bitmask(PMask);

			public static readonly IExplicitShape<GridPoint2> TShape = ExplicitShape.Bitmask(TMask);
			public static readonly IExplicitShape<GridPoint2> UShape = ExplicitShape.Bitmask(UMask);
			public static readonly IExplicitShape<GridPoint2> VShape = ExplicitShape.Bitmask(VMask);
			public static readonly IExplicitShape<GridPoint2> WShape = ExplicitShape.Bitmask(WMask);
			public static readonly IExplicitShape<GridPoint2> XShape = ExplicitShape.Bitmask(XMask);

			public static readonly IExplicitShape<GridPoint2> YShape = ExplicitShape.Bitmask(YMask);
			public static readonly IExplicitShape<GridPoint2> ZShape = ExplicitShape.Bitmask(ZMask);

			/// <summary>
			/// The dictionary that maps pentomino Types with their associated grid shapes.
			/// </summary>
			public static readonly Dictionary<Type, IExplicitShape<GridPoint2>> Shapes = new Dictionary<Type, IExplicitShape<GridPoint2>>
			{
				{Type.F, FShape },
				{Type.I, IShape },
				{Type.L, LShape },
				{Type.N, NShape },
				{Type.P, PShape },

				{Type.T, TShape },
				{Type.U, UShape },
				{Type.V, VShape },
				{Type.W, WShape },
				{Type.X, XShape },

				{Type.Y, YShape },
				{Type.Z, ZShape },
			};
		}

		/// <summary>
		/// Contains functions for making hexomino shapes (six connected squares).
		/// </summary>
		public class Hexomino
		{
			/// <summary>
			/// Represents the hexomino types by their standard names.
			/// </summary>
			public enum Type
			{
				A,
				C,
				D,
				E,
				HighF,
				LowF,
				G,
				H,
				I,
				J,
				K,
				L,
				M,
				LongN,
				ShortN,
				O,
				P,
				Q,
				R,
				S,
				TallT,
				ShortT,
				U,
				V,
				Wa,
				Wb,
				Wc,
				X,
				ItalicX,
				HighY,
				LowY,
				ShortZ,
				TallZ,
				High4,
				Low4
			}

			private static readonly string[] AMask = new[] { "111", "110", "100" };
			private static readonly string[] CMask = new[] { "11", "10", "10", "11" };
			private static readonly string[] DMask = new[] { "10", "11", "11", "10" };
			private static readonly string[] EMask = new[] { "011", "110", "011" };
			private static readonly string[] HighFMask = new[] { "011", "110", "010", "010" };
			private static readonly string[] LowFMask = new[] { "011", "010", "110", "010" };
			private static readonly string[] GMask = new[] { "11", "10", "11", "01" };
			private static readonly string[] HMask = new[] { "100", "111", "101" };
			private static readonly string[] IMask = new[] { "1", "1", "1", "1", "1", "1" };
			private static readonly string[] JMask = new[] { "001", "101", "111" };

			private static readonly string[] KMask = new[] { "010", "111", "110" };
			private static readonly string[] LMask = new[] { "10", "10", "10", "10", "11" };
			private static readonly string[] MMask = new[] { "011", "010", "110", "100" };
			private static readonly string[] LongNMask = new[] { "01", "11", "10", "10", "10" };
			private static readonly string[] ShortNMask = new[] { "01", "11", "11", "10" };
			private static readonly string[] OMask = new[] { "11", "11", "11" };
			private static readonly string[] PMask = new[] { "11", "11", "10", "10" };
			private static readonly string[] QMask = new[] { "110", "110", "011" };
			private static readonly string[] RMask = new[] { "110", "111", "100" };
			private static readonly string[] SMask = new[] { "10", "10", "11", "01", "01" };

			private static readonly string[] TallTMask = new[] { "111", "010", "010", "010" };
			private static readonly string[] ShortTMask = new[] { "1111", "0100", "0100" };
			private static readonly string[] UMask = new[] { "1010", "1111" };
			private static readonly string[] VMask = new[] { "1000", "1000", "1111" };
			private static readonly string[] WaMask = new[] { "1000", "1100", "0111" };
			private static readonly string[] WbMask = new[] { "100", "110", "011", "001" };
			private static readonly string[] WcMask = new[] { "100", "110", "011", "010" };
			private static readonly string[] XMask = new[] { "0100", "1111", "0100" };
			private static readonly string[] ItalicXMask = new[] { "010", "110", "011", "010" };
			private static readonly string[] HighYMask = new[] { "01", "11", "01", "01", "01" };

			private static readonly string[] LowYMask = new[] { "01", "01", "11", "01", "01" };
			private static readonly string[] ShortZMask = new[] { "1100", "0100", "0111" };
			private static readonly string[] TallZMask = new[] { "110", "010", "010", "011" };
			private static readonly string[] High4Mask = new[] { "100", "111", "010", "010" };
			private static readonly string[] Low4Mask = new[] { "100", "100", "111", "010" };

			public static readonly IExplicitShape<GridPoint2> AShape = ExplicitShape.Bitmask(AMask);
			public static readonly IExplicitShape<GridPoint2> CShape = ExplicitShape.Bitmask(CMask);
			public static readonly IExplicitShape<GridPoint2> DShape = ExplicitShape.Bitmask(DMask);
			public static readonly IExplicitShape<GridPoint2> EShape = ExplicitShape.Bitmask(EMask);
			public static readonly IExplicitShape<GridPoint2> LowFShape = ExplicitShape.Bitmask(LowFMask);

			public static readonly IExplicitShape<GridPoint2> HighFShape = ExplicitShape.Bitmask(HighFMask);
			public static readonly IExplicitShape<GridPoint2> GShape = ExplicitShape.Bitmask(GMask);
			public static readonly IExplicitShape<GridPoint2> HShape = ExplicitShape.Bitmask(HMask);
			public static readonly IExplicitShape<GridPoint2> IShape = ExplicitShape.Bitmask(IMask);
			public static readonly IExplicitShape<GridPoint2> JShape = ExplicitShape.Bitmask(JMask);

			public static readonly IExplicitShape<GridPoint2> KShape = ExplicitShape.Bitmask(KMask);
			public static readonly IExplicitShape<GridPoint2> LShape = ExplicitShape.Bitmask(LMask);
			public static readonly IExplicitShape<GridPoint2> MShape = ExplicitShape.Bitmask(MMask);
			public static readonly IExplicitShape<GridPoint2> LongNShape = ExplicitShape.Bitmask(LongNMask);
			public static readonly IExplicitShape<GridPoint2> ShortNShape = ExplicitShape.Bitmask(ShortNMask);

			public static readonly IExplicitShape<GridPoint2> OShape = ExplicitShape.Bitmask(OMask);
			public static readonly IExplicitShape<GridPoint2> PShape = ExplicitShape.Bitmask(PMask);
			public static readonly IExplicitShape<GridPoint2> QShape = ExplicitShape.Bitmask(QMask);
			public static readonly IExplicitShape<GridPoint2> RShape = ExplicitShape.Bitmask(RMask);
			public static readonly IExplicitShape<GridPoint2> SShape = ExplicitShape.Bitmask(SMask);

			public static readonly IExplicitShape<GridPoint2> TallTShape = ExplicitShape.Bitmask(TallTMask);
			public static readonly IExplicitShape<GridPoint2> ShortTShape = ExplicitShape.Bitmask(ShortTMask);
			public static readonly IExplicitShape<GridPoint2> UShape = ExplicitShape.Bitmask(UMask);
			public static readonly IExplicitShape<GridPoint2> VShape = ExplicitShape.Bitmask(VMask);
			public static readonly IExplicitShape<GridPoint2> WaShape = ExplicitShape.Bitmask(WaMask);

			public static readonly IExplicitShape<GridPoint2> WbShape = ExplicitShape.Bitmask(WbMask);
			public static readonly IExplicitShape<GridPoint2> WcShape = ExplicitShape.Bitmask(WcMask);
			public static readonly IExplicitShape<GridPoint2> XShape = ExplicitShape.Bitmask(XMask);
			public static readonly IExplicitShape<GridPoint2> ItalicXShape = ExplicitShape.Bitmask(ItalicXMask);
			public static readonly IExplicitShape<GridPoint2> HighYShape = ExplicitShape.Bitmask(HighYMask);

			public static readonly IExplicitShape<GridPoint2> LowYShape = ExplicitShape.Bitmask(LowYMask);
			public static readonly IExplicitShape<GridPoint2> TallZShape = ExplicitShape.Bitmask(TallZMask);
			public static readonly IExplicitShape<GridPoint2> ShortZShape = ExplicitShape.Bitmask(ShortZMask);
			public static readonly IExplicitShape<GridPoint2> High4Shape = ExplicitShape.Bitmask(High4Mask);
			public static readonly IExplicitShape<GridPoint2> Low4Shape = ExplicitShape.Bitmask(Low4Mask);

			/// <summary>
			/// The dictionary that maps hexomino Types with their associated grid shapes.
			/// </summary>
			public static readonly Dictionary<Type, IExplicitShape<GridPoint2>> Shapes = new Dictionary
				<Type, IExplicitShape<GridPoint2>>
			{
				{Type.A, AShape},
				{Type.C, CShape},
				{Type.D, DShape},
				{Type.E, EShape},
				{Type.LowF, LowFShape},

				{Type.HighF, HighFShape},
				{Type.G, GShape},
				{Type.H, HShape},
				{Type.I, IShape},
				{Type.J, JShape},

				{Type.K, KShape},
				{Type.L, LShape},
				{Type.M, MShape},
				{Type.LongN, LongNShape},
				{Type.ShortN, ShortNShape},

				{Type.O, OShape},
				{Type.P, PShape},
				{Type.Q, QShape},
				{Type.R, RShape},
				{Type.S, SShape},

				{Type.TallT, TallTShape},
				{Type.ShortT, ShortTShape},
				{Type.U, UShape},
				{Type.V, VShape},
				{Type.Wa, WaShape},

				{Type.Wb, WbShape},
				{Type.Wc, WcShape},
				{Type.X, XShape},
				{Type.ItalicX, ItalicXShape},
				{Type.HighY, HighYShape},

				{Type.LowY, LowYShape},
				{Type.TallZ, TallZShape},
				{Type.ShortZ, ShortZShape},
				{Type.High4, High4Shape},
				{Type.Low4, Low4Shape},
			};
		}
	}
}