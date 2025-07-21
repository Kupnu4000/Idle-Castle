using System.Threading;
using Cysharp.Threading.Tasks;
using IdleCastle.Runtime.PersistentData;
using JetBrains.Annotations;
using Modules.Bootstrap.Interfaces;


namespace IdleCastle.Runtime.Bootstrap.Stages
{
	[UsedImplicitly]
	public class PersistentDataBootstrapStage : IBootstrapStage
	{
		private readonly UserData    _userData;
		private readonly Preferences _preferences;

		public PersistentDataBootstrapStage (
			UserData userData,
			Preferences preferences
		)
		{
			_userData    = userData;
			_preferences = preferences;
		}

		public UniTask Execute (CancellationToken cancellationToken = default)
		{
			_preferences.TryLoad();
			_userData.TryLoad();

			// TODO: cleanup
			// _appEventsMonitor.AppPaused    += SavePersistentData;
			// _appEventsMonitor.AppUnfocused += SavePersistentData;
			// _appEventsMonitor.AppQuit      += SavePersistentData;

			return UniTask.CompletedTask;
		}
	}
}
