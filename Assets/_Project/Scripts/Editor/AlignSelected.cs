using System.Linq;
using UnityEditor;
using UnityEngine;


namespace GoblinFortress.Editor
{
	public static class AlignSelected
	{
		[MenuItem("Edit/Align Selected #a")]
		private static void PerformAlignSelected ()
		{
			Transform[] transforms = Selection.transforms
			                                  .Where(static transform => transform.gameObject.scene.IsValid())
			                                  .ToArray();

			if (transforms.Length == 0)
				return;

			Object[] objects = transforms.Select(static transform => (Object)transform)
			                             .ToArray();

			Undo.RegisterCompleteObjectUndo(objects, "Align Selected to Grid");

			Vector3 gridSize = EditorSnapSettings.gridSize;
			float   rotate   = EditorSnapSettings.rotate;

			foreach (Transform transform in transforms)
			{
				SnapPosition(transform, gridSize);
				SnapRotation(transform, rotate);

				EditorUtility.SetDirty(transform);
			}
		}

		private static void SnapPosition (Transform transform, Vector3 gridSize)
		{
			Vector3 position = transform.position;

			position.x = Mathf.Round(position.x / gridSize.x) * gridSize.x;
			position.y = Mathf.Round(position.y / gridSize.y) * gridSize.y;
			position.z = Mathf.Round(position.z / gridSize.z) * gridSize.z;

			transform.position = position;
		}

		private static void SnapRotation (Transform transform, float rotate)
		{
			Vector3 eulerAngles = transform.eulerAngles;

			eulerAngles.x = Mathf.Round(eulerAngles.x / rotate) * rotate;
			eulerAngles.y = Mathf.Round(eulerAngles.y / rotate) * rotate;
			eulerAngles.z = Mathf.Round(eulerAngles.z / rotate) * rotate;

			transform.eulerAngles = eulerAngles;
		}
	}
}
