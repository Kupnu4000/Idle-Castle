using Modules.UISystem;
using UnityEngine;


namespace GoblinFortress.Runtime.UI.Widgets
{
	public class BuildingWidgetView : MonoBehaviour, IUIView
	{
		[SerializeField] private ProgressBar _progressBar;

		private void Awake ()
		{
			_progressBar.value = 0;
		}

		public void SetNormalizedProgress (float value)
		{
			_progressBar.value = value;
		}
	}
}
