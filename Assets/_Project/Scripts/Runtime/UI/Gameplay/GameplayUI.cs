using JetBrains.Annotations;
using Modules.UISystem;


namespace IdleCastle.UI.Gameplay
{
	[UsedImplicitly]
	public class GameplayUI : UIScreen<GameplayUIView, GameplayUIPresenter>
	{
		public GameplayUI (
			IUIViewFactory<GameplayUIView> viewFactory,
			IUIPresenterFactory<GameplayUIPresenter, GameplayUIView> presenterFactory
		) : base(viewFactory, presenterFactory) {}
	}
}
