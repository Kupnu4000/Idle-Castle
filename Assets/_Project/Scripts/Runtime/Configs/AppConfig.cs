using System.Diagnostics.CodeAnalysis;
using GoblinFortress.Runtime.Utilities;
using JetBrains.Annotations;
using Modules.Configs;
using UnityEngine;


namespace GoblinFortress.Runtime.Configs
{
	[PublicAPI]
	[CreateAssetMenu(fileName = "App Config", menuName = ProjectUtils.MenuPath.Configs + "App Config")]
	public class AppConfig : Config
	{
		[SerializeField] private ThreadPriority       _backgroundLoadingPriority = ThreadPriority.Normal;
		[SerializeField] private FrameRate            _targetFrameRate           = FrameRate.PlatformDefault;
		[SerializeField] private SleepTimeoutInternal _sleepTimeout              = SleepTimeoutInternal.NeverSleep;
		[SerializeField] private bool                 _multiTouchEnabled;

		#if UNITY_ANDROID
		[Space]
		[SerializeField] private StackTraceLogType _androidStackTraceLogType = StackTraceLogType.ScriptOnly;
		#endif

		public int            TargetFrameRate           => (int)_targetFrameRate;
		public int            SleepTimeout              => (int)_sleepTimeout;
		public bool           MultiTouchEnabled         => _multiTouchEnabled;
		public ThreadPriority BackgroundLoadingPriority => _backgroundLoadingPriority;
		#if UNITY_ANDROID
		public StackTraceLogType AndroidStackTraceLogType  => _androidStackTraceLogType;
		#endif

		[SuppressMessage("ReSharper", "UnusedMember.Local")]
		private enum FrameRate
		{
			PlatformDefault = -1,
			_30             = 30,
			_60             = 60,
			_90             = 90,
			_120            = 120
		}

		[SuppressMessage("ReSharper", "UnusedMember.Local")]
		private enum SleepTimeoutInternal
		{
			NeverSleep    = UnityEngine.SleepTimeout.NeverSleep,
			SystemSetting = UnityEngine.SleepTimeout.SystemSetting
		}
	}
}
