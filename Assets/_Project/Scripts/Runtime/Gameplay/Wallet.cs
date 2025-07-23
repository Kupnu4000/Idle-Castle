using System;
using System.Collections.Generic;
using IdleCastle.Runtime.Gameplay.BuildingLogic;


namespace IdleCastle.Runtime.Gameplay
{
	public class Wallet
	{
		private readonly Dictionary<ItemId, double> _currencies = new();

		public event Action<ItemId, double> CurrencyChanged; // TODO Refactor: использовать кастомный тип

		// TODO: optimize
		public void Add (GeneratedIncome income)
		{
			_currencies.TryAdd(income.CurrencyId, 0);

			_currencies[income.CurrencyId] += income.Amount;

			CurrencyChanged?.Invoke(income.CurrencyId, _currencies[income.CurrencyId]);
		}
	}
}
