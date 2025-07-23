using IdleCastle.Runtime.AppCore;
using IdleCastle.Runtime.Gameplay;
using Modules.AppCore.Zenject;
using Modules.ApplicationEvents.Zenject;
using Modules.StateMachine.Zenject;
using Zenject;


namespace IdleCastle.Runtime.Installers
{
	public class ProjectInstaller : MonoInstaller
	{
		public override void InstallBindings ()
		{
			Container.BindInterfacesTo<AppStatesSetup>().AsSingle();

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
