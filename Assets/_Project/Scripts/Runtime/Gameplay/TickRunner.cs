using System;
using GoblinFortress.Runtime.Extensions;
using JetBrains.Annotations;
using UnityEngine;


namespace GoblinFortress.Runtime.Gameplay
{
	// TODO: нужны ли тут длинные и короткие тики?
	[UsedImplicitly]
	public class TickRunner : MonoBehaviour, ITickRunner
	{
		public event Action<float> OnTick;
		public event Action<float> OnLateTick;
		public event Action<ulong> OnShortTick;
		public event Action<ulong> OnLongTick;

		private const float ShortTickInterval = 0.1f;
		private const float LongTickInterval  = 1.0f;

		private float _shortTickAccumulator;
		private float _longTickAccumulator;

		private bool _isPaused;

		public ulong ShortTicks {get; private set;}
		public ulong LongTicks  {get; private set;}

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
				int shortTicks = _shortTickAccumulator.DivRem(ShortTickInterval, out _shortTickAccumulator);

				for (int i = 0; i < shortTicks; i++)
				{
					ShortTicks++;
					OnShortTick?.Invoke(ShortTicks);
				}
			}

			if (_longTickAccumulator >= LongTickInterval)
			{
				int longTicks = _longTickAccumulator.DivRem(LongTickInterval, out _longTickAccumulator);

				for (int i = 0; i < longTicks; i++)
				{
					LongTicks++;
					OnLongTick?.Invoke(LongTicks);
				}
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
			OnLateTick  = null;
			OnShortTick = null;
			OnLongTick  = null;
		}
	}
}
