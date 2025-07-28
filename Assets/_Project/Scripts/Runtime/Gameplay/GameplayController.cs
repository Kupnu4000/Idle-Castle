using System;
using Cysharp.Threading.Tasks;
using IdleCastle.Runtime.Configs;
using IdleCastle.Runtime.Gameplay.Messages;
using IdleCastle.Runtime.UI.Gameplay;
using JetBrains.Annotations;
using MessagePipe;
using Modules.AddressablesCache;
using Modules.UISystem;
using Modules.UISystem.Interfaces;


namespace IdleCastle.Runtime.Gameplay
{
	[UsedImplicitly]
	public class GameplayController : IDisposable
	{
		private readonly IUISystem _uiSystem; // TODO Refactor: тут это только чтобы прокинуть рут трансформ в UIFacadeFactory, постараться избавиться от этого
		private readonly UIFacadeFactory _screenFacadeFactory; // TODO Refactor: это могла бы делать и IUISystem
		private readonly AssetReferenceProvider _assetProvider;
		private readonly AddressablesCache _addressablesCache;
		private readonly GameWorld _gameWorld;

		private GameplayUI  _gameplayUI;
		private IDisposable _loadedAssets;

		public GameplayController (
			IPublisher<IncomeGenerated> incomePublisher,
			UIFacadeFactory screenFacadeFactory,
			GameWorld gameWorld,
			IUISystem uiSystem,
			AddressablesCache addressablesCache,
			AssetReferenceProvider assetProvider
		)
		{
			_screenFacadeFactory = screenFacadeFactory;
			_gameWorld           = gameWorld;
			_uiSystem            = uiSystem;
			_addressablesCache   = addressablesCache;
			_assetProvider       = assetProvider;
		}

		public async UniTask Initialize ()
		{
			_loadedAssets = await PreloadAssets();
			_gameplayUI   = await _screenFacadeFactory.Create<GameplayUI>(_uiSystem.Canvas.transform);

			_gameWorld.Create<GoldMine>();
		}

		private async UniTask<IDisposable> PreloadAssets ()
		{
			return await _addressablesCache
			             .BuildLoadingGroup()
			             .Add(_assetProvider.BuildingWidgetView)
			             .LoadAsync();
		}

		public void Dispose ()
		{
			_loadedAssets.Dispose();
			_loadedAssets = null;

			_gameplayUI.Dispose();
			_gameplayUI = null;
		}
	}
}
