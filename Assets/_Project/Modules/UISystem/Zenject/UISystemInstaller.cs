using Modules.UISystem.Interfaces;
using UnityEngine;
using Zenject;


namespace Modules.UISystem.Zenject
{
	[CreateAssetMenu(fileName = "UI System Installer", menuName = "Modules/UI System/Installer")]
	internal class UISystemInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private UIRoot _uiRoot;

		[Tooltip("The number of screens \\ dialogs to cache for faster spawning. If set to 0, caching will be disabled.")]
		[SerializeField, Min(0)] private int _cacheSize = 5;

		public override void InstallBindings ()
		{
			Container.Bind<ScreenFacadeFactory>()
			         .AsSingle()
			         .MoveIntoDirectSubContainers();

			Container.Bind<UIRoot>()
			         .FromComponentInNewPrefab(_uiRoot)
			         .WithGameObjectName(_uiRoot.name)
			         .AsSingle();

			Container.Bind<IUISystem>()
			         .To<UISystem>()
			         .AsSingle()
			         .WithArguments(_cacheSize);
		}
	}
}
