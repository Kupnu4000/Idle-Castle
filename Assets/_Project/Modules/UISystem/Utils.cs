using System;
using System.Reflection;


namespace Modules.UISystem
{
	internal static class Utils
	{
		internal static bool TryGetAddressablesAutoKey (MemberInfo memberInfo, out string autoKey)
		{
			Attribute attribute = Attribute.GetCustomAttribute(memberInfo, typeof(AddressableAutoKeyAttribute));

			if (attribute is AddressableAutoKeyAttribute keyAttribute)
			{
				autoKey = keyAttribute.Key;
				return true;
			}

			autoKey = null;
			return false;
		}
	}
}
