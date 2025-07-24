using IdleCastle.Runtime.Gameplay;
using JetBrains.Annotations;
using Zenject;


namespace IdleCastle.Runtime.Zenject
{
	[UsedImplicitly]
	public class GenericFactory
	{
		private readonly IInstantiator _instantiator;

		public GenericFactory (IInstantiator instantiator)
		{
			_instantiator = instantiator;
		}

		public TBuilding Create<TBuilding> () where TBuilding : IBuilding
		{
			return _instantiator.Instantiate<TBuilding>();
		}
	}
}
