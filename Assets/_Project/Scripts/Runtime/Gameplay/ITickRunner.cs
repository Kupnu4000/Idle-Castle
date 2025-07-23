using System;


namespace IdleCastle.Runtime.Gameplay
{
	// TODO: cleanup
	public interface ITickRunner : IDisposable
	{
		void Tick (float deltaTime);
		// void RegisterBuilding (IBuilding building);
		// void UnregisterBuilding (IBuilding building);
	}
}
