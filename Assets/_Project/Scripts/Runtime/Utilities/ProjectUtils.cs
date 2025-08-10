using JetBrains.Annotations;


namespace GoblinFortress.Runtime.Utilities
{
	[PublicAPI]
	public static class ProjectUtils
	{
		public const string ProjectName = "Idle Castle";

		public static class MenuPath
		{
			public const string Configs  = ProjectName + "/Configs/";
			public const string Gameplay = ProjectName + "/Gameplay/";
		}
	}
}
