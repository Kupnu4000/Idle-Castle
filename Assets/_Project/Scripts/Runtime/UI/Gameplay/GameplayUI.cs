using Cysharp.Threading.Tasks;
using GoblinFortress.Runtime.Zenject;
using JetBrains.Annotations;
using Modules.UISystem;
using Modules.UISystem.Interfaces;
using UnityEngine;


// TODO: для UI можно сделать какой-то может быть MVPHandle, который будет управлять созданием Presenter и View
namespace GoblinFortress.Runtime.UI.Gameplay
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
