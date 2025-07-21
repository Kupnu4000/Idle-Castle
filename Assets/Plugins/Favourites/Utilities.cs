using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Favourites
{
	internal static class Utilities
	{
		[MenuItem("Assets/Add to Favourites", priority = 9999)]
		private static void AddToFavourites ()
		{
			Favourites favourites = GetOrCreateFavourites();

			Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);

			favourites.Add(selection);
		}

		public static Favourites GetOrCreateFavourites ()
		{
			const string directory = "Assets/Plugins/MyNewFavourite";
			string       assetPath = Path.Join(directory, "Favourites.asset");

			if (Directory.Exists(directory) == false)
				Directory.CreateDirectory(directory);

			Favourites favourites = AssetDatabase.LoadAssetAtPath<Favourites>(assetPath);

			if (favourites != null)
				return favourites;

			favourites = ScriptableObject.CreateInstance<Favourites>();

			AssetDatabase.CreateAsset(favourites, assetPath);
			AssetDatabase.SaveAssets();

			return favourites;
		}

		public static bool TryOpenFolderInProjectBrowser (AssetInfo assetInfo)
		{
			if (assetInfo.IsFolder == false)
				return false;

			Type projectBrowserType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ProjectBrowser");

			if (projectBrowserType == null)
				throw new Exception("Type UnityEditor.ProjectBrowser not found");

			EditorWindow projectBrowser = projectBrowserType.GetField(
				"s_LastInteractedProjectBrowser",
				BindingFlags.Static | BindingFlags.Public
			)?.GetValue(null) as EditorWindow;

			if (projectBrowser == null)
				return false;

			MethodInfo showFolderContents = projectBrowserType.GetMethod(
				"ShowFolderContents",
				BindingFlags.NonPublic | BindingFlags.Instance
			);

			if (showFolderContents == null)
				throw new Exception("Method ShowFolderContents not found");

			object[] parameters = { assetInfo.InstanceId, false };
			showFolderContents.Invoke(projectBrowser, parameters);

			return true;
		}
	}
}
