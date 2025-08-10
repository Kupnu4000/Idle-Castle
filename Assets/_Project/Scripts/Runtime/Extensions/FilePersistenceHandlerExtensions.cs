using System;
using Modules.Persistence;
using Newtonsoft.Json;
using UnityEngine;


namespace GoblinFortress.Runtime.Extensions
{
	public static class FilePersistenceHandlerExtensions
	{
		public static bool TryLoadJsonInto<T> (this FilePersistenceHandler handler, string filePath, T target) where T : class
		{
			// TODO Refactor: когда заберу нормальные версии модулей, можно будет использовать здесь инстанс хендлера
			if (FilePersistenceHandler.TryLoad(filePath, out string data) == false)
				return false;

			try
			{
				JsonConvert.PopulateObject(data, target);
				return true;
			} catch (Exception e)
			{
				Debug.LogException(new Exception($"Failed to load data from {filePath}", e));
				return false;
			}
		}
	}
}
