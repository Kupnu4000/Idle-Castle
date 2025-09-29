using IdleCastle.Gameplay.GameEvents;
using JetBrains.Annotations;
using MessagePipe;
using Zenject;


namespace IdleCastle.Installers
{
	[UsedImplicitly]
	public class GameplayMessagePipeInstaller : Installer<GameplayMessagePipeInstaller>
	{
		public override void InstallBindings ()
		{
			MessagePipeOptions options = Container.BindMessagePipe();

			Container
				.BindMessageBroker<BuildingCreated>(options)
				.BindMessageBroker<CurrencyGenerated>(options)
				.BindMessageBroker<CurrencyAmountChanged>(options);
		}
	}
}
