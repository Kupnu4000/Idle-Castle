namespace GoblinFortress.Runtime.Gameplay.GameEvents
{
	public readonly struct CurrencyGenerated
	{
		public readonly ItemId CurrencyId;
		public readonly double Amount;

		public CurrencyGenerated (ItemId currencyId, double amount)
		{
			CurrencyId = currencyId;
			Amount     = amount;
		}

		public override string ToString ()
		{
			return $"Generated [Currency: {CurrencyId}, Amount: {Amount}]";
		}
	}
}
