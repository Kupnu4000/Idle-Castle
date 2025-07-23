using UnityEngine;


namespace Modules.UISystem.Factories
{
	internal class BuiltInScreenFactory : IScreenFactory
	{
		public UIScreen Create (UIScreen prototype, Transform parent)
		{
			UIScreen screen = Object.Instantiate(prototype, parent);

			screen.gameObject.name = prototype.name;

			return screen;
		}
	}
}
