using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Utg.HR.Common.Helpers
{
	public static class EnumExtensions
	{
		public static string GetDisplayName(this Enum enumValue)
		{
			return enumValue.GetType()
							.GetMember(enumValue.ToString())
							.First()
							.GetCustomAttribute<DisplayAttribute>()
							.GetName();
		}

		public static T GetValueByShortName<T>(string displayName, T defaultValue)
		{
			var returnValue = defaultValue;

			var values = from f in typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public)
						 let attribute = Attribute.GetCustomAttribute(f, typeof(DisplayAttribute)) as DisplayAttribute
						 where attribute != null && attribute.Name == displayName
						 select (T)f.GetValue(null);

			if (values.Count() > 0)
			{
				returnValue = (T)values.FirstOrDefault();
			}
			return returnValue;
		}
	}
}
