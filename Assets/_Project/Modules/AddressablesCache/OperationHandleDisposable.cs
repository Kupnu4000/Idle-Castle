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
		private readonly string _subObjectName;
		private readonly string _assetGuid;

		private AsyncOperationHandle _handle;

		public Object Asset => (Object)_handle.Result;

		public OperationHandleDisposable (AssetReference assetReference)
		{
			_subObjectName = assetReference.SubObjectName;
			_assetGuid     = assetReference.AssetGUID;
			_handle        = assetReference.LoadAssetAsync<Object>();
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

			if (_handle.Status == AsyncOperationStatus.Failed)
			{
				throw new Exception($"Failed to load asset {_subObjectName} with GUID: {_assetGuid}");
			}
		}

		public void Dispose ()
		{
			if (_handle.IsValid())
				Addressables.Release(_handle);
		}
	}
}
