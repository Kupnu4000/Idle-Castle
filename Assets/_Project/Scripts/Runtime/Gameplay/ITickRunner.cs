using System;


namespace IdleCastle.Runtime.Gameplay
{
	public interface ITickRunner : IDisposable
	{
		event Action<float> OnTick;
		event Action<float> OnLateTick;
		event Action<ulong> OnShortTick; // TODO: remove?
		event Action<ulong> OnLongTick;  // TODO: remove?

		ulong ShortTicks {get;}
		ulong LongTicks  {get;}

		void Pause ();
		void Resume ();
	}
}
