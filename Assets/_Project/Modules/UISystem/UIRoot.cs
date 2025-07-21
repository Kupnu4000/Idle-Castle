using UnityEngine;


namespace Modules.UISystem
{
	[DisallowMultipleComponent]
	internal class UIRoot : MonoBehaviour
	{
		[SerializeField] private Camera _camera;
		[SerializeField] private Canvas _canvas;
		[SerializeField] private Fade   _fade;

		public Camera Camera => _camera;
		public Canvas Canvas => _canvas;
		public Fade   Fade   => _fade;
	}
}
