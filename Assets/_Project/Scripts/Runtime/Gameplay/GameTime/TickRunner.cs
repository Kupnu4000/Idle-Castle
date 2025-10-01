using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Modules.Timers.Ticker;
using UnityEngine;


namespace IdleCastle.Gameplay.GameTime
{
	[UsedImplicitly]
	public class TickRunner : MonoBehaviour, ITickRunner
	{
		private const float ShortTickInterval = 0.1f;
		private const float LongTickInterval  = 1.0f;

		private readonly TickableCollection<ITickable>      _tickables      = new();
		private readonly TickableCollection<ILateTickable>  _lateTickables  = new();
		private readonly TickableCollection<IShortTickable> _shortTickables = new();
		private readonly TickableCollection<ILongTickable>  _longTickables  = new();

		private Ticker _shortTicker = new Ticker(ShortTickInterval);
		private Ticker _longTicker  = new Ticker(LongTickInterval);

		private bool _isPaused;

		public static TickRunner Create ()
		{
			return new GameObject("Tick Runner").AddComponent<TickRunner>();
		}

		public void Pause () => _isPaused = true;
		public void Resume () => _isPaused = false;

		public void Register (ITickable tickable) => _tickables.Add(tickable);
		public void Register (ILateTickable tickable) => _lateTickables.Add(tickable);
		public void Register (IShortTickable tickable) => _shortTickables.Add(tickable);
		public void Register (ILongTickable tickable) => _longTickables.Add(tickable);

		public void Unregister (ITickable tickable) => _tickables.Remove(tickable);
		public void Unregister (ILateTickable tickable) => _lateTickables.Remove(tickable);
		public void Unregister (IShortTickable tickable) => _shortTickables.Remove(tickable);
		public void Unregister (ILongTickable tickable) => _longTickables.Remove(tickable);

		private void Update ()
		{
			if (_isPaused) return;

			float deltaTime = Time.deltaTime;

			OnTick(deltaTime);

			_shortTicker.DoEach(deltaTime, OnShortTick);
			_longTicker.DoEach(deltaTime, OnLongTick);
		}

		private void LateUpdate ()
		{
			if (_isPaused) return;

			OnLateTick(Time.deltaTime);
		}

		private void OnTick (float deltaTime)
		{
			_tickables.Preprocess();

			foreach (ITickable tickable in _tickables.Active)
			{
				if (!_tickables.IsPendingRemove(tickable))
					tickable.Tick(deltaTime);
			}
		}

		private void OnLateTick (float deltaTime)
		{
			_lateTickables.Preprocess();

			foreach (ILateTickable tickable in _lateTickables.Active)
			{
				if (!_lateTickables.IsPendingRemove(tickable))
					tickable.LateTick(deltaTime);
			}
		}

		private void OnShortTick ()
		{
			_shortTickables.Preprocess();

			foreach (IShortTickable tickable in _shortTickables.Active)
			{
				if (!_shortTickables.IsPendingRemove(tickable))
					tickable.ShortTick();
			}
		}

		private void OnLongTick ()
		{
			_longTickables.Preprocess();

			foreach (ILongTickable tickable in _longTickables.Active)
			{
				if (!_longTickables.IsPendingRemove(tickable))
					tickable.LongTick();
			}
		}

		public void Dispose ()
		{
			_tickables.Dispose();
			_lateTickables.Dispose();
			_shortTickables.Dispose();
			_longTickables.Dispose();
		}

		private sealed class TickableCollection<TTickable> : IDisposable where TTickable : class
		{
			private readonly Dictionary<TTickable, Operation> _pending   = new(128);
			private readonly HashSet<TTickable>               _activeSet = new();

			internal readonly List<TTickable> Active = new();

			public bool IsPendingRemove (TTickable tickable)
			{
				return _pending.Count > 0 &&
				       _pending.TryGetValue(tickable, out Operation operation) &&
				       operation == Operation.Remove;
			}

			public void Add (TTickable tickable)
			{
				if (tickable == null) throw new ArgumentNullException(nameof(tickable));

				_pending[tickable] = Operation.Add;
			}

			public void Remove (TTickable tickable)
			{
				if (tickable == null) throw new ArgumentNullException(nameof(tickable));

				_pending[tickable] = Operation.Remove;
			}

			public void Preprocess ()
			{
				if (_pending.Count == 0) return;

				foreach ((TTickable tickable, Operation operation) in _pending)
				{
					switch (operation)
					{
						case Operation.Add:
							TryAdd(tickable);
							break;
						case Operation.Remove:
							TryRemove(tickable);
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
				}

				_pending.Clear();
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void TryAdd (TTickable tickable)
			{
				if (_activeSet.Add(tickable))
					Active.Add(tickable);
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void TryRemove (TTickable tickable)
			{
				if (_activeSet.Remove(tickable))
				{
					int indexOf = Active.IndexOf(tickable);

					if (indexOf >= 0)
					{
						int last = Active.Count - 1;
						Active[indexOf] = Active[last];
						Active.RemoveAt(last);
					}
				}
			}

			public void Dispose ()
			{
				_pending.Clear();
				_activeSet.Clear();
				Active.Clear();
			}

			private enum Operation : sbyte
			{
				Add    = 0,
				Remove = 1
			}
		}
	}
}
