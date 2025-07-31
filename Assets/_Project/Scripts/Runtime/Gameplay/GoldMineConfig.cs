using IdleCastle.Runtime.Gameplay.Buildings;
using IdleCastle.Runtime.Utilities;
using UnityEngine;


namespace IdleCastle.Runtime.Gameplay
{
	[CreateAssetMenu(fileName = "Gold Mine", menuName = ProjectUtils.MenuPath.Gameplay + "BuildingIds/Gold Mine")]
	public class GoldMineConfig : BuildingConfig
	{
		[SerializeField, Min(float.Epsilon)] private float _progressTime = 5f;

		public override ItemId BuildingId => ItemDef.BuildingIds.GoldMine;

		public float ProgressTime => _progressTime;
	}
}
