using IdleCastle.Runtime.Gameplay.Messages;
using JetBrains.Annotations;
using MessagePipe;


namespace IdleCastle.Runtime.Gameplay
{
	// TODO Refactor: может передавать конфиг в конструктор, а экземпляр класса создавать в фабрике?
	[UsedImplicitly]
	public class GoldMine : IBuilding
	{
		public ItemId Id => ItemDef.BuildingIds.GoldMine;

		public ItemId CurrencyId => ItemDef.Currencies.Gold;

		private readonly float _progressTime;

		private float _incomeTimer;

		private readonly IPublisher<IncomeGenerated> _incomePublisher;

		private const float Income = 1f; // TODO Refactor: это надо хранить в конфиге

		public GoldMine (GoldMineConfig config, IPublisher<IncomeGenerated> incomePublisher)
		{
			_incomePublisher = incomePublisher;
			_progressTime    = config.ProgressTime;
		}

		public void Tick (float deltaTime)
		{
			_incomeTimer += deltaTime;

			if (_incomeTimer >= _progressTime)
			{
				int incomeMultiplier = (int)(_incomeTimer / _progressTime);

				_incomeTimer %= _progressTime;

				float income = Income * incomeMultiplier;

				_incomePublisher.Publish(new IncomeGenerated(CurrencyId, income));
			}
		}
	}
}
