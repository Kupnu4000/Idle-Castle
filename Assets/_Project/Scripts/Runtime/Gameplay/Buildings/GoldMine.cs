using IdleCastle.Extensions;
using IdleCastle.Gameplay.GameEvents;
using IdleCastle.Gameplay.GameTime;
using JetBrains.Annotations;
using MessagePipe;


namespace IdleCastle.Gameplay.Buildings
{
	[UsedImplicitly]
	public class GoldMine : IBuilding, ITickable
	{
		public ItemId Id => ItemDef.BuildingIds.GoldMine;

		public ItemId CurrencyId => ItemDef.Currencies.Gold;

		private readonly ITickRunner                   _tickRunner;
		private readonly IPublisher<CurrencyGenerated> _currencyGeneratedPub;
		private readonly float                         _productionTime;

		private float _productionProgress;

		public float NormalizedProgress => _productionProgress.Normalized(_productionTime);

		private const float Income = 1f; // TODO Refactor: это надо хранить в конфиге

		public GoldMine (
			ITickRunner tickRunner,
			IPublisher<CurrencyGenerated> currencyGeneratedPub,
			GoldMineConfig config
		)
		{
			_tickRunner           = tickRunner;
			_currencyGeneratedPub = currencyGeneratedPub;
			_productionTime       = config.ProgressTime;

			_tickRunner.Register(this);
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

		public void Dispose ()
		{
			_tickRunner.Unregister(this);
		}
	}
}
