using System.Threading;
using Cysharp.Threading.Tasks;
using IdleCastle.Bootstrap;
using JetBrains.Annotations;
using Modules.AppCore.Interfaces;
using Modules.Bootstrap.Runtime.Interfaces;
using Modules.StateMachine.Interfaces;


namespace IdleCastle.AppCore.States
{
	[UsedImplicitly]
	public class LoadingState : IState<IAppStateController>
	{
		private readonly IBootstrapPlanner _bootstrapPlanner;

		public IAppStateController Context {get;}

		public LoadingState (IAppStateController context, IBootstrapPlanner bootstrapPlanner)
		{
			Context           = context;
			_bootstrapPlanner = bootstrapPlanner;
		}

		public async UniTask OnEnterAsync (CancellationToken cancellationToken = default)
		{
			IBootstrapPlan bootstrapPlan = _bootstrapPlanner
			                               .Plan()
			                               .Phase("Phase 1")
			                               .Add<UnityLocalizationInitialization>()
			                               .Add<PersistentDataInitialization>()
			                               .Add<ApplyAppConfigBootstrapCommand>()
			                               .Build();

			await bootstrapPlan.RunAsync(cancellationToken);

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
