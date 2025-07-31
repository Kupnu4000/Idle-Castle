using IdleCastle.Runtime.Gameplay;
using JetBrains.Annotations;
using Modules.AddressablesUtils;
using Zenject;


namespace IdleCastle.Runtime.Installers
{
	[UsedImplicitly]
	public class GameplayInstaller : Installer<GameplayInstaller>
	{
		public override void InstallBindings ()
		{
			GameplayMessagePipeInstaller.Install(Container);

			Container.Bind<ITickRunner>()
			         .To<TickRunner>()
			         .FromMethod(TickRunner.Create)
			         .AsSingle()
			         .NonLazy();

			Container.Bind<Wallet>()
			         .AsSingle()
			         .NonLazy();

			Container.Bind<AddressablesCache>().AsSingle();
			Container.Bind<GameplayController>().AsSingle();
			Container.Bind<GameWorld>().AsSingle();
		}
	}
}
