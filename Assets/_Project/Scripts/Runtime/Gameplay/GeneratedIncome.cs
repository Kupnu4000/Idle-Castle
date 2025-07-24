namespace IdleCastle.Runtime.Gameplay
{
	public readonly struct GeneratedIncome
	{
		public readonly ItemId CurrencyId;
		public readonly double Amount;

		public GeneratedIncome (ItemId currencyId, double amount)
		{
			CurrencyId = currencyId;
			Amount     = amount;
		}

		public override string ToString ()
		{
			return $"Generated [CurrencyId: {CurrencyId}, Amount: {Amount}]";
		}
	}
}
