// using System;
// using System.Collections.Generic;
// using IdleCastle.Runtime.Gameplay;
// using UnityEngine;
//
//
// namespace _Project.Scenes.Prototypes.Day_Night_Cycle
// {
// 	public class DayNightCycle : MonoBehaviour
// 	{
// 		[SerializeField] private TickRunner     _tickRunner;
// 		[SerializeField] private SpriteRenderer _spriteRenderer;
//
// 		[Header("Settings")]
// 		[SerializeField] private int _ticksPerHour = 6;
// 		[SerializeField] private int _dawnStartHour = 6;
// 		[SerializeField] private int _dawnDuration  = 2;
// 		[SerializeField] private int _duskStartHour = 18;
// 		[SerializeField] private int _duskDuration  = 2;
// 		// [SerializeField] private int _dayStartHour  = 6;
//
// 		[SerializeField] private AnimationCurve _sunCurve;
//
// 		[Header("Colors")]
// 		[SerializeField] private Color _dayColor = Color.white;
// 		[SerializeField] private Color _nightColor = Color.black;
//
// 		private DayPhase _phase;
// 		private ulong    _tick;
//
// 		private ulong _ticksInFullDay;
//
// 		private int _dayHours;
// 		private int _nightHours;
//
// 		// private int _fullDayTicks;
// 		// private int _dawnTicks;
// 		// private int _dayTicks;
// 		// private int _duskTicks;
// 		// private int _nightTicks;
//
// 		// private ulong _dawnStartTick;
// 		// private ulong _dayStartTick;
// 		// private ulong _duskStartTick;
// 		// private ulong _nightStartTick;
//
// 		private const ulong HoursInFullDay = 24;
//
// 		private void Awake ()
// 		{
// 			// _dayHours   = _duskStartHour - _dawnStartHour - _dawnDuration;
// 			// _nightHours = _dawnStartHour + HoursInFullDay - (_duskStartHour + _duskDuration);
// 			//
// 			// _ticksInFullDay = HoursInFullDay * _ticksPerHour;
// 			//
// 			// // _tick = 6 * _ticksPerHour; // TODO Refactor: 6 это старт первого дня в игре. Вынести в конфиг
// 			//
// 			// _dawnStartTick  = _dawnStartHour * _ticksPerHour;
// 			// _dayStartTick   = _dawnStartTick + _dawnDuration * _ticksPerHour;
// 			// _duskStartTick  = _duskStartHour * _ticksPerHour;
// 			// _nightStartTick = _duskStartTick + _duskDuration * _ticksPerHour;
// 			//
// 			// // _dawnTicks = _dawnDuration * _ticksPerHour;
// 			// // _dayTicks  = _dayStart - _dawnStart;
// 			// // _duskTicks = _duskDuration * _ticksPerHour;
// 			// // _nightTicks = 0 + _dawnStart -
// 			//
// 			// // _tickRunner.OnShortTick += HandleLongTick;
// 		}
//
// 		private void Start ()
// 		{
// 			UpdateDayPhase();
// 		}
//
// 		private void HandleLongTick ()
// 		{
// 			// _tick++;
// 			//
// 			// if (_tick >= _fullDayTicks)
// 			// {
// 			// 	_tick = 0;
// 			// 	_day++;
// 			// }
// 		}
//
// 		private void UpdateDayPhase ()
// 		{
// 			_phase = GetDayPhase();
//
// 			// UpdateColor();
// 		}
//
// 		private readonly Comparer<int> _rangeComparer = Comparer<int>.Default;
//
// 		private DayPhase GetDayPhase ()
// 		{
// 			throw new NotImplementedException();
// 			// ulong tick = _tick % _ticksInFullDay;
// 			//
// 			// if (tick.IsInRange(_dawnStartTick, _dayStartTick - 1, _rangeComparer))
// 			// 	return DayPhase.Dawn;
// 			//
// 			// if (tick.IsInRange(_dayStartTick, _duskStartTick - 1, _rangeComparer))
// 			// 	return DayPhase.Day;
// 			//
// 			// if (tick.IsInRange(_duskStartTick, _nightStartTick - 1, _rangeComparer))
// 			// 	return DayPhase.Dusk;
// 			//
// 			// if (tick.IsInRange(_nightStartTick, _dawnStartTick + _ticksInFullDay - 1, _rangeComparer))
// 			//
// 			// // if (tick.IsInRange(_nightStartTick, _dawnStartTick - 1, _rangeComparer))
// 			// // 	return DayPhase.Night;
// 		}
//
// 		private void OnDestroy ()
// 		{
// 			// _tickRunner.OnShortTick -= HandleLongTick;
// 		}
//
// 		private enum DayPhase
// 		{
// 			Dawn,
// 			Day,
// 			Dusk,
// 			Night
// 		}
// 	}
// }
