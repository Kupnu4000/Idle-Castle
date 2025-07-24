using System;
using JetBrains.Annotations;
using Modules.UISystem;


namespace IdleCastle.Runtime.UI.Lobby
{
	[UsedImplicitly]
	public class LobbyScreenPresenter : IScreenPresenter<LobbyScreenView>
	{
		private LobbyScreenView _view;

		public void Initialize (LobbyScreenView view)
		{
			_view = view;

			_view.PlayButtonClicked.AddListener(HandlePlayButtonClicked);
		}

		private void HandlePlayButtonClicked ()
		{
			throw new NotImplementedException();
		}

		public void Dispose ()
		{
			// _view.SettingsButtonClicked.RemoveAllListeners();
			_view.PlayButtonClicked.RemoveAllListeners();

			_view = null;
		}
	}
}
