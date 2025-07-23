using UnityEditor;


namespace Modules.UISystem.Editor
{
	[CustomEditor(typeof(UIDialog), true)]
	public class UIDialogInspector : UnityEditor.Editor
	{
		private UIDialog _target;

		private void OnEnable ()
		{
			_target = (UIDialog)target;
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
