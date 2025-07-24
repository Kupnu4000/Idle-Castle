using IdleCastle.Runtime.Gameplay;
using JetBrains.Annotations;
using Zenject;


namespace IdleCastle.Runtime.Installers
{
	[UsedImplicitly]
	public class GameplayInstaller : Installer<GameplayInstaller>
	{
		public override void InstallBindings ()
		{
			Container.Bind<GameplayController>()
			         .AsSingle();

			Container.Bind<BuildingManager>()
			         .AsSingle();

			Container.BindInterfacesTo<TickRuner>()
			         .AsSingle();
		}
	}
}
