using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdleCastle.Configs;
using JetBrains.Annotations;
using Modules.Bootstrap.Runtime.Interfaces;
using UnityEngine;


namespace IdleCastle.Bootstrap
{
	[UsedImplicitly]
	public class ApplyAppConfigBootstrapCommand : IBootstrapCommand
	{
		public string    Name        => nameof(ApplyAppConfigBootstrapCommand);
		public bool      IsEssential => false;
		public TimeSpan? Timeout     => null;

		private readonly AppConfig _config;

		public ApplyAppConfigBootstrapCommand (AppConfig config)
		{
			_config = config;
		}

		public UniTask ExecuteAsync (CancellationToken cancellationToken)
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
