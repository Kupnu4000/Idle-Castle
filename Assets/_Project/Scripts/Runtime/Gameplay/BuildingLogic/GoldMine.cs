using System;
using JetBrains.Annotations;


namespace IdleCastle.Runtime.Gameplay.BuildingLogic
{
	// TODO Refactor: может передавать конфиг в конструктор, а экземпляр класса создавать в фабрике?
	[UsedImplicitly]
	public class GoldMine : IBuilding
	{
		public ItemId Id => ItemDef.BuildingIds.GoldMine;

		public ItemId CurrencyId => ItemDef.Currencies.Gold;

		public event Action<GeneratedIncome> IncomeGenerated;

		private readonly float _progressTime;

		private float _incomeTimer;

		private const float Income = 1f; // TODO Refactor: это надо хранить в конфиге

		public GoldMine (GoldMineConfig config)
		{
			_progressTime = config.ProgressTime;
		}

		public void Tick (float deltaTime)
		{
			_incomeTimer += deltaTime;

			if (_incomeTimer >= _progressTime)
			{
				int incomeMultiplier = (int)(_incomeTimer / _progressTime);

				_incomeTimer %= _progressTime;

				GeneratedIncome income = new(CurrencyId, Income * incomeMultiplier);

				IncomeGenerated?.Invoke(income);
			}
		}
	}
}
