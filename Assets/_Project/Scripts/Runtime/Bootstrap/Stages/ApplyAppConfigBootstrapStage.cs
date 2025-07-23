using System.Threading;
using Cysharp.Threading.Tasks;
using IdleCastle.Runtime.Configs;
using JetBrains.Annotations;
using Modules.Bootstrap.Interfaces;
using UnityEngine;


namespace IdleCastle.Runtime.Bootstrap.Stages
{
	[UsedImplicitly]
	public class ApplyAppConfigBootstrapStage : IBootstrapStage
	{
		private readonly AppConfig _config;

		public ApplyAppConfigBootstrapStage (AppConfig config)
		{
			_config = config;
		}

		public UniTask Execute (CancellationToken cancellationToken = default)
		{
			Application.backgroundLoadingPriority = _config.BackgroundLoadingPriority;
			Screen.sleepTimeout                   = _config.SleepTimeout;
			Application.targetFrameRate           = _config.TargetFrameRate;
			Input.multiTouchEnabled               = _config.MultiTouchEnabled;

			#if UNITY_ANDROID && DEVELOPMENT_BUILD && !UNITY_EDITOR
			Application.SetStackTraceLogType(LogType.Log,     _config.AndroidStackTraceLogType);
			Application.SetStackTraceLogType(LogType.Warning, _config.AndroidStackTraceLogType);
			Application.SetStackTraceLogType(LogType.Error,   _config.AndroidStackTraceLogType);
			#endif

			return UniTask.CompletedTask;
		}
	}
}
