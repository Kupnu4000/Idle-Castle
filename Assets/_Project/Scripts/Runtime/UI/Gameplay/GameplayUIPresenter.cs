using System.Collections.Generic;
using IdleCastle.Runtime.Gameplay;
using IdleCastle.Runtime.UI.Widgets;
using JetBrains.Annotations;
using Modules.UISystem;
using UnityEngine;


namespace IdleCastle.Runtime.UI.Gameplay
{
	// TODO Refactor: иконки берём из таблички, именуем item:gold_ore
	[UsedImplicitly]
	public class GameplayUIPresenter : IScreenPresenter<GameplayUIView>
	{
		private readonly Dictionary<ItemId, CurrencyWidget> _currencyWidgets = new();
		private readonly Dictionary<ItemId, CurrencyConfig> _currencyConfigs;

		private GameplayUIView _view;

		public GameplayUIPresenter (GoldCurrencyConfig goldConfig)
		{
			_currencyConfigs = new Dictionary<ItemId, CurrencyConfig> {
				[goldConfig.CurrencyId] = goldConfig
			};

			// TODO: optimize
			// _currencyConfigs = configs.Where(static config => config is CurrencyConfig)
			//                           .Cast<CurrencyConfig>()
			//                           .ToDictionary(static config => config.CurrencyId);
		}

		public void Initialize (GameplayUIView view)
		{
			_view = view;
		}

		public void HandleCurrencyChanged (ItemId currencyId, double newValue)
		{
			if (!_currencyWidgets.TryGetValue(currencyId, out CurrencyWidget widget))
			{
				widget = _view.CreateCurrencyWidget();

				_currencyWidgets.Add(currencyId, widget);

				widget.Initialize(_currencyConfigs[currencyId]);
			}

			widget.SetValue(newValue);
		}

		public void Dispose ()
		{
			Object.Destroy(_view);
			_view = null;
		}
	}
}
