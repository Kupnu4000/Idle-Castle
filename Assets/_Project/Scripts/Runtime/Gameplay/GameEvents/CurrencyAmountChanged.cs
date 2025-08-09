namespace IdleCastle.Runtime.Gameplay.GameEvents
{
	public readonly struct CurrencyAmountChanged
	{
		public readonly ItemId CurrencyId;
		public readonly double NewValue;

		public CurrencyAmountChanged (ItemId currencyId, double newValue)
		{
			CurrencyId = currencyId;
			NewValue   = newValue;
		}

		public override string ToString ()
		{
			return $"Generated [Currency: {CurrencyId}, Amount: {NewValue}]";
		}
	}
}
