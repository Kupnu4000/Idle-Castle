namespace IdleCastle.Runtime.Gameplay
{
	// TODO Refactor: rename это по идее IIncomeGenerator
	public interface IBuilding
	{
		ItemId Id         {get;}
		ItemId CurrencyId {get;}

		void Tick (float deltaTime);
	}
}
