using IdleCastle.Runtime.Utilities;
using UnityEngine;


namespace IdleCastle.Runtime.Gameplay.Currencies
{
	[CreateAssetMenu(fileName = "Gold", menuName = ProjectUtils.MenuPath.Gameplay + "Currencies/Gold")]
	public class GoldCurrencyConfig : CurrencyConfig
	{
		public override ItemId CurrencyId => ItemDef.Currencies.Gold;
	}
}
