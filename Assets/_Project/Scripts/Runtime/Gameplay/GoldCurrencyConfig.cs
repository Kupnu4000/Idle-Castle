using IdleCastle.Runtime.Utilities;
using UnityEngine;


namespace IdleCastle.Runtime.Gameplay
{
	// TODO Refactor: всё таки проще будет создать таблицу
	[CreateAssetMenu(fileName = "Gold", menuName = ProjectUtils.MenuPath.Gameplay + "Currency Configs/Gold")]
	public class GoldCurrencyConfig : CurrencyConfig
	{
		public override ItemId CurrencyId => ItemDef.Currencies.Gold;
	}
}
