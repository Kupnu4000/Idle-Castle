using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace Modules.UISystem
{
	[UsedImplicitly]
	internal class PrototypeProvider
	{
		private readonly int _capacity;

		private readonly Dictionary<Type, Component> _prototypes = new();
		private readonly Queue<Type>                 _order      = new();

		internal PrototypeProvider (int capacity)
		{
			_capacity = capacity;
		}

		internal TScreen GetScreenPrototype<TScreen> () where TScreen : UIScreen
		{
			return GetPrototype<TScreen>();
		}

		internal TDialog GetDialogPrototype<TDialog> () where TDialog : UIDialog
		{
			return GetPrototype<TDialog>();
		}

		private TComponent GetPrototype<TComponent> () where TComponent : Component
		{
			Type type = typeof(TComponent);

			if (TryGetCachedPrototype(out TComponent prototype))
			{
				return prototype;
			}

			if (Utils.TryGetAddressablesAutoKey(type, out string key) == false)
			{
				throw new Exception($"'{type.Name}' is missing '{nameof(AddressableAutoKeyAttribute)}' attribute.");
			}

			GameObject asset = Addressables.LoadAssetAsync<GameObject>(key).WaitForCompletion();

			if (!asset)
			{
				throw new Exception($"Asset '{key}' is missing.");
			}

			if (asset.TryGetComponent(out prototype) == false)
			{
				throw new Exception($"Asset '{asset.gameObject.name}' is missing '{type.Name}' component.");
			}

			RegisterPrototype(prototype);

			return prototype;
		}

		private bool TryGetCachedPrototype<TComponent> (out TComponent prototype) where TComponent : Component
		{
			if (_prototypes.TryGetValue(typeof(TComponent), out Component cachedPrototype))
			{
				prototype = (TComponent)cachedPrototype;
				return true;
			}

			prototype = null;
			return false;
		}

		private void RegisterPrototype<TComponent> (TComponent prototype) where TComponent : Component
		{
			Type type = typeof(TComponent);

			if (_prototypes.ContainsKey(type))
				return;

			if (_prototypes.Count >= _capacity)
			{
				Type oldestType = _order.Dequeue();
				_prototypes.Remove(oldestType);
			}

			_prototypes.Add(type, prototype);
			_order.Enqueue(type);
		}
	}
}
