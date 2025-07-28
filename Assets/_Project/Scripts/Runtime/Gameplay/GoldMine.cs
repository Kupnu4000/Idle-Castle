using IdleCastle.Runtime.Gameplay.Messages;
using JetBrains.Annotations;
using MessagePipe;
using UnityEngine;


namespace IdleCastle.Runtime.Gameplay
{
	// TODO Refactor: может передавать конфиг в конструктор, а экземпляр класса создавать в фабрике?
	[UsedImplicitly]
	public class GoldMine : IBuilding
	{
		public ItemId Id => ItemDef.BuildingIds.GoldMine;

		public ItemId CurrencyId => ItemDef.Currencies.Gold;

		private readonly float _productionTime;

		private float _productionProgress;

		private readonly IPublisher<IncomeGenerated> _incomePublisher;

		public float NormalizedProgress =>
			Mathf.Clamp01(_productionProgress / _productionTime);

		private const float Income = 1f; // TODO Refactor: это надо хранить в конфиге

		public GoldMine (GoldMineConfig config, IPublisher<IncomeGenerated> incomePublisher)
		{
			_incomePublisher = incomePublisher;
			_productionTime  = config.ProgressTime;
		}

		public void Tick (float deltaTime)
		{
			_productionProgress += deltaTime;

			if (_productionProgress >= _productionTime)
			{
				int incomeMultiplier = (int)(_productionProgress / _productionTime);

				_productionProgress %= _productionTime;

				float income = Income * incomeMultiplier;

				_incomePublisher.Publish(new IncomeGenerated(CurrencyId, income));
			}
		}
	}
}
