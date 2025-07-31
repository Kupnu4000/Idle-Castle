using Modules.Configs;
using UnityEngine;


namespace IdleCastle.Runtime.Gameplay.Currencies
{
	public abstract class CurrencyConfig : Config
	{
		[SerializeField] private Sprite _icon;
		[SerializeField] private string _title;

		public abstract ItemId CurrencyId {get;} // TODO: remove?

		public Sprite Icon  => _icon;
		public string Title => _title; // TODO Refactor: это должен быть тэг локализации
	}
}
