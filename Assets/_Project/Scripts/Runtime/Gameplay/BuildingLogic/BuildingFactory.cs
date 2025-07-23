using JetBrains.Annotations;
using Zenject;


namespace IdleCastle.Runtime.Gameplay.BuildingLogic
{
	[UsedImplicitly]
	public class BuildingFactory
	{
		// TODO Refactor: тут можно использовать ServiceProvider
		private readonly IInstantiator _instantiator;

		public BuildingFactory (IInstantiator instantiator)
		{
			_instantiator = instantiator;
		}

		public TBuilding Create<TBuilding> () where TBuilding : IBuilding
		{
			return _instantiator.Instantiate<TBuilding>();
		}
	}
}
