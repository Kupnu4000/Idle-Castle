using GoblinFortress.Runtime.Extensions;
using GoblinFortress.Runtime.Gameplay.GameEvents;
using JetBrains.Annotations;
using MessagePipe;


namespace GoblinFortress.Runtime.Gameplay.Buildings
{
	[UsedImplicitly]
	public class GoldMine : IBuilding
	{
		public ItemId Id => ItemDef.BuildingIds.GoldMine;

		public ItemId CurrencyId => ItemDef.Currencies.Gold;

		private readonly float _productionTime;

		private float _productionProgress;

		private readonly IPublisher<CurrencyGenerated> _currencyGeneratedPub;

		public float NormalizedProgress => _productionProgress.Normalized(_productionTime);

		private const float Income = 1f; // TODO Refactor: это надо хранить в конфиге

		public GoldMine (GoldMineConfig config, IPublisher<CurrencyGenerated> currencyGeneratedPub)
		{
			_currencyGeneratedPub = currencyGeneratedPub;
			_productionTime       = config.ProgressTime;
		}

		public void Tick (float deltaTime)
		{
			_productionProgress += deltaTime;

			if (_productionProgress >= _productionTime)
			{
				int incomeMultiplier = (int)(_productionProgress / _productionTime);

				_productionProgress %= _productionTime;

				float income = Income * incomeMultiplier;

				_currencyGeneratedPub.Publish(new CurrencyGenerated(CurrencyId, income));
			}
		}
	}
}
