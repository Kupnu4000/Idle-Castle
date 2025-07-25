using System;
using IdleCastle.Runtime.Gameplay.Messages;
using IdleCastle.Runtime.UI.Gameplay;
using JetBrains.Annotations;
using MessagePipe;
using Modules.UISystem;


namespace IdleCastle.Runtime.Gameplay
{
	[UsedImplicitly]
	public class GameplayController : IDisposable
	{
		private readonly ScreenFacadeFactory _screenFacadeFactory; // TODO Refactor: это могла бы делать и IUISystem
		private readonly GameWorld           _gameWorld;

		private GameplayUI _gameplayUI;

		public GameplayController (
			IPublisher<IncomeGenerated> incomePublisher,
			ScreenFacadeFactory screenFacadeFactory,
			GameWorld gameWorld
		)
		{
			_screenFacadeFactory = screenFacadeFactory;
			_gameWorld           = gameWorld;
		}

		public void Initialize ()
		{
			_gameplayUI = _screenFacadeFactory.Create<GameplayUI>();

			_gameWorld.Create<GoldMine>();
		}

		public void Dispose ()
		{
			_gameplayUI.Dispose();
			_gameplayUI = null;
		}
	}
}
