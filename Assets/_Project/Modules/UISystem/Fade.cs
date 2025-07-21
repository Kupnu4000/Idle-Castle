using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;


namespace Modules.UISystem
{
	[RequireComponent(typeof(CanvasGroup))]
	internal class Fade : MonoBehaviour
	{
		private CanvasGroup _canvasGroup;
		private Tween       _tween;

		private void Awake ()
		{
			_canvasGroup       = GetComponent<CanvasGroup>();
			_canvasGroup.alpha = 0;

			_tween = _canvasGroup.DOFade(1, 1)
			                     .SetAutoKill(false)
			                     .Pause()
			                     .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
		}

		internal async UniTask FadeIn (float duration = 1)
		{
			_canvasGroup.blocksRaycasts = true;

			_tween.timeScale = 1 / duration;
			_tween.PlayForward();

			await _tween.AsyncWaitForPosition(1);
		}

		internal async UniTask FadeOut (float duration = 1)
		{
			_canvasGroup.blocksRaycasts = false;

			_tween.timeScale = 1 / duration;
			_tween.PlayBackwards();

			await _tween.AsyncWaitForPosition(0);
		}
	}
}
