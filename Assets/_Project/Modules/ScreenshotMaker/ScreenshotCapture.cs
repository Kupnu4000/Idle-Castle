using System;
using System.IO;
using UnityEngine;


namespace _Project.Modules.ScreenshotMaker
{
	public class ScreenshotCapture : MonoBehaviour
	{
		#if UNITY_EDITOR
		[SerializeField, Range(0, 2)] private int _upscaleFactor = 1;

		private const string ScreenshotDirectory = "Screenshots";

		public static event Action BeforeScreenshotTaken;
		public static event Action AfterScreenshotTaken;

		private bool _takeScreenshotOnNextFrame;

		private void Start ()
		{
			ScreenshotCaptureHotkey.TakeScreenshot += TakeScreenshot;
		}

		private void TakeScreenshot ()
		{
			BeforeScreenshotTaken?.Invoke();

			_takeScreenshotOnNextFrame = true;
		}

		private void OnPostRender ()
		{
			if (!_takeScreenshotOnNextFrame)
			{
				return;
			}

			_takeScreenshotOnNextFrame = false;

			if (!Directory.Exists(ScreenshotDirectory))
			{
				Directory.CreateDirectory(ScreenshotDirectory);
			}

			string filename = GetScreenshotFilename();
			ScreenCapture.CaptureScreenshot(filename, _upscaleFactor);
			Debug.Log($"Screenshot saved to: {filename}");

			AfterScreenshotTaken?.Invoke();
		}

		private void OnDestroy ()
		{
			ScreenshotCaptureHotkey.TakeScreenshot -= TakeScreenshot;
		}

		private static string GetScreenshotFilename ()
		{
			return $"{ScreenshotDirectory}/screenshot_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
		}
		#endif
	}
}
