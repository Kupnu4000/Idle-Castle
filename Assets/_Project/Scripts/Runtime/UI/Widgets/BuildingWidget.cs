using IdleCastle.Gameplay.Buildings;
using JetBrains.Annotations;
using Modules.UISystem;
using UnityEngine;


namespace IdleCastle.UI.Widgets
{
	// TODO Refactor: dispose. Presenter subscribes to the ticker, so it should be disposed
	[UsedImplicitly]
	public class BuildingWidget : UIWidget<BuildingWidgetView, BuildingWidgetPresenter>
	{
		public BuildingWidget (
			IBuilding building,
			RectTransform parent,
			IUIViewFactory<BuildingWidgetView> viewFactory,
			IUIPresenterFactory<BuildingWidgetPresenter, BuildingWidgetView> presenterFactory
		) : base(viewFactory, presenterFactory)
		{
			Presenter.SetBuilding(building);
			View.transform.SetParent(parent, worldPositionStays: false);
		}
	}
}
