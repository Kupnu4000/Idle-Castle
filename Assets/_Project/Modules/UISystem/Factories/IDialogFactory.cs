using UnityEngine;


namespace Modules.UISystem.Factories
{
	internal interface IDialogFactory
	{
		UIDialog Create (UIDialog prototype, Transform parent);
	}
}
