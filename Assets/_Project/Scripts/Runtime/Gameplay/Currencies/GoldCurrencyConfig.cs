using ProjectUtils;
using UnityEngine;


namespace IdleCastle.Gameplay.Currencies
{
	[CreateAssetMenu(fileName = "Gold", menuName = ProjectInfo.MenuPath.Gameplay + "Currencies/Gold")]
	public class GoldCurrencyConfig : CurrencyConfig
	{
		public override ItemId CurrencyId => ItemDef.Currencies.Gold;
	}
}
