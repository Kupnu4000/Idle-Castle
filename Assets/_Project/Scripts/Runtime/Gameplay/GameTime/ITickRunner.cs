using System;


namespace IdleCastle.Gameplay.GameTime
{
	public interface ITickRunner : IDisposable
	{
		void Pause ();
		void Resume ();

		void Register (ITickable tickable);
		void Register (ILateTickable tickable);
		void Register (IShortTickable tickable);
		void Register (ILongTickable tickable);

		void Unregister (ITickable tickable);
		void Unregister (ILateTickable tickable);
		void Unregister (IShortTickable tickable);
		void Unregister (ILongTickable tickable);
	}
}
