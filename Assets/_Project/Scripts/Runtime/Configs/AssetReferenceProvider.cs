using GoblinFortress.Runtime.UI.Widgets;
using GoblinFortress.Runtime.Utilities;
using Modules.AddressablesUtils;
using Modules.Configs;
using UnityEngine;


namespace GoblinFortress.Runtime.Configs
{
	[CreateAssetMenu(fileName = "Asset Reference Provider", menuName = ProjectUtils.MenuPath.Configs + "Asset Reference Provider")]
	public class AssetReferenceProvider : Config
	{
		[SerializeField] private ComponentReference<BuildingWidgetView> _buildingWidgetView;

		public ComponentReference<BuildingWidgetView> BuildingWidgetView => _buildingWidgetView;
	}
}
