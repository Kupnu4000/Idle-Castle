using IdleCastle.Runtime.UI.Gameplay;
using IdleCastle.Runtime.UI.Lobby;
using JetBrains.Annotations;
using Zenject;


namespace IdleCastle.Runtime.Installers
{
	[UsedImplicitly]
	public class UIInstaller : Installer<UIInstaller>
	{
		public override void InstallBindings ()
		{
			// TODO Refactor: тут приходится биндить презентеры.
			// Возможно, создавать через фасадную фабрику или даже саму IUISystem будет лучше.

			Container.Bind<LobbyScreenPresenter>().AsTransient();
			Container.Bind<GameplayUIPresenter>().AsTransient();
		}
	}
}
