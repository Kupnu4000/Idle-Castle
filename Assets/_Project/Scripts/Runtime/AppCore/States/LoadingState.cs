using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GoblinFortress.Runtime.PersistentData;
using JetBrains.Annotations;
using Modules.AppCore.Interfaces;
using Modules.Bootstrap;
using Modules.StateMachine.Interfaces;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace GoblinFortress.Runtime.AppCore.States
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
			await InitializeLocalizationAsync();

			await UniTask.WhenAll(
				UniTask.Delay(TimeSpan.FromSeconds(MinDisplayTime), cancellationToken: cancellationToken),
				_bootstrapper.BootstrapAsync()
			);

			_userData.IncrementSessionCount();

			Context.GoToGameplay();
			// Context.GoToLobby();
		}

		// TODO Refactor: это должно делаться в бутстраппере, но до того, как он приступит к выполнению последовательности загрузки
		private async UniTask InitializeLocalizationAsync ()
		{
			await LocalizationSettings.InitializationOperation.Task.AsUniTask();

			if (LocalizationSettings.InitializationOperation.Status != AsyncOperationStatus.Succeeded)
			{
				throw new Exception("Localization failed to initialize.");
			}
		}

		public UniTask OnExitAsync (CancellationToken cancellationToken = default)
		{
			return UniTask.CompletedTask;
		}

		public void Tick (float deltaTime) {}

		public void Dispose () {}
	}
}
