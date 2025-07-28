using System;
using System.Collections.Generic;
using IdleCastle.Runtime.Gameplay.Messages;
using IdleCastle.Runtime.Zenject;
using JetBrains.Annotations;
using MessagePipe;


namespace IdleCastle.Runtime.Gameplay
{
	[UsedImplicitly]
	public class GameWorld : IDisposable
	{
		private readonly GenericFactory                _genericFactory;
		private readonly ITickRunner                   _tickRunner;
		private readonly IPublisher<BuildingCreated>   _buildingCreated;
		private readonly Dictionary<ItemId, IBuilding> _buildings = new();

		public GameWorld (
			GenericFactory genericFactory,
			ITickRunner tickRunner,
			IPublisher<BuildingCreated> buildingCreated
		)
		{
			_genericFactory  = genericFactory;
			_tickRunner      = tickRunner;
			_buildingCreated = buildingCreated;
		}

		public void Create<TBuilding> () where TBuilding : class, IBuilding
		{
			TBuilding building = _genericFactory.Create<TBuilding>();

			if (!_buildings.TryAdd(building.Id, building))
			{
				throw new InvalidOperationException($"Building with ID {building.Id.ToString()} is already registered.");
			}

			_buildingCreated.Publish(new BuildingCreated(building));

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
