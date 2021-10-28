using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utg.HR.Dal.SqlContext
{
	public static class ModelBuilderExtensions
	{
		public static void AddEnumConverters(this ModelBuilder modelBuilder)
		{
			var props = typeof(UtgContext)
			.GetProperties(BindingFlags.Instance | BindingFlags.Public)
			.Where(p => p.PropertyType.IsGenericType &&
			p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

			var convertersDict = new Dictionary<Type, ValueConverter>();

			foreach (var prop in props)
			{
				var entityType = prop.PropertyType.GenericTypeArguments[0];

				var enumProps = entityType
				.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.Where(p => p.PropertyType.IsEnum ||
				p.PropertyType.IsGenericType &&
				p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
				p.PropertyType.GenericTypeArguments[0].IsEnum);

				var modelBuilderEntity = modelBuilder.Entity(entityType);

				foreach (var enumProp in enumProps)
				{
					var enumType = enumProp.PropertyType.IsEnum
					? enumProp.PropertyType
					: enumProp.PropertyType.GenericTypeArguments[0];

					if (!convertersDict.TryGetValue(enumType, out var converter))
					{
						converter = (ValueConverter)Activator.CreateInstance(
						typeof(EnumToStringConverter<>).MakeGenericType(enumType),
						new object[] { null });

						convertersDict[enumType] = converter;
					}

					modelBuilderEntity
					.Property(enumProp.Name)
					.HasConversion(converter);
				}
			}
		}
	}
}
