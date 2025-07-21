using UnityEditor;


namespace Favourites
{
	internal sealed class FavouritesAssetPostprocessor : AssetPostprocessor
	{
		private static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			if (deletedAssets.Length > 0)
				Utilities.GetOrCreateFavourites().RemoveMissingAssets();
		}
	}
}