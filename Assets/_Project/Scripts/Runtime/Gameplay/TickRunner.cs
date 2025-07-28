using System;
using JetBrains.Annotations;
using UnityEngine;


namespace IdleCastle.Runtime.Gameplay
{
	// TODO: нужны ли тут длинные и короткие тики?
	[UsedImplicitly]
	public class TickRunner : MonoBehaviour, ITickRunner
	{
		public event Action<float> OnTick;
		public event Action<float> OnLateTick;
		public event Action        OnShortTick;
		public event Action        OnLongTick;

		private const float ShortTickInterval = 0.1f;
		private const float LongTickInterval  = 1.0f;

		private float _shortTickAccumulator;
		private float _longTickAccumulator;

		private bool _isPaused;

		public static TickRunner Create ()
		{
			return new GameObject("Tick Runner").AddComponent<TickRunner>();
		}

		private void Update ()
		{
			if (_isPaused) return;

			OnTick?.Invoke(Time.deltaTime);

			_shortTickAccumulator += Time.deltaTime;
			_longTickAccumulator  += Time.deltaTime;

			if (_shortTickAccumulator >= ShortTickInterval)
			{
				int shortTicks = (int)(_shortTickAccumulator / ShortTickInterval);

				for (int i = 0; i < shortTicks; i++)
					OnShortTick?.Invoke();

				_shortTickAccumulator %= ShortTickInterval;
			}

			if (_longTickAccumulator >= LongTickInterval)
			{
				int longTicks = (int)(_longTickAccumulator / LongTickInterval);

				for (int i = 0; i < longTicks; i++)
					OnLongTick?.Invoke();

				_longTickAccumulator %= LongTickInterval;
			}
		}

		private void LateUpdate ()
		{
			if (_isPaused) return;

			OnLateTick?.Invoke(Time.deltaTime);
		}

		public void Pause () => _isPaused = true;

		public void Resume () => _isPaused = false;

		public void Dispose ()
		{
			OnTick      = null;
			OnShortTick = null;
			OnLongTick  = null;
		}
	}
}
