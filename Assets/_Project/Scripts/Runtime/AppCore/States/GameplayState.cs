using System.Threading;
using Cysharp.Threading.Tasks;
using IdleCastle.Runtime.UI.Gameplay;
using JetBrains.Annotations;
using Modules.AppCore.Interfaces;
using Modules.StateMachine.Interfaces;
using Modules.UISystem;
using Modules.UISystem.Interfaces;


namespace IdleCastle.Runtime.AppCore.States
{
	[UsedImplicitly]
	public class GameplayState : IState<IAppStateController>
	{
		private readonly IUISystem           _uiSystem;
		private readonly ScreenFacadeFactory _screenFacadeFactory; // TODO Refactor: это могла бы делать и IUISystem

		private GameplayUI _gameplayUI;

		public IAppStateController Context {get;}

		public GameplayState (IAppStateController context, ScreenFacadeFactory screenFacadeFactory)
		{
			Context              = context;
			_screenFacadeFactory = screenFacadeFactory;
		}

		public UniTask OnEnterAsync (CancellationToken cancellationToken = default)
		{
			_uiSystem.AttachToMainCamera();

			_gameplayUI = _screenFacadeFactory.Create<GameplayUI>();

			return UniTask.CompletedTask;
		}

		public UniTask OnExitAsync (CancellationToken cancellationToken = default)
		{
			_gameplayUI.Dispose();
			_gameplayUI = null;

			return UniTask.CompletedTask;
		}

		public void Tick (float deltaTime) {}

		public void Dispose () {}
	}
}
