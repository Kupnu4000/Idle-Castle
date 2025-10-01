using System;
using System.Collections.Generic;
using IdleCastle.Gameplay.Buildings;
using IdleCastle.Gameplay.GameEvents;
using IdleCastle.Zenject;
using JetBrains.Annotations;
using MessagePipe;


namespace IdleCastle.Gameplay
{
	// this has to be a model of the game world
	[UsedImplicitly]
	public class GameWorld : IDisposable
	{
		private readonly GenericFactory                _genericFactory;
		private readonly IPublisher<BuildingCreated>   _buildingCreatedPub;
		private readonly Dictionary<ItemId, IBuilding> _buildings = new();

		public GameWorld (
			GenericFactory genericFactory,
			IPublisher<BuildingCreated> buildingCreatedPub
		)
		{
			_genericFactory     = genericFactory;
			_buildingCreatedPub = buildingCreatedPub;
		}

		public void CreateBuilding<TBuilding> () where TBuilding : class, IBuilding
		{
			TBuilding building = _genericFactory.Create<TBuilding>();

			if (!_buildings.TryAdd(building.Id, building))
			{
				throw new InvalidOperationException($"Building with ID {building.Id.ToString()} is already registered.");
			}

			_buildingCreatedPub.Publish(new BuildingCreated(building));
		}

		public void Dispose ()
		{
			foreach (IBuilding building in _buildings.Values)
			{
				building.Dispose();
			}

			_buildings.Clear();
		}
	}
}
