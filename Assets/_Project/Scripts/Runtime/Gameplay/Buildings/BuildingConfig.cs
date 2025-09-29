using Modules.Configs;


namespace IdleCastle.Gameplay.Buildings
{
	public abstract class BuildingConfig : Config
	{
		public abstract ItemId BuildingId {get;} // TODO: remove?
	}
}
