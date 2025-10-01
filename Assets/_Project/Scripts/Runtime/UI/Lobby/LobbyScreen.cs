using JetBrains.Annotations;
using Modules.UISystem;


namespace IdleCastle.UI.Lobby
{
	[UsedImplicitly]
	public class LobbyScreen : UIScreen<LobbyScreenView, LobbyScreenPresenter>
	{
		public LobbyScreen (
			IUIViewFactory<LobbyScreenView> viewFactory,
			IUIPresenterFactory<LobbyScreenPresenter, LobbyScreenView> presenterFactory
		) : base(viewFactory, presenterFactory) {}
	}
}
