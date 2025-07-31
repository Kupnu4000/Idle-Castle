using Modules.Configs;


namespace IdleCastle.Runtime.Gameplay.Buildings
{
	public abstract class BuildingConfig : Config
	{
		public abstract ItemId BuildingId {get;} // TODO: remove?
	}
}
