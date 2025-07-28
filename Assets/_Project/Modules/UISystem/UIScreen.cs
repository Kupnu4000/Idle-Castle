using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;


namespace Modules.UISystem
{
	[PublicAPI]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(CanvasGroup))]
	public abstract class UIScreen : MonoBehaviour, IUIView
	{
		[SerializeField, Min(float.Epsilon)] private float _fadeDuration = 0.25f;

		private CanvasGroup _canvasGroup;
		private Tween       _fadeTween;

		protected void Awake ()
		{
			_canvasGroup = GetComponent<CanvasGroup>();

			_fadeTween = _canvasGroup.DOFade(1, _fadeDuration)
			                         .From(0)
			                         .Pause()
			                         .SetAutoKill(false)
			                         .SetLink(gameObject, LinkBehaviour.KillOnDestroy);

			OnAfterAwake();
		}

		protected virtual void OnAfterAwake () {}

		public void FadeIn ()
		{
			_canvasGroup.interactable = true;

			_fadeTween.PlayForward();
		}

		public async UniTask FadeOutAndDestroy ()
		{
			_canvasGroup.interactable = false;

			Tween tween = _fadeTween;
			_fadeTween = null;

			tween.PlayBackwards();

			await tween.AsyncWaitForPosition(0);

			Destroy(gameObject);
		}
	}
}
