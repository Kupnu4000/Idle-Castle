using Modules.UISystem;
using UnityEngine;
using UnityEngine.UI;


namespace IdleCastle.UI.Lobby
{
	public class LobbyScreenView : UIScreenView
	{
		[SerializeField] private Button _playButton;

		public Button.ButtonClickedEvent PlayButtonClicked => _playButton.onClick;

		public override void Dispose ()
		{
			_playButton.onClick.RemoveAllListeners();
		}
	}
}
