using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdleCastle.Runtime.PersistentData;
using JetBrains.Annotations;
using Modules.AppCore.Interfaces;
using Modules.Bootstrap;
using Modules.StateMachine.Interfaces;


namespace IdleCastle.Runtime.AppCore.States
{
	[UsedImplicitly]
	public class LoadingState : IState<IAppStateController>
	{
		private readonly Bootstrapper _bootstrapper;
		private readonly UserData     _userData;

		private const float MinDisplayTime = 0.0f;

		public IAppStateController Context {get;}

		public LoadingState (
			IAppStateController context,
			Bootstrapper bootstrapper,
			UserData userData
		)
		{
			Context       = context;
			_bootstrapper = bootstrapper;
			_userData     = userData;
		}

		public async UniTask OnEnterAsync (CancellationToken cancellationToken = default)
		{
			await UniTask.WhenAll(
				UniTask.Delay(TimeSpan.FromSeconds(MinDisplayTime), cancellationToken: cancellationToken),
				_bootstrapper.BootstrapAsync()
			);

			_userData.IncrementSessionCount();

			Context.GoToGameplay();
			// Context.GoToLobby();
		}

		public UniTask OnExitAsync (CancellationToken cancellationToken = default)
		{
			return UniTask.CompletedTask;
		}

		public void Tick (float deltaTime) {}

		public void Dispose () {}
	}
}
