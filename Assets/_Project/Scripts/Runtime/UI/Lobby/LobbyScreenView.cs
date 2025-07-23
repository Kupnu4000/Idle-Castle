using Modules.UISystem;
using UnityEngine;
using UnityEngine.UI;


namespace IdleCastle.Runtime.UI.Lobby
{
	[AddressableAutoKey("Lobby Screen")]
	public class LobbyScreenView : UIScreen
	{
		// [SerializeField] private Button _settingsButton;
		[SerializeField] private Button _playButton;

		// public Button.ButtonClickedEvent SettingsButtonClicked => _settingsButton.onClick;
		public Button.ButtonClickedEvent PlayButtonClicked => _playButton.onClick;
	}
}
