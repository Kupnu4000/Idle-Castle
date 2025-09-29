using JetBrains.Annotations;


namespace ProjectUtils
{
	[PublicAPI]
	public static class ProjectInfo
	{
		public const string ProjectName = "Idle Castle";

		public static class MenuPath
		{
			public const string Configs  = ProjectName + "/Configs/";
			public const string Gameplay = ProjectName + "/Gameplay/";
		}
	}
}
