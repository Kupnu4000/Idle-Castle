using Modules.Configs;


namespace IdleCastle.Runtime.Gameplay
{
	// TODO: выделить под конфиги зданий отдельный инсталлер
	// TODO: remove?
	public abstract class BuildingConfig : Config
	{
		public abstract ItemId BuildingId {get;}
	}
}
