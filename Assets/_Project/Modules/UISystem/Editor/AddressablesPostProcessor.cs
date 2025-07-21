using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;


namespace Modules.UISystem.Editor
{
	[InitializeOnLoad]
	public static class AddressablesPostProcessor
	{
		private static readonly AddressableAssetSettings.ModificationEvent[] TrackedEvents = {
			AddressableAssetSettings.ModificationEvent.EntryCreated,
			AddressableAssetSettings.ModificationEvent.EntryAdded,
			AddressableAssetSettings.ModificationEvent.EntryModified
		};

		static AddressablesPostProcessor ()
		{
			AddressableAssetSettingsDefaultObject.Settings.OnModification -= OnSettingsModification;
			AddressableAssetSettingsDefaultObject.Settings.OnModification += OnSettingsModification;
		}

		private static void OnSettingsModification (AddressableAssetSettings settings, AddressableAssetSettings.ModificationEvent @event, object obj)
		{
			if (TrackedEvents.Contains(@event) == false)
				return;

			if (obj is not List<AddressableAssetEntry> entries)
				return;

			List<AddressableAssetEntry> suitableEntries = entries.Where(static entry => IsScreenEntry(entry) || IsDialogEntry(entry))
			                                                     .ToList();

			if (suitableEntries.Any() == false)
				return;

			AddressableAssetGroup group = GetOrCreateScreenGroup();

			settings.MoveEntries(suitableEntries, group, false, false);

			group.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, suitableEntries, false, true);

			settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, suitableEntries, true);
		}

		private static bool IsScreenEntry (AddressableAssetEntry entry)
		{
			if (TryGetComponent(entry, out UIScreen screen) == false)
				return false;

			if (Utils.TryGetAddressablesAutoKey(screen.GetType(), out string key) == false)
			{
				Debug.LogWarning($"UI Screen '{screen.GetType().Name}' doesn't have an '{nameof(AddressableAutoKeyAttribute)}' attribute.");
				return false;
			}

			entry.address = key;

			return true;
		}

		private static bool IsDialogEntry (AddressableAssetEntry entry)
		{
			if (TryGetComponent(entry, out UIDialog dialog) == false)
				return false;

			if (Utils.TryGetAddressablesAutoKey(dialog.GetType(), out string key) == false)
			{
				Debug.LogWarning($"Dialog '{dialog.GetType().Name}' doesn't have an '{nameof(AddressableAutoKeyAttribute)}' attribute.");
				return false;
			}

			entry.address = key;

			return true;
		}

		private static bool TryGetComponent<TComponent> (AddressableAssetEntry entry, out TComponent component) where TComponent : Component
		{
			component = null;

			return entry.MainAsset is GameObject mainAsset &&
			       mainAsset.TryGetComponent(out component);
		}

		private static AddressableAssetGroup GetOrCreateScreenGroup ()
		{
			const string groupName = "UI Screens";

			AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

			if (settings == null)
			{
				Debug.LogError("AddressableAssetSettings not found.");
				return null;
			}

			AddressableAssetGroup group = settings.FindGroup(groupName);

			if (group != null)
				return group;

			group = settings.CreateGroup(
				groupName,
				false,
				false,
				true,
				null,
				typeof(BundledAssetGroupSchema),
				typeof(ContentUpdateGroupSchema)
			);

			Debug.Log($"Addressables Asset Group '{groupName}' Created .");

			return group;
		}
	}
}
