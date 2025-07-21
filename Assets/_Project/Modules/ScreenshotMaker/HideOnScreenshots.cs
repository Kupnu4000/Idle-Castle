using UnityEngine;


namespace AnagramQuiz.Runtime.Utilities.ScreenshotMaker
{
	public class HideOnScreenshots : MonoBehaviour
	{
		#if UNITY_EDITOR
		private void Awake ()
		{
			ScreenshotCapture.BeforeScreenshotTaken += Hide;
			ScreenshotCapture.AfterScreenshotTaken  += Show;
		}

		// ReSharper disable once Unity.RedundantEventFunction
		private void Start () {}

		private void OnDestroy ()
		{
			ScreenshotCapture.BeforeScreenshotTaken -= Hide;
			ScreenshotCapture.AfterScreenshotTaken  -= Show;
		}

		private void Hide ()
		{
			gameObject.SetActive(false);
		}

		private void Show ()
		{
			gameObject.SetActive(true);
		}
		#endif
	}
}
