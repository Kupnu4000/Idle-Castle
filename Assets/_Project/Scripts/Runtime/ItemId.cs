using System;


namespace IdleCastle.Runtime
{
	public readonly struct ItemId : IEquatable<ItemId>
	{
		private readonly string _value;

		public ItemId (string value)
		{
			_value = value;
		}

		public override bool Equals (object obj)
		{
			return obj is ItemId itemId && _value == itemId._value;
		}

		public bool Equals (ItemId other)
		{
			return _value == other._value;
		}

		public static bool operator == (ItemId left, ItemId right)
		{
			return left.Equals(right);
		}

		public static bool operator != (ItemId left, ItemId right)
		{
			return !left.Equals(right);
		}

		public override string ToString ()
		{
			return _value;
		}

		public override int GetHashCode ()
		{
			return _value.GetHashCode();
		}
	}
}
