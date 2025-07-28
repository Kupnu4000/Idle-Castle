using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using IdleCastle.Runtime.Extensions;
using IdleCastle.Runtime.Gameplay;
using IdleCastle.Runtime.Gameplay.Messages;
using IdleCastle.Runtime.UI.Widgets;
using JetBrains.Annotations;
using MessagePipe;
using Modules.AddressablesCache;
using Modules.UISystem;


namespace IdleCastle.Runtime.UI.Gameplay
{
	// TODO Refactor: иконки берём из таблички, именуем item:gold_ore
	[UsedImplicitly]
	public class GameplayUIPresenter : IUIPresenter<GameplayUIView>
	{
		private readonly Dictionary<ItemId, CurrencyWidget> _currencyWidgets = new();
		private readonly Dictionary<ItemId, BuildingWidget> _buildingWidgets = new();
		private readonly Dictionary<ItemId, CurrencyConfig> _currencyConfigs;
		private readonly UIFacadeFactory                    _uiFacadeFactory;
		private readonly CompositeDisposable                _disposables = new();

		private GameplayUIView _view;

		// TODO: cleanup
		public GameplayUIPresenter (
			GoldCurrencyConfig goldConfig,
			ISubscriber<BuildingCreated> buildingCreated,
			ISubscriber<CurrencyAmountChanged> currencyAmountChanged,
			UIFacadeFactory uiFacadeFactory
		)
		{
			_uiFacadeFactory = uiFacadeFactory;

			buildingCreated.Subscribe(HandleBuildingCreated).AddTo(_disposables);
			currencyAmountChanged.Subscribe(HandleCurrencyChanged).AddTo(_disposables);

			_currencyConfigs = new Dictionary<ItemId, CurrencyConfig> {
				[goldConfig.CurrencyId] = goldConfig
			};

			// TODO: optimize
			// _currencyConfigs = configs.Where(static config => config is CurrencyConfig)
			//                           .Cast<CurrencyConfig>()
			//                           .ToDictionary(static config => config.BuildingId);
		}

		public void Initialize (GameplayUIView view)
		{
			_view = view;
		}

		private void HandleBuildingCreated (BuildingCreated message)
		{
			Impl(message.Building).Forget();
			return;

			async UniTask Impl (IBuilding building)
			{
				if (_buildingWidgets.ContainsKey(building.Id))
					throw new InvalidOperationException($"Building widget for {building.Id} already exists.");

				BuildingWidget widget = await _uiFacadeFactory.Create<BuildingWidget>(_view.BuildingProgressMeterRoot);

				widget.SetBuilding(building);

				_buildingWidgets.Add(building.Id, widget);
			}
		}

		private void HandleCurrencyChanged (CurrencyAmountChanged message)
		{
			if (!_currencyWidgets.TryGetValue(message.CurrencyId, out CurrencyWidget widget))
			{
				widget = _view.CreateCurrencyWidget();

				_currencyWidgets.Add(message.CurrencyId, widget);

				widget.Initialize(_currencyConfigs[message.CurrencyId]);
			}

			widget.SetValue(message.NewValue);
		}

		public void Dispose ()
		{
			_disposables.Dispose();

			_currencyWidgets.Clear();
		}
	}
}
