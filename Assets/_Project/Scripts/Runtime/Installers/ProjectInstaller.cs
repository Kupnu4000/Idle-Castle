using IdleCastle.AppCore;
using IdleCastle.Gameplay;
using IdleCastle.Zenject;
using Modules.AppCore.Zenject;
using Modules.ApplicationEvents.Zenject;
using Modules.Bootstrap.Zenject;
using Modules.StateMachine.Zenject;
using Zenject;


namespace IdleCastle.Installers
{
	public class ProjectInstaller : MonoInstaller
	{
		public override void InstallBindings ()
		{
			Container.BindInterfacesTo<AppStatesSetup>().AsSingle();

			Container.Bind<GenericFactory>()
			         .AsSingle()
			         .MoveIntoAllSubContainers();

			AppCoreInstaller.Install(Container);
			AppEventsMonitorInstaller.Install(Container);
			BootstrapInstaller.Install(Container);
			PersistentDataInstaller.Install(Container);
			StateMachineInstaller.Install(Container);
			UIInstaller.Install(Container);

			Container.Bind<GameplayController>()
			         .FromSubContainerResolve()
			         .ByInstaller<GameplayInstaller>()
			         .AsTransient();
		}
	}
}
