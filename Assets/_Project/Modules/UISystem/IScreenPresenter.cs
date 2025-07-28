using System;


namespace Modules.UISystem
{
	public interface IUIPresenter<in TView> : IDisposable where TView : IUIView
	{
		void Initialize (TView view);
	}
}
