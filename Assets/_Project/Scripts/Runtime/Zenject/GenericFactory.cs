using JetBrains.Annotations;
using Zenject;


namespace GoblinFortress.Runtime.Zenject
{
	[UsedImplicitly]
	public class GenericFactory
	{
		private readonly IInstantiator _instantiator;

		public GenericFactory (IInstantiator instantiator)
		{
			_instantiator = instantiator;
		}

		public T Create<T> ()
		{
			return _instantiator.Instantiate<T>();
		}
	}
}
