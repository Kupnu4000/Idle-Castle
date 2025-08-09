using System.Runtime.CompilerServices;


namespace IdleCastle.Runtime.Extensions
{
	public static class FloatExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DivRem (this float value, int divisor, out float remainder)
		{
			if (divisor == 0)
			{
				remainder = 0f;
				return 0;
			}

			int quotient = (int)(value / divisor);
			remainder = value - quotient * divisor;
			return quotient;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int DivRem (this float value, float divisor, out float remainder)
		{
			if (divisor == 0)
			{
				remainder = 0f;
				return 0;
			}

			int quotient = (int)(value / divisor);
			remainder = value - quotient * divisor;
			return quotient;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Normalized (this float value, float min, float max)
		{
			if (min >= max) return 0f;

			return (value - min) / (max - min);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Normalized (this float value, float max)
		{
			if (max <= 0f) return 0f;

			return value / max;
		}
	}
}
