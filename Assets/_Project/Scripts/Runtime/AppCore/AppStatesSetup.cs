using System;
using IdleCastle.AppCore.States;
using JetBrains.Annotations;
using Modules.AppCore.Interfaces;


namespace IdleCastle.AppCore
{
	[UsedImplicitly]
	public class AppStatesSetup : IAppStatesSetup
	{
		public Type InitialStateType => typeof(LoadingState);

		public void SetupStates (IStateConfigurator configurator)
		{
			configurator.ConfigureState<LoadingState>()
			            .AllowTransition<GameplayState>(AppStateTrigger.Gameplay)
			            .AllowTransition<LobbyState>(AppStateTrigger.Lobby)
			            .AsTransient();

			configurator.ConfigureState<LobbyState>()
			            .AllowTransition<GameplayState>(AppStateTrigger.Gameplay)
			            .AsTransient();

			configurator.ConfigureState<GameplayState>()
			            .AllowTransition<GameplayState>(AppStateTrigger.Gameplay)
			            .AllowTransition<LobbyState>(AppStateTrigger.Lobby)
			            .AsTransient();
		}
	}
}
