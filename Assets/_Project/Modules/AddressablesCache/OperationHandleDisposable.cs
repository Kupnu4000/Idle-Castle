using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;


namespace Modules.AddressablesCache
{
	internal class OperationHandleDisposable : IDisposable
	{
		private AsyncOperationHandle _handle;

		public Object Asset => (Object)_handle.Result;

		private OperationHandleDisposable (AsyncOperationHandle handle)
		{
			_handle = handle;
		}

		public static OperationHandleDisposable Create (AssetReference assetReference)
		{
			string guid = assetReference.AssetGUID;

			AsyncOperationHandle handle = assetReference.LoadAssetAsync<Object>();

			if (handle.Status == AsyncOperationStatus.Failed)
			{
				handle.Release();
				throw new Exception($"Failed to load asset {assetReference.SubObjectName} with GUID: {guid}");
			}

			return new OperationHandleDisposable(handle);
		}

		private bool IsLoaded ()
		{
			return _handle.IsValid() && _handle.Status == AsyncOperationStatus.Succeeded;
		}

		public async UniTask LoadAssetAsync (CancellationToken cancellationToken = default)
		{
			if (IsLoaded())
				return;

			await _handle.WithCancellation(cancellationToken);
		}

		public void Dispose ()
		{
			if (_handle.IsValid())
				Addressables.Release(_handle);
		}
	}
}
