using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;


namespace Modules.UISystem.Interfaces
{
	[PublicAPI]
	public interface IUISystem
	{
		Camera Camera {get;}
		Canvas Canvas {get;}

		void AttachToMainCamera ();

		UniTask<TScreen> SpawnScreen<TScreen> () where TScreen : UIScreen;
		UniTask<TDialog> SpawnDialog<TDialog> () where TDialog : UIDialog;

		UniTask FadeInAsync (float duration = 1);
		UniTask FadeOutAsync (float duration = 1);
	}
}
