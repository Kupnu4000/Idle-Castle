using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Modules.Bootstrap.Runtime.Interfaces;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace IdleCastle.Bootstrap
{
	[UsedImplicitly]
	public class UnityLocalizationInitialization : IBootstrapCommand
	{
		public string    Name        => nameof(UnityLocalizationInitialization);
		public bool      IsEssential => true;
		public TimeSpan? Timeout     => null;

		public async UniTask ExecuteAsync (CancellationToken cancellationToken)
		{
			await LocalizationSettings.InitializationOperation.Task.AsUniTask();

			if (LocalizationSettings.InitializationOperation.Status != AsyncOperationStatus.Succeeded)
			{
				throw new Exception("Localization failed to initialize.");
			}
		}
	}
}
