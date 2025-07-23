using Modules.UISystem;
using UnityEngine;


namespace IdleCastle.Runtime.UI.Gameplay
{
	[AddressableAutoKey("Gameplay UI")]
	public class GameplayUIView : UIScreen
	{
		[Space]
		[SerializeField] private Transform _currencyWidgetRoot;
		[SerializeField] private CurrencyWidget _currencyWidget;

		public CurrencyWidget CreateCurrencyWidget ()
		{
			return Instantiate(_currencyWidget, _currencyWidgetRoot);
		}
	}
}
