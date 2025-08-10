using System.Threading;
using Cysharp.Threading.Tasks;
using GoblinFortress.Runtime.Gameplay;
using JetBrains.Annotations;
using Modules.AppCore.Interfaces;
using Modules.StateMachine.Interfaces;
using Modules.UISystem.Interfaces;


namespace GoblinFortress.Runtime.AppCore.States
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
			_uiSystem           = uiSystem;
			_gameplayController = gameplayController;
		}

		public async UniTask OnEnterAsync (CancellationToken cancellationToken = default)
		{
			_uiSystem.AttachToMainCamera();

			await _gameplayController.Initialize();
		}

		public UniTask OnExitAsync (CancellationToken cancellationToken = default)
		{
			return UniTask.CompletedTask;
		}

		public void Tick (float deltaTime) {}

		public void Dispose ()
		{
			_gameplayController.Dispose();
		}
	}
}
