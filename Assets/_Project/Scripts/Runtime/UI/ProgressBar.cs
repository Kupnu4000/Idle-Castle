using UnityEngine;
using UnityEngine.EventSystems;


namespace IdleCastle.Runtime.UI
{
	[ExecuteAlways]
	public sealed class ProgressBar : UIBehaviour
	{
		[SerializeField, Range(0, 1)] private float _value;

		[SerializeField] private RectTransform _fillRect;

		public float Value
		{
			get => _value;
			set => Set(value);
		}

		#if UNITY_EDITOR
		private bool _delayedUpdateVisuals;

		protected override void OnValidate ()
		{
			base.OnValidate();

			_delayedUpdateVisuals = true;
		}

		private void Update ()
		{
			if (_delayedUpdateVisuals)
			{
				_delayedUpdateVisuals = false;
				UpdateVisuals();
			}
		}
		#endif

		protected override void OnEnable ()
		{
			base.OnEnable();
			Set(_value);
		}

		private void Set (float input)
		{
			float newValue = Mathf.Clamp(input, 0, 1);

			if (Mathf.Approximately(_value, newValue))
				return;

			_value = newValue;

			UpdateVisuals();
		}

		private void UpdateVisuals ()
		{
			if (!_fillRect) return;

			Vector2 anchorMax = _fillRect.anchorMax;
			anchorMax.x         = _value;
			_fillRect.anchorMax = anchorMax;
		}
	}
}
