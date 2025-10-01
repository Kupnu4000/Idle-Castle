using System.Threading;
using Cysharp.Threading.Tasks;
using IdleCastle.Gameplay;
using JetBrains.Annotations;
using Modules.AppCore.Interfaces;
using Modules.StateMachine.Interfaces;


namespace IdleCastle.AppCore.States
{
	[UsedImplicitly]
	public class GameplayState : IState<IAppStateController>
	{
		private readonly GameplayController _gameplayController;

		public IAppStateController Context {get;}

		public GameplayState (
			IAppStateController context,
			GameplayController gameplayController
		)
		{
			Context             = context;
			_gameplayController = gameplayController;
		}

		public UniTask OnEnterAsync (CancellationToken cancellationToken = default)
		{
			_gameplayController.Initialize();

			return UniTask.CompletedTask;
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

		// TODO: это типа загрузка ассетов, но пока не используется
		// private async UniTask<IDisposable> PreloadAssets ()
		// {
		// 	// return await _addressablesCache
		// 	//              .BuildLoadingGroup()
		// 	//              .Add(_assetProvider.BuildingWidgetView)
		// 	//              .LoadAsync();
		//
		//
		// }
	}
}
