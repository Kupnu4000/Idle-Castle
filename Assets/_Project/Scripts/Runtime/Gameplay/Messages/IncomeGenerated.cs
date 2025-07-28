namespace IdleCastle.Runtime.Gameplay.Messages
{
	public readonly struct IncomeGenerated
	{
		public readonly ItemId CurrencyId;
		public readonly double Amount;

		public IncomeGenerated (ItemId currencyId, double amount)
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
