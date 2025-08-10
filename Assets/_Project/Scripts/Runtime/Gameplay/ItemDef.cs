namespace GoblinFortress.Runtime.Gameplay
{
	// TODO Refactor: тут нужен аттрибут на локализуемые строки, чтобы можно было использовать их в UI
	// также надо будет валидировать, что локализуемые строки есть в словаре локализации (возможно, перед билдом)
	public static class ItemDef
	{
		public static class BuildingIds
		{
			public static ItemId GoldMine => new("b_gold_mine");
		}

		public static class Currencies
		{
			public static ItemId Gold => new("c_gold");
		}
	}
}
