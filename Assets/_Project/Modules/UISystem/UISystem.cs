using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Modules.UISystem.Factories;
using Modules.UISystem.Interfaces;
using UnityEngine;
using UnityEngine.Rendering.Universal;


namespace Modules.UISystem
{
	[UsedImplicitly]
	internal class UISystem : IUISystem
	{
		private readonly PrototypeProvider _prototypeProvider;

		private readonly IScreenFactory _screenFactory;
		private readonly IDialogFactory _dialogFactory;

		private readonly UIRoot _root;

		public Camera Camera => _root.Camera;
		public Canvas Canvas => _root.Canvas;

		public UISystem (
			int cacheSize,
			UIRoot root,
			IScreenFactory screenFactory = null,
			IDialogFactory dialogFactory = null
		)
		{
			_root              = root;
			_screenFactory     = screenFactory ?? new BuiltInScreenFactory();
			_dialogFactory     = dialogFactory ?? new BuiltInDialogFactory();
			_prototypeProvider = new PrototypeProvider(cacheSize);
		}

		public void AttachToMainCamera ()
		{
			Camera mainCamera = Camera.main;

			if (!mainCamera)
			{
				Debug.LogError("UI System: No main camera found.");
				return;
			}

			UniversalAdditionalCameraData cameraData = mainCamera.GetUniversalAdditionalCameraData();

			cameraData.cameraStack.Add(_root.Camera);
		}

		public UniTask<TScreen> SpawnScreen<TScreen> () where TScreen : UIScreen
		{
			UIScreen prototype = _prototypeProvider.GetScreenPrototype<TScreen>();
			UIScreen screen    = _screenFactory.Create(prototype, _root.Canvas.transform);

			screen.gameObject.name = prototype.name;

			screen.FadeIn();

			return UniTask.FromResult((TScreen)screen);
		}

		public UniTask<TDialog> SpawnDialog<TDialog> () where TDialog : UIDialog
		{
			UIDialog prototype = _prototypeProvider.GetDialogPrototype<TDialog>();
			UIDialog dialog    = _dialogFactory.Create(prototype, _root.Canvas.transform);

			dialog.gameObject.name = prototype.name;

			dialog.FadeIn();

			return UniTask.FromResult((TDialog)dialog);
		}

		public UniTask FadeInAsync (float duration = 1)
		{
			return _root.Fade.FadeIn(duration);
		}

		public UniTask FadeOutAsync (float duration = 1)
		{
			return _root.Fade.FadeOut(duration);
		}
	}
}
