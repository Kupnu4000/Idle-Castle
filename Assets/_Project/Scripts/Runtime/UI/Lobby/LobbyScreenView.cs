using Modules.UISystem;
using UnityEngine;
using UnityEngine.UI;


namespace GoblinFortress.Runtime.UI.Lobby
{
	[AddressableAutoKey("Lobby Screen")]
	public class LobbyScreenView : UIScreen
	{
		[SerializeField] private Button _playButton;

		public Button.ButtonClickedEvent PlayButtonClicked => _playButton.onClick;
	}
}
