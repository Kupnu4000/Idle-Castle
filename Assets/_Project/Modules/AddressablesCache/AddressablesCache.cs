using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;


namespace Modules.AddressablesCache
{
	[PublicAPI]
	public class AddressablesCache : IDisposable
	{
		private readonly Dictionary<string, OperationHandleDisposable> _assetCache = new();

		public async UniTask<IDisposable> LoadAssetAsync (AssetReference assetReference, CancellationToken cancellationToken = default)
		{
			TryAddAssetReference(assetReference, out OperationHandleDisposable handle);

			await handle.LoadAssetAsync(cancellationToken);

			return handle;
		}

		public Object Get (AssetReference assetReference)
		{
			string guid = assetReference.AssetGUID;

			if (_assetCache.TryGetValue(guid, out OperationHandleDisposable cachedAsset))
			{
				return cachedAsset.Asset;
			}

			throw new KeyNotFoundException($"Asset {assetReference.SubObjectName} with GUID: {guid} not loaded.");
		}

		public T Get<T> (AssetReferenceT<T> assetReference) where T : Object
		{
			string guid = assetReference.AssetGUID;

			if (_assetCache.TryGetValue(guid, out OperationHandleDisposable cachedAsset))
			{
				return (T)cachedAsset.Asset;
			}

			throw new KeyNotFoundException($"Asset {assetReference.SubObjectName} with GUID: {guid} not loaded.");
		}

		public T Get<T> (ComponentReference<T> assetReference) where T : Component
		{
			string guid = assetReference.AssetGUID;

			if (_assetCache.TryGetValue(guid, out OperationHandleDisposable cachedAsset))
			{
				GameObject gameObject = (GameObject)cachedAsset.Asset;

				return gameObject.GetComponent<T>();
			}

			throw new KeyNotFoundException($"Asset {assetReference.SubObjectName} with GUID: {guid} not loaded.");
		}

		public GameObject Instantiate (
			AssetReferenceGameObject assetReference,
			Vector3 position = default,
			Quaternion rotation = default,
			Transform parent = null
		)
		{
			string guid = assetReference.AssetGUID;

			if (!_assetCache.TryGetValue(guid, out OperationHandleDisposable cachedAsset))
				throw new KeyNotFoundException($"Asset {assetReference.SubObjectName} with GUID: {guid} not loaded.");

			GameObject prefab = (GameObject)cachedAsset.Asset;
			return Object.Instantiate(prefab, position, rotation, parent);
		}

		public T Instantiate<T> (
			ComponentReference<T> assetReference,
			Vector3 position = default,
			Quaternion rotation = default,
			Transform parent = null
		) where T : Component
		{
			string guid = assetReference.AssetGUID;

			if (!_assetCache.TryGetValue(guid, out OperationHandleDisposable cachedAsset))
				throw new KeyNotFoundException($"Asset {assetReference.SubObjectName} with GUID: {guid} not loaded.");

			GameObject gameObject = (GameObject)cachedAsset.Asset;
			GameObject instance   = Object.Instantiate(gameObject, position, rotation, parent);

			return instance.GetComponent<T>();
		}

		public GameObject Instantiate (AssetReferenceGameObject assetReference, Transform parent)
		{
			return Instantiate(assetReference, Vector3.zero, Quaternion.identity, parent);
		}

		public T Instantiate<T> (ComponentReference<T> assetReference, Transform parent) where T : Component
		{
			return Instantiate(assetReference, Vector3.zero, Quaternion.identity, parent);
		}

		public void UnloadAsset (AssetReference assetReference)
		{
			string guid = assetReference.AssetGUID;

			if (!_assetCache.Remove(guid))
				return;

			_assetCache.Remove(guid, out OperationHandleDisposable handle);

			handle.Dispose();
		}

		public void UnloadAll ()
		{
			foreach (OperationHandleDisposable handle in _assetCache.Values)
			{
				handle.Dispose();
			}

			_assetCache.Clear();
		}

		public ILoadingGroupBuilder BuildLoadingGroup ()
		{
			return new AssetLoadingGroupBuilder(this);
		}

		internal bool TryAddAssetReference (AssetReference assetReference, out OperationHandleDisposable handle)
		{
			string guid = assetReference.AssetGUID;

			if (_assetCache.TryGetValue(guid, out handle))
				return false;

			handle = OperationHandleDisposable.Create(assetReference);

			_assetCache[guid] = handle;

			return true;
		}

		public void Dispose ()
		{
			UnloadAll();
		}
	}
}
