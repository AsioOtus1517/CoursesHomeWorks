using System.ComponentModel;
using System.Text;

namespace HwReflections;

public class JsonConvertWithReflection
{
    // сериализация
    public static string Serialize<T>(T obj)
    {
        var builder = new StringBuilder();

        var className = typeof(T).Name;
        builder.Append(className);
        builder.Append("{ ");

        var properties = typeof(T).GetProperties();
        var str = string.Join(",", properties.Select(x => $"{x.Name} = \"{x.GetValue(obj)}\""));

        builder.Append(str);
        builder.Append(" }");

        return builder.ToString();
    }

    // десериализация
    public static T Deserialize<T>(string json)
    {
        var className = Parser.ParseClassName(json);

        if(className != typeof(T).Name) return default;

        var instance = Activator.CreateInstance(typeof(T));
        var propertyData = Parser.ParseProperties(json);

        foreach (var data in propertyData) 
        {
            var property = instance.GetType().GetProperty(data.Item1);
            var converter = TypeDescriptor.GetConverter(property.PropertyType);

            if(converter.CanConvertFrom(typeof(string))) 
            {
                property.SetValue(instance, converter.ConvertFrom(data.Item2));
            }
        }

        return (T)instance;
    }
}