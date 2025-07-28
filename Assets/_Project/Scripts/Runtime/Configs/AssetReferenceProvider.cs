using IdleCastle.Runtime.UI.Widgets;
using IdleCastle.Runtime.Utilities;
using Modules.AddressablesCache;
using Modules.Configs;
using UnityEngine;


namespace IdleCastle.Runtime.Configs
{
	[CreateAssetMenu(fileName = "Asset Reference Provider", menuName = ProjectUtils.MenuPath.Configs + "Asset Reference Provider")]
	public class AssetReferenceProvider : Config
	{
		[SerializeField] private ComponentReference<BuildingWidgetView> _buildingWidgetView;

		public ComponentReference<BuildingWidgetView> BuildingWidgetView => _buildingWidgetView;
	}
}
