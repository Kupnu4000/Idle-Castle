using JetBrains.Annotations;
using Zenject;


namespace GoblinFortress.Runtime.Installers
{
	// TODO Refactor: this
	[UsedImplicitly]
	public class UIInstaller : Installer<UIInstaller>
	{
		public override void InstallBindings ()
		{
			// TODO Refactor: тут приходится биндить презентеры.
			// Возможно, создавать через фасадную фабрику или даже саму IUISystem будет лучше.

			// Container.Bind<LobbyScreenPresenter>().AsTransient();
		}
	}
}
