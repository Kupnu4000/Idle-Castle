using System.Threading;
using Cysharp.Threading.Tasks;
using IdleCastle.Runtime.UI.Lobby;
using JetBrains.Annotations;
using Modules.AppCore.Interfaces;
using Modules.StateMachine.Interfaces;
using Modules.UISystem;
using Modules.UISystem.Interfaces;


namespace IdleCastle.Runtime.AppCore.States
{
	[UsedImplicitly]
	public class LobbyState : IState<IAppStateController>
	{
		private readonly IUISystem _uiSystem;

		private LobbyScreen _lobbyScreen;

		public IAppStateController Context {get;}

		public LobbyState (
			IAppStateController context,
			IUISystem uiSystem,
			ScreenFacadeFactory screenFacadeFactory
		)
		{
			Context      = context;
			_uiSystem    = uiSystem;
			_lobbyScreen = screenFacadeFactory.Create<LobbyScreen>();
		}

		public UniTask OnEnterAsync (CancellationToken cancellationToken = default)
		{
			_uiSystem.AttachToMainCamera();

			return UniTask.CompletedTask;
		}

		public UniTask OnExitAsync (CancellationToken cancellationToken = default)
		{
			_lobbyScreen.Dispose();
			_lobbyScreen = null;

			return UniTask.CompletedTask;
		}

		public void Tick (float deltaTime) {}

		public void Dispose () {}
	}
}
