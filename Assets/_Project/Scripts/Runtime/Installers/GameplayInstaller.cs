using IdleCastle.Runtime.Gameplay;
using IdleCastle.Runtime.Gameplay.Messages;
using IdleCastle.Runtime.UI.Gameplay;
using JetBrains.Annotations;
using MessagePipe;
using Zenject;


namespace IdleCastle.Runtime.Installers
{
	[UsedImplicitly]
	public class GameplayInstaller : Installer<GameplayInstaller>
	{
		public override void InstallBindings ()
		{
			BindMessagePipe();

			Container.Bind<ITickRunner>()
			         .To<TickRunner>()
			         .FromMethod(TickRunner.Create)
			         .AsSingle()
			         .NonLazy();

			Container.Bind<GameplayUIPresenter>().AsSingle();
			Container.Bind<GameplayController>().AsSingle();
			Container.Bind<Wallet>().AsSingle();
			Container.Bind<GameWorld>().AsSingle();
		}

		private void BindMessagePipe ()
		{
			MessagePipeOptions options = Container.BindMessagePipe();

			Container.BindMessageBroker<IncomeGenerated>(options);
			Container.BindMessageBroker<CurrencyAmountChanged>(options);
		}
	}
}
