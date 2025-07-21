using Modules.UISystem;
using UnityEngine;
using UnityEngine.UI;


namespace IdleCastle.Runtime.UI.Gameplay
{
	public class GameplayUIView : UIScreen
	{
		[SerializeField] private Button _buyUpgradeButton;

		public Button.ButtonClickedEvent OnBuyUpgradeButtonClicked;
	}
}
