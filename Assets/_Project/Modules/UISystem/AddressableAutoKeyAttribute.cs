using System;


namespace Modules.UISystem
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class AddressableAutoKeyAttribute : Attribute
	{
		internal string Key {get; private set;}

		public AddressableAutoKeyAttribute (string key)
		{
			Key = key;
		}
	}
}
