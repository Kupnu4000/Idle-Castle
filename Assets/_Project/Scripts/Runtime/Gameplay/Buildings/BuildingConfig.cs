using Modules.Configs;


namespace GoblinFortress.Runtime.Gameplay.Buildings
{
	public abstract class BuildingConfig : Config
	{
		public abstract ItemId BuildingId {get;} // TODO: remove?
	}
}
