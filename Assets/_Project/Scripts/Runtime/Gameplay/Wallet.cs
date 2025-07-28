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

			_incomeGenerated = incomeGenerated.Subscribe(HandleIncomeGenerated);
		}

		private void HandleIncomeGenerated (IncomeGenerated @event)
		{
			_currencies.TryAdd(@event.CurrencyId, 0);

			_currencies[@event.CurrencyId] += @event.Amount;

			_currencyAmountChanged.Publish(new CurrencyAmountChanged(@event.CurrencyId, _currencies[@event.CurrencyId]));
		}

		public void Dispose ()
		{
			_incomeGenerated.Dispose();
			_incomeGenerated = null;
		}
	}
}
