using IdleCastle.UI.Widgets;
using Modules.AddressablesUtils;
using Modules.Configs;
using ProjectUtils;
using UnityEngine;


namespace IdleCastle.Configs
{
	[CreateAssetMenu(fileName = "Asset Reference Provider", menuName = ProjectInfo.MenuPath.Configs + "Asset Reference Provider")]
	public class AssetReferenceProvider : Config
	{
		[SerializeField] private ComponentReference<BuildingWidgetView> _buildingWidgetView;

		public ComponentReference<BuildingWidgetView> BuildingWidgetView => _buildingWidgetView;
	}
}
