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

	public abstract class ScreenFacade<TView, TPresenter> : IScreenFacade
		where TView : UIScreen
		where TPresenter : IScreenPresenter<TView>
	{
		protected readonly TPresenter Presenter;
		protected readonly TView      View;

		protected ScreenFacade (TPresenter presenter, IUISystem uiSystem)
		{
			Presenter = presenter;
			View      = uiSystem.SpawnScreen<TView>();
		}

		public void Initialize ()
		{
			Presenter.Initialize(View);
		}

		public void Dispose ()
		{
			Presenter.Dispose();
			Object.Destroy(View.gameObject);
		}
	}

	public interface IScreenPresenter<in TView> : IDisposable where TView : UIScreen
	{
		void Initialize (TView view);
	}
}
