using System;


namespace IdleCastle.Runtime.Gameplay
{
	public interface ITickRunner : IDisposable
	{
		event Action<float> OnTick;
		event Action        OnShortTick;
		event Action        OnLongTick;

		void Pause ();
		void Resume ();
	}
}
