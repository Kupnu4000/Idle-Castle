using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;


namespace Modules.UISystem
{
	[PublicAPI]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(CanvasGroup))]
	public abstract class UIDialog : MonoBehaviour
	{
		[SerializeField, Min(float.Epsilon)] private float _fadeDuration = 0.25f;

		[Header("Response Buttons")]
		[SerializeField, CanBeNull] private Button _confirmButton;
		[SerializeField, CanBeNull] private Button _cancelButton;

		public static event Action<UIDialog> DialogSpawned;
		public static event Action<UIDialog> DialogClosed;

		private CanvasGroup _canvasGroup;
		private Tween       _fadeTween;

		private UniTaskCompletionSource                 _destroyAwaiter;
		private UniTaskCompletionSource<DialogResponse> _responseAwaiter;

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

		protected void Start ()
		{
			if (_confirmButton)
			{
				_confirmButton.onClick.AddListener(Confirm);
			}

			if (_cancelButton)
			{
				_cancelButton.onClick.AddListener(Cancel);
			}

			DialogSpawned?.Invoke(this);

			OnAfterStart();
		}

		protected virtual void OnAfterAwake () {}

		protected virtual void OnAfterStart () {}

		public void FadeIn ()
		{
			_canvasGroup.interactable = true;

			_fadeTween.PlayForward();
		}

		public void Confirm ()
		{
			_responseAwaiter?.TrySetResult(DialogResponse.Confirmed);

			FadeOutAndDestroy();
		}

		public void Cancel ()
		{
			_responseAwaiter?.TrySetResult(DialogResponse.Canceled);

			FadeOutAndDestroy();
		}

		public UIDialog FadeOutAndDestroy ()
		{
			_canvasGroup.interactable = false;

			_fadeTween.OnStepComplete(() => Destroy(gameObject));
			_fadeTween.PlayBackwards();
			_fadeTween = null;

			return this;
		}

		public UniTask<DialogResponse> WaitForResponse ()
		{
			_responseAwaiter ??= new UniTaskCompletionSource<DialogResponse>();

			return _responseAwaiter.Task;
		}

		public UniTask WaitForDestroy ()
		{
			_destroyAwaiter ??= new UniTaskCompletionSource();

			return _destroyAwaiter.Task;
		}

		private void SetResponse (DialogResponse response)
		{
			_responseAwaiter?.TrySetResult(response);
		}

		protected void OnDestroy ()
		{
			_responseAwaiter?.TrySetCanceled();
			_destroyAwaiter?.TrySetResult();

			DialogClosed?.Invoke(this);
		}
	}
}
