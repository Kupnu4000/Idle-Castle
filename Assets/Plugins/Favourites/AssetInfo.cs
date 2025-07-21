using UnityEditor;
using UnityEngine;


namespace Favourites
{
	internal sealed class AssetInfo
	{
		public int InstanceId {get;}

		public Object  Asset    => EditorUtility.InstanceIDToObject(InstanceId);
		public string  Path     => AssetDatabase.GetAssetPath(InstanceId);
		public Texture Icon     => AssetDatabase.GetCachedIcon(Path);
		public bool    IsFolder => AssetDatabase.IsValidFolder(Path);

		public AssetInfo (Object asset)
		{
			InstanceId = asset.GetInstanceID();
		}
	}
}
