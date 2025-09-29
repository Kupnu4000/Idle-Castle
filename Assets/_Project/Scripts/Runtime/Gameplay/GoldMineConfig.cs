using GoblinFortress.Runtime.Gameplay.Buildings;
using ProjectUtils;
using UnityEngine;


namespace GoblinFortress.Runtime.Gameplay
{
	[CreateAssetMenu(fileName = "Gold Mine", menuName = ProjectInfo.MenuPath.Gameplay + "BuildingIds/Gold Mine")]
	public class GoldMineConfig : BuildingConfig
	{
		[SerializeField, Min(float.Epsilon)] private float _progressTime = 5f;

		public override ItemId BuildingId => ItemDef.BuildingIds.GoldMine;

		public float ProgressTime => _progressTime;
	}
}
