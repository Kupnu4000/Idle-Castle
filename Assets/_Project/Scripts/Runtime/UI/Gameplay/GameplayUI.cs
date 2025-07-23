using JetBrains.Annotations;
using Modules.UISystem;
using Modules.UISystem.Interfaces;


namespace IdleCastle.Runtime.UI.Gameplay
{
	[UsedImplicitly]
	public class GameplayUI : ScreenFacade<GameplayUIView, GameplayUIPresenter>
	{
		public GameplayUI (GameplayUIPresenter presenter, IUISystem uiSystem) : base(presenter, uiSystem) {}

		public void HandleCurrencyChanged (ItemId currencyId, double newValue)
		{
			Presenter.HandleCurrencyChanged(currencyId, newValue);
		}
	}
}
