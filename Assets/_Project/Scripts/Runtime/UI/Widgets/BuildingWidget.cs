using Cysharp.Threading.Tasks;
using IdleCastle.Runtime.Configs;
using IdleCastle.Runtime.Gameplay;
using IdleCastle.Runtime.Zenject;
using JetBrains.Annotations;
using Modules.AddressablesUtils;
using Modules.UISystem;
using UnityEngine;


namespace IdleCastle.Runtime.UI.Widgets
{
	// TODO Refactor: dispose. Presenter subscribes to the ticker, so it should be disposed
	[UsedImplicitly]
	public class BuildingWidget : UIFacade<BuildingWidgetView, BuildingWidgetPresenter>
	{
		private readonly AssetReferenceProvider _assetProvider;
		private readonly AddressablesCache      _addressablesCache;
		private readonly GenericFactory         _factory;

		public BuildingWidget (
			AssetReferenceProvider assetProvider,
			AddressablesCache addressablesCache,
			GenericFactory factory
		)
		{
			_assetProvider     = assetProvider;
			_addressablesCache = addressablesCache;
			_factory           = factory;
		}

		protected override UniTask<BuildingWidgetPresenter> CreatePresenter ()
		{
			BuildingWidgetPresenter presenter = _factory.Create<BuildingWidgetPresenter>();

			return UniTask.FromResult(presenter);
		}

		protected override UniTask<BuildingWidgetView> CreateView (Transform parent)
		{
			BuildingWidgetView view = _addressablesCache.Instantiate(_assetProvider.BuildingWidgetView, parent);

			return UniTask.FromResult(view);
		}

		// TODO Refactor: это Initialize, по идее
		public void SetBuilding (IBuilding building)
		{
			Presenter.SetBuilding(building);
		}
	}
}
