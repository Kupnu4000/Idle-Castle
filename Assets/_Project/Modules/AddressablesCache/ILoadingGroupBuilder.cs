using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;


namespace Modules.AddressablesCache
{
	public interface ILoadingGroupBuilder
	{
		ILoadingGroupBuilder Add (AssetReference assetReference);

		UniTask<IDisposable> LoadAsync (CancellationToken cancellationToken = default);
	}
}
