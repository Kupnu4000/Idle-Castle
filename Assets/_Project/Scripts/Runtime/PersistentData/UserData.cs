using System;
using System.IO;
using Modules.Persistence;
using Newtonsoft.Json;
using UnityEngine;


namespace IdleCastle.PersistentData
{
	[Serializable]
	public class UserData
	{
		[JsonIgnore] public string FilePath => Path.Combine(Application.persistentDataPath, "user_data.sav");

		public bool TryLoad ()
		{
			// TODO Refactor: использовать здесь метод TryLoadInto из FilePersistenceHandlerExtensions
			if (!FilePersistenceHandler.TryLoad(FilePath, out string data))
			{
				return false;
			}

			try
			{
				JsonConvert.PopulateObject(data, this);
				return true;
			} catch (Exception e)
			{
				Debug.LogException(new Exception($"Failed to load user data from {FilePath}", e));
				return false;
			}
		}

		public void Save ()
		{
			string json = JsonConvert.SerializeObject(this, Formatting.Indented);

			FilePersistenceHandler.Save(FilePath, json);
		}
	}
}
