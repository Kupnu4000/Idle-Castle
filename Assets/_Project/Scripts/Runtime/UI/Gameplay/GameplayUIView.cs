using GoblinFortress.Runtime.UI.Widgets;
using Modules.UISystem;
using UnityEngine;


namespace GoblinFortress.Runtime.UI.Gameplay
{
	[AddressableAutoKey("Gameplay UI")]
	public class GameplayUIView : UIScreen
	{
		[Space]
		[SerializeField] private Transform _currencyWidgetRoot;
		[SerializeField] private CurrencyWidget _currencyWidget;

		[Space]
		[SerializeField] private Transform _buildingProgressMeterRoot;
		[SerializeField] private BuildingWidgetView _buildingWidgetView;

		public Transform BuildingProgressMeterRoot => _buildingProgressMeterRoot;

		// TODO Refactor: тут сделать по аналогии с BuildingProgressMeter
		public CurrencyWidget CreateCurrencyWidget ()
		{
			return Instantiate(_currencyWidget, _currencyWidgetRoot);
		}
	}
}
