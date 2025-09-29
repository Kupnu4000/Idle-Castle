using System.Collections.Generic;
using GoblinFortress.Runtime.Gameplay.Buildings;
using GoblinFortress.Runtime.Gameplay.Currencies;
using Modules.Configs;
using ProjectUtils;
using UnityEngine;
using Zenject;


namespace GoblinFortress.Runtime.Installers
{
	[CreateAssetMenu(fileName = "Gameplay Configs Installer", menuName = ProjectInfo.MenuPath.Gameplay + "Configs/Installer")]
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
