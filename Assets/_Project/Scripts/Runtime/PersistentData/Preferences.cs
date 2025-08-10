using System;
using Modules.Persistence;
using Newtonsoft.Json;
using UnityEngine;


namespace GoblinFortress.Runtime.PersistentData
{
	[Serializable]
	public sealed class Preferences
	{
		private const string Key = "preferences";

		[JsonProperty("sound_enabled")] private bool _isSoundEnabled = true;

		public bool IsSoundEnabled => _isSoundEnabled;

		public bool TryLoad ()
		{
			if (!PlayerPrefsPersistenceHandler.TryLoad(Key, out string data))
			{
				return false;
			}

			try
			{
				JsonConvert.PopulateObject(data, this);
				return true;
			} catch (Exception e)
			{
				Debug.LogException(new Exception($"Failed to load preferences: {e.Message}"));
				return false;
			}
		}

		public void Reset ()
		{
			string json = JsonConvert.SerializeObject(new Preferences());

			JsonConvert.PopulateObject(json, this);
			Save();
		}

		private void Save ()
		{
			string json = JsonConvert.SerializeObject(this, Formatting.Indented);

			PlayerPrefsPersistenceHandler.Save(Key, json);
		}

		public void SetSoundEnabled (bool value)
		{
			_isSoundEnabled = value;

			Save();
		}
	}
}
