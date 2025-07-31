using IdleCastle.Runtime.Gameplay.Messages;
using JetBrains.Annotations;
using MessagePipe;
using Zenject;


namespace IdleCastle.Runtime.Installers
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
