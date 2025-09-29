using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdleCastle.PersistentData;
using JetBrains.Annotations;
using Modules.Bootstrap.Runtime.Interfaces;


namespace IdleCastle.Bootstrap
{
	[UsedImplicitly]
	public class PersistentDataInitialization : IBootstrapCommand
	{
		public string    Name        => nameof(PersistentDataInitialization);
		public bool      IsEssential => false;
		public TimeSpan? Timeout     => null;

		private readonly UserData    _userData;
		private readonly Preferences _preferences;

		public PersistentDataInitialization (
			UserData userData,
			Preferences preferences
		)
		{
			_userData    = userData;
			_preferences = preferences;
		}

		public UniTask ExecuteAsync (CancellationToken cancellationToken)
		{
			_preferences.TryLoad();
			_userData.TryLoad();

			return UniTask.CompletedTask;
		}
	}
}
