using IdleCastle.Gameplay.Buildings;
using IdleCastle.Gameplay.GameTime;
using JetBrains.Annotations;
using Modules.UISystem;
using UnityEngine;


namespace IdleCastle.UI.Widgets
{
	[UsedImplicitly]
	public class BuildingWidgetPresenter : IUIPresenter<BuildingWidgetView>, ILateTickable
	{
		private readonly ITickRunner _tickRunner;

		private IBuilding          _model;
		private BuildingWidgetView _view;

		public BuildingWidgetPresenter (ITickRunner tickRunner)
		{
			_tickRunner = tickRunner;
		}

		public void Initialize (BuildingWidgetView view)
		{
			_view = view;

			_tickRunner.Register(this);
		}

		public void SetBuilding (IBuilding building)
		{
			_model = building;
		}

		public void LateTick (float unused)
		{
			_view.SetNormalizedProgress(_model.NormalizedProgress);
		}

		public void Dispose ()
		{
			_tickRunner?.Unregister(this);

			// TODO: убедиться, что вызывается хоть где-то
			Debug.Log("Disposed!");
		}
	}
}
