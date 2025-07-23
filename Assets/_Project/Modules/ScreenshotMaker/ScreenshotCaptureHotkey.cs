#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;


namespace _Project.Modules.ScreenshotMaker
{
	public static class ScreenshotCaptureHotkey
	{
		public static event Action TakeScreenshot;

		[MenuItem("Tools/Take Screenshot #s")]
		public static void InvokeTakeScreenshot ()
		{
			if (TakeScreenshot == null)
			{
				Debug.LogWarning($"No subscribers to TakeScreenshot event. Attach {nameof(ScreenshotCapture)} component to a GameObject.");

				return;
			}

			TakeScreenshot?.Invoke();
		}

		[MenuItem("Tools/Take Screenshot #s", true)]
		private static bool ValidateTakeScreenshot ()
		{
			return Application.isPlaying;
		}
	}
}
#endif
