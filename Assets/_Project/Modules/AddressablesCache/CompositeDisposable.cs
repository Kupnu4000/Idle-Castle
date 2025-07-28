using System;
using System.Collections.Generic;


namespace Modules.AddressablesCache
{
	public class CompositeDisposable : IDisposable
	{
		private readonly List<IDisposable> _disposables = new();

		private bool _isDisposed;

		public void Add (IDisposable disposable)
		{
			if (_isDisposed)
			{
				disposable.Dispose();
				return;
			}

			_disposables.Add(disposable);
		}

		public void Dispose ()
		{
			if (_isDisposed) return;

			_isDisposed = true;

			foreach (IDisposable disposable in _disposables)
				disposable.Dispose();

			_disposables.Clear();
		}
	}
}
