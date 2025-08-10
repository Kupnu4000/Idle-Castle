using System;
using System.Collections.Generic;
using GoblinFortress.Runtime.Gameplay.Buildings;
using GoblinFortress.Runtime.Gameplay.GameEvents;
using GoblinFortress.Runtime.Zenject;
using JetBrains.Annotations;
using MessagePipe;


namespace GoblinFortress.Runtime.Gameplay
{
	// this has to be a model of the game world
	[UsedImplicitly]
	public class GameWorld : IDisposable
	{
		private readonly GenericFactory                _genericFactory;
		private readonly ITickRunner                   _tickRunner;
		private readonly IPublisher<BuildingCreated>   _buildingCreatedPub;
		private readonly Dictionary<ItemId, IBuilding> _buildings = new();

		public GameWorld (
			GenericFactory genericFactory,
			ITickRunner tickRunner,
			IPublisher<BuildingCreated> buildingCreatedPub
		)
		{
			_genericFactory     = genericFactory;
			_tickRunner         = tickRunner;
			_buildingCreatedPub = buildingCreatedPub;
		}

		public void Create<TBuilding> () where TBuilding : class, IBuilding
		{
			TBuilding building = _genericFactory.Create<TBuilding>();

			if (!_buildings.TryAdd(building.Id, building))
			{
				throw new InvalidOperationException($"Building with ID {building.Id.ToString()} is already registered.");
			}

			_buildingCreatedPub.Publish(new BuildingCreated(building));

			_tickRunner.OnTick += building.Tick; // TODO Refactor: эта подписка может находиться в самом здании, а не в GameWorld
		}

		public void Dispose ()
		{
			foreach (IBuilding building in _buildings.Values)
			{
				_tickRunner.OnTick -= building.Tick;
			}

			_buildings.Clear();
		}
	}
}
