using Cysharp.Threading.Tasks;
using UnityEngine;


namespace Modules.UISystem
{
	public abstract class UIFacade<TView, TPresenter> : IUIFacade
		where TView : IUIView
		where TPresenter : IUIPresenter<TView>
	{
		protected TPresenter Presenter;
		protected TView      View;

		protected abstract UniTask<TPresenter> CreatePresenter ();

		protected abstract UniTask<TView> CreateView (Transform parent);

		public async UniTask Initialize (Transform parent)
		{
			Presenter = await CreatePresenter();
			View      = await CreateView(parent);

			Presenter.Initialize(View);
		}

		public virtual void Dispose ()
		{
			Presenter.Dispose();
		}
	}
}
