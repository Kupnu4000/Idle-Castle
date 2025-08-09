using System;
using System.Collections.Generic;
using IdleCastle.Runtime.Gameplay.GameEvents;
using JetBrains.Annotations;
using MessagePipe;


namespace IdleCastle.Runtime.Gameplay
{
	[UsedImplicitly]
	public class Wallet : IDisposable
	{
		private readonly Dictionary<ItemId, double> _currencies = new();

		private readonly IPublisher<CurrencyAmountChanged> _currencyAmountChangedPub;

		private IDisposable _incomeGenerated;

		public Wallet (
			IPublisher<CurrencyAmountChanged> currencyAmountChangedPub,
			ISubscriber<CurrencyGenerated> currencyGeneratedSub
		)
		{
			_currencyAmountChangedPub = currencyAmountChangedPub;

			_incomeGenerated = currencyGeneratedSub.Subscribe(HandleIncomeGenerated);
		}

		private void HandleIncomeGenerated (CurrencyGenerated @event)
		{
			_currencies.TryAdd(@event.CurrencyId, 0);

			_currencies[@event.CurrencyId] += @event.Amount;

			_currencyAmountChangedPub.Publish(new CurrencyAmountChanged(@event.CurrencyId, _currencies[@event.CurrencyId]));
		}

		public void Dispose ()
		{
			_incomeGenerated.Dispose();
			_incomeGenerated = null;
		}
	}
}
