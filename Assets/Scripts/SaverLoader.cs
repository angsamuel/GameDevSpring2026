using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class SaverLoader
{
    public static string fileName = "saves.txt";
    public static string saveFolder = "saves";
    public static string directoryPath;

    public static Dictionary<string, string> dataDict;


    static SaverLoader()
    {
        dataDict = new Dictionary<string, string>();
        directoryPath = Path.Combine(Application.persistentDataPath, "saves");
        Debug.Log(directoryPath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    public static void SaveString(string key, string value)
    {
        dataDict[key] = value;
    }

    public static string LoadString(string key, string defaultValue = "")
    {
        if (!dataDict.ContainsKey(key))
        {
            return defaultValue;
        }
        return dataDict[key];
    }

    public static void SaveToFile()
    {
        string filePath = Path.Combine(directoryPath,fileName);

        try
        {
            using(StreamWriter sw = new StreamWriter(filePath, false))
            {
                foreach(KeyValuePair<string,string> pair in dataDict)
                {
                    sw.WriteLine($"{pair.Key}={pair.Value}");
                }
            }
        }
        catch
        {
            Debug.Log($"Oh no! could not open {filePath}");
        }
    }

    public static void LoadFromFile()
    {
        string filePath = Path.Combine(directoryPath, fileName);
        dataDict.Clear();
        if (File.Exists(filePath)){
            string[] lines = File.ReadAllLines(filePath);
            foreach(string line in lines)
            {
                string[] parts = line.Split('=');
                if(parts.Length == 2)
                {
                    dataDict[parts[0]] = parts[1];
                }
            }
        }
        else
        {
            Debug.Log("FILE DOESN't EXIST D:");
        }
    }

    public static void SaveFloat(string key, float value)
    {
        dataDict[key] = value.ToString();
    }

    public static float LoadFloat(string key, float defaultValue = 0.0f)
    {
        if (!dataDict.ContainsKey(key))
        {
            return defaultValue;
        }
        return float.Parse(dataDict[key]);
    }

    public static void TouchMe()
    {

    }


}
