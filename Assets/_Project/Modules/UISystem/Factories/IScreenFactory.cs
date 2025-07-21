using UnityEngine;


namespace Modules.UISystem.Factories
{
	internal interface IScreenFactory
	{
		UIScreen Create (UIScreen prototype, Transform parent);
	}
}
