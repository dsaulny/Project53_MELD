using System;
using System.Collections.Generic;

[Serializable]
public class SerializableDictionary
{
    public List<string> keys;
    public List<UserData> values;

    public SerializableDictionary(Dictionary<string, UserData> dictionary)
    {
        keys = new List<string>(dictionary.Keys);
        values = new List<UserData>(dictionary.Values);
    }

    public Dictionary<string, UserData> ToDictionary()
    {
        Dictionary<string, UserData> dictionary = new Dictionary<string, UserData>();
        for (int i = 0; i < keys.Count; i++)
        {
            dictionary.Add(keys[i], values[i]);
        }
        return dictionary;
    }
}
