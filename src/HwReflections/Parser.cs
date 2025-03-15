namespace HwReflections;

public class Parser 
{
    public static string ParseClassName(string json)
    {
        var str = json.Trim();
        if(str.IndexOf('{') > 0) 
        {
            return str.Split('{')[0];
        }
        
        return null;
    }

    public static IList<Tuple<string, string>> ParseProperties(string json)
    {
        json = json.Trim().Split('{')[1];
        json = json.Split('}')[0];

        var list = new List<Tuple<string, string>>();
        var properties = json.Split(',');

        foreach (var property in properties) 
        {
            var values = property.Split('=');
            list.Add(new Tuple<string, string>(values[0], values[1]));
        }

        return list;
    }
}