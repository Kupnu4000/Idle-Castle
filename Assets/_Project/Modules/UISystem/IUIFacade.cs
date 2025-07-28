using System;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace Modules.UISystem
{
	public interface IUIFacade : IDisposable
	{
		UniTask Initialize (Transform parent);
	}

	// TODO Refactor: this
	public interface IUIFacade<in TModel> : IDisposable
	{
		void Initialize (TModel model);
	}
}
