using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;


// TODO Refactor: move to Modules.AddressablesUtils
namespace Modules.AddressablesUtils
{
	[PublicAPI]
	[Serializable]
	public class ComponentReference<TComponent> : AssetReference where TComponent : Component
	{
		public ComponentReference (string guid) : base(guid) {}

		public new AsyncOperationHandle<TComponent> InstantiateAsync (Vector3 position, Quaternion rotation, Transform parent = null)
		{
			return Addressables.ResourceManager.CreateChainOperation(
				base.InstantiateAsync(position, rotation, parent),
				GameObjectReady
			);
		}

		public new AsyncOperationHandle<TComponent> InstantiateAsync (Transform parent = null, bool instantiateInWorldSpace = false)
		{
			return Addressables.ResourceManager.CreateChainOperation(base.InstantiateAsync(parent, instantiateInWorldSpace), GameObjectReady);
		}

		public AsyncOperationHandle<TComponent> LoadAssetAsync ()
		{
			return Addressables.ResourceManager.CreateChainOperation(base.LoadAssetAsync<GameObject>(), GameObjectReady);
		}

		private static AsyncOperationHandle<TComponent> GameObjectReady (AsyncOperationHandle<GameObject> arg)
		{
			TComponent comp = arg.Result.GetComponent<TComponent>();

			return Addressables.ResourceManager.CreateCompletedOperation(comp, string.Empty);
		}

		public override bool ValidateAsset (Object obj)
		{
			GameObject go = obj as GameObject;

			return go && go!.GetComponent<TComponent>();
		}

		public override bool ValidateAsset (string path)
		{
			#if UNITY_EDITOR
			GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(path);

			return go && go.GetComponent<TComponent>();
			#else
      return false;
			#endif
		}

		public void ReleaseInstance (AsyncOperationHandle<TComponent> op)
		{
			Component component = op.Result;

			if (component)
			{
				Addressables.ReleaseInstance(component.gameObject);
			}

			op.Release();
		}
	}
}
