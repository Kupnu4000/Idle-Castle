using UnityEngine;


namespace Modules.UISystem
{
	public sealed class ScreenReference : MonoBehaviour
	{
		private void Start ()
		{
			#if UNITY_EDITOR || DEVELOPMENT_BUILD
			Debug.LogWarning($"Derelict Screen Reference: {GetHierarchyPath()}");
			#endif

			Destroy(gameObject);
		}

		private string GetHierarchyPath ()
		{
			Transform currentTransform = transform;

			string path = currentTransform.name;

			while (currentTransform.parent != null)
			{
				currentTransform = currentTransform.parent;

				path = currentTransform.name + "/" + path;
			}

			return path;
		}
	}
}
