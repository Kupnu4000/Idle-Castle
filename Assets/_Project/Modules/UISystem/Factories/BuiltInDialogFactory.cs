using UnityEngine;
using Object = UnityEngine.Object;


namespace Modules.UISystem.Factories
{
	internal class BuiltInDialogFactory : IDialogFactory
	{
		public UIDialog Create (UIDialog prototype, Transform parent)
		{
			UIDialog dialog = Object.Instantiate(prototype, parent);

			dialog.gameObject.name = prototype.name;

			return dialog;
		}
	}
}
