using System.Threading;
using Cysharp.Threading.Tasks;
using IdleCastle.Runtime.Gameplay;
using JetBrains.Annotations;
using Modules.AppCore.Interfaces;
using Modules.StateMachine.Interfaces;
using Modules.UISystem.Interfaces;


namespace IdleCastle.Runtime.AppCore.States
{
	[UsedImplicitly]
	public class GameplayState : IState<IAppStateController>
	{
		private readonly IUISystem          _uiSystem;
		private readonly GameplayController _gameplayController;

		public IAppStateController Context {get;}

		public GameplayState (
			IAppStateController context,
			IUISystem uiSystem,
			GameplayController gameplayController
		)
		{
			Context             = context;
			_gameplayController = gameplayController;
			_uiSystem           = uiSystem;
		}

		public UniTask OnEnterAsync (CancellationToken cancellationToken = default)
		{
			_uiSystem.AttachToMainCamera();

			_gameplayController.Initialize();

			return UniTask.CompletedTask;
		}

		public UniTask OnExitAsync (CancellationToken cancellationToken = default)
		{
			_gameplayController.Dispose();

			return UniTask.CompletedTask;
		}

		public void Tick (float deltaTime) {}

		public void Dispose () {}
	}
}
