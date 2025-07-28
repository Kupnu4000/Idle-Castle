using Cysharp.Threading.Tasks;
using IdleCastle.Runtime.Zenject;
using JetBrains.Annotations;
using Modules.UISystem;
using Modules.UISystem.Interfaces;
using UnityEngine;


namespace IdleCastle.Runtime.UI.Gameplay
{
	[UsedImplicitly]
	public class GameplayUI : UIFacade<GameplayUIView, GameplayUIPresenter>
	{
		private readonly IUISystem      _uiSystem;
		private readonly GenericFactory _factory;

		public GameplayUI (IUISystem uiSystem, GenericFactory factory)
		{
			_uiSystem = uiSystem;
			_factory  = factory;
		}

		protected override UniTask<GameplayUIPresenter> CreatePresenter ()
		{
			GameplayUIPresenter presenter = _factory.Create<GameplayUIPresenter>();

			return UniTask.FromResult(presenter);
		}

		protected override UniTask<GameplayUIView> CreateView (Transform parent)
		{
			return _uiSystem.SpawnScreen<GameplayUIView>();
		}
	}
}
