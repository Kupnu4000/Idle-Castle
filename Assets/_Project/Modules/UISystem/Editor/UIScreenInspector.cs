using UnityEditor;


namespace Modules.UISystem.Editor
{
	[CustomEditor(typeof(UIScreen), true)]
	internal class UIScreenInspector : UnityEditor.Editor
	{
		private UIScreen _target;

		private void OnEnable ()
		{
			_target = (UIScreen)target;
		}

		public override void OnInspectorGUI ()
		{
			if (Utils.TryGetAddressablesAutoKey(_target.GetType(), out string autoKey) == false)
			{
				EditorGUILayout.HelpBox($"{nameof(AddressableAutoKeyAttribute)} attribute is missing", MessageType.Warning);
			} else
			{
				EditorGUILayout.HelpBox($"Auto key: {autoKey}", MessageType.None, false);
			}

			base.OnInspectorGUI();
		}
	}
}
