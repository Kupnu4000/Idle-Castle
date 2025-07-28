using System;


namespace IdleCastle.Runtime.Gameplay
{
	public interface ITickRunner : IDisposable
	{
		event Action<float> OnTick;
		event Action<float> OnLateTick;
		event Action        OnShortTick; // TODO: remove?
		event Action        OnLongTick;  // TODO: remove?

		void Pause ();
		void Resume ();
	}
}
