using Cysharp.Threading.Tasks;
using GoblinFortress.Runtime.Zenject;
using JetBrains.Annotations;
using Modules.UISystem;
using Modules.UISystem.Interfaces;
using UnityEngine;


namespace GoblinFortress.Runtime.UI.Lobby
{
	[UsedImplicitly]
	public class LobbyScreen : UIFacade<LobbyScreenView, LobbyScreenPresenter>
	{
		private readonly IUISystem      _uiSystem;
		private readonly GenericFactory _factory;

		public LobbyScreen (IUISystem uiSystem, GenericFactory factory)
		{
			_uiSystem = uiSystem;
			_factory  = factory;
		}

		protected override UniTask<LobbyScreenPresenter> CreatePresenter ()
		{
			LobbyScreenPresenter presenter = _factory.Create<LobbyScreenPresenter>();

			return UniTask.FromResult(presenter);
		}

		protected override UniTask<LobbyScreenView> CreateView (Transform parent)
		{
			return _uiSystem.SpawnScreen<LobbyScreenView>();
		}
	}
}
