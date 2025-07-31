using System.Collections.Generic;
using IdleCastle.Runtime.Gameplay.Buildings;
using IdleCastle.Runtime.Gameplay.Currencies;
using IdleCastle.Runtime.Utilities;
using Modules.Configs;
using UnityEngine;
using Zenject;


namespace IdleCastle.Runtime.Installers
{
	[CreateAssetMenu(fileName = "Gameplay Configs Installer", menuName = ProjectUtils.MenuPath.Gameplay + "Configs/Installer")]
	public class GameplayConfigsInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private List<CurrencyConfig> _currencies;
		[SerializeField] private List<BuildingConfig> _buildings;

		public override void InstallBindings ()
		{
			List<Config> configs = new();

			configs.AddRange(_currencies);
			configs.AddRange(_buildings);

			foreach (Config config in configs)
			{
				config.Validate();

				Container.BindInterfacesAndSelfTo(config.GetType())
				         .FromInstance(config)
				         .AsSingle();
			}
		}
	}
}
