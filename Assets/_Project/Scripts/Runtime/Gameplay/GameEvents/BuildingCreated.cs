using IdleCastle.Runtime.Gameplay.Buildings;


namespace IdleCastle.Runtime.Gameplay.GameEvents
{
	public readonly struct BuildingCreated
	{
		public readonly IBuilding Building;

		public BuildingCreated (IBuilding building)
		{
			Building = building;
		}

		public override string ToString ()
		{
			return $"Created [BuildingId: {Building.Id.ToString()}]";
		}
	}
}
