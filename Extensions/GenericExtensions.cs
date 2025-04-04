using System.Collections.ObjectModel;
using DevBox.WkHtmlToPdf.Configurations.Attributes;
using Newtonsoft.Json;

namespace DevBox.WkHtmlToPdf.Extensions;

internal static class GenericExtensions
{
    internal static T Clone<T>(this T obj) where T : class
    {
        var json = JsonConvert.SerializeObject(obj);
        return JsonConvert.DeserializeObject<T>(json);
    }

    internal static string GetCommandFlags<T>(this T obj) where T : class
    {
        var flags = new Collection<string>();

        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(obj);
            if (value == null)
                continue;

            if (property.PropertyType != typeof(string) && property.PropertyType.IsClass)
            {
                var recursiveFlags = value.GetCommandFlags();
                if (!string.IsNullOrEmpty(recursiveFlags))
                    flags.Add($" {recursiveFlags}");

                continue;
            }

            if (property.GetCustomAttributes(typeof(BooleanCommandFlag), true).FirstOrDefault() is BooleanCommandFlag booleanCommandFlag)
            {
                if (property.PropertyType == typeof(bool))
                {
                    if ((bool)value)
                        flags.Add(booleanCommandFlag.GetTrueFlag());
                    else if (booleanCommandFlag.FalseFlag != null)
                        flags.Add(booleanCommandFlag.GetFalseFlag());
                }

                continue;
            }

            if (property.GetCustomAttributes(typeof(CommandFlag), true).FirstOrDefault() is CommandFlag commandFlag)
            {
                var flag = commandFlag.GetFlag();

                if (property.PropertyType == typeof(Dictionary<string, string>))
                {
                    var dictionary = (Dictionary<string, string>)value;
                    foreach (var item in dictionary)
                        flags.Add($"{flag} \"{item.Key}\" \"{item.Value}\"");

                    continue;
                }

                if (property.PropertyType == typeof(IEnumerable<string>))
                {
                    var array = (IEnumerable<string>)value;
                    foreach (var item in array)
                        flags.Add($"{flag} \"{item}\"");

                    continue;
                }

                flags.Add($"{flag} \"{value}\"");
            }
        }

        return string.Join(" ", flags);
    }
}
