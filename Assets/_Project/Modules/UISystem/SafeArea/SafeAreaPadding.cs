using UnityEngine;


namespace Modules.UISystem.SafeArea
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	internal class SafeAreaPadding : MonoBehaviour
	{
		[SerializeField] private ScreenEdgeFlags _edges = ScreenEdgeFlags.None;

		private RectTransform _rectTransform;
		private float         _canvasScaleFactor;

		private void Awake ()
		{
			_rectTransform     = GetComponent<RectTransform>();
			_canvasScaleFactor = GetComponentInParent<Canvas>().scaleFactor;
		}

		private void Start ()
		{
			SetMargins();
		}

		private void SetMargins ()
		{
			if (_edges.HasFlag(ScreenEdgeFlags.Top))
			{
				_rectTransform.offsetMax -= new Vector2(0, SafeAreaUtilities.TopMargin / _canvasScaleFactor);
			}

			if (_edges.HasFlag(ScreenEdgeFlags.Bottom))
			{
				_rectTransform.offsetMin += new Vector2(0, SafeAreaUtilities.BottomMargin / _canvasScaleFactor);
			}

			if (_edges.HasFlag(ScreenEdgeFlags.Left))
			{
				_rectTransform.offsetMin += new Vector2(SafeAreaUtilities.LeftMargin / _canvasScaleFactor, 0);
			}

			if (_edges.HasFlag(ScreenEdgeFlags.Right))
			{
				_rectTransform.offsetMax -= new Vector2(SafeAreaUtilities.RightMargin / _canvasScaleFactor, 0);
			}
		}
	}
}
