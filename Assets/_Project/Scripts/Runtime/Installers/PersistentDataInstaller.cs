using IdleCastle.Runtime.PersistentData;
using JetBrains.Annotations;
using Zenject;


namespace IdleCastle.Runtime.Installers
{
	[UsedImplicitly]
	public class PersistentDataInstaller : Installer<PersistentDataInstaller>
	{
		public override void InstallBindings ()
		{
			Container.Bind<UserData>()
			         .AsSingle();

			Container.Bind<Preferences>()
			         .AsSingle();
		}
	}
}
