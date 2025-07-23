using System;
using System.Collections.Generic;
using System.Linq;
using IdleCastle.Runtime.Gameplay.BuildingLogic;
using JetBrains.Annotations;


namespace IdleCastle.Runtime.Gameplay
{
	// TODO Refactor: rename это будет GameWorld
	[UsedImplicitly]
	public class BuildingManager : IDisposable
	{
		private readonly BuildingFactory    _buildingFactory;
		private readonly HashSet<IBuilding> _buildings = new();
		private readonly Wallet             _wallet    = new Wallet(); // TODO: load from save

		public event Action<ItemId, double> CurrencyChanged
		{
			add => _wallet.CurrencyChanged += value;
			remove => _wallet.CurrencyChanged -= value;
		}

		public BuildingManager (BuildingFactory buildingFactory)
		{
			_buildingFactory = buildingFactory;
		}

		public void Create<TBuilding> () where TBuilding : class, IBuilding
		{
			TBuilding building = _buildingFactory.Create<TBuilding>();

			RegisterBuilding(building);
		}

		private void RegisterBuilding<TBuilding> (TBuilding building) where TBuilding : class, IBuilding
		{
			ItemId buildingId = building.Id;

			if (_buildings.Any(b => b.Id == buildingId))
			{
				throw new InvalidOperationException($"Building with ID {buildingId.ToString()} is already registered.");
			}

			_buildings.Add(building);

			building.IncomeGenerated += HandleIncomeGenerated;
		}

		public void Tick (float deltaTime)
		{
			foreach (IBuilding building in _buildings)
			{
				building.Tick(deltaTime);
			}
		}

		private void HandleIncomeGenerated (GeneratedIncome income)
		{
			_wallet.Add(income);
		}

		public void Dispose ()
		{
			foreach (IBuilding building in _buildings)
			{
				// TODO Refactor: это должно быть в методе UnregisterBuilding
				building.IncomeGenerated -= HandleIncomeGenerated;
			}

			_buildings.Clear();
		}
	}
}
