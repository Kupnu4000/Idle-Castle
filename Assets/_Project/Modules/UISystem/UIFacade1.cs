namespace Modules.UISystem
{
	// TODO: rename file
	public abstract class UIFacade<TModel, TView, TPresenter> : IUIFacade<TModel>
		where TView : IUIView
		where TPresenter : IUIPresenter<TView>
	{
		protected readonly TPresenter Presenter;
		private            TView      _view;

		protected UIFacade (TPresenter presenter)
		{
			Presenter = presenter;
		}

		protected abstract TView CreateView ();

		public void Initialize (TModel model)
		{
			_view = CreateView();

			Presenter.Initialize(_view);
		}

		public virtual void Dispose ()
		{
			Presenter.Dispose();
		}
	}
}
