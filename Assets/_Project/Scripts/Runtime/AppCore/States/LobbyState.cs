using System.Threading;
using Cysharp.Threading.Tasks;
using IdleCastle.UI.Lobby;
using JetBrains.Annotations;
using Modules.AppCore.Interfaces;
using Modules.StateMachine.Interfaces;
using Modules.UISystem;


namespace IdleCastle.AppCore.States
{
	[UsedImplicitly]
	public class LobbyState : IState<IAppStateController>
	{
		private readonly IUIFactory<LobbyScreen> _lobbyScreenFactory;

		private LobbyScreen _lobbyScreen;

		public IAppStateController Context {get;}

		public LobbyState (
			IAppStateController context,
			IUIFactory<LobbyScreen> lobbyScreenFactory
		)
		{
			Context             = context;
			_lobbyScreenFactory = lobbyScreenFactory;
		}

		public UniTask OnEnterAsync (CancellationToken cancellationToken = default)
		{
			_lobbyScreen = _lobbyScreenFactory.Create();

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
