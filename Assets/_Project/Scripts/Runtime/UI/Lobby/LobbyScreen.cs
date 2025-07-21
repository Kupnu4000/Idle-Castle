using JetBrains.Annotations;
using Modules.UISystem;
using Modules.UISystem.Interfaces;


namespace IdleCastle.Runtime.UI.Lobby
{
	[UsedImplicitly]
	public class LobbyScreen : ScreenFacade<LobbyScreenView, LobbyScreenPresenter>
	{
		public LobbyScreen (LobbyScreenPresenter presenter, IUISystem uiSystem)
			: base(presenter, uiSystem) {}
	}
}
