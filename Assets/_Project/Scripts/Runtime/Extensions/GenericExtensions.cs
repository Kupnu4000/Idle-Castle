using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace IdleCastle.Runtime.Extensions
{
	public static class GenericExtensions
	{
		public static bool IsInRange<T> (this T item, T start, T end) where T : IComparable<T>
		{
			return Comparer<T>.Default.Compare(item, start) >= 0 &&
			       Comparer<T>.Default.Compare(item, end) <= 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInRange<T> (this T item, T min, T max, Comparer<T> comparer) where T : IComparable<T>
		{
			return comparer.Compare(item, min) >= 0 &&
			       comparer.Compare(item, max) <= 0;
		}
	}
}
