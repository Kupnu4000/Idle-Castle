using JetBrains.Annotations;
using Modules.UISystem;


namespace IdleCastle.Runtime.UI.Gameplay
{
	[UsedImplicitly]
	public class GameplayUIPresenter : IScreenPresenter<GameplayUIView>
	{
		public void Initialize (GameplayUIView view) {}

		public void Dispose ()
		{
			throw new System.NotImplementedException();
		}
	}
}
