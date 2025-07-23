using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Modules.AppCore.Interfaces;


namespace IdleCastle.Runtime.AppCore
{
	[PublicAPI]
	public static class AppStateControllerExtensions
	{
		public static void GoToLoading (this IAppStateController controller)
		{
			controller.FireAsync(AppStateTrigger.Loading).Forget();
		}

		public static void GoToLobby (this IAppStateController controller)
		{
			controller.FireAsync(AppStateTrigger.Lobby).Forget();
		}

		public static void GoToGameplay (this IAppStateController controller)
		{
			controller.FireAsync(AppStateTrigger.Gameplay).Forget();
		}
	}
}
