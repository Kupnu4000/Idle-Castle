using JetBrains.Annotations;


namespace IdleCastle.Runtime.Gameplay
{
	// TODO: remove?
	// TODO Refactor: this
	[UsedImplicitly]
	public class TickRuner : ITickRunner
	{
		public void Tick (float deltaTime) {}

		public void Dispose () {}

		// private const float TickSize = 1f;

		// private float _tickDelta;

		// public event Action<float> OnTick;

		// private void Start ()
		// {
		// 	_tickDelta = 0f;
		// }

		// private int _counter;

		// private void Update ()
		// {
		// 	// // TODO: Time.unscaledDeltaTime, по-моему, отрабатывает когда приложение в фоне.
		// 	// // Надо ли это исправлять?
		// 	// _tickDelta += Time.unscaledDeltaTime;
		// 	//
		// 	// if (_tickDelta >= TickSize)
		// 	// {
		// 	// 	Debug.Log(++_counter);
		// 	// 	OnTick?.Invoke(_tickDelta);
		// 	// 	_tickDelta -= TickSize;
		// 	// }
		// }
	}
}
