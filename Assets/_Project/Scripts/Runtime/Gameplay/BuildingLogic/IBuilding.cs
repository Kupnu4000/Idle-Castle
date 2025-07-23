using System;


namespace IdleCastle.Runtime.Gameplay.BuildingLogic
{
	// TODO Refactor: rename это по идее IIncomeGenerator
	public interface IBuilding
	{
		event Action<GeneratedIncome> IncomeGenerated;

		ItemId Id         {get;}
		ItemId CurrencyId {get;}

		void Tick (float deltaTime);
	}
}
