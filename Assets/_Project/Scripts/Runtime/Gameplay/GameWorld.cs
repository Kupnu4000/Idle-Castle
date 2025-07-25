using System;
using System.Collections.Generic;
using IdleCastle.Runtime.Zenject;
using JetBrains.Annotations;


namespace IdleCastle.Runtime.Gameplay
{
	[UsedImplicitly]
	public class GameWorld : IDisposable
	{
		private readonly Dictionary<ItemId, IBuilding> _buildings = new();

		private readonly GenericFactory _genericFactory;

		public GameWorld (GenericFactory genericFactory)
		{
			_genericFactory = genericFactory;
		}

		public void Create<TBuilding> () where TBuilding : class, IBuilding
		{
			TBuilding building = _genericFactory.Create<TBuilding>();

			if (!_buildings.TryAdd(building.Id, building))
			{
				throw new InvalidOperationException($"Building with ID {building.Id.ToString()} is already registered.");
			}
		}

		public void Dispose ()
		{
			_buildings.Clear();
		}
	}
}
