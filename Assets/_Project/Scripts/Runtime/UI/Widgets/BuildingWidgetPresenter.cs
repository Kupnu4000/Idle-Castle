using GoblinFortress.Runtime.Gameplay;
using GoblinFortress.Runtime.Gameplay.Buildings;
using JetBrains.Annotations;
using Modules.UISystem;
using UnityEngine;


namespace GoblinFortress.Runtime.UI.Widgets
{
	[UsedImplicitly]
	public class BuildingWidgetPresenter : IUIPresenter<BuildingWidgetView>
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

			_tickRunner.OnLateTick += HandleLateTick;
		}

		// TODO Refactor: это не нравится. Можно было бы здание передавать в конструктор, а можно попробовать сделать билдер фасадов
		public void SetBuilding (IBuilding building)
		{
			_model = building;
		}

		private void HandleLateTick (float deltaTime)
		{
			_view.SetNormalizedProgress(_model.NormalizedProgress);
		}

		public void Dispose ()
		{
			if (_tickRunner != null)
			{
				_tickRunner.OnLateTick -= HandleLateTick;
			}

			Debug.Log("Disposed!");
		}
	}
}
