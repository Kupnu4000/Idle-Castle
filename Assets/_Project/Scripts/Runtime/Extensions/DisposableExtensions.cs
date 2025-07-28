using System;
using Modules.AddressablesCache;


namespace IdleCastle.Runtime.Extensions
{
	public static class DisposableExtensions
	{
		public static void AddTo (this IDisposable disposable, CompositeDisposable compositeDisposable)
		{
			if (disposable == null) throw new ArgumentNullException(nameof(disposable));
			if (compositeDisposable == null) throw new ArgumentNullException(nameof(compositeDisposable));

			compositeDisposable.Add(disposable);
		}
	}
}
