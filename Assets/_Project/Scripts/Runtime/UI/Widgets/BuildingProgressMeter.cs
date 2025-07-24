using UnityEngine;


namespace IdleCastle.Runtime.UI.Widgets
{
	public class BuildingProgressMeter : MonoBehaviour
	{
		[SerializeField] private ProgressBar _progressBar;

		public void SetNormalizedProgress (float value)
		{
			_progressBar.value = value;
		}
	}
}