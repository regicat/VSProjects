using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MvcTodoTest.Utilities;

public static class TestUtil
{
	public static string? GetDisplayName(object? instance, string propertyName)
	{
		var property = instance?.GetType()?.GetProperty(propertyName);
		var displayNameAttribute = property?.GetCustomAttribute<DisplayNameAttribute>();
		return displayNameAttribute?.DisplayName;
	}

	public static string? GetFormattedDate(object? instance, string propertyName)
	{
		var type = instance?.GetType();
		var property = type?.GetProperty(propertyName);
		var attribute = property?.GetCustomAttribute<DisplayFormatAttribute>();

		if (attribute?.DataFormatString == null) return null;
		var date = property?.GetValue(instance);
		return date == null ? null : string.Format(attribute.DataFormatString, date);
	}
}