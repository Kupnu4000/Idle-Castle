using System.Collections.Generic;
using IdleCastle.Runtime.Bootstrap.Stages;
using JetBrains.Annotations;
using Modules.Bootstrap;
using Modules.Bootstrap.Interfaces;
using Zenject;


namespace IdleCastle.Runtime.Installers
{
	[UsedImplicitly]
	public class BootstrapInstaller : Installer<BootstrapInstaller>
	{
		public override void InstallBindings ()
		{
			Container.Bind<Bootstrapper>().AsSingle();

			Container.Bind<IBootstrapStage>()
			         .FromMethodMultiple(BootstrapStageQueueFactory);
		}

		private static IEnumerable<IBootstrapStage> BootstrapStageQueueFactory (InjectContext injectContext)
		{
			DiContainer container = injectContext.Container;

			yield return container.Instantiate<ApplyAppConfigBootstrapStage>();
			yield return container.Instantiate<PersistentDataBootstrapStage>();
		}
	}
}
