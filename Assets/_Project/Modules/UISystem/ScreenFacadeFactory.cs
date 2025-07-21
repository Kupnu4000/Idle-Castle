using System;
using JetBrains.Annotations;
using Modules.UISystem.Interfaces;
using Zenject;
using Object = UnityEngine.Object;


namespace Modules.UISystem
{
	[PublicAPI]
	public class ScreenFacadeFactory
	{
		private readonly IInstantiator _instantiator;

		public ScreenFacadeFactory (IInstantiator instantiator)
		{
			_instantiator = instantiator;
		}

		public TFacade Create<TFacade> () where TFacade : IScreenFacade
		{
			TFacade facade = _instantiator.Instantiate<TFacade>();

			facade.Initialize();

			return facade;
		}
	}

	public interface IScreenFacade : IDisposable
	{
		void Initialize ();
	}

	public class ScreenFacade<TView, TPresenter> : IScreenFacade
		where TView : UIScreen
		where TPresenter : IScreenPresenter<TView>
	{
		private readonly TPresenter _presenter;
		private readonly TView      _view;

		public ScreenFacade (TPresenter presenter, IUISystem uiSystem)
		{
			_presenter = presenter;
			_view      = uiSystem.SpawnScreen<TView>();
		}

		public void Initialize ()
		{
			_presenter.Initialize(_view);
		}

		public void Dispose ()
		{
			_presenter.Dispose();
			Object.Destroy(_view.gameObject);
		}
	}

	public interface IScreenPresenter<in TView> : IDisposable where TView : UIScreen
	{
		void Initialize (TView view);
	}
}
