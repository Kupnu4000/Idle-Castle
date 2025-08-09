namespace IdleCastle.Runtime.Gameplay.Buildings
{
	// TODO Refactor: rename это по идее IIncomeGenerator
	public interface IBuilding
	{
		ItemId Id                 {get;}
		ItemId CurrencyId         {get;} // TODO Refactor: у здания не будет валюты. Будут рецепты, которые генерируют ресурсы
		float  NormalizedProgress {get;}

		void Tick (float deltaTime);
	}
}
