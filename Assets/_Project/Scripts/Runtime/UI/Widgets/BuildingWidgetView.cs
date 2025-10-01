using Modules.UISystem;
using UnityEngine;


namespace IdleCastle.UI.Widgets
{
	public class BuildingWidgetView : UIView
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

		public override void Dispose () {}
	}
}
