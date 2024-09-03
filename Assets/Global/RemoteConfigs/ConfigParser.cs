using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public static class ConfigParser
{
    private readonly static CultureInfo _ci;
    private readonly static NumberStyles _any;

    static ConfigParser()
    {
        _any = NumberStyles.Any;

        _ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        _ci.NumberFormat.CurrencyDecimalSeparator = ".";
    }

    public static Resource ParseResource(string data)
    {
        var resourceData = data.Split(',');

        if (resourceData.Length <= 1)
            throw new Exception($"Error in parse resource. There must be at least 2 parameters separated by commas!{data}");

        if (!Enum.TryParse(resourceData[0].Trim(), out ResourceType type))
            throw new Exception($"Error in parse resource. Invalid resource type!\n{data}");

        var count = ParseInt(resourceData[1].Trim());
        var id = resourceData.Length > 2 ? resourceData[2].Trim() : string.Empty;

        return new Resource(type, count);
    }

    public static int ParseInt(string data)
    {
        if (string.IsNullOrEmpty(data))
            return 0;

        return int.Parse(data, _any);
    }

    public static float ParseFloat(string data)
    {
        if (string.IsNullOrEmpty(data))
            return 0;

        return float.Parse(data, _any, _ci);
    }

    public static long ParseLong(string data)
    {
        if (string.IsNullOrEmpty(data))
            return 0;

        return long.Parse(data, _any);
    }

    public static Vector2Int ParseVector2Int(string data)
    {
        try
        {
            if (string.IsNullOrEmpty(data))
                return Vector2Int.zero;

            string[] coordsData = data.Split(",");
            int x = ParseInt(coordsData[0]);
            int y = ParseInt(coordsData[1]);
            return new Vector2Int(x, y);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to parse Vector2Int: {data}\n{ex.Message}");
            return Vector2Int.zero;
        }
    }

    public static List<int> ParseIntList(string data)
    {
        List<int> list = new List<int>();
        string[] splittedData = data.Split(",");
        foreach(string entry in splittedData)
        {
            var entryData = entry.Trim();
            int val = ParseInt(entryData);
            list.Add(val);
        }

        return list;
    }

}
