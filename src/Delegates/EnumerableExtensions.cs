using System.Collections;

namespace Delegates;

public static class EnumerableExtensions
{
    public static T GetMax<T>(this IEnumerable collection, Func<T, float> convertToNumber) where T : class 
    {
        if(collection is null) return null;

        T max = null;
        foreach (var item in collection) 
        {
            var tItem = item as T;
            if (max == null) 
            {
                max = tItem;
                continue;
            }

            if (convertToNumber(tItem) > convertToNumber(max)) 
            {
                max = tItem;
            }
        }

        return max;
    }
}