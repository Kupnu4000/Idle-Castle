using System;
using Modules.UISystem;
using Object = UnityEngine.Object;


namespace IdleCastle.Runtime.UI.Lobby
{
	public class LobbyScreenPresenter : IScreenPresenter<LobbyScreenView>
	{
		private LobbyScreenView _view;

		public void Initialize (LobbyScreenView view)
		{
			_view = view;
			// _view.SettingsButtonClicked.AddListener(HandleSettingsButtonClicked);
			_view.PlayButtonClicked.AddListener(HandlePlayButtonClicked);
		}

		private void HandleSettingsButtonClicked ()
		{
			throw new NotImplementedException();
		}

		private void HandlePlayButtonClicked ()
		{
			throw new NotImplementedException();
		}

		public void Dispose ()
		{
			// _view.SettingsButtonClicked.RemoveAllListeners();
			_view.PlayButtonClicked.RemoveAllListeners();

			Object.Destroy(_view.gameObject);

			_view = null;
		}
	}
}
