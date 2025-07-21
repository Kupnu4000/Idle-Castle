using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Favourites
{
	internal class Favourites : ScriptableObject
	{
		public event Action Modified;

		[SerializeField]
		[HideInInspector]
		private List<Object> _assets = new List<Object>();

		public List<Object> Assets => _assets;

		public Object this [int index] => _assets[index];

		public void Add (IEnumerable<Object> assets)
		{
			if (TryAdd(assets) == false)
				return;

			Sort();

			EditorUtility.SetDirty(this);

			Modified?.Invoke();
		}

		public void RemoveInternal (Object asset)
		{
			if (_assets.Remove(asset) == false)
				return;

			EditorUtility.SetDirty(this);

			Modified?.Invoke();
		}

		private bool TryAdd (IEnumerable<Object> assets)
		{
			bool isChanged = false;

			foreach (Object asset in assets.Where(obj => !_assets.Contains(obj)))
			{
				_assets.Add(asset);
				isChanged = true;
			}

			return isChanged;
		}

		private void Sort ()
		{
			_assets.Sort(static (a, b) => string.Compare(a.name, b.name, StringComparison.OrdinalIgnoreCase));

			_assets.Sort(static (a, b) => string.Compare(GetAssetExtension(a), GetAssetExtension(b), StringComparison.OrdinalIgnoreCase));
		}

		private static string GetAssetExtension (Object asset)
		{
			return Path.GetExtension(AssetDatabase.GetAssetPath(asset));
		}

		public void RemoveMissingAssets ()
		{
			bool removed = false;

			foreach (Object asset in _assets.ToList())
			{
				if (asset == null)
					removed |= _assets.Remove(asset);
			}

			if (removed)
				EditorUtility.SetDirty(this);
		}

		public void Clear ()
		{
			_assets.Clear();

			EditorUtility.SetDirty(this);

			Modified?.Invoke();
		}
	}
}
