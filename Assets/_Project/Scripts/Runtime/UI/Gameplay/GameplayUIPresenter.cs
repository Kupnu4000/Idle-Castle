using System;
using System.Collections.Generic;
using IdleCastle.Runtime.Gameplay;
using IdleCastle.Runtime.Gameplay.Messages;
using IdleCastle.Runtime.UI.Widgets;
using JetBrains.Annotations;
using MessagePipe;
using Modules.UISystem;


namespace IdleCastle.Runtime.UI.Gameplay
{
	// TODO Refactor: иконки берём из таблички, именуем item:gold_ore
	[UsedImplicitly]
	public class GameplayUIPresenter : IScreenPresenter<GameplayUIView>
	{
		private readonly Dictionary<ItemId, CurrencyWidget> _currencyWidgets = new();
		private readonly Dictionary<ItemId, CurrencyConfig> _currencyConfigs;

		private GameplayUIView _view;

		private IDisposable _currencyAmountChanged;

		public GameplayUIPresenter (
			GoldCurrencyConfig goldConfig,
			ISubscriber<CurrencyAmountChanged> currencyAmountChanged
		)
		{
			_currencyAmountChanged = currencyAmountChanged.Subscribe(HandleCurrencyChanged);

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

		private void HandleCurrencyChanged (CurrencyAmountChanged @event)
		{
			if (!_currencyWidgets.TryGetValue(@event.CurrencyId, out CurrencyWidget widget))
			{
				widget = _view.CreateCurrencyWidget();

				_currencyWidgets.Add(@event.CurrencyId, widget);

				widget.Initialize(_currencyConfigs[@event.CurrencyId]);
			}

			widget.SetValue(@event.NewValue);
		}

		public void Dispose ()
		{
			_currencyAmountChanged?.Dispose();
			_currencyAmountChanged = null;

			_currencyWidgets.Clear();
		}
	}
}
