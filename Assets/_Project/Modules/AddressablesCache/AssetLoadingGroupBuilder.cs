using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;


namespace Modules.AddressablesCache
{
	internal class AssetLoadingGroupBuilder : ILoadingGroupBuilder
	{
		private readonly AddressablesCache               _cache;
		private readonly List<OperationHandleDisposable> _handles = new();

		public AssetLoadingGroupBuilder (AddressablesCache cache)
		{
			_cache = cache;
		}

		public ILoadingGroupBuilder Add (AssetReference assetReference)
		{
			if (assetReference == null)
				throw new ArgumentNullException(nameof(assetReference));

			_cache.TryAddAssetReference(assetReference, out OperationHandleDisposable handle);

			_handles.Add(handle);

			return this;
		}

		public async UniTask<IDisposable> LoadAsync (CancellationToken cancellationToken = default)
		{
			CompositeDisposable disposables = new();

			List<UniTask> tasks = new();

			foreach (OperationHandleDisposable handle in _handles)
			{
				disposables.Add(handle);

				tasks.Add(handle.LoadAssetAsync(cancellationToken));
			}

			await UniTask.WhenAll(tasks);

			return disposables;
		}
	}
}
