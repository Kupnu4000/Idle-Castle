using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;


namespace Modules.UISystem
{
	[PublicAPI]
	public class UIFacadeFactory
	{
		private readonly IInstantiator _instantiator;

		public UIFacadeFactory (IInstantiator instantiator)
		{
			_instantiator = instantiator;
		}

		public async UniTask<TFacade> Create<TFacade> (Transform parent) where TFacade : IUIFacade
		{
			TFacade facade = _instantiator.Instantiate<TFacade>();

			await facade.Initialize(parent);

			return facade;
		}
	}
}
