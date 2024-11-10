using System.Text;
using Newtonsoft.Json;

namespace CleanService.Src.Utils;

public class PaymentUtil
{
    public static Dictionary<string, object> SortObjDataByKey(Dictionary<string, object> dict)
    {
        return dict.OrderBy(k => k.Key).ToDictionary(k => k.Key, k => k.Value);
    }
    
    public static Dictionary<string, object> ConvertObjToDict(object obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
    }

    public static string ConvertObjToQueryStr(Dictionary<string, object> dict, List<string> pickKeys,
        string seperator = "&")
    {
        var queryStr = new StringBuilder();

        foreach (var key in pickKeys)
        {
            if (!dict.ContainsKey(key))
                throw new Exception($"Key {key} not found in dictionary");
            var value = dict[key];
            if (value != null)
            {
                if (value is List<Dictionary<string, object>> list)
                {
                    value = JsonConvert.SerializeObject(list.Select(l => SortObjDataByKey(l)));
                }
                else if (value == null || value.ToString() == "undefined" || value.ToString() == "null")
                {
                    value = "";
                }

                queryStr.Append($"{key}={value}{seperator}");
            }
        }

        if (queryStr.Length > 0)
        {
            queryStr.Length--; // Remove last '&'
        }

        return queryStr.ToString();
    }
}