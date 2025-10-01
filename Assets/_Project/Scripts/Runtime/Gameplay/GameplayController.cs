using System;
using IdleCastle.Gameplay.Buildings;
using IdleCastle.UI.Gameplay;
using JetBrains.Annotations;
using Modules.UISystem;


namespace IdleCastle.Gameplay
{
	[UsedImplicitly]
	public class GameplayController : IDisposable
	{
		private readonly GameWorld              _gameWorld;
		private readonly IUIFactory<GameplayUI> _gameplayUIFactory;

		private GameplayUI  _gameplayUI;
		private IDisposable _loadedAssets;

		public GameplayController (
			GameWorld gameWorld,
			IUIFactory<GameplayUI> gameplayUIFactory
		)
		{
			_gameWorld         = gameWorld;
			_gameplayUIFactory = gameplayUIFactory;
		}

		public void Initialize ()
		{
			_gameplayUI = _gameplayUIFactory.Create();

			_gameWorld.Create<GoldMine>();
		}

		public void Dispose ()
		{
			_loadedAssets.Dispose();
			_loadedAssets = null;

			_gameWorld.Dispose();

			_gameplayUI.Dispose();
			_gameplayUI = null;
		}
	}
}
