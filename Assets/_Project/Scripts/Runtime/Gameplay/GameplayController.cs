using System;
using IdleCastle.Runtime.Gameplay.BuildingLogic;
using IdleCastle.Runtime.UI.Gameplay;
using JetBrains.Annotations;
using Modules.UISystem;


namespace IdleCastle.Runtime.Gameplay
{
	[UsedImplicitly]
	public class GameplayController : IDisposable
	{
		private readonly ScreenFacadeFactory _screenFacadeFactory; // TODO Refactor: это могла бы делать и IUISystem
		private readonly BuildingManager     _buildingManager;

		private GameplayUI _gameplayUI;

		public GameplayController (
			ScreenFacadeFactory screenFacadeFactory,
			BuildingManager buildingManager
		)
		{
			_screenFacadeFactory = screenFacadeFactory;
			_buildingManager     = buildingManager;
		}

		public void Initialize ()
		{
			_gameplayUI = _screenFacadeFactory.Create<GameplayUI>();

			_buildingManager.CurrencyChanged += _gameplayUI.HandleCurrencyChanged;

			_buildingManager.Create<GoldMine>();
		}

		public void Tick (float deltaTime)
		{
			_buildingManager.Tick(deltaTime);
		}

		public void Dispose ()
		{
			_buildingManager.CurrencyChanged -= _gameplayUI.HandleCurrencyChanged;

			_gameplayUI.Dispose();
			_gameplayUI = null;
		}
	}
}
