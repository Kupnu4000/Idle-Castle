using System;
using System.Collections.Generic;
using IdleCastle.Runtime.Gameplay.Messages;
using JetBrains.Annotations;
using MessagePipe;


namespace IdleCastle.Runtime.Gameplay
{
	[UsedImplicitly]
	public class Wallet : IDisposable
	{
		private readonly Dictionary<ItemId, double> _currencies = new();

		private readonly IPublisher<CurrencyAmountChanged> _currencyAmountChanged;

		private IDisposable _incomeGenerated;

		public Wallet (
			IPublisher<CurrencyAmountChanged> currencyAmountChanged,
			ISubscriber<IncomeGenerated> incomeGenerated
		)
		{
			_currencyAmountChanged = currencyAmountChanged;

			_incomeGenerated = incomeGenerated.Subscribe(Add);
		}

		private void Add (IncomeGenerated income)
		{
			_currencies.TryAdd(income.CurrencyId, 0);

			_currencies[income.CurrencyId] += income.Amount;

			_currencyAmountChanged.Publish(new CurrencyAmountChanged(income.CurrencyId, _currencies[income.CurrencyId]));
		}

		public void Dispose ()
		{
			_incomeGenerated.Dispose();
			_incomeGenerated = null;
		}
	}
}
